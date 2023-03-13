using LojaDiversidadeComuniki.Domain.Model.Order;
using LojaDiversidadeComuniki.Domain.Model.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void SetOrder(Order order, List<ShoppingCart> shoppingCarts);
    }
}
