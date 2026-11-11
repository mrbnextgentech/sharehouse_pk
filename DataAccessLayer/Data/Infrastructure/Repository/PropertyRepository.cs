using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    public class PropertyRepository : IRepository<Property>, IPropertyRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public PropertyRepository(ApplicationDBContext context):base(context)
        {
            _dbContext = context;
        }
        public void update(Property entity)
        {
            _dbContext.Properties.Update(entity);
        }
        //public IEnumerable<Property> GetAllData(Expression<Func<Property, bool>> predicate,
        //                             Func<IQueryable<Property>, IQueryable<Property>>? includes = null,
        //                             bool disableTracking = true)
        //{
        //    IQueryable<Property> query = _dbContext.Set<Property>().Where(predicate);

        //    if (includes != null)
        //    {
        //        query = includes(query);
        //    }

        //    query = query.AsNoTracking();

        //    return query.ToList();
        //}


    }
}
