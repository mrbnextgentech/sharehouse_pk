using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class ListingEmploymentRepository : IRepository<ListingEmployment>, IListingEmploymentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ListingEmploymentRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(ListingEmployment listingEmployment)
        {
            _dbContext.ListingEmployments.Update(listingEmployment);
        }
    }
}
