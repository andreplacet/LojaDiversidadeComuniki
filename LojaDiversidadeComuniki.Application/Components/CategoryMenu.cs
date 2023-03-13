using LojaDiversidadeComuniki.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaDiversidadeComuniki.Application.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoryRepository.Categories().OrderBy(x=> x.Name);
            return View(categorias.ToList());
        }
    }
}
