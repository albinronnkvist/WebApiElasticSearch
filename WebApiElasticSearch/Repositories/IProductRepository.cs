using Acme.Elasticsearch.Web.Entities;
using OneOf;
using OneOf.Types;
using Sieve.Models;

namespace Acme.Elasticsearch.Web.Repositories;

public interface IProductRepository
{
    Task<OneOf<Product, NotFound, Error>> GetAsync(Guid id);
    Task<OneOf<IReadOnlyCollection<Product>, Error>> SearchAsync(SieveModel sieveModel);
    Task<OneOf<Success, Error>> CreateAsync(Product product);
    Task<OneOf<Success, NotFound, Error>> UpdateAsync(Product product);
    Task<OneOf<Success, NotFound, Error>> DeleteAsync(Guid id);
}
