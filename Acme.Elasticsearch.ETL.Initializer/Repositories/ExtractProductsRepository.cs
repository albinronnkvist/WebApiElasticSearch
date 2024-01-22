using Acme.Elasticsearch.Core.Entities;
using Acme.Elasticsearch.ETL.Initializer.Records;
using Acme.Elasticsearch.ETL.Initializer.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Acme.Elasticsearch.ETL.Initializer.Repositories;

public class ExtractProductsRepository : IExtractRepository<Product, int>
{
    private readonly SqlConnection _dbConnection;

    public ExtractProductsRepository(SqlConnectionFactory sqlConnectionFactory)
    {
        _dbConnection = sqlConnectionFactory.CreateProductConnection();
    }
    
    public async Task<IEnumerable<Product>> Get(KeysetPagination<int> pagination)
    {
        const string query = "SELECT TOP (@BatchSize) * FROM Products WHERE Id > @Cursor";
    
        var parameters = new { pagination.Cursor, pagination.BatchSize };
        var products = await _dbConnection.QueryAsync<Product>(query, parameters);
        return products;
    }
}