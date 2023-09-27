namespace ContactManagement.Client.Services;

public class ConfigurationService
{
    private readonly HttpService _httpService;
    private readonly ILogger<ConfigurationService> _logger;

    public ConfigurationService(HttpService httpService, ILogger<ConfigurationService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }
}