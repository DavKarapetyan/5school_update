using _5school.DAL.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Controllers
{
    public class BaseController : Controller
    {
        protected CultureType CurrentCulture
        {
            get
            {
                var cultureInfo = Request.HttpContext.Features.Get<IRequestCultureFeature>();

                var culture = cultureInfo.RequestCulture.Culture.Name;
                return (CultureType)Enum.Parse(typeof(CultureType), culture);
            }
        }
    }
}