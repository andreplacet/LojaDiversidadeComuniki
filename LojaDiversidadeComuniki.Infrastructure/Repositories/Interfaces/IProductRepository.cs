using LojaDiversidadeComuniki.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> Products();
    }
}
