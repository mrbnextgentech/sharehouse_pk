using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class LifestyleRepository : IRepository<Lifestyle>, ILifestyleRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public LifestyleRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(Lifestyle lifestyle)
        {
            _dbContext.Lifestyles.Update(lifestyle);
        }
    }
}
