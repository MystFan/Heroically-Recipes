namespace HeroicallyRecipes.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IUsersService : IService
    {
        IQueryable<User> GetAll();
    }
}
