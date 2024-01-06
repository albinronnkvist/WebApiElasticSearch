namespace Acme.Elasticsearch.Core.Options;

public record DatabaseOptions
{
    public required string ConnectionString { get; init; }
}