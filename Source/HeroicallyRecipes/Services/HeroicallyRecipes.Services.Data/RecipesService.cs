namespace HeroicallyRecipes.Services.Data
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Common.Providers;

    public class RecipesService : IRecipesService
    {
        private const int DefaultPage = 3;
        private IDbRepository<Recipe> recipes;
        private IIdentifierProvider idProvider;

        public RecipesService(IDbRepository<Recipe> recipes, IIdentifierProvider idProvider)
        {
            this.recipes = recipes;
            this.idProvider = idProvider;
        }

        public int Add(string title, string preparation, int categoryId, string userId, IEnumerable<string> ingradients, IEnumerable<HttpPostedFileBase> images, IEnumerable<string> tags)
        {
            var recipeImages = HttpFileToRecipeImage(images);

            var newRecipe = new Recipe()
            {
                Title = title,
                Preparation = preparation,
                Ingredients = ingradients.Select(i => new Ingredient()
                {
                    Text = i
                }).ToList(),
                Images = recipeImages,
                Tags = null,
                UserId = userId,
                CategoryId = categoryId
            };

            this.recipes.Add(newRecipe);
            this.recipes.SaveChanges();

            return newRecipe.Id;
        }

        public IQueryable<Recipe> Get(int page)
        {
            return this.recipes
                .All()
                .OrderBy(f => f.CreatedOn)
                .Skip((page - 1) * DefaultPage)
                .Take(DefaultPage);
        }

        public IQueryable<Recipe> GetAll()
        {
            return this.recipes.All();
        }

        public Recipe GetById(string id)
        {
            int decodedId = this.idProvider.DecodeId(id);

            var resultRecipe = GetRecipeById(decodedId);

            return resultRecipe;
        }

        public Recipe GetById(int id)
        {
            var resultRecipe = GetRecipeById(id);

            return resultRecipe;
        }

        private Recipe GetRecipeById(int id)
        {
            var resultRecipe = this.recipes
                .All()
                .FirstOrDefault(r => r.Id == id);

            return resultRecipe;
        }

        private List<RecipeImage> HttpFileToRecipeImage(IEnumerable<HttpPostedFileBase> files)
        {
            List<RecipeImage> filesDataResult = new List<RecipeImage>();

            foreach (var file in files)
            {
                if (file == null)
                {
                    continue;
                }

                RecipeImage image = new RecipeImage();
                image.OriginalName = file.FileName;
                image.Extension = Path.GetExtension(file.FileName);

                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                image.Content = target.ToArray();

                filesDataResult.Add(image);
            }

            return filesDataResult;
        }
    }
}
