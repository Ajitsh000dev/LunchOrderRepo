using LunchOrder.Data.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LunchOrder.Data.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T FirstorDefault(Expression<Func<T, bool>> Filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(Filter != null)
                query = query.Where(Filter);
            if(includeProperties != null)
            {
                foreach (var includeprop in includeProperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(Filter != null)
                query = query.Where(Filter);
            if(includeProperties != null) // to show data from multiple table
            {
                foreach (var includeProp in includeProperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries)) //it will pick table one by one
                {
                    query = query.Include(includeProp);
                }
            }
            if(orderby != null)
                return orderby(query).ToList();
            return query.ToList(); // return one record
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            dbSet.Update(entity);
        }
    }
}
