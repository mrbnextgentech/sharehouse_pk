using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class UserPaymentRepository : IRepository<UserPayment>, IUserPaymentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserPaymentRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(UserPayment userpayment)
        {
            _dbContext.UserPayments.Update(userpayment);
        }
    }
}
