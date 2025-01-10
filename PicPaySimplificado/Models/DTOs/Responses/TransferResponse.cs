namespace PicPaySimplificado.Models.DTOs.Responses;

public record TransferResponse(Guid TransferId, WalletEntity Sender, WalletEntity Receiver, decimal Value);