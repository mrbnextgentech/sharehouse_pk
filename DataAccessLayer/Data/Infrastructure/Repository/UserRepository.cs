using Gharshare.DataAccessLayer;
using Gharshare.Models;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class UserRepository : IRepository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(ApplicationUser user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
