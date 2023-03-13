using LojaDiversidadeComuniki.Domain.Model.Products;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojaDiversidadeComuniki.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Administrador")]
    public class AdminProductsController : Controller
    {
        private readonly EntityContext _context;

        public AdminProductsController(EntityContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminLanches
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(l => l.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminLanches/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Products
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // GET: Admin/AdminLanches/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/AdminLanches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image,Price,Category")] Product product)
        {
            var CategoryName = _context.Categories.FirstOrDefault(x => x.Id == product.Category.Id);
            product.Id = Guid.NewGuid().ToString();
            product.Category = CategoryName;
            _context.Add(product);
            await _context.SaveChangesAsync();
            ViewData["CategoriaId"] = new SelectList(_context.Categories, "Id", "Name");

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminLanches/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id.ToString());
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(product);
        }

        // POST: Admin/AdminLanches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Image,Price,Category")] Product product)
        {
            //var imageBase64 = Convert.ToBase64String(product.Image);

            if (id.ToString() != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExiste(product.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(product);
        }

        // GET: Admin/AdminLanches/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Products
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // POST: Admin/AdminLanches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lanche = await _context.Products.FindAsync(id.ToString());
            _context.Products.Remove(lanche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExiste(string id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
