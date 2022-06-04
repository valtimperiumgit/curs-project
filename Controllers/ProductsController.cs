#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Valtimperium;
using Valtimperium.ViewModels;

namespace Valtimperium.Controllers
{
    public class ProductsController : Controller
    {
      private readonly ValtimperContext _context;

        public ProductsController(ValtimperContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var valtimperiumContext = _context.Products.ToList();
        
           List<ProductViewModel> viewProducts = new List<ProductViewModel>();

            foreach (var item in valtimperiumContext)
            {
                Type type = await _context.Types.FirstOrDefaultAsync(t => t.IdType == item.IdType);
                ProductViewModel model = new ProductViewModel {
                    IdProduct = item.IdProduct,
                    Name = item.Name,
                    Photo = item.Photo,
                    Availability = item.Availability,
                    Description = item.Description,
                    Price = item.Price,
                    Type = type.Name
                };
                viewProducts.Add(model);
            }

            return View(viewProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.Types, "Name", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Name,Price,Type,Description,Availability,Photo")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                int newIdType = _context.Types.FirstOrDefault(t => t.Name == product.Type).IdType;
                Product newProduct = new Product
                {
                    IdProduct = product.IdProduct,
                    Name = product.Name,
                    Description = product.Description,
                    Availability = product.Availability,
                    IdType = newIdType,
                    Photo = product.Photo,
                    Price = product.Price,
                };
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdType"] = new SelectList(_context.Types, "Name", "Name", product.Type);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            var type = await _context.Types.FirstOrDefaultAsync(t => t.IdType == product.IdType);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel
            {
                IdProduct = product.IdProduct,
                Name = product.Name,
                Photo = product.Photo,
                Availability = product.Availability,
                Description = product.Description,
                Price = product.Price,
                Type = type.Name
            };

            ViewData["IdType"] = new SelectList(_context.Types, "Name", "Name", type.Name);
            ViewData["Photo"] = new SelectList(_context.Products, "Photo", "Photo", product.Photo);
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,Name,Price,Type,Description,Availability,Photo")] ProductViewModel product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int newIdType = _context.Types.FirstOrDefault(t => t.Name == product.Type).IdType;
                Product oldProductPhoto = _context.Products.FirstOrDefault(t => t.IdProduct == id);
                
                Product newProduct = new Product
                {
                    IdProduct = product.IdProduct,
                    Name = product.Name,
                    Description = product.Description,
                    Availability = product.Availability,
                    IdType = newIdType,
                    Photo = oldProductPhoto.Photo,
                    Price = product.Price,
                };
                
                try
                {
                    _context.Products.Remove(oldProductPhoto);
                    _context.Update(newProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
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
            ViewData["IdType"] = new SelectList(_context.Types, "Type", "Type", product.Type);
            
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
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
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.IdProduct == id);
        }
    }
}
