using LojaDiversidadeComuniki.Application.Migrations;
using LojaDiversidadeComuniki.Application.Services;

namespace LojaDiversidadeComuniki.Application.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartService ShoppingCart { get; set; }
        public decimal Total { get; set; }
    }
}
