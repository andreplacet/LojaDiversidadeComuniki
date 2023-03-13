using LojaDiversidadeComuniki.Application.Services;
using LojaDiversidadeComuniki.Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LojaDiversidadeComuniki.Domain.Model.ShoppingCart;

namespace LojaDiversidadeComuniki.Application.Components
{
    public class ShoppingCartResume : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartResume(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IViewComponentResult Invoke()
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
    }
}
