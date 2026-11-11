using Microsoft.AspNetCore.Mvc;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Gharshare.CommonHelper;

namespace Gharshare.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private IUnitofWork _unitofWork;

        public MessageController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index(int pg=1)
        {
            IEnumerable<Message> messages=_unitofWork.Message.GetAll();
            //return View(messages);
            #region Pagination            
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
            IConfiguration configuration = builder.Build();
            int pageSize = Convert.ToInt32(configuration.GetValue<string>("PageSize:Size"));
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = messages.Count();
            var pager = new Pager(recsCount, pg, "Message", "index", pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = messages.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
            #endregion end pagination

        }
        public IActionResult Reply()
        {
        return View(); 
        }
        public IActionResult Delete(Int32 ID)
        {
            return View();
        }

    }
}
