using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.IRepository
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> GetAll();
        IEnumerable<T> GetWithChildrenAndGrandChildren();
        IEnumerable<T> GetAll(string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> Predicate, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> Predicate, string? includeProperties = null);
        T GetwithAllObjects(Expression<Func<T, bool>> Predicate, Func<IQueryable<T>, IQueryable<T>>? includes = null,
                                     bool disableTracking = true);

        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void AddRange(List<T> entities);
        void UpdateRange(List<T> entities);
        void Detach(T entity);
        void StopTracking(T entity);
        void StopTrackingRange(List<T> entities);
        IEnumerable<T> GetAllData(Expression<Func<T, bool>> predicate,
                       Func<IQueryable<T>, IQueryable<T>>? includes = null,
                       bool disableTracking = true);

    }
}
