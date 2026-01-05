

using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Domain.Entities;
using OnionPronia.Persistence.Contexts;

namespace OnionPronia.Persistence.Implementations.Repositories
{
    internal class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
       
    }
}
