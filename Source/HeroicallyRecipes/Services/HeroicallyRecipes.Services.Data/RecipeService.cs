namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Data.Models;

    public class RecipeService : IRecipeService
    {
        private IDbRepository<Recipe> recipes;

        public RecipeService(IDbRepository<Recipe> recipes)
        {
            this.recipes = recipes;
        }

        public IQueryable<Recipe> GetAll()
        {
            return this.recipes.All();
        }
    }
}
