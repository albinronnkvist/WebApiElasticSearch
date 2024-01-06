namespace Acme.Elasticsearch.ETL.Initializer.Repositories;

public interface IIndexVersionRepository
{
    public Task<int> GetLatestVersion();
    public Task<int> GetLatestCompletedVersion();
    public Task<int> CreateVersion();
    public Task CompleteVersion(int indexVersion);
}