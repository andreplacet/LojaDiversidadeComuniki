using LojaDiversidadeComuniki.Application.Models.ViewModels;
using LojaDiversidadeComuniki.Application.Services;
using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDiversidadeComuniki.Application.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(IProductRepository productRepository,
            ShoppingCartService shoppingCartService)
        {
            _productRepository = productRepository;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var cartItens = _shoppingCartService.GetShoppingCartItens();
            _shoppingCartService.ShoppingCarts = cartItens;

            var ShoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCartService,
                Total = _shoppingCartService.GetToTal()
            };

            return View(ShoppingCartViewModel);
        }

        [Authorize]
        public IActionResult AddItemToCart(string productId)
        {
            var selectedProduct = _productRepository.Products()
                .FirstOrDefault(p => p.Id == productId.ToString());

            if(selectedProduct != null)
            {
                _shoppingCartService.AddToCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItemFromCart(int productId)
        {
            var selectedProduct = _productRepository.Products()
                .FirstOrDefault(p => p.Id == productId.ToString());

            if(selectedProduct != null)
            {
                _shoppingCartService.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}
