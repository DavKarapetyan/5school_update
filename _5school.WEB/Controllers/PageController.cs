using _5school.BLL.Services.Interfaces;
using _5school.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService; 
        }
        public IActionResult GetPage(int id) 
        {
            var data = _pageService.GetById(id, CurrentCulture);
            return View(data);
        }
    }
}
