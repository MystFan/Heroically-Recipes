namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;

    public interface ITagsService : IService
    {
        IQueryable<Tag> GetAll();

        IQueryable<Tag> InRecipe();
    }
}
