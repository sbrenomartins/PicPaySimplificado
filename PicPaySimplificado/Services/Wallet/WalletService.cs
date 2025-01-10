using PicPaySimplificado.Infra.Repositories.Wallet;
using PicPaySimplificado.Models;
using PicPaySimplificado.Models.DTOs;
using PicPaySimplificado.Models.DTOs.Requests;

namespace PicPaySimplificado.Services.Wallet;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<Result<bool>> ExecuteAsync(WalletRequest request)
    {
        var walletExists = await _walletRepository.GetByCpfCnpjAsync(request.CPFCNPJ, request.Email);

        if (walletExists is not null)
            return Result<bool>.Failure("Já existe uma carteira cadastrada com o CPF/CNPJ e/ou Email informados.");

        var wallet = new WalletEntity(
            request.Name,
            request.CPFCNPJ,
            request.Email,
            request.Password,
            request.Balance,
            request.UserType
        );
        
        await _walletRepository.AddAsync(wallet);
        await _walletRepository.CommitAsync();
        
        return Result<bool>.Success(true);
    }
}