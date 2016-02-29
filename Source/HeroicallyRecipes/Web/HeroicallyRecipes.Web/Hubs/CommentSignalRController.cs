namespace HeroicallyRecipes.Web.Hubs
{
    using System;
    using System.Linq;

    using HeroicallyRecipes.Data;
    using HeroicallyRecipes.Data.Models;
    using Microsoft.AspNet.SignalR;

    public class CommentSignalRController : Hub
    {
        private HeroicallyRecipesDbContext db = new HeroicallyRecipesDbContext();

        public void GetComment(int articleId, string userId, string comment)
        {
            if (comment.Length < 10)
            {
                var connectId = Context.ConnectionId;
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CommentSignalRController>();
                context.Clients.Client(connectId).sendMessage("Comment length must be at least 10 characters long.");
                return;
            }

            if (userId != null && comment != null)
            {
                var article = this.db.Articles.FirstOrDefault(a => a.Id == articleId);

                if (article == null)
                {
                    throw new ArgumentNullException();
                }

                var newComment = new Comment()
                {
                    Content = comment,
                    AuthorId = userId,
                    ArticleId = articleId,
                    CreatedOn = DateTime.Now
                };

                this.db.Comments.Add(newComment);
                this.db.SaveChanges();
            }
        }
    }
}