using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.IRepository
{
    public interface IPropertyRepository:IRepository<Property>
    {
        void update(Property entity);
        //public IEnumerable<Property> GetAllData(Expression<Func<Property, bool>> predicate,
        //               Func<IQueryable<Property>, IQueryable<Property>>? includes = null,
        //               bool disableTracking = true);

    }
}
