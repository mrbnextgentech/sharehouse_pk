using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class RoomRepository : IRepository<Room>, IRoomRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RoomRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(Room room)
        {
            _dbContext.Rooms.Update(room);
        }
    }
}
