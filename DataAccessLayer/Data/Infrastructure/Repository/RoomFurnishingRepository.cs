using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class RoomFurnishingRepository : IRepository<RoomFurnishing>, IRoomFurnishingRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RoomFurnishingRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(RoomFurnishing roomFurnishing)
        {
            _dbContext.RoomFurnishings.Update(roomFurnishing);
        }
    }
}
