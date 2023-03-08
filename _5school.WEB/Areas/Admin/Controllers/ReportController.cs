using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IReportService reportService,IWebHostEnvironment webHostEnvironment)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = _reportService.GetReports(CultureType.am);
            return View(data);
        }

        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            ReportVM model = id.HasValue ? _reportService.GetReportById(id.Value,culture) : new ReportVM() { Id = 0};
            model.Culture = culture;
            return PartialView("_AddEdit" ,model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ReportVM model, IFormFile formFile)
        {
            if (formFile != null)
            { 
                string path = "/Files/" + formFile.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await formFile.CopyToAsync(fileStream); }
                model.FilePath = path;
                if (model.Id == 0)
                {
                    _reportService.Add(model);
                }
                else
                {
                    _reportService.Update(model, model.Culture);
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
            _reportService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
