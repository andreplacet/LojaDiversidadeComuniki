using LojaDiversidadeComuniki.Domain.Model.Products;

namespace LojaDiversidadeComuniki.Domain.Model.ShoppingCart
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
