using System.Text.Json;
using System.Text.Json.Serialization;

namespace PicPaySimplificado.Services.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private readonly HttpClient _httpClient;
    private string AUTH_URL_BASE;

    public AuthorizationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        AUTH_URL_BASE = configuration.GetSection("API:Authorization:UrlBase").Value ?? "";
    }

    public async Task<bool> AuthorizeAsync()
    {
        string content = string.Empty;
        
        var response = await _httpClient.GetAsync(AUTH_URL_BASE);

        if (!response.IsSuccessStatusCode)
            return false;

        response.EnsureSuccessStatusCode();
        
        content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ApiResponse>(content);
        
        return result?.Status == "success";
    }
}

public class ApiResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("data")]
    public DataResponse Data { get; set; }
}

public class DataResponse
{
    [JsonPropertyName("authorization")]
    public bool Authorization { get; set; }
}