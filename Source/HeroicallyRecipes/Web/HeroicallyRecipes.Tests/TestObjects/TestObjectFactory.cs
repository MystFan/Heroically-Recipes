using HeroicallyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroicallyRecipes.Tests.TestObjects
{
    public class TestObjectFactory
    {
        public static MemoryRepository<User> GetUsersRepository()
        {
            var usersRepo = new MemoryRepository<User>();


            return usersRepo;
        }
    }
}
