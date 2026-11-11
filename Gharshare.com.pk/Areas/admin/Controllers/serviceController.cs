using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System.Dynamic;
using Gharshare.Views.Shared.Components.SearchBar;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=WebSiteRoles.Role_Admin)]
    public class ServiceController : Controller
    {
        private IUnitofWork _unitofWork;

        public ServiceController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public JsonResult selectservice(Int64 id)
        {
            Service service = null;
            try
            {
                service = _unitofWork.Service.Get(s => s.id == id);   
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/service/selectservice");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
             
            return (new JsonResult(service));
        }
        [HttpPost]
        public JsonResult saveservice(Service service)
        {
            List<Service> services = null;
            try
            {
                service.createdbyUserID = HttpContext.Session.GetString("_UserId");
                service.status = Service.Status.active;
                service.UId= Guid.NewGuid().ToString();
                _unitofWork.Service.Add(service);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/service/saveservice");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            services = _unitofWork.Service.GetAll().ToList();
            return (new JsonResult(services));
        }
        [HttpGet]
        public IActionResult list(int pg = 1)
        {
            var services = _unitofWork.Service.GetAll().ToList();
            List<Service> seviceData = null;
            try
            {
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = services.Count();                
                int recSkip = (pg - 1) * pageSize;
                seviceData = services.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "service", "list", pageSize,"","",
                    seviceData.Count());
                this.ViewBag.Pager = pager;
                #endregion end pagination
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(seviceData);
        }
        public IActionResult Index(int pg=1, string SearchText="",string sortOrder="")
        {
            List<Service> services;

            #region Get Data on Search criterion
            ViewBag.SearchText = SearchText;
            if (SearchText == string.Empty || SearchText == null)
                services = _unitofWork.Service.GetAll().ToList();
            else
                services = _unitofWork.Service.GetAll(s => s.title.Contains(SearchText.Trim()) || s.description.Contains(SearchText.Trim())).ToList();

            Search search = new Search() { Action = "index", Controller = "Service", SearchText = SearchText };
            ViewBag.Search = search;
            #endregion

            #region Apply Pagination and Sorting 
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
            IConfiguration configuration = builder.Build();
            int pageSize = Convert.ToInt32(configuration.GetValue<string>("PageSize:Size"));

            if (pg < 1)
            {
                pg = 1;
            }
            ViewBag.pg = pg;
            int recsCount = services.Count();
            var pager = new Pager(recsCount, pg, "Service", "index", pageSize);
            pager.SortOrder = sortOrder;
            int recSkip = (pg - 1) * pageSize;
            List<Service> data;
            switch (sortOrder)
            {
                case "name_desc":
                      data = services.Skip(recSkip).Take(pager.PageSize).OrderByDescending(s => s.title).ToList();
                    ViewData["SortIcon4Name"] = "fa fa-arrow-up";
                    
                    break;
                default:
                      data = services.Skip(recSkip).Take(pager.PageSize).OrderBy(s => s.title).ToList();
                    ViewData["SortIcon4Name"] = "fa fa-arrow-down";
                    break;
            }            
            this.ViewBag.Pager = pager;
            #endregion end pagination            

            return View(data);


        }

        [HttpGet]
        public IActionResult create()
        {
            Service service = new Service();
            return View(service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                service.UId= Guid.NewGuid().ToString();
                _unitofWork.Service.Add(service);
                _unitofWork.Save();
                TempData["success"] = "Service created successfully!";
            }
            else
            {
                TempData["error"] = "Error creating new service.";
            }
            ModelState.Clear();
            Service serviceEmpty = new Service();
            return View(serviceEmpty);

        }

        [HttpGet]
        public IActionResult delete(int serviceid)
        {
            var service = _unitofWork.Service.Get(x => x.id == serviceid,
                includeProperties: "planServices");
            return View("delete",service);
        }
        [HttpPost]
        public IActionResult delete(Service service)
        {
            if (service != null)
            {                 
                _unitofWork.Service.Delete(service);
                _unitofWork.Save();
            }
            var servicelist = _unitofWork.Service.GetAll().ToList();
            return Json(servicelist);
        }
        [HttpPost]
        public IActionResult update(Service service)
        {
            if (service!=null)
            {
                var serviceDb = _unitofWork.Service.Get(x => x.id == service.id);
                serviceDb.title = service.title;
                serviceDb.description = service.description;
                serviceDb.updatedOn = DateTime.UtcNow;
                serviceDb.updatedbyUserID = HttpContext.Session.GetString("_UserId");
                _unitofWork.Service.update(serviceDb);
                _unitofWork.Save();
            }
            var servicelist = _unitofWork.Service.GetAll().ToList();
            return Json(servicelist);
        }
        [HttpGet]
        public IActionResult edit(int serviceid)
        {             
                var service = _unitofWork.Service.Get(x => x.id == serviceid);
                return View(service);
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

                //throw;
            }
        }
    }

}
