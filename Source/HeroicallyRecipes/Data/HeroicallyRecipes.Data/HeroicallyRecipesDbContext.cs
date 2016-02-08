namespace HeroicallyRecipes.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using HeroicallyRecipes.Models;

    public class HeroicallyRecipesDbContext : IdentityDbContext<User>, IHeroicallyRecipesDbContext
    {
        public HeroicallyRecipesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static HeroicallyRecipesDbContext Create()
        {
            return new HeroicallyRecipesDbContext();
        }
    }
}
