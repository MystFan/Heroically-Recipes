namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private IDbRepository<Tag> recipeTags;

        public TagsService(IDbRepository<Tag> recipeTags)
        {
            this.recipeTags = recipeTags;
        }

        public IQueryable<Tag> GetAll()
        {
            var allTags = this.recipeTags.All();
            return allTags;
        }
    }
}
