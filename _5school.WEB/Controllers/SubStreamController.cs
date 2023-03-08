using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using _5school.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Controllers
{
    public class SubStreamController : BaseController
    {
        private readonly ISubStreamService _subStreamService;
        public SubStreamController(ISubStreamService subStreamService)
        {
            _subStreamService = subStreamService;
        }
        public IActionResult Index()
        {
            var data = _subStreamService.GetSubStreams(CurrentCulture);
            return View(data);
        }
    }
}