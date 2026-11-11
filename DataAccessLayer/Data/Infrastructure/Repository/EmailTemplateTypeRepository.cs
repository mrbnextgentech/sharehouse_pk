using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class EmailTemplateTypeRepository : IRepository<EmailTemplateType>, IEmailTemplateTypeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public EmailTemplateTypeRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(EmailTemplateType emailTemplateType)
        {
            _dbContext.EmailTemplateTypes.Update(emailTemplateType);
        }
    }
}
