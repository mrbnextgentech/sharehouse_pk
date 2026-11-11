using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PlanRepository : IRepository<Plan>, IPlanRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PlanRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
        }
    }
}
