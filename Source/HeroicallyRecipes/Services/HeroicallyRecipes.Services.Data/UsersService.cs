namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;

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
