using PicPaySimplificado.Models;
using PicPaySimplificado.Models.DTOs.Responses;

namespace PicPaySimplificado.Mappers;

public static class TransferMapper
{
    public static TransferResponse ToTransferResponse(this TransferEntity transferEntity)
    {
        return new TransferResponse(
            transferEntity.TransferId,
            transferEntity.Sender,
            transferEntity.Receiver,
            transferEntity.Value);
    }
}