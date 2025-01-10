using PicPaySimplificado.Models;

namespace PicPaySimplificado.Infra.Repositories.Wallet;

public interface IWalletRepository
{
    Task AddAsync(WalletEntity wallet);
    Task UpdateAsync(WalletEntity wallet);
    Task<WalletEntity?> GetByIdAsync(int id);
    Task<WalletEntity?> GetByCpfCnpjAsync(string cpfCnpj, string email);
    Task CommitAsync();
}