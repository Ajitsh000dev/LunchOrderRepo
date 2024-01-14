using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LunchOrder.Data.Data.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void Remove(int id); //remove only one record
        void Update(T entity);
        void RemoveRange(IEnumerable<T> entity);// remove multiple or bulk data
        T Get(int id); //find
        IEnumerable<T> GetAll(   //Display// for sorting
            Expression<Func<T, bool>> Filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeProperties = null
            );
        T FirstorDefault(
            Expression<Func<T, bool>> Filter = null,
            string includeProperties = null
            );
    }
}
// <T> Generic class .. it accepts any kind of objects.
