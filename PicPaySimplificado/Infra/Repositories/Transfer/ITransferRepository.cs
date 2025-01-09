using Microsoft.EntityFrameworkCore.Storage;
using PicPaySimplificado.Models;

namespace PicPaySimplificado.Infra.Repositories.Transfer;

public interface ITransferRepository
{
    Task AddTransaction(TransferEntity transfer);
    Task CommitAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}