using LojaDiversidadeComuniki.Domain.Model.Products;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaDiversidadeComuniki.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Administrador")]
    public class AdminCategoriesController : Controller
    {
        private readonly EntityContext _context;

        public AdminCategoriesController(EntityContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCategorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/AdminCategorias/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Admin/AdminCategorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category categoria)
        {
            categoria.Id = Guid.NewGuid().ToString();
             _context.Add(categoria);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminCategorias/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categories.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Admin/AdminCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] Category categoria)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            category.Name = categoria.Name;

            if (id.ToString() != category.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             }
             catch (DbUpdateConcurrencyException)
             {
                if (!CategoriaExists(categoria.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Admin/AdminCategorias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Admin/AdminCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var categoria = await _context.Categories.FindAsync(id.ToString());
            _context.Categories.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
