using Dapper;

namespace Acme.Elasticsearch.ETL.Initializer.Repositories;

public class ProductIndexVersionRepository : IIndexVersionRepository
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;
    private const string TableName = "ProductIndexVersion";

    public ProductIndexVersionRepository(SqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<int> GetLatestVersion()
    {
        var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $@"SELECT MAX(Id) FROM {TableName}";

        return await connection.QuerySingleAsync(sql);
    }

    public async Task<int> GetLatestCompletedVersion()
    {
        var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $@"SELECT MAX(Id) 
                                FROM {TableName} 
                                WHERE CompletedDate IS NOT NULL";

        return await connection.QuerySingleAsync(sql);
    }

    public async Task<int> CreateVersion()
    {
        var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $@"INSERT INTO {TableName} (CreatedDate)
                            VALUES (GETDATE())
                            SELECT SCOPE_IDENTITY()";

        return await connection.ExecuteAsync(sql);
    }

    public async Task CompleteVersion(int indexVersion)
    {
        var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $@"UPDATE {TableName}
                            SET CompletedDate = GETDATE()
	                        WHERE Id = @id";

        await connection.ExecuteAsync(sql, new { id = indexVersion });
    }
}