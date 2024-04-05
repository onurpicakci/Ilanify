using Ilanify.Application.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/RealEstate/[action]")]
    public class AdminRealEstateController : Controller
    {
        private readonly IRealEstateService _realEstateService;
        private readonly ICategoryService _categoryService;

        public AdminRealEstateController(IRealEstateService realEstateService, ICategoryService categoryService)
        {
            _realEstateService = realEstateService;
            _categoryService = categoryService;
        }
        
        public async Task<IActionResult> Index()
        {
            var realEstates = await _realEstateService.GetAllAsync();
            return View(realEstates);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            @ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View(realEstate);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(RealEstate realEstate)
        {
            
                realEstate.Category = await _categoryService.GetByIdAsync(realEstate.CategoryId);
                await _realEstateService.UpdateAsync(realEstate);
                return RedirectToAction(nameof(Index));
            return View(realEstate);
        }
    }
}
