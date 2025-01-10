using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PicPaySimplificado.Models.Enums;
using PicPaySimplificado.Utils;

namespace PicPaySimplificado.Models.DTOs.Requests;

public class WalletRequest
{
    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O CPF ou CNPJ é obrigatório.")]
    [CpfCnpjValidation(ErrorMessage = "O CPF ou CNPJ informado é inválido.")]
    public string CPFCNPJ { get; set; }
    
    [Required(ErrorMessage = "O Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O Email deve ser válido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A Senha é obrigatória.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "O Tipo de Usuário é obrigatório.")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserType UserType { get; set; }
    
    [Required(ErrorMessage = "O Saldo é obrigatório.")]
    public decimal Balance { get; set; }
}