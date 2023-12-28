namespace Acme.Elasticsearch.Api.Dtos;

public record ReadProduct
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
}
