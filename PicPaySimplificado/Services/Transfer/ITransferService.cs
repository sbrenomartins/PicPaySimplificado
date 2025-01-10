using PicPaySimplificado.Models.DTOs;
using PicPaySimplificado.Models.DTOs.Requests;
using PicPaySimplificado.Models.DTOs.Responses;

namespace PicPaySimplificado.Services.Transfer;

public interface ITransferService
{
    Task<Result<TransferResponse>> ExecuteAsync(TransferRequest request);
}