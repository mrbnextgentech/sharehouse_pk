using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models; 
//using System.Dynamic;
using System.Text;
using Newtonsoft.Json;
using Gharshare.DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Gharshare.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebSiteRoles.Role_Admin)]
    public class PlanController : Controller
    {
        private IUnitofWork _unitofWork;

        public PlanController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [HttpPost]
        public JsonResult saveplanservice(PlanserviceVM planservicevm)
        {
            try
            {
                planservicevm.planservice.CreatedbyUserID = HttpContext.Session.GetString("_UserId");
                planservicevm.planservice.CreatedOn = DateTime.UtcNow;
                planservicevm.planservice.UId = Guid.NewGuid().ToString();
                _unitofWork.PlanService.Add(planservicevm.planservice);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/plan/saveplanservice");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public IActionResult loadviewAddservice2Plan(Int64 planid)
        {
            var services = _unitofWork.Service.GetAll(s => s.status == Service.Status.active).ToList();
            PlanserviceVM planservicevm = null;
            try
            {
                planservicevm = new PlanserviceVM
                {
                    plan = _unitofWork.Plan.Get(p => p.ID == planid),
                    services = GetServicesSelectList(),
                    planservice = new PlanService
                    {
                        PlanID = planid
                    }
                };
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/service/loadaddservice2plan");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return View("addserviceinplan", planservicevm);
        }
        private List<SelectListItem> GetServicesSelectList()
        {
            IEnumerable<SelectListItem> services = _unitofWork.Service.GetAll(s => s.status == Service.Status.active)
                .OrderBy(s => s.title).ToList().Select(
                    x => new SelectListItem()
                    {
                        Text = x.title,
                        Value = Convert.ToString(x.id),
                    }
                    );
            return services.ToList();
        }
        [HttpPost]
        public JsonResult updateplan(PlanVM planvm)
        {
            List<Plan> plans = null;            
            try
            {
                if (planvm.planservices != null)
                {
                    _unitofWork.PlanService.UpdateRange(planvm.planservices);
                     _unitofWork.Save();                     
                }
                var planDb = _unitofWork.Plan.Get(p => p.ID == planvm.plan.ID);
                if (planvm.plan != null)
                {
                    planDb.Title = planvm.plan.Title;
                    planDb.Price = planvm.plan.Price;
                    planDb.PlanStatus = planvm.plan.PlanStatus;
                    planDb.PlanType = planvm.plan.PlanType;
                    planDb.Tagline = planvm.plan.Tagline;
                    planDb.Days = planvm.plan.Days;
                    planDb.Guaranteed = planvm.plan.Guaranteed;
                    planDb.Description = planvm.plan.Description;
                    planDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    planDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Plan.update(planDb);                   
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/plan/saveplan");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
           //plans = _unitofWork.Plan.GetAll().ToList();
            return new JsonResult("success");
        }
        [HttpGet]
        public IActionResult loadplan2delete(Int64 planId)
        {
            Plan plan = null;
            try
            {
                plan = _unitofWork.Plan.Get(p => p.ID == planId,
                    includeProperties: "UsersonPlan");
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }

            return View("deleteplan", plan);
        }

        [HttpPost]
        public IActionResult Delete(Plan plan)
        {
            if (plan != null)
            {
                _unitofWork.Plan.Delete(plan);
                _unitofWork.Save();
            }
            var plans = _unitofWork.Plan.GetAll().ToList();
            return Json(plans);
        }
        [HttpGet]
        public IActionResult loadplan2edit(Int64 planId)
        {
            PlanVM planvm = null;
            
            try
            {
                Plan plan = _unitofWork.Plan.Get(p => p.ID == planId,
                    includeProperties: "UsersonPlan");
                var planServices = _unitofWork.PlanService.GetAll(ps => ps.PlanID == plan.ID,
                    "Service").OrderBy(ps => ps.ServiceID).ToList();
                planvm = new PlanVM
                {
                    plan = plan,
                    planservices = planServices
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("editplan", planvm);
        }
        [HttpGet]
        public JsonResult getplanlist(Int32 plantype)
        {
            List<Plan> plans = null;
            try
            {
                if (plantype > 0)
                {
                    plans = _unitofWork.Plan.GetAll(p => p.PlanType == (Plan.Plantype)plantype)
                        .ToList();
                }
                else
                    plans = _unitofWork.Plan.GetAll().ToList();
                #region Pagination
                int pageSize = Pager.GetPageSize();
                int pg = 1;
                int recsCount = plans.Count();
                var pager = new Pager(recsCount, pg, "Plan", "list", pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = plans.Skip(recSkip).Take(pager.PageSize).ToList();
                plans = data;
                this.ViewBag.Pager = pager;
                #endregion end pagination
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/plan/getplanlist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }

            return (new JsonResult(plans));
        }
        [HttpPost]
        public JsonResult saveplan(Plan plan)
        {
            List<Plan> plans = null;
            try
            {
                plan.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                plan.UId = Guid.NewGuid().ToString();
                _unitofWork.Plan.Add(plan);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/plan/saveplan");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            plans = _unitofWork.Plan.GetAll().ToList();
            return (new JsonResult(plans));
        }

        [HttpGet]
        public IActionResult newplan()
        {
            Plan plan = new Plan { Guaranteed = false };
            return View("newplan", plan);
        }
        [HttpGet]
        public IActionResult list(int pg = 1)
        {
            var plans = _unitofWork.Plan.GetAll().ToList();
            #region Pagination
            int pageSize = Pager.GetPageSize();
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = plans.Count();
            int recSkip = (pg - 1) * pageSize;
            var data = plans.Skip(recSkip).Take(pageSize).ToList();
            var pager = new Pager(recsCount, pg, "Plan", "list", pageSize, "", "", data.Count());
            ViewBag.Pager = pager;
            #endregion end pagination
            return View(data);
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
