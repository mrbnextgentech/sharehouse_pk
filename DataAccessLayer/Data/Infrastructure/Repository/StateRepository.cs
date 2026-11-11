using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class StateRepository : IRepository<State>, IStateRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StateRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(State state)
        {
            _dbContext.States.Update(state);
        }
    }
}
