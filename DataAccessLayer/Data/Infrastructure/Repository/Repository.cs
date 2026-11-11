using Gharshare.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    public class IRepository<T> : IRepository.IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private DbSet<T> _dbSet;

        public IRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public  IEnumerable<T> GetWithChildrenAndGrandChildren()
        {
            var childrenProperties = typeof(T).GetProperties()
                .Where(p => p.PropertyType.IsGenericType
                    && p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));

            IQueryable<T> query = _dbSet;
            foreach (var childrenProperty in childrenProperties)
            {
                var childType = childrenProperty.PropertyType.GetGenericArguments()[0];
                var childDbSet = _context.Set<T>();
                var childChildrenProperties = childType.GetProperties()
                    .Where(p => p.PropertyType.IsGenericType
                        && p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));

                query = query.Include(childrenProperty.Name);
                foreach (var childChildrenProperty in childChildrenProperties)
                {
                    query = query.Include(childrenProperty.Name + "." + childChildrenProperty.Name);
                }
            }

            return query.ToList();
        }

        public void Detach(T entity)
        {
            try
            {
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void StopTracking(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void StopTrackingRange(List<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public Int32 Count(T entity) need to be implemented later to get count of any entity
        //{
        //    Int32 count = _dbSet.Count();
        //    return count;
        //}
        public void Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Delete(T entity)            
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void AddRange(List<T> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void UpdateRange(List<T> entities)
        {
            try
            {
                _dbSet.UpdateRange(entities);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public T Get(Expression<Func<T, bool>> Predicate, string? includeProperties = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                query = query.Where(Predicate);
                if (includeProperties != null)
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(item);
                    }
                }
                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (includeProperties != null)
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(item);
                    }                     
                }
                
                return query.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Predicate, string? includeProperties = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                query = query.Where(Predicate);
                if (includeProperties != null)
                {
                    foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(item);
                    }
                }
                return query.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<T> GetAllData(Expression<Func<T, bool>> predicate,
                                     Func<IQueryable<T>, IQueryable<T>>? includes = null,
                                     bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            query = query.AsNoTracking();

            return query.ToList();
        }
        public T GetwithAllObjects(Expression<Func<T, bool>> Predicate, 
            Func<IQueryable<T>, IQueryable<T>>? includes = null,
                                     bool disableTracking = true)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().Where(Predicate);
                //query = query.Where(Predicate);

                if (includes != null)
                {
                    query = includes(query);
                }

                query = query.AsNoTracking();

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
