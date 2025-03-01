using System.Net.Http.Json;

namespace Money.ApiClient;

public class AccountClient(MoneyClient apiClient) : ApiClientExecutor(apiClient)
{
    private const string BaseUri = "/Account";

    protected override string ApiPrefix => "";

    public Task<ApiClientResponse> RegisterAsync(RegisterRequest request)
    {
        return PostAsync($"{BaseUri}/register", request);
    }

    public class RegisterRequest
    {
        public required string UserName { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}
