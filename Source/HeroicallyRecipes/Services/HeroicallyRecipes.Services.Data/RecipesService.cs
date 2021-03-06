﻿namespace HeroicallyRecipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Common.Providers;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

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

        public int Add(string title, string preparation, int categoryId, string userId, IEnumerable<string> ingradients, IEnumerable<HttpPostedFileBase> images, string tags)
        {
            var recipeImages = this.HttpFileToRecipeImage(images);
            var recipeTags = tags.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => new Tag() { Text = t }).ToList();

            var newRecipe = new Recipe()
            {
                Title = title,
                Preparation = preparation,
                Ingredients = ingradients.Select(i => new Ingredient()
                {
                    Text = i
                }).ToList(),
                Images = recipeImages,
                Tags = recipeTags,
                UserId = userId,
                CategoryId = categoryId
            };

            newRecipe.Tags.Add(new Tag() { Text = GlobalConstants.DefaultTagName });
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

            var resultRecipe = this.GetRecipeById(decodedId);

            return resultRecipe;
        }

        public Recipe GetById(int id)
        {
            var resultRecipe = this.GetRecipeById(id);

            return resultRecipe;
        }

        public IQueryable<Recipe> GetTop(int count)
        {
            return this.GetAll()
                .OrderBy(r => r.Votes.Count)
                .ThenBy(r => r.Id)
                .Take(count);
        }

        public IQueryable<Recipe> GetByTitle(string title)
        {
            var resultRecipes = this.recipes
                .All()
                .Where(r => r.Title.ToLower().Contains(title.ToLower()));

            return resultRecipes;
        }

        public IQueryable<Recipe> GetByTagName(string tagName)
        {
            var resultTags = this.recipes
                .All()
                .Where(r => r.Tags.FirstOrDefault(t => t.Text.ToLower() == tagName.ToLower()) != null);

            return resultTags;
        }

        public IQueryable<Recipe> GetByNickname(string nickname)
        {
            var resultRecipes = this.recipes
                .All()
                .Where(r => r.Creator.NickName == nickname);

            return resultRecipes;
        }

        public void Update(int recipeId, string userId, string title, string preparation, int votes)
        {
            var databaseRecipe = this.recipes
                .All()
                .FirstOrDefault(r => r.Id == recipeId);

            databaseRecipe.Title = title;
            databaseRecipe.Preparation = preparation;
            databaseRecipe.Votes.Clear();

            if (votes < 0)
            {
                votes *= -1;
                for (int i = 0; i < votes; i++)
                {
                    databaseRecipe.Votes.Add(new RecipeVote()
                    {
                        Type = VoteType.Negativ,
                        AuthorId = userId
                    });
                }
            }
            else if (votes > 0)
            {
                for (int i = 0; i < votes; i++)
                {
                    databaseRecipe.Votes.Add(new RecipeVote()
                    {
                        Type = VoteType.Positive,
                        AuthorId = userId
                    });
                }
            }

            this.recipes.SaveChanges();
        }

        public void Delete(int recipeId)
        {
            var databaseRecipe = this.recipes
                .All()
                .FirstOrDefault(r => r.Id == recipeId);

            this.recipes.Delete(databaseRecipe);
            this.recipes.SaveChanges();
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
