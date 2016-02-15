namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;
    using HeroicallyRecipes.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();
    }
}
