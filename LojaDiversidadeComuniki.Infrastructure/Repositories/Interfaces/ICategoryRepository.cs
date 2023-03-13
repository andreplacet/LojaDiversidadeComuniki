using LojaDiversidadeComuniki.Domain.Model.Products;

namespace LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> Categories();
    }
}
