using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecruitmentTask.Client;
using RecruitmentTask.File;

namespace RecruitmentTask.Service;

public class ApplicationService(
    IApiClient apiClient,
    IFileWriter fileWriter)
{
    private readonly IApiClient _apiClient = apiClient;
    private readonly IFileWriter _fileWriter = fileWriter;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var data = await _apiClient.GetDataAsync(cancellationToken);

        await _fileWriter.AppendAsync(data, cancellationToken);
    }
}
