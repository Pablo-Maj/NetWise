using System.Text.Json;
using RecruitmentTask.Model;

namespace RecruitmentTask.File;

public class FileWriter(string filePath) : IFileWriter
{
    private readonly string _filePath = filePath;

    public async Task AppendAsync(CatFact data, CancellationToken cancellationToken)
    {
        string json = JsonSerializer.Serialize(data);
        await System.IO.File.AppendAllTextAsync(_filePath, json + Environment.NewLine, cancellationToken);

        Console.WriteLine($"FileWriter: data saved to {_filePath}");
    }
}
