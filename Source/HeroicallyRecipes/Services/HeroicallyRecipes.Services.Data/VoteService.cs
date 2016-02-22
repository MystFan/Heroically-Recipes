namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class VoteService : IVoteService
    {
        private IDbRepository<RecipeVote> votes;

        public VoteService(IDbRepository<RecipeVote> votes)
        {
            this.votes = votes;
        }

        public void Add(int recipeId, string userId, int vote)
        {
            var databaseVote = Get(recipeId, userId);

            if (databaseVote == null)
            {
                this.votes.Add(new RecipeVote()
                {
                    AuthorId = userId,
                    RecipeId = recipeId,
                    Type = (VoteType)vote
                });
            }
            else
            {
                if(databaseVote.Type == (VoteType)vote)
                {
                    databaseVote.Type = VoteType.Neutral;
                }
                else
                {
                    databaseVote.Type = (VoteType)vote;
                }
            }           

            this.votes.SaveChanges();
        }

        public IQueryable<RecipeVote> GetAll()
        {
            var allVotes = this.votes.All();
            return allVotes;
        }

        public int GetTotalRecipeVotes(int recipeId)
        {
            var totalVotes = this.votes
                .All()
                .Where(v => v.RecipeId == recipeId)
                .Sum(v => (int)v.Type);

            return totalVotes;
        }

        private RecipeVote Get(int recipeId, string userId)
        {
            var dbVote = this.votes
                .All()
                .Where(v => v.AuthorId == userId && v.RecipeId == recipeId)
                .FirstOrDefault();

            return dbVote;
        }
    }
}
