using LojaDiversidadeComuniki.Application.Services;
using LojaDiversidadeComuniki.Domain.Model.Order;
using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDiversidadeComuniki.Application.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCartService _shoppingCartService;

        public OrderController(IOrderRepository orderRepository, ShoppingCartService shoppingCartService)
        {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            decimal totalOrderValue = 0;

            order.Id = Guid.NewGuid().ToString();
            order.Name = $"{Guid.NewGuid()}-LojaComuniki";

            var itens = _shoppingCartService.GetShoppingCartItens();

            foreach(var item in itens)
            {
                totalOrderValue = item.Product.Price * decimal.Parse(item.Quantity.ToString());
            }

            ViewBag.QuantidadeItendPedido = itens.Count;
            ViewBag.TotalPedido = _shoppingCartService.GetToTal();

            order.Total = totalOrderValue;

            _orderRepository.SetOrder(order, itens);

            _shoppingCartService.CleanCart();

            return View("~/Views/Order/CheckoutComplete.cshtml", order);
        }
    }
}
