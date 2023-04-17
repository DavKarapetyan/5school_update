using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace _5school.WEB.Components
{
    public class PageViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;
        protected CultureType CurrentCulture
        {
            get
            {
                var cultureInfo = Request.HttpContext.Features.Get<IRequestCultureFeature>();

                var culture = cultureInfo.RequestCulture.Culture.Name;
                return (CultureType)Enum.Parse(typeof(CultureType), culture);
            }
        }
        public PageViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }
        public IViewComponentResult Invoke() 
        {
            var data = _pageService.GetAll(CurrentCulture);
            return View(data);
        }
    }
}
