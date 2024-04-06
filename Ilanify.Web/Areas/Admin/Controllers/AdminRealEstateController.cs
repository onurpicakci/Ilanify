using Ilanify.Application.Interfaces;
using Ilanify.Areas.Admin.Models.ViewModels;
using Ilanify.Domain.Enums;
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
        private readonly IAttributeValueService _attributeValueService;

        public AdminRealEstateController(IRealEstateService realEstateService, ICategoryService categoryService,
            IAttributeValueService attributeValueService)
        {
            _realEstateService = realEstateService;
            _categoryService = categoryService;
            _attributeValueService = attributeValueService;
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
            @ViewBag.Types = Enum.GetValues(typeof(RealEstateType)).Cast<RealEstateType>().Select(v =>
                new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            return View(new AdminRealEstateEditViewModel
            {
                Id = realEstate.Id,
                Title = realEstate.Title,
                Description = realEstate.Description,
                Price = realEstate.Price,
                CategoryId = realEstate.CategoryId,
                SquareMeters = realEstate.SquareMeters,
                City = realEstate.Location.City,
                District = realEstate.Location.District,
                Neighborhood = realEstate.Location.Neighborhood,
                Type = realEstate.Type,
                IsActive = realEstate.IsActive
                //AttributeValues = realEstate.AttributeValues,
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
                entity.UpdatedDate = DateTime.Now;
                entity.Location.City = realEstate.City;
                entity.Location.District = realEstate.District;
                entity.Location.Neighborhood = realEstate.Neighborhood;
                entity.Type = realEstate.Type;
                entity.IsActive = realEstate.IsActive;
                //entity.AttributeValues = realEstate.AttributeValues;
                await _realEstateService.UpdateAsync(entity);
                return RedirectToAction(nameof(Index));
            }

            return View(realEstate);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            await _realEstateService.DeleteAsync(realEstate);
            return RedirectToAction(nameof(Index));
        }
    }
}