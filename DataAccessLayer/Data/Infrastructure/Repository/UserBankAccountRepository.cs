using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class UserBankAccountRepository : IRepository<UserBankAccount>, IUserBankAccountRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserBankAccountRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(UserBankAccount userBankAccount)
        {
            _dbContext.UserBankAccounts.Update(userBankAccount);
        }
    }
}
