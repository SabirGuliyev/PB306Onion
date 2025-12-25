using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Domain.Entities;
using OnionPronia.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Persistence.Implementations.Repositories
{
    internal class TagRepository:Repository<Tag>,ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context) { }

    }
}
