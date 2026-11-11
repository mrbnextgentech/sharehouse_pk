using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PropertyInspectionRepository : IRepository<PropertyInspection>, IPropertyInspectionRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PropertyInspectionRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(PropertyInspection propertyInspection)
        {
            _dbContext.PropertyInspections.Update(propertyInspection);
        }
    }
}
