using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    public class MeetingRepository : IRepository<Meeting>, IMeetingRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public MeetingRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(Meeting meeting)
        {
            _dbContext.Meetings.Update(meeting);
        }
    }
}
