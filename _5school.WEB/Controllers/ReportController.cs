using _5school.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IActionResult Index()
        {
            var data = _reportService.GetReports(CurrentCulture);
            return View(data);
        }
        public IActionResult Details(int id) {
            var data = _reportService.GetReportById(id, CurrentCulture);
            return View(data);
        }
    }
}
