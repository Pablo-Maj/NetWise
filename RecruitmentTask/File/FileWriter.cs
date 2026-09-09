using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RecruitmentTask.Model;

namespace RecruitmentTask.File
{
    public class FileWriter(string filePath) : IFileWriter
    {
        private readonly string _filePath = filePath;

        public async Task AppendAsync(CatFact data, CancellationToken cancellationToken)
        {
            string json = JsonSerializer.Serialize(data);

            await System.IO.File.AppendAllTextAsync(_filePath, json + Environment.NewLine, cancellationToken);
        }
    }
}
