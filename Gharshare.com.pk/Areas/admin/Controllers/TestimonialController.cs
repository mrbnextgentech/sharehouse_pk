using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Mvc;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace Gharshare.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private IUnitofWork _unitofWork;
        private IWebHostEnvironment _hostingEnvironment;
        private Utility _utility;

        public TestimonialController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment, Utility utility)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
            _utility = utility;
        }

        public IActionResult Index(int pg=1)
        {
            IEnumerable<Testimonial> _testimonials = _unitofWork.Testimonial.GetAll().ToList();
           
            #region Pagination            
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
            IConfiguration configuration = builder.Build();
            int pageSize = Convert.ToInt32(configuration.GetValue<string>("PageSize:Size"));
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = _testimonials.Count();
            var pager = new Pager(recsCount, pg, "Testimonial", "index", pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _testimonials.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
            #endregion end pagination
        }
        [HttpGet]
        public IActionResult Create()
        {
            var userID = HttpContext.Session.GetString("_UserId");
            Testimonial testimonial = new Testimonial { CreatedbyUserID = userID };
            return View(testimonial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Testimonial _testimonial, IFormFile? file)
        {
            if (ModelState.IsValid)
            {                 
                string _imagePath = _utility.SaveFile(file, @"assets/TestimonialImages");
                _testimonial.ImagePath = _imagePath;
                _testimonial.UId= Guid.NewGuid().ToString();
                _unitofWork.Testimonial.Add(_testimonial);
                _unitofWork.Save();
                TempData["success"] = "Testimonial created successfully!";
                return RedirectToAction("Index");
            }
            else
                return View(_testimonial);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != 0)
            {
                var _testimonial = _unitofWork.Testimonial.Get(x => x.Id == id);
                return View(_testimonial);
            }
            else
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Testimonial testimonial, IFormFile? file)
        {             
            string _oldImagePath = testimonial.ImagePath;

            if (ModelState.IsValid)
            {
                if (file != null && _oldImagePath != null)
                {
                    _utility.DeletFile(_oldImagePath);
                    string _imagePath = _utility.SaveFile(file, @"assets/TestimonialImages");
                    testimonial.ImagePath = _imagePath;
                }
                else if (file != null && _oldImagePath == null)
                {
                    string _imagePath = _utility.SaveFile(file, @"assets/TestimonialImages");
                    testimonial.ImagePath = _imagePath;
                }
                _unitofWork.Testimonial.update(testimonial);
                _unitofWork.Save();
                TempData["success"] = "Testimonial updated successfully!";
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var testimonial = _unitofWork.Testimonial.Get(x => x.Id == Id);
            return View(testimonial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Testimonial testimonial)
        {
           var _testimonials = _unitofWork.Testimonial.GetAll(x=>x.Id != testimonial.Id).ToList();
            string _ImagePath = testimonial.ImagePath;
            if (_testimonials.Count > 0)
            {
                if (_ImagePath != null || _ImagePath == string.Empty)
                    _utility.DeletFile(_ImagePath);

                _unitofWork.Testimonial.Delete(testimonial);
                _unitofWork.Save();
                TempData["success"] = "Testimonial deleted successfully!";                
            }
            else
                TempData["success"] = "Last Testimonial can't be deleted!";

            return RedirectToAction("Index");
            
        }
    }
}
