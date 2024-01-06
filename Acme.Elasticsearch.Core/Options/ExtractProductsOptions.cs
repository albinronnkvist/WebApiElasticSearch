namespace Acme.Elasticsearch.Core.Options;

public record ExtractProductsOptions
{
    public required int StartFromId { get; init; }
    public required string DatabaseConnectionString { get; init; }
}