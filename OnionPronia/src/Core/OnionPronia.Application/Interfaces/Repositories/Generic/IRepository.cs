using OnionPronia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {

        IQueryable<T> GetAll(
            Expression<Func<T, bool>>? func = null,
             Expression<Func<T, object>>? sort = null,
            bool isDesc = false,
            int page = 0,
            int take = 0,
            params string[]? includes


            );
        Task<T> GetByIdAsync(int id, params string[] includes);


        void Add(T entity);

        void Update(T entity);


        void Delete(T entity);
        Task SaveChangesAsync();


    }
}
