namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;

    public interface IArticlesService : IService
    {
        IQueryable<Article> GetAll();

        IQueryable<Article> GetNewest(int count);

        Article GetById(int id);
    }
}
