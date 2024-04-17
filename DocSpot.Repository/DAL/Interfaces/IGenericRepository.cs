using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.Repository.DAL.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                string includeProperties = "");
        Task<T> GetById(int id);
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<T> Delete(T item);
    }


}
