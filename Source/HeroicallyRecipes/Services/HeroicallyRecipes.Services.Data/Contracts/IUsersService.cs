namespace HeroicallyRecipes.Services.Data.Contracts
{
    using System.Linq;
    using Data.Contracts;
    using HeroicallyRecipes.Data.Models;

    public interface IUsersService : IService
    {
        IQueryable<User> GetAll();
    }
}
