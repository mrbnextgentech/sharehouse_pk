using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class DemoRequestRepository : IRepository<DemoRequest>, IDemoRequestRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public DemoRequestRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(DemoRequest demoRequest)
        {
            _dbContext.DemoRequests.Update(demoRequest);
        }
    }
}
