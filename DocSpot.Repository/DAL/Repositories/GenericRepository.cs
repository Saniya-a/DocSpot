using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.Repository.DAL.Repositories
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private bool disposed = false;
        private readonly DocSpotDBContext _dbContext;
        internal DbSet<T> dbSet;
        public GenericRepository(DocSpotDBContext dBContext)
        {
            _dbContext = dBContext;
            dbSet = _dbContext.Set<T>();
        }
        public async Task<T> Add(T item)
        {
            await dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(T item)
        {
            dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(property);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public (IEnumerable<T> items, int total) GetAll(int pageNo, int perPage,
                    Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var total = query.Count();
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
            query = query.Skip((pageNo) * perPage).Take(perPage);
            if (orderBy != null)
            {
                return (orderBy(query).ToList(), total);
            }
            return (query.ToList(), total);
        }


        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<List<T>> Read()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> Update(T item)
        {
            dbSet.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
