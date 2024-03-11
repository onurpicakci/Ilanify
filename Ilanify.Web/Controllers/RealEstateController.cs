using Ilanify.Application.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Controllers;

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

    public async Task<IActionResult> Index()
    {
        var realEstates = await _realEstateService.GetAllAsync();
        return View(realEstates);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RealEstate realEstate, [FromForm] List<AttributeValue> attributeValues)
    {
        var loggedUser = await _userManager.GetUserAsync(HttpContext.User);
        if (loggedUser == null)
        {
            return RedirectToAction("Login", "Account");
        }

        realEstate.ApplicationUserId = loggedUser.Id;
        
        await _realEstateService.AddAsync(realEstate);
        
        foreach (var attributeValue in attributeValues)
        {
            attributeValue.RealEstateId = realEstate.Id;
        }
        
        realEstate.AttributeValues = attributeValues;
        
        return RedirectToAction("Index");
    }

    
    [HttpGet("GetCategoryAttributes/{categoryId}")]
    public async Task<IActionResult> GetCategoryAttributes(int categoryId)
    {
        var attributes = await _categoryService.GetCategoryAttributesAsync(categoryId);
        if (attributes == null || !attributes.Any())
        {
            return NotFound();
        }

        return Ok(attributes);
    }
}