using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using _5school.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace _5school.WEB.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public ArticleController(IArticleService articleService,IStringLocalizer<SharedResource> stringLocalizer)
        {
            _articleService = articleService;
            _stringLocalizer = stringLocalizer;
        }
        public IActionResult Index(ArticleType articleType)
        {
            var data = _articleService.GetAllByArticleType(CurrentCulture,articleType);
            ViewBag.Title = articleType == ArticleType.News ? _stringLocalizer["nav3"] : _stringLocalizer["nav4"];
            return View(data);
        }
        public IActionResult Main(int id)
        {
            var data = _articleService.GetById(id, CurrentCulture);
            return View(data);
        }
    }
}