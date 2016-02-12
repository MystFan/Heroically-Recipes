namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

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
