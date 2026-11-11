using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class FurnishingRepository : IRepository<Furnishing>, IFurnishingRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public FurnishingRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(Furnishing furnishing)
        {
            _dbContext.Furnishings.Update(furnishing);
        }
    }
}
