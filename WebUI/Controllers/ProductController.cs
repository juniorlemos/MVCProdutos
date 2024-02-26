using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.InsertAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId =
                            new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            }
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {            
            var productDto = await _productService.GetByIdAsync(id);

            if (productDto == null) return NotFound();

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }
        public async Task<IActionResult> Details(int id)
        {
            var productDto = await _productService.GetByIdAsync(id);

            if (productDto == null) return NotFound();
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetByIdAsync(id);

            if (productDto == null) return NotFound();

            return View(productDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
