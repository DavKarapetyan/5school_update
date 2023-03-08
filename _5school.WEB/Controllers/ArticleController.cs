using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using _5school.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IActionResult Index(ArticleType articleType)
        {
            var data = _articleService.GetAllByArticleType(CurrentCulture,articleType);
            return View(data);
        }
        public IActionResult Main(int id)
        {
            var data = _articleService.GetById(id, CurrentCulture);
            return View(data);
        }
    }
}