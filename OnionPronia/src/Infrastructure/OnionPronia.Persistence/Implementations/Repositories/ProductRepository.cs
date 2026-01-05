

using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Domain.Entities;
using OnionPronia.Persistence.Contexts;

namespace OnionPronia.Persistence.Implementations.Repositories
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

    }
}
