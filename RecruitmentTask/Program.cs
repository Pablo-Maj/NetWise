using RecruitmentTask.Client;
using RecruitmentTask.File;
using RecruitmentTask.Service;

namespace RecruitmentTask;
internal class Program
{
    static async Task Main(string[] args)
    {
        HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://catfact.ninja/fact")
        };
        ApiClient apiClient = new(httpClient);
        FileWriter fileWriter = new("result.txt");
        ApplicationService appService = new(apiClient, fileWriter);
        await appService.ExecuteAsync(CancellationToken.None);
    }
}
