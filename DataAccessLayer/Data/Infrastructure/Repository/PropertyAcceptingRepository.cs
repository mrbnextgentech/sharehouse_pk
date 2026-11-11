using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PropertyAcceptingRepository : IRepository<PropertyAccepting>, IPropertyAcceptingRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PropertyAcceptingRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(PropertyAccepting propertyAccepting)
        {
            _dbContext.PropertyAcceptings.Update(propertyAccepting);
        }
    }
}
