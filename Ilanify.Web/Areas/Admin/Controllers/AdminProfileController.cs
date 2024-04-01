using Ilanify.Areas.Admin.Models.ViewModels;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Profile/[action]")]
    public class AdminProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminProfileController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new AdminEditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AdminEditUserViewModel user)
        {
            var userToUpdate = await _userManager.FindByIdAsync(user.Id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.UserName = user.UserName;
            userToUpdate.PhoneNumber = user.PhoneNumber;

            await _userManager.UpdateAsync(userToUpdate);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> AddRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();
            var model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles.ToList(),
                AllRoles = allRoles
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddRole(ChangeRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = model.UserRoles.Except(userRoles);
            var removedRoles = userRoles.Except(model.UserRoles);

            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("Index");
        }
    }
}
