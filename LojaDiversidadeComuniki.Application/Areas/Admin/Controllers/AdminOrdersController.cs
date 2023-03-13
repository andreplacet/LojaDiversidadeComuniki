using LojaDiversidadeComuniki.Domain.Model.Order;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaDiversidadeComuniki.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Administrador")]
    public class AdminOrdersController : Controller
    {
        private readonly EntityContext _context;

        public AdminOrdersController(EntityContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPedidos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }
    }

}
