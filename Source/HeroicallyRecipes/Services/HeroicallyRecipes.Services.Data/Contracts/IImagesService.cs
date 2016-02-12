namespace HeroicallyRecipes.Services.Data.Contracts
{
    using HeroicallyRecipes.Data.Models;

    public interface IImagesService : IService
    {
        RecipeImage GetByRecipeId(string id);
    }
}
