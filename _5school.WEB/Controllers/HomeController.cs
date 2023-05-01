using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using _5school.WEB.Models;
using Azure;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace _5school.WEB.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IStreamService _streamService;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(IStreamService streamService, IStringLocalizer<SharedResource> sharedLocalizer, IStringLocalizer<HomeController> localizer)
        {
            _streamService = streamService;
            _sharedLocalizer = sharedLocalizer;
            _localizer = localizer;

        }
        public string Test()
        {
            string text = _sharedLocalizer["aaaa"];
            return text;
        }

        public IActionResult Index()
        {
            var data = _streamService.GetStreams(CurrentCulture);
            return View(data);
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            returnUrl = returnUrl.Replace(CurrentCulture.ToString(), culture);
            return LocalRedirect(returnUrl);
        }
        public ActionResult RedirectToDefaultLanguage()
        {
            var lang = CurrentCulture.ToString();
            if (lang == "am")
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", new { lang = lang});
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}