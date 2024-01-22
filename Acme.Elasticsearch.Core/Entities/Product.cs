namespace Acme.Elasticsearch.Core.Entities;

public class Product
{
    public required int Id { get; set; }
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }
    public required decimal Price { get; set; }
}