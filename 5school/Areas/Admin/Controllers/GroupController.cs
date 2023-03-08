using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace _5school.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public IActionResult Index()
        {
            var data = _groupService.GetGroups(CultureType.am);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            GroupVM group = id.HasValue ? _groupService.GetGroupById(id.Value,culture) : new GroupVM() { Id = 0};
            group.Culture = culture;
            return PartialView("_AddEdit", group);
        }
        [HttpPost]
        public IActionResult AddEdit(GroupVM model) 
        {
            if (model.Id == 0)
            {
                _groupService.Add(model);
            }
            else 
            {
                _groupService.Update(model, model.Culture);
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
            _groupService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
