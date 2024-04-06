using Ilanify.Application.Interfaces;
using Ilanify.Areas.Admin.Models.ViewModels;
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
            @ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View(new AdminRealEstateEditViewModel
            {
                Id = realEstate.Id,
                Title = realEstate.Title,
                Description = realEstate.Description,
                Price = realEstate.Price,
                CategoryId = realEstate.CategoryId,
                SquareMeters = realEstate.SquareMeters
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AdminRealEstateEditViewModel realEstate)
        {
            if (ModelState.IsValid)
            {
                var entity = await _realEstateService.GetByIdAsync(realEstate.Id);
                entity.Title = realEstate.Title;
                entity.Price = realEstate.Price;
                entity.Description = realEstate.Description;
                entity.CategoryId = realEstate.CategoryId;
                entity.SquareMeters = realEstate.SquareMeters;
                await _realEstateService.UpdateAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(realEstate);
        }
    }
}
