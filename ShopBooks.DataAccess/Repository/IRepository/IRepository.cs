using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBooks.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Get first
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);

        //IEnumerable<> Collection of entities RemoveRange
        void RemoveRange(IEnumerable<T> entity);
    }
}
