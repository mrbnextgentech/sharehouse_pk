using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class UserNotificationTypeRepository : IRepository<UserNotificationType>, IUserNotificationTypeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserNotificationTypeRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(UserNotificationType userNotificationType)
        {
            _dbContext.UserNotificationTypes.Update(userNotificationType);
        }
    }
}
