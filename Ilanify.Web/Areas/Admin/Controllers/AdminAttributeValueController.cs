using Ilanify.Application.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AttributeValue/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminAttributeValueController : Controller
    {
       private readonly IAttributeValueService _attributeValueService;
       private readonly ICategoryAttributeService _categoryAttributeService;

        public AdminAttributeValueController(IAttributeValueService attributeValueService, ICategoryAttributeService categoryAttributeService)
        {
            _attributeValueService = attributeValueService;
            _categoryAttributeService = categoryAttributeService;
        }

        public async Task<IActionResult> Index()
        {
            var attributeValues = await _attributeValueService.GetAllAttributeValuesAsync();
            return View(attributeValues);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var attributeValue = await _attributeValueService.GetByIdAsync(id);
            if (attributeValue == null)
            {
                return NotFound();
            }
            var categoryAttributes = await _categoryAttributeService.GetAllCategoryAttributesAsync();
            ViewBag.CategoryAttributeId = new SelectList(categoryAttributes, "Id", "Name", attributeValue.CategoryAttributeId);
            return View(attributeValue);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                await _attributeValueService.UpdateAsync(attributeValue);
                return RedirectToAction(nameof(Index));
            }
            return View(attributeValue);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var attributeValue = await _attributeValueService.GetByIdAsync(id);
            if (attributeValue == null)
            {
                return NotFound();
            }
            return View(attributeValue);
        }
    }
}
