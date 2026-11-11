using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class ShortlistRepository : IRepository<Shortlist>, IShortlistRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ShortlistRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(Shortlist shortlist)
        {
            _dbContext.Shortlist.Update(shortlist);
        }
    }
}
