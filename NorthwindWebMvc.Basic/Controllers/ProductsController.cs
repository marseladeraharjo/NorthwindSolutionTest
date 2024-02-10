using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthwindWebMvc.Basic.Models;
using NorthwindWebMvc.Basic.Models.Dto;
using NorthwindWebMvc.Basic.RepositoryContext;
using NorthwindWebMvc.Basic.Service;

namespace NorthwindWebMvc.Basic.Controllers
{
    public class ProductsController : Controller
    {
        private readonly RepositoryDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService<CategoryDto> _categoryService;

        public ProductsController(IProductService productService, RepositoryDbContext context, ICategoryService<CategoryDto> categoryService)
        {
            _productService = productService;
            _context = context;
            _categoryService = categoryService;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            var allProducts = await _productService.FindAll(false);

            foreach(var item in allProducts)
            {
                item.Category = await _categoryService.FindById(item.CategoryId, false);
            }

            return View(allProducts);
            //var repositoryDbContext = _context.Products.Include(p => p.Category);
            //return View(await repositoryDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.FindAll(true).Result.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Category = await _categoryService.FindById(product.CategoryId, false);

            return View(product);

            ////var product = await _context.Products
            ////    .Include(p => p.Category)
            ////    .FirstOrDefaultAsync(m => m.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.FindAll(false);
            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Price,Stock,Photo,CategoryId")] ProductDtoRequest product)
        {
            if (!ModelState.IsValid)
            {

                try
                {
                    var file = product.Photo;
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //collect data from dto dan filename
                        var productDto = new ProductDto
                        {
                            ProductName = product.ProductName,
                            Price = product.Price,
                            Stock = product.Stock,
                            Photo = fileName,
                            CategoryId = product.CategoryId,
                            Category = product.Category
                        };
                        _productService.Create(productDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
            //if (ModelState.IsValid)
            //{
            //    _context.Add(product);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
            //return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.FindById((int)id, true);
            var productDtoRequest = new ProductDtoRequest
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
            };
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
            return View(productDtoRequest);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,Stock,Photo,CategoryId")] ProductDtoRequest product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    var file = product.Photo;
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //collect data from dto dan filename
                        var productDto = new ProductDto
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            Stock = product.Stock,
                            Photo = fileName,
                            CategoryId = product.CategoryId,
                            Category = product.Category
                        };
                        _productService.Update(productDto);

                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products
            //    .Include(p => p.Category)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var product = _productService.FindAll(true).Result.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return Problem("Entity set 'RepositoryDbContext.Products'  is null.");
            }
            //var product = await _context.Products.FindAsync(id);
            var product = _productService.FindAll(true).Result.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _productService.Delete(product);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_productService.FindAll(true)?.Result.Any(p => p.Id == id)).GetValueOrDefault();
        }
    }
}
