using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Mvc;
using _5school.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IGroupService _groupService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeacherController(ITeacherService teacherService, IGroupService groupService, IWebHostEnvironment webHostEnvironment)
        {
            _teacherService = teacherService;
            _groupService = groupService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var data = _teacherService.GetTeachers(CultureType.am);
            return View(data);
        }

        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            var teacher = id.HasValue ? _teacherService.GetTeacherForEdit(id.Value, culture) : new TeacherAddEditVM() { Id = 0 };
            teacher.Culture = culture;
            ViewBag.Groups = _groupService.GetGroups(CultureType.am);
            return PartialView("_AddEdit", teacher);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(TeacherAddEditVM model, IFormFile formFile)
        {
            if (formFile != null)
            {
                string path = "/Files/" + formFile.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await formFile.CopyToAsync(fileStream); }
                model.ImagePath = path;
                if (model.Id == 0)
                {
                    _teacherService.Add(model);
                }
                else
                {
                    _teacherService.Update(model, model.Culture);
                }
            }
            else if (formFile == null && model.ImagePath != null)
            {
                if (model.Id == 0)
                {
                    _teacherService.Add(model);
                }
                else
                {
                    _teacherService.Update(model, model.Culture);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _teacherService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
