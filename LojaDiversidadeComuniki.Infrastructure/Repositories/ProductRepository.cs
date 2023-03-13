using LojaDiversidadeComuniki.Domain.Model.Products;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaDiversidadeComuniki.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EntityContext _entityContext;

        public ProductRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public IEnumerable<Product> Products()
        {
            return _entityContext.Products.Include(x => x.Category).ToList();
        }
    }
}
