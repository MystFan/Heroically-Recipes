namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;

    public interface IUsersService : IService
    {
        IQueryable<User> GetAll();

        User GetById(string id);
    }
}
