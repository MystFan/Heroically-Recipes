namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;

    using Common.Providers;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class ImagesService : IImagesService
    {
        private IDbRepository<RecipeImage> recipeImages;
        private IIdentifierProvider idProvider;

        public ImagesService(IDbRepository<RecipeImage> recipeImages, IIdentifierProvider idProvider)
        {
            this.recipeImages = recipeImages;
            this.idProvider = idProvider;
        }

        public RecipeImage GetByRecipeId(string id)
        {
            var decodedId = this.idProvider.DecodeId(id);

            return this.recipeImages
                .All()
                .FirstOrDefault(img => img.RecipeId == decodedId);
        }
    }
}
