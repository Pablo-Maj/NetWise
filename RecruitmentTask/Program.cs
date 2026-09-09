using RecruitmentTask.Client;
using RecruitmentTask.File;
using RecruitmentTask.Service;
using Microsoft.Extensions.DependencyInjection;

namespace RecruitmentTask;
internal class Program
{
    const string API_BASE_URL = "https://catfact.ninja/fact";
    const string RESULT_PATH = "result.txt";
    static async Task Main(string[] args)
    {
        try
        {
            ServiceCollection services = [];

            //Client Api
            services.AddSingleton<HttpClient>(_ => new HttpClient
            {
                BaseAddress = new Uri(API_BASE_URL)
            });
            services.AddSingleton<IApiClient, ApiClient>();

            //File Writer
            services.AddSingleton<IFileWriter>(new FileWriter(RESULT_PATH));

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
