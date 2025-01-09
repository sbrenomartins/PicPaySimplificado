namespace PicPaySimplificado.Models;

public class TransferEntity
{
    public Guid TransferId { get; set; }
    public decimal Value { get; set; }

    public int SenderId { get; set; }
    public WalletEntity Sender { get; set; }

    public int ReceiverId { get; set; }
    public WalletEntity Receiver { get; set; }
    
    private TransferEntity() { }

    public TransferEntity(int senderId, decimal value, int receiverId)
    {
        TransferId = Guid.NewGuid();
        SenderId = senderId;
        Value = value;
        ReceiverId = receiverId;
    }
}