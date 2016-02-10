namespace HeroicallyRecipes.Services
{
    using System.Linq;
    using HeroicallyRecipes.Models;
    using HeroicallyRecipes.Services.Contracts;
    using HeroicallyRecipes.Data.Repositories;

    public class RecipeService : IRecipeService
    {
        private IRepository<Recipe> recipes;

        public RecipeService(IRepository<Recipe> recipes)
        {
            this.recipes = recipes;
        }

        public IQueryable<Recipe> GetAll()
        {
            return this.recipes.All();
        }
    }
}
