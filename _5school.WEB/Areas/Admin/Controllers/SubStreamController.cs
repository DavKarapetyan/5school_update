using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using _5school.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class SubStreamController : Controller
    {
        private readonly ISubStreamService _subStreamService;
        private readonly IStreamService _streamService;
        private readonly ITeacherService _teacherService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SubStreamController(ISubStreamService subStreamService,IStreamService streamService, ITeacherService teacherService, IWebHostEnvironment webHostEnvironment)
        {
            _subStreamService = subStreamService;
            _streamService = streamService;
            _teacherService = teacherService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = _subStreamService.GetSubStreams(CultureType.am);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            var model = id.HasValue ? _subStreamService.GetSubStreamForEdit(id.Value,culture) : new SubStreamAddEditVM() { Id = 0};
            model.Culture = culture;
            ViewBag.Streams = _streamService.GetStreams(CultureType.am);
            ViewBag.Teachers = _teacherService.GetTeachers(CultureType.am);
            return PartialView("_AddEdit",model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(SubStreamAddEditVM model, IFormFile formFile)
        {
            if (formFile != null)
            {
                string path = "/Files/" + formFile.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await formFile.CopyToAsync(fileStream); }
                model.ImageFile = path;
                if (model.Id == 0)
                {
                    _subStreamService.Add(model);
                }
                else
                {
                    _subStreamService.Update(model, model.Culture);
                }
            }
            else if (formFile == null && model.ImageFile != null)
            {
                if (model.Id == 0)
                {
                    _subStreamService.Add(model);
                }
                else
                {
                    _subStreamService.Update(model, model.Culture);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            _subStreamService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
