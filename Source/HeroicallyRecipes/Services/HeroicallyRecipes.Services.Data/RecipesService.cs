﻿namespace HeroicallyRecipes.Services.Data
{
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class RecipesService : IRecipesService
    {
        private IDbRepository<Recipe> recipes;

        public RecipesService(IDbRepository<Recipe> recipes)
        {
            this.recipes = recipes;
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
                Tags = tags.Select(t => new Tag()
                {
                    Text = t
                }).ToList(),
                UserId = userId,
                CategoryId = categoryId
            };

            this.recipes.Add(newRecipe);
            this.recipes.SaveChanges();

            return newRecipe.Id;
        }

        public IQueryable<Recipe> GetAll()
        {
            return this.recipes.All();
        }

        private List<RecipeImage> HttpFileToRecipeImage(IEnumerable<HttpPostedFileBase> files)
        {
            List<RecipeImage> filesDataResult = new List<RecipeImage>();

            foreach (var file in files)
            {
                if(file == null)
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