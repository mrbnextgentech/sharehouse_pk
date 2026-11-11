using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PropertyFeatureRepository : IRepository<PropertyFeature>, IPropertyFeatureRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PropertyFeatureRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(PropertyFeature propertyFeature)
        {
            _dbContext.PropertyFeatures.Update(propertyFeature);
        }
    }
}
