using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class FeatureRepository : IRepository<Feature>, IFeatureRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public FeatureRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(Feature feature)
        {
            _dbContext.Features.Update(feature);
        }
    }
}
