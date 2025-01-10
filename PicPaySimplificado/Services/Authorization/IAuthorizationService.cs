namespace PicPaySimplificado.Services.Authorization;

public interface IAuthorizationService
{
    Task<bool> AuthorizeAsync();
}