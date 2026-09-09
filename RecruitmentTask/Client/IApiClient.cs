using RecruitmentTask.Model;

namespace RecruitmentTask.Client
{
    public interface IApiClient
    {
        Task<CatFact> GetDataAsync(CancellationToken cancellationToken);
    }
}
