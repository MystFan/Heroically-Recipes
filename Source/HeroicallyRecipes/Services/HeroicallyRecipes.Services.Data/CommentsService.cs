namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        private IDbRepository<Comment> comments;

        public CommentsService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public void Delete(int commentId)
        {
            var comment = this.GetById(commentId);
            this.comments.Delete(comment);

            this.comments.SaveChanges();
        }

        public IQueryable<Comment> GetAll()
        {
            var allComments = this.comments.All();

            return allComments;
        }

        public Comment GetById(int commentId)
        {
            var comment = this.comments
                .All()
                .FirstOrDefault(c => c.Id == commentId);

            return comment;
        }

        public void Update(int commentId, string content)
        {
            var comment = this.GetById(commentId);
            comment.Content = content;

            this.comments.SaveChanges();
        }
    }
}
