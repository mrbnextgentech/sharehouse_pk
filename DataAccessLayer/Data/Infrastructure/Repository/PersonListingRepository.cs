using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class PersonListingRepository : IRepository<PersonListing>, IPersonListingRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PersonListingRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(PersonListing personListing)
        {
            _dbContext.PersonListings.Update(personListing);
        }
    }
}
