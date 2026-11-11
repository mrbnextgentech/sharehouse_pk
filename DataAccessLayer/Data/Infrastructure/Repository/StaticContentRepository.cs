using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class StaticContentRepository : IRepository<StaticContent>, IStaticContentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StaticContentRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(StaticContent content)
        {
            _dbContext.StaticContent.Update(content);
        }
    }
}
