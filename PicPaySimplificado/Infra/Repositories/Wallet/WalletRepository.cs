using Microsoft.EntityFrameworkCore;
using PicPaySimplificado.Infra.Database;
using PicPaySimplificado.Models;

namespace PicPaySimplificado.Infra.Repositories.Wallet;

public class WalletRepository : IWalletRepository
{
    private readonly ApplicationDbContext _context;

    public WalletRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(WalletEntity wallet)
    {
        await _context.Wallets.AddAsync(wallet);
    }

    public async Task UpdateAsync(WalletEntity wallet)
    {
        _context.Wallets.Update(wallet);
    }

    public async Task<WalletEntity?> GetByIdAsync(int id)
    {
        return await _context.Wallets.FindAsync(id);
    }

    public async Task<WalletEntity?> GetByCpfCnpjAsync(string cpfCnpj, string email)
    {
        return await _context.Wallets
            .FirstOrDefaultAsync(w => w.CPFCNPJ.Equals(cpfCnpj) || w.Email.Equals(email));
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}