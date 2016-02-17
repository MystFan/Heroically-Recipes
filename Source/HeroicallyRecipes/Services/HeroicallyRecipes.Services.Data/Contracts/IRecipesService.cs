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

        int Add(string title, string preparation, int categoryId, string userId, IEnumerable<string> ingradients, IEnumerable<HttpPostedFileBase> images, IEnumerable<string> tags);
    }
}
