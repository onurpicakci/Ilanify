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

    public RealEstateController(IRealEstateService realEstateService, ICategoryService categoryService,
        UserManager<ApplicationUser> userManager)
    {
        _realEstateService = realEstateService;
        _categoryService = categoryService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _realEstateService.GetRealEstatesGroupedByLocationAsync();
        return View(locations);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RealEstate realEstate, [FromForm] List<AttributeValue> attributeValues,
        List<IFormFile> images)
    {
        var loggedUser = await _userManager.GetUserAsync(HttpContext.User);
        if (loggedUser == null)
        {
            return RedirectToAction("Login", "Account");
        }

        realEstate.ApplicationUserId = loggedUser.Id;

        await _realEstateService.AddAsync(realEstate);

        if (images != null && images.Any())
        {
            foreach (var image in images)
            {
                var imageUrl = await UploadImage(image);
                if (imageUrl != null)
                {
                    var realEstateImage = new RealEstateImage
                    {
                        ImageUrl = imageUrl,
                        RealEstateId = realEstate.Id
                    };
                    await _realEstateService.UploadImageAsync(realEstateImage);
                }
            }
        }

        foreach (var attributeValue in attributeValues)
        {
            attributeValue.RealEstateId = realEstate.Id;
        }

        realEstate.AttributeValues = attributeValues;

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var realEstate = await _realEstateService.GetByIdAsync(id);
        if (realEstate == null)
        {
            return NotFound();
        }

        return View(realEstate);
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

    private async Task<string> UploadImage(IFormFile image)
    {
        if (image == null)
        {
            return null;
        }

        var uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return uniqueFileName;
    }
}