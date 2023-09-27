namespace ContactManagement.BlazorShared;

public class BaseUrlConfiguration
{
    public const string ConfigName = "baseUrls";

    public string ApiBase { get; set; } = string.Empty;
    public string WebBase { get; set; } = string.Empty;
}