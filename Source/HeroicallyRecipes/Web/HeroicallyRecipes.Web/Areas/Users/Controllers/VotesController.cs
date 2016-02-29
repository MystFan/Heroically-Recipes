namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Infrastructure.CustomFilters;
    using Microsoft.AspNet.Identity;

    public class VotesController : UsersBaseController
    {
        private IVoteService votes;

        public VotesController(IVoteService votes)
        {
            this.votes = votes;
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult Vote(int recipeId, int vote)
        {
            if (vote < -1)
            {
                vote = -1;
            }

            if (vote > 1)
            {
                vote = 1;
            }

            string userId = this.User.Identity.GetUserId();
            this.votes.Add(recipeId, userId, vote);

            var recipeVotes = this.votes.GetTotalRecipeVotes(recipeId);

            return this.Json(new { Count = recipeVotes });
        }
    }
}