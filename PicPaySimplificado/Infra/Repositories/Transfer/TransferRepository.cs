using Microsoft.EntityFrameworkCore.Storage;
using PicPaySimplificado.Infra.Database;
using PicPaySimplificado.Models;

namespace PicPaySimplificado.Infra.Repositories.Transfer;

public class TransferRepository : ITransferRepository
{
    private readonly ApplicationDbContext _context;

    public TransferRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTransaction(TransferEntity transfer)
    {
        await _context.Transfers.AddAsync(transfer);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}