using LojaDiversidadeComuniki.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDiversidadeComuniki.Domain.Model.Order
{
    public class OrderDetail
    {
        public string Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
