using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CategoryAttribute/[action]")]
    public class AdminCategoryAttributeController : Controller
    {
        private readonly ICategoryAttributeRepository _categoryAttributeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminCategoryAttributeController(ICategoryAttributeRepository categoryAttributeRepository, ICategoryRepository categoryRepository)
        {
            _categoryAttributeRepository = categoryAttributeRepository;
            _categoryRepository = categoryRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var categoryAttributes = await _categoryAttributeRepository.GetAllCategoryAttributesAsync();
            return View(categoryAttributes);
        }
        
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAttribute categoryAttribute)
        {
            await _categoryAttributeRepository.AddAsync(categoryAttribute);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var categoryAttribute = await _categoryAttributeRepository.GetByIdAsync(id);
            if (categoryAttribute == null)
            {
                return NotFound();
            }
            @ViewBag.Categories = new SelectList(await _categoryRepository.GetCategoriesAsync(), "Id", "Name");
            return View(categoryAttribute);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryAttribute categoryAttribute)
        {
            await _categoryAttributeRepository.UpdateAsync(categoryAttribute);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryAttribute = await _categoryAttributeRepository.GetByIdAsync(id);
            if (categoryAttribute == null)
            {
                return NotFound();
            }
           
            await _categoryAttributeRepository.DeleteAsync(categoryAttribute);
            return RedirectToAction("Index");
        }
    }
}
