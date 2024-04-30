using Ilanify.Application.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IRealEstateService _realEstateService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminHomeController(IRealEstateService realEstateService, ICategoryService categoryService, UserManager<ApplicationUser> userManager)
        {
            _realEstateService = realEstateService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var realEstatesCount = await _realEstateService.GetRealEstatesCount();
            
            var totalCategories = await _categoryService.GetAllAsync();
            ViewBag.Categories = totalCategories.Count();

            var totalRegisteredUsers = await _userManager.Users.CountAsync();
            ViewBag.RegisteredUsers = totalRegisteredUsers;
            return View(realEstatesCount);
        }
    }
}
