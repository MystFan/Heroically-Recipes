namespace HeroicallyRecipes.Services
{
    using System;
    using System.Linq;
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

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }
    }
}
