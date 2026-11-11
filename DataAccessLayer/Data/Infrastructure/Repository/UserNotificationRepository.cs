using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class UserNotificationRepository : IRepository<UserNotification>, IUserNotificationRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserNotificationRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(UserNotification userNotification)
        {
            _dbContext.UserNotifications.Update(userNotification);
        }
    }
}
