using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Ilanify.Application.Interfaces;

public class RealEstateController : Controller
{
    private readonly IRealEstateService _realEstateService;

    public RealEstateController(IRealEstateService realEstateService)
    {
        _realEstateService = realEstateService;
    }

    public IActionResult Index()
    {
       var realEstates = _realEstateService.GetAllAsync();
        return View(realEstates.Result);
    }

    public IActionResult Create()
    {
        
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