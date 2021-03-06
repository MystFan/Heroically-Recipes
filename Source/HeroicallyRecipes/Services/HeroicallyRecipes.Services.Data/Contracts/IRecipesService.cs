﻿namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using HeroicallyRecipes.Data.Models;

    public interface IRecipesService : IService
    {
        IQueryable<Recipe> GetAll();

        IQueryable<Recipe> Get(int page);

        IQueryable<Recipe> GetTop(int count);

        IQueryable<Recipe> GetByTitle(string title);

        IQueryable<Recipe> GetByTagName(string tagName);

        IQueryable<Recipe> GetByNickname(string nickname);

        void Update(int recipeId, string userId, string title, string preparation, int votes);

        void Delete(int recipeId);

        Recipe GetById(string id);

        Recipe GetById(int id);

        int Add(string title, string preparation, int categoryId, string userId, IEnumerable<string> ingradients, IEnumerable<HttpPostedFileBase> images, string tags);
    }
}
