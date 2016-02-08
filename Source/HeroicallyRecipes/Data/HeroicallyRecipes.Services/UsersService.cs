namespace HeroicallyRecipes.Services
{
    using Data.Repositories;
    using HeroicallyRecipes.Services.Contracts;
    using Models;

    public class UsersService : IUsersService
    {
        private IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }
    }
}
