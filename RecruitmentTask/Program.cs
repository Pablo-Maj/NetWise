using RecruitmentTask.Client;
using RecruitmentTask.File;
using RecruitmentTask.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace RecruitmentTask;
internal class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            ServiceCollection services = [];

            //Client Api
            services.AddSingleton<HttpClient>(_ => new HttpClient
            {
                BaseAddress = new Uri(configuration["Api:BaseUrl"]!)
            });
            services.AddSingleton<IApiClient, ApiClient>();

            //File Writer
            services.AddSingleton<IFileWriter>(new FileWriter(configuration["File:ResultPath"]!));

            //Application Service
            services.AddTransient<ApplicationService>();

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            ApplicationService appService = serviceProvider.GetRequiredService<ApplicationService>();
        
            await appService.ExecuteAsync(CancellationToken.None);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"API error: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
