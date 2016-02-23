namespace HeroicallyRecipes.Web.Hubs
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.SignalR;

    using HeroicallyRecipes.Data;
    using HeroicallyRecipes.Data.Models;

    public class CommentSignalRController : Hub
    {
        HeroicallyRecipesDbContext db = new HeroicallyRecipesDbContext();

        public void GetComment(int articleId, string userId, string comment)
        {
            if(comment.Length < 10)
            {
                var connectId = Context.ConnectionId;
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CommentSignalRController>();
                context.Clients.Client(connectId).sendMessage("Comment length must be at least 10 characters long.");
                return;
            }

            if(userId != null && comment != null)
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == articleId);

                if(article == null)
                {
                    throw new ArgumentNullException();
                }

                var newComment = (new Comment()
                {
                    Content = comment,
                    AuthorId = userId,
                    ArticleId = articleId,
                    CreatedOn = DateTime.Now
                });

                db.Comments.Add(newComment);
                db.SaveChanges();
            }
        }
    }
}