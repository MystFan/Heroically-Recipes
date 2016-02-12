namespace HeroicallyRecipes.Data.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Recipe> recipes;

        public User()
        {
            this.recipes = new HashSet<Recipe>();
        }

        public virtual ICollection<Recipe> Recipes
        {
            get
            {
                return this.recipes;
            }

            set
            {
                this.recipes = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
