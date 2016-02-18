namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;

    public interface IVoteService : IService
    {
        IQueryable<RecipeVote> GetAll();

        int GetTotalRecipeVotes(int recipeId);

        void Add(int recipeId, string userId, int vote);
    }
}
