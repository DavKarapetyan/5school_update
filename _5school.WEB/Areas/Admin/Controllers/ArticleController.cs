using _5school.BLL.Services;
using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArticleController(IArticleService articleService, IWebHostEnvironment webHostEnvironment)
        {
            _articleService = articleService;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var data = _articleService.GetAll(CultureType.am);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id,CultureType culture) 
        {
            var model = id.HasValue ? _articleService.GetForEdit(id.Value,culture) : new ArticleVM() { Id = 0};
            model.CultureType = culture;
            return PartialView("_AddEdit",model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ArticleVM model,IFormFile formFile)
        {
            if (formFile != null)
            {
                string path = "/Files/" + formFile.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await formFile.CopyToAsync(fileStream); }
                model.ImageFile = path;
                if (model.Id == 0)
                {
                    _articleService.Add(model);
                }
                else
                {
                    _articleService.Update(model, model.CultureType);
                }
            }
            else if (formFile == null && model.ImageFile != null)
            {
                if (model.Id == 0)
                {
                    _articleService.Add(model);
                }
                else
                {
                    _articleService.Update(model, model.CultureType);
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
            _articleService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
