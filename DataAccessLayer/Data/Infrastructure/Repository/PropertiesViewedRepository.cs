using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PropertiesViewedRepository : IRepository<PropertiesViewed>, IPropertiesViewedRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PropertiesViewedRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(PropertiesViewed propertiesViewed)
        {
            _dbContext.PropertiesViewed.Update(propertiesViewed);
        }
    }
}
