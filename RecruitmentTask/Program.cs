using RecruitmentTask.Client;
using RecruitmentTask.File;
using RecruitmentTask.Service;

namespace RecruitmentTask;
internal class Program
{
    const string API_BASE_URL = "https://catfact.ninja/fact";
    const string RESULT_PATH = "result.txt";
    static async Task Main(string[] args)
    {
        try
        {
            HttpClient httpClient = new()
            {
                BaseAddress = new Uri(API_BASE_URL)
            };
            ApiClient apiClient = new(httpClient);
            FileWriter fileWriter = new(RESULT_PATH);
            ApplicationService appService = new(apiClient, fileWriter);
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
