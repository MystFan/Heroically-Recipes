namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;

    public interface ICommentsService : IService
    {
        IQueryable<Comment> GetAll();

        void Update(int commentId, string content);

        void Delete(int commentId);

        Comment GetById(int commentId);
    }
}
