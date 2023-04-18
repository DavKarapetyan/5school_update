using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PageController(IPageService pageService, IWebHostEnvironment webHostEnvironment)
        {
            _pageService = pageService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var data = _pageService.GetAll(CultureType.am);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            PageVM model = id.HasValue ? _pageService.GetForEdit(id.Value, culture) : new PageVM { Id = 0 };
            model.CultureType = culture;
            return PartialView("_AddEdit", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(PageVM model, IFormFile formFile)
        {
            if (formFile != null)
            {
                string path = "/Files/" + formFile.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await formFile.CopyToAsync(fileStream); }
                model.ImageFile = path;
            }
            else {
                model.ImageFile = null;
            }
            if (model.Id == 0)
            {
                _pageService.Add(model);
            }
            else 
            {
                _pageService.Update(model, model.CultureType);
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
            _pageService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
