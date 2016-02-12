namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class ImagesService : IImagesService
    {
        private IDbRepository<RecipeImage> recipeImages;

        public ImagesService(IDbRepository<RecipeImage> recipeImages)
        {
            this.recipeImages = recipeImages;
        }

        public RecipeImage GetByRecipeId(string id)
        {
            return this.recipeImages
                .All()
                .FirstOrDefault(img => img.Recipe.ViewId.ToString() == id);
        }
    }
}
