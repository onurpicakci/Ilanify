using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Dtos;
using Ilanify.Domain.Entities;
using Ilanify.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Controllers;

public class RealEstateController : Controller
{
    private readonly IRealEstateService _realEstateService;
    private readonly ICategoryService _categoryService;
    private readonly ICategoryAttributeService _categoryAttributeService;
    private readonly IFavoriteService _favoriteService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RealEstateController(IRealEstateService realEstateService, ICategoryService categoryService,
        UserManager<ApplicationUser> userManager, ICategoryAttributeService categoryAttributeService,
        IFavoriteService favoriteService)
    {
        _realEstateService = realEstateService;
        _categoryService = categoryService;
        _userManager = userManager;
        _categoryAttributeService = categoryAttributeService;
        _favoriteService = favoriteService;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _realEstateService.GetTop4CitiesByRealEstateCountAsync();
        var cityRealEstateCounts = locations.ToList();
        var cities = new List<CityRealEstateCount>
        {
            new CityRealEstateCount
            {
                City = "Istanbul", Count = cityRealEstateCounts.FirstOrDefault(l => l.City == "Istanbul")?.Count ?? 0,
                ImageUrl = "istanbul.jpg"
            },
            new CityRealEstateCount
            {
                City = "Ankara", Count = cityRealEstateCounts.FirstOrDefault(l => l.City == "Ankara")?.Count ?? 0,
                ImageUrl = "ankara.jpg"
            },
            new CityRealEstateCount
            {
                City = "Izmir", Count = cityRealEstateCounts.FirstOrDefault(l => l.City == "Izmir")?.Count ?? 0,
                ImageUrl = "izmir.jpg"
            },
            new CityRealEstateCount
            {
                City = "Bursa", Count = cityRealEstateCounts.FirstOrDefault(l => l.City == "Bursa")?.Count ?? 0,
                ImageUrl = "bursa.jpg"
            }
        };

        return View(cities);
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
        realEstate.ListingDate = DateTime.Today;
        realEstate.IsActive = true;

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
    public async Task<IActionResult> Details(int realEstateId)
    {
        var realEstate = await _realEstateService.GetRealEstateByIdWithDetailsAsync(realEstateId);
        if (realEstate == null)
        {
            return NotFound();
        }

        return View(realEstate);
    }

    [HttpGet("GetCategoryAttributes/{categoryId}")]
    public async Task<IActionResult> GetCategoryAttributes(int categoryId)
    {
        var attributes = await _categoryAttributeService.GetCategoryAttributesAsync(categoryId);
        if (attributes == null || !attributes.Any())
        {
            return NotFound();
        }

        return Ok(attributes);
    }

    [HttpGet]
    public async Task<IActionResult> ListRealEstatesByLocation(string location)
    {
        var realEstates = await _realEstateService.GetRealEstatesByLocationAsync(location);
        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.City = location;
        ViewBag.Count = realEstates.Count();
        return View(realEstates);
    }

    [HttpGet]
    public async Task<IActionResult> ListRealEstatesByType(int realEstateType)
    {
        var realEstates = await _realEstateService.GetRealEstatesByTypeAsync((RealEstateType)realEstateType);
        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.Type = (RealEstateType)realEstateType;
        ViewBag.Count = realEstates.Count();
        return View(realEstates);
    }

    [HttpGet]
    public async Task<IActionResult> ListRealEstatesByCategory(int categoryId)
    {
        var realEstates = await _realEstateService.GetRealEstatesByCategoryAsync(categoryId);
        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.CategoryName = categories.FirstOrDefault(c => c.Id == categoryId)?.Name;
        ViewBag.Count = realEstates.Count();

        return View(realEstates);
    }

    public async Task<IActionResult> Search(string searchText)
    {
        var searchFilter = new RealEstateSearchQuery { SearchText = searchText };
        var realEstates = await _realEstateService.SearchRealEstatesAsync(searchFilter);
        var categories = await _categoryService.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.SearchQuery = searchText;
        ViewBag.Count = realEstates.Count();
        return View(realEstates);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite(string userId, int realEstateId)
    {
        await _favoriteService.AddFavoriteAsync(userId, realEstateId);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> RemoveFavorite(string userId, int realEstateId)
    {
        await _favoriteService.RemoveFavoriteAsync(userId, realEstateId);
        return RedirectToAction("Index");
    }

    private async Task<string> UploadImage(IFormFile image)
    {
        if (image == null)
        {
            return null;
        }

        var uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/real-estate-images/",
            uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return uniqueFileName;
    }
}