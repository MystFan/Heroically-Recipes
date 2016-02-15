namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using Contracts;

    public class CategoriesService : ICategoriesService
    {
        private IDbRepository<Category> categories;

        public CategoriesService(IDbRepository<Category> categoriesRepo)
        {
            this.categories = categoriesRepo;
        }

        public IQueryable<Category> GetAll()
        {
            return this.categories.All();
        }
    }
}
