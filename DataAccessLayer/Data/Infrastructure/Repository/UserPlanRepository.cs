using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class UserPlanRepository : IRepository<UserPlan>, IUserPlanRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserPlanRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(UserPlan userplan)
        {
            _dbContext.UserPlans.Update(userplan);
        }
    }
}
