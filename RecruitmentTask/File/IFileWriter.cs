using RecruitmentTask.Model;

namespace RecruitmentTask.File;

public interface IFileWriter
{
    Task AppendAsync(CatFact data, CancellationToken cancellationToken);
}
