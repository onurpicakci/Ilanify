using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Ilanify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

public class RealEstateController : Controller
{
    private readonly IRealEstateService _realEstateService;
    private readonly ICategoryService _categoryService;

    public RealEstateController(IRealEstateService realEstateService, ICategoryService categoryService)
    {
        _realEstateService = realEstateService;
        _categoryService = categoryService;
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
        // Yeni bir ilan eklemeyi deneyin
        await _realEstateService.AddAsync(realEstate);
        
        // İlanlar sayfasına yönlendir
        return RedirectToAction("Index");
    }
}