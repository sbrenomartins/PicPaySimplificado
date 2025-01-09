using PicPaySimplificado.Models.Enums;

namespace PicPaySimplificado.Models;

public class WalletEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CPFCNPJ { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
    public UserType UserType { get; set; }
    
    private WalletEntity() { }

    public WalletEntity(string name, string cpfcnpj, string email, string password, decimal balance, UserType userType)
    {
        Name = name;
        CPFCNPJ = cpfcnpj;
        Email = email;
        Password = password;
        Balance = balance;
        UserType = userType;
    }

    public void DebitBalance(decimal value)
    {
        Balance -= value;
    }

    public void CreditBalance(decimal value)
    {
        Balance += value;
    }
}