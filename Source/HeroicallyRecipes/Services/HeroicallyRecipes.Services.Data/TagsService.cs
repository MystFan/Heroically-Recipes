namespace HeroicallyRecipes.Services.Data
{
    using System;
    using System.Linq;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private IDbRepository<Tag> tags;
        private IDbRepository<Recipe> recipes;

        public TagsService(IDbRepository<Tag> tags, IDbRepository<Recipe> recipes)
        {
            this.tags = tags;
            this.recipes = recipes;
        }

        public IQueryable<Tag> GetAll()
        {
            var allTags = this.tags.All();
            return allTags;
        }

        public IQueryable<Tag> InRecipe()
        {
            var allRecipeTags = this.recipes
                .All()
                .SelectMany(r => r.Tags);

            return allRecipeTags;
        }
    }
}
