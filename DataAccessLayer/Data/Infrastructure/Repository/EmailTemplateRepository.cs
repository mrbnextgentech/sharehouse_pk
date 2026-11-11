using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.DataAccessLayer.Data.Infrastructure.Repository
{
    internal class EmailTemplateRepository : IRepository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public EmailTemplateRepository(ApplicationDBContext context) : base(context)
        {
            _dbContext= context;
        }

        public void update(EmailTemplate emailTemplate)
        {
            _dbContext.EmailTemplates.Update(emailTemplate);
        }
    }
}
