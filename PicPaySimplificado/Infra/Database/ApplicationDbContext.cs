using Microsoft.EntityFrameworkCore;
using PicPaySimplificado.Models;

namespace PicPaySimplificado.Infra.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) 
{
    public DbSet<WalletEntity> Wallets { get; set; }
    public DbSet<TransferEntity> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WalletEntity>()
            .HasIndex(w => new { w.CPFCNPJ, w.Email })
            .IsUnique();
        
        modelBuilder.Entity<WalletEntity>()
            .Property(w => w.Balance)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<WalletEntity>()
            .Property(w => w.UserType)
            .HasConversion<string>();

        modelBuilder.Entity<TransferEntity>()
            .HasKey(t => t.TransferId);
        
        modelBuilder.Entity<TransferEntity>()
            .HasOne(t => t.Sender)
            .WithMany()
            .HasForeignKey(t => t.SenderId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Transaction_Sender");
        
        modelBuilder.Entity<TransferEntity>()
            .Property(t => t.Value)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<TransferEntity>()
            .HasOne(t => t.Receiver)
            .WithMany()
            .HasForeignKey(t => t.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Transaction_Receiver");
    }
}