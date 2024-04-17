using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CategoryAttribute/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoryAttributeController : Controller
    {
        private readonly ICategoryAttributeService _categoryAttributeService;
        private readonly ICategoryService _categoryService;


        public AdminCategoryAttributeController(ICategoryAttributeService categoryAttributeService, ICategoryService categoryService)
        {
            _categoryAttributeService = categoryAttributeService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categoryAttributes = await _categoryAttributeService.GetAllCategoryAttributesAsync();
            return View(categoryAttributes);
        }
        
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAttribute categoryAttribute)
        {
            await _categoryAttributeService.AddAsync(categoryAttribute);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var categoryAttribute = await _categoryAttributeService.GetByIdAsync(id);
            if (categoryAttribute == null)
            {
                return NotFound();
            }
            @ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View(categoryAttribute);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryAttribute categoryAttribute)
        {
            await _categoryAttributeService.UpdateAsync(categoryAttribute);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryAttribute = await _categoryAttributeService.GetByIdAsync(id);
            if (categoryAttribute == null)
            {
                return NotFound();
            }
           
            await _categoryAttributeService.DeleteAsync(categoryAttribute);
            return RedirectToAction("Index");
        }
    }
}
