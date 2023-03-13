using LojaDiversidadeComuniki.Domain.Model.Order;
using LojaDiversidadeComuniki.Domain.Model.ShoppingCart;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;

namespace LojaDiversidadeComuniki.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EntityContext _entityContext;

        public OrderRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void SetOrder(Order order, List<ShoppingCart> shoppingCarts)
        {
            foreach(var item in shoppingCarts)
            {
                var details = new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    Product = item.Product,
                    Order = order,
                };
                _entityContext.OrderDetails.Add(details);
            }

            _entityContext.Orders.Add(order);
            _entityContext.SaveChanges();
        }
    }
}
