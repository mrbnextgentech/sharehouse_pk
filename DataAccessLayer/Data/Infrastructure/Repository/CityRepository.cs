using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class CityRepository : IRepository<City>, ICityRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CityRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(City city)
        {
            _dbContext.Cities.Update(city);
        }
    }
}
