using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class  EmploymentRepository : IRepository<Employment>, IEmploymentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public EmploymentRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(Employment employment)
        {
            _dbContext.Employments.Update(employment);
        }
    }
}
