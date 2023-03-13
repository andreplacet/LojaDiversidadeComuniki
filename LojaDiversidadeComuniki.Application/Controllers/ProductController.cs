using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaDiversidadeComuniki.Application.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(string categoria)
        {
            if(string.IsNullOrWhiteSpace(categoria))
            {
                var products = _productRepository.Products();
                return View(products);
            }

            return View(_productRepository.Products()
                .Where(c => c.Category.Name.Equals(categoria)).OrderBy(c => c.Name));
        }
    }
}
