using _5school.BLL.ViewModels;
using _5school.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _5school.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM model)
        {
            if (ModelState.IsValid)
            { 
                User user = new User() { Email = model.Email, UserName = model.Email, DateOfBirth = model.DOB };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return PartialView();
        }
        public async Task<IActionResult> Edit(string id)
        { 
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            { 
                return NotFound();
            }
            EditUserVM model = new EditUserVM() { Id = Convert.ToString(user.Id), Email = user.Email, DOB = user.DateOfBirth};
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM model)
        { 
            if (ModelState.IsValid) 
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null) 
                { 
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.DateOfBirth = model.DOB;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null) 
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
