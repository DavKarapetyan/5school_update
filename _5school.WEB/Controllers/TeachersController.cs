using _5school.BLL.Services.Interfaces;
using _5school.DAL.Enums;
using _5school.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Controllers
{
    public class TeachersController : BaseController
    {
        private readonly IGroupService _groupService;
        public TeachersController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public IActionResult Index()
        {
            var data = _groupService.GetGroups(CurrentCulture);
            return View(data);
        }
    }
}