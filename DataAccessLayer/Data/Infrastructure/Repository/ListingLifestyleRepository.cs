using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class ListingLifestyleRepository : IRepository<ListingLifestyle>, IListingLifestyleRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ListingLifestyleRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }
        public void update(ListingLifestyle listingLifestyle)
        {
            _dbContext.ListingLifestyles.Update(listingLifestyle);
        }
    }
}
