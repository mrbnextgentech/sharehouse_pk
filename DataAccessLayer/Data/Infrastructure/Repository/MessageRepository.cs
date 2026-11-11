using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    public class MessageRepository : IRepository<Message>, IMessageRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public MessageRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(Message message)
        {
            _dbContext.Messages.Update(message);
        }
    }
}
