using _5school.BLL.Services;
using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class StreamController : Controller
    {
        private readonly IStreamService _streamService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StreamController(IStreamService streamService, IWebHostEnvironment webHostEnvironment)
        {
            _streamService = streamService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var data = _streamService.GetStreams(CultureType.am);
            return View(data);
        }

        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            var stream = id.HasValue ? _streamService.GetStreamById(id.Value, culture) : new BLL.ViewModels.StreamVM() { Id = 0 };
            stream.Culture = culture;
            return PartialView("_AddEdit", stream);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(StreamVM model,IFormFile formFile)
        {
            if (formFile != null)
            {
                string path = "/Files/" + formFile.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await formFile.CopyToAsync(fileStream); }
                model.ImageFile = path;
                if (model.Id == 0)
                {
                    _streamService.Add(model);
                }
                else
                {
                    _streamService.Update(model, model.Culture);
                }
            }
            else if (formFile == null && model.ImageFile != null)
            {
                if (model.Id == 0)
                {
                    _streamService.Add(model);
                }
                else
                {
                    _streamService.Update(model, model.Culture);
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
            _streamService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
