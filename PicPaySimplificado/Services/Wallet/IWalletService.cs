using PicPaySimplificado.Models.DTOs.Requests;
using PicPaySimplificado.Models.DTOs.Responses;

namespace PicPaySimplificado.Services.Wallet;

public interface IWalletService
{
    Task<Result<bool>> ExecuteAsync(WalletRequest request);
}