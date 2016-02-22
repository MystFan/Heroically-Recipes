namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Web;

    using HeroicallyRecipes.Data.Models;

    public interface IRecipesService : IService
    {
        IQueryable<Recipe> GetAll();

        IQueryable<Recipe> Get(int page);

        IQueryable<Recipe> GetTop(int count);

        IQueryable<Recipe> GetByTitle(string title);

        IQueryable<Recipe> GetByTagName(string tagName);

        Recipe GetById(string id);

        Recipe GetById(int id);

        int Add(string title, string preparation, int categoryId, string userId, IEnumerable<string> ingradients, IEnumerable<HttpPostedFileBase> images, IEnumerable<string> tags);
    }
}
