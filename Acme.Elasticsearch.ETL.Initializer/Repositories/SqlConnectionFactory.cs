using Acme.Elasticsearch.Core.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Acme.Elasticsearch.ETL.Initializer.Repositories;

public class SqlConnectionFactory
{
    private readonly string _databaseConnectionString;
    private readonly string _productDatabaseConnectionString;

    public SqlConnectionFactory(IOptions<DatabaseOptions> databaseOptions, 
        IOptions<ExtractProductsOptions> extractProductsOptions)
    {
        _databaseConnectionString = databaseOptions.Value.ConnectionString;
        _productDatabaseConnectionString = extractProductsOptions.Value.DatabaseConnectionString;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_databaseConnectionString);
    }
    
    public SqlConnection CreateProductConnection()
    {
        return new SqlConnection(_productDatabaseConnectionString);
    }
}