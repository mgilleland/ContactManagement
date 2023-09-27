using ContactManagement.BlazorShared;
using ContactManagement.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;

namespace ContactManagement.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var baseUrlConfig = new BaseUrlConfiguration();
            builder.Configuration.Bind(BaseUrlConfiguration.ConfigName, baseUrlConfig);
            builder.Services.AddScoped(sp => baseUrlConfig);

            // register the HttpClient and HttpService
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrlConfig.ApiBase) });
            builder.Services.AddScoped<HttpService>();

            builder.Services.AddScoped<ContactService>();
            builder.Services.AddScoped<ConfigurationService>();

            builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            await builder.Build().RunAsync();
        }
    }
}