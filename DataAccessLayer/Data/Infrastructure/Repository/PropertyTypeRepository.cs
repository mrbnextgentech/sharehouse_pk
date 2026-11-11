using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PropertyTypeRepository : IRepository<PropertyType>, IPropertyTypeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PropertyTypeRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(PropertyType propertyType)
        {
            _dbContext.PropertyTypes.Update(propertyType);
        }
    }
}
