namespace Acme.Elasticsearch.Web.Entities;

public class Product
{
    public required Guid Id { get; set; } = default!;
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }
    public required decimal Price { get; set; }
}
