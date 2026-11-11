using Gharshare.CommonHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Gharshare.com.pk.Controllers
{
    public class aboutusController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private readonly TwilioService _twilioService;

        public aboutusController(IUnitofWork unitofWork, Utility utility)
        {
            _unitofWork = unitofWork;
            _utility = utility;
        }
        public IActionResult howitworks()
        {
            return View();
        }
        public IActionResult privacy()
        {
            var privacy = _unitofWork.StaticContent.Get(sc => sc.ContentType == StaticContent.Type.privacypolicy);
            return View(privacy);
        }
        public IActionResult faqs()
        {
            var faq = _unitofWork.StaticContent.Get(sc => sc.ContentType == StaticContent.Type.FAQs);
            return View(faq);
        }
        public IActionResult terms()
        {
            var terms = _unitofWork.StaticContent.Get(sc => sc.ContentType == StaticContent.Type.termsandconidtions);
            return View(terms);
        }
        public IActionResult claims()
        {
            var claims = _unitofWork.StaticContent.Get(sc => sc.ContentType == StaticContent.Type.claims);
            return View(claims);
        }
        public IActionResult localguides()
        {
            var terms = _unitofWork.StaticContent.Get(sc => sc.ContentType == StaticContent.Type.localguide);
            return View(terms);
        }
        [HttpGet]
        public IActionResult index()
        {
            try
            {

            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }

            return View();
        }
        private void LogErrorDetail(Exception ex, string errorPath)
        {
            try
            {
                var errorLog = new ErrorLog
                {
                    ErrorDetail = ex.Message,
                    ErrorSource = ex.Source,
                    StackTrace = ex.StackTrace,
                    ErrorPath = errorPath,
                    LoggedInUserId = HttpContext.Session.GetString("_UserId"),
                    status = ErrorLog.Status.Open,
                    UId = Guid.NewGuid().ToString()
                };
                _unitofWork.ErrorLog.Add(errorLog);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }
}
