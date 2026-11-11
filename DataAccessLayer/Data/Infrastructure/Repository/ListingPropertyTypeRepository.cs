using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class ListingPropertyTypeRepository : IRepository<ListingPropertyType>, IListingPropertyTypeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ListingPropertyTypeRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(ListingPropertyType listingPropertyType)
        {
            _dbContext.ListingPropertyTypes.Update(listingPropertyType);
        }
    }
}
