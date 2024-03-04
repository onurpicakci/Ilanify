using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Ilanify.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

public class RealEstateController : Controller
{
    private readonly IRealEstateService _realEstateService;
    private readonly ICategoryService _categoryService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RealEstateController(IRealEstateService realEstateService, ICategoryService categoryService, UserManager<ApplicationUser> userManager)
    {
        _realEstateService = realEstateService;
        _categoryService = categoryService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
       var realEstates = _realEstateService.GetAllAsync();
        return View(realEstates.Result);
    }

    public IActionResult Create()
    {
        var categories = _categoryService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories.Result, "Id", "Name");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RealEstate realEstate)
    {
        if (!ModelState.IsValid)
        {
            var loggedUser = await _userManager.GetUserAsync(HttpContext.User);
            
            if (loggedUser != null)
            {
                realEstate.ApplicationUserId = loggedUser.Id;
                await _realEstateService.AddAsync(realEstate);
        
                return RedirectToAction("Index");
            }
        }
        
        var categories = _categoryService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories.Result, "Id", "Name");
        return View(realEstate);
    }
}