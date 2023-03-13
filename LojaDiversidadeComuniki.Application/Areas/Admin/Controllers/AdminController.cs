using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDiversidadeComuniki.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Administrador")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
