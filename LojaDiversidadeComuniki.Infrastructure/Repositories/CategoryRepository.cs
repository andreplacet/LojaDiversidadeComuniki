using LojaDiversidadeComuniki.Domain.Model.Products;
using LojaDiversidadeComuniki.Infrastructure.DataContext;
using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;

namespace LojaDiversidadeComuniki.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EntityContext _entityContext;

        public CategoryRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public IEnumerable<Category> Categories()
        {
            return _entityContext.Categories.ToList().Any() ?
                _entityContext.Categories.ToList() : 
                new List<Category>() { };
        }
    }
}
