using Gharshare.CommonHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Gharshare.com.pk.Controllers
{
    public class infoController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private readonly TwilioService _twilioService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public infoController(IUnitofWork unitofWork, Utility utility,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitofWork = unitofWork;
            _utility = utility;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult detail(string Id,string title)
        {
            StaticContent article = null;
            List<StaticContent> allArticles = null;
            StaticContent.Type contentType;
            string articleType = "";
            string pageTitle = "";
            cmsVm cmsVM = null;
            try
            {
                article = _unitofWork.StaticContent.Get(sc => sc.UId==Id,
                    includeProperties: "CreatedbyUser");
                contentType = (StaticContent.Type)article.ContentType;
                switch (contentType)
                {
                    case StaticContent.Type.article:
                        allArticles = _unitofWork.StaticContent.GetAll(sc => sc.ContentType ==
                        StaticContent.Type.article, includeProperties: "CreatedbyUser").
                        OrderByDescending(sc => sc.CreatedOn).ToList();                         
                        articleType = "articles";
                        pageTitle = "Latest Articles";
                        break;
                    case StaticContent.Type.howtoguide:
                        allArticles = _unitofWork.StaticContent.GetAll(sc => sc.ContentType ==
                        StaticContent.Type.howtoguide, includeProperties: "CreatedbyUser").
                        OrderByDescending(sc => sc.CreatedOn).ToList();
                        articleType = "howtoguide";
                        pageTitle = "How to Guides";
                        break;
                    case StaticContent.Type.legalguides:
                        allArticles = _unitofWork.StaticContent.GetAll(sc => sc.ContentType ==
                        StaticContent.Type.legalguides, includeProperties: "CreatedbyUser").
                        OrderByDescending(sc => sc.CreatedOn).ToList();
                        articleType = "legalguide";
                        pageTitle = "Legal Guides";
                        break;
                    case StaticContent.Type.aboutus:
                        allArticles = _unitofWork.StaticContent.GetAll(sc => sc.ContentType ==
                        StaticContent.Type.aboutus, includeProperties: "CreatedbyUser").
                        OrderByDescending(sc => sc.CreatedOn).ToList();
                        articleType = "aboutus";
                        pageTitle = "About Us";
                        break;
                }
                cmsVM = new cmsVm
                {
                    selectedContent=article,
                    contents=allArticles,
                    articleType=articleType,
                    pageTitle= pageTitle
                };
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }

            return View(cmsVM);
        }

        [HttpGet]
        public IActionResult content(string type,int pg=1)
        {
            List<StaticContent> staticContent = null;
            cmsVm cmsVM = null;
            try
            {
                string pageTitle=string.Empty;
                staticContent = _unitofWork.StaticContent.GetAll(sc => sc.Id > 0,
                    includeProperties: "CreatedbyUser")
                    .OrderByDescending(sc=>sc.CreatedOn).ToList();

                switch(type)
                {
                    case "articles":
                        staticContent = staticContent.Where(sc =>
                        sc.ContentType == StaticContent.Type.article).ToList();
                        pageTitle = "Latest Articles";
                    break;
                    case "howtoguide":
                        staticContent = staticContent.Where(sc =>
                        sc.ContentType == StaticContent.Type.howtoguide).ToList();
                        pageTitle = "How to Guides";
                        break;
                    case "legalguide":
                        staticContent = staticContent.Where(sc =>
                        sc.ContentType == StaticContent.Type.legalguides).ToList();
                        pageTitle = "Legal Guides";
                        break;
                    case "aboutus":
                        staticContent = staticContent.Where(sc =>
                        sc.ContentType == StaticContent.Type.aboutus).ToList();
                        pageTitle = "About Us";
                        break;
                }
                #region pagination 
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                this.ViewBag.pg = pg;
                int recsCount = staticContent.Count();
                int recSkip = (pg - 1) * pageSize;
                staticContent = staticContent.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "info", "content", pageSize, "", type, 
                    staticContent.Count());
                this.ViewBag.Pager = pager;
                #endregion
                
                cmsVM = new cmsVm
                {
                    contents=staticContent,
                    pageTitle=pageTitle,
                };
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(cmsVM);
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
                    UId= Guid.NewGuid().ToString()
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
