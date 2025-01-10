namespace PicPaySimplificado.Models.DTOs.Requests;

public class TransferRequest
{
    public decimal Value { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}