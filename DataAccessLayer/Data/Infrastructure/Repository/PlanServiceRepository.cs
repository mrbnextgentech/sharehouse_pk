using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PlanServiceRepository : IRepository<PlanService>, IPlanServiceRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public PlanServiceRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext = context;
        }
        public void update(PlanService planservice)
        {
            _dbContext.PlanServices.Update(planservice);
        }
    }
}
