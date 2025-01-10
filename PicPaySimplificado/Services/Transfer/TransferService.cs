using PicPaySimplificado.Infra.Repositories.Transfer;
using PicPaySimplificado.Infra.Repositories.Wallet;
using PicPaySimplificado.Mappers;
using PicPaySimplificado.Models;
using PicPaySimplificado.Models.DTOs;
using PicPaySimplificado.Models.DTOs.Requests;
using PicPaySimplificado.Models.DTOs.Responses;
using PicPaySimplificado.Models.Enums;
using PicPaySimplificado.Services.Authorization;
using PicPaySimplificado.Services.Notification;

namespace PicPaySimplificado.Services.Transfer;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly INotificationService _notificationService;

    public TransferService(ITransferRepository transferRepository, 
                           IWalletRepository walletRepository,
                           INotificationService notificationService,
                           IAuthorizationService authorizationService)
    {
        _transferRepository = transferRepository;
        _walletRepository = walletRepository;
        _notificationService = notificationService;
        _authorizationService = authorizationService;
    }

    public async Task<Result<TransferResponse>> ExecuteAsync(TransferRequest request)
    {
        if (!await _authorizationService.AuthorizeAsync())
            return Result<TransferResponse>.Failure("Não autorizado.");

        var sender = await _walletRepository.GetByIdAsync(request.SenderId);
        var receiver = await _walletRepository.GetByIdAsync(request.ReceiverId);

        if (sender is null || receiver is null)
            return Result<TransferResponse>.Failure("Nenhuma carteira encontrada.");
        
        if (sender.Balance < request.Value || sender.Balance == 0)
            return Result<TransferResponse>.Failure("Saldo insuficiente para realizar a transação.");

        if (sender.UserType == UserType.Shopkeeper)
            return Result<TransferResponse>.Failure(
                "O tipo de usuário não tem permissão para realizar transferências.");
        
        sender.DebitBalance(request.Value);
        receiver.CreditBalance(request.Value);

        var transfer = new TransferEntity(sender.Id, request.Value, receiver.Id);

        using (var transferScope = await _transferRepository.BeginTransactionAsync())
        {
            try
            {
                var updateTasks = new List<Task>
                {
                    _walletRepository.UpdateAsync(sender),
                    _walletRepository.UpdateAsync(receiver),
                    _transferRepository.AddTransaction(transfer)
                };
                
                await Task.WhenAll(updateTasks);
                
                await _walletRepository.CommitAsync();
                await _transferRepository.CommitAsync();
                
                await transferScope.CommitAsync();
            }
            catch (Exception e)
            {
                await transferScope.RollbackAsync();
                return Result<TransferResponse>
                    .Failure("Não foi possível efetuar a transação devido à um erro: " + e.Message);
            }
        }

        await _notificationService.SendNotificationAsync();
        return Result<TransferResponse>.Success(transfer.ToTransferResponse());
    }
}