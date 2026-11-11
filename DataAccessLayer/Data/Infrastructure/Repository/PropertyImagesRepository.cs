using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    public class PropertyImagesRepository : IRepository<PropertyImage>, IPropertyImagesRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public PropertyImagesRepository(ApplicationDBContext context):base(context)
        {
            _dbContext = context;
        }
        public void update(PropertyImage entity)
        {
            _dbContext.PropertyImages.Update(entity);
        }
        
    }
}
