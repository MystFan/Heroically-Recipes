namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }
    }
}
