using LojaDiversidadeComuniki.Domain.Model.Products;
using LojaDiversidadeComuniki.Domain.Model.ShoppingCart;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace LojaDiversidadeComuniki.Application.Services
{
    public class ShoppingCartService
    {
        private readonly EntityContext _context;

        public ShoppingCartService(EntityContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCart> ShoppingCarts { get; set; }

        public static ShoppingCartService GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<EntityContext>();

            string ShoppingCartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();

            session.SetString("ShoppingCartId", ShoppingCartId);

            return new ShoppingCartService(context)
            {
                ShoppingCartId = ShoppingCartId
            };
        }

        public void AddToCart(Product product)
        {
            var item = _context.ShoppingCarts.SingleOrDefault(i => i.Product.Id == product.Id && i.ShoppingCartId == ShoppingCartId);

            if (item == null)
            {
                var shoppingCart = new ShoppingCart()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Quantity = 1
                };
                _context.ShoppingCarts.Add(shoppingCart);
            }
            else
            {
                item.Quantity++;
            }
            _context.SaveChanges();
        }

        public void RemoveFromCart(Product product)
        {
            var item = _context.ShoppingCarts.SingleOrDefault(i => i.Product.Id == product.Id && i.ShoppingCartId == ShoppingCartId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    _context.ShoppingCarts.Remove(item);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCart> GetShoppingCartItens()
        {
            return ShoppingCarts ??= _context.ShoppingCarts
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList();
        }

        public void CleanCart()
        {
            var cartItens = _context.ShoppingCarts
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCarts.RemoveRange(cartItens);
            _context.SaveChanges();
        }

        public decimal GetToTal()
        {
            var total = _context.ShoppingCarts
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Join(
                    inner: _context.Products,
                    outerKeySelector: s => s.Product.Id,
                    innerKeySelector: p => p.Id,
                    resultSelector: (o, i) => new { ShoppingCart = o, Product = i }
                )
                .AsEnumerable() // Executa a consulta em memória
                .Select(s => Convert.ToDouble(s.Product.Price * s.ShoppingCart.Quantity))
                .Sum();

            return decimal.Parse(total.ToString());
        }
    }

}
