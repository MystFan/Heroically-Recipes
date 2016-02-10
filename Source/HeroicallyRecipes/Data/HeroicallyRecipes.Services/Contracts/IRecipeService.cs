namespace HeroicallyRecipes.Services.Contracts
{
    using System.Linq;
    using HeroicallyRecipes.Models;

    public interface IRecipeService : IService
    {
        IQueryable<Recipe> GetAll();
    }
}
