namespace HeroicallyRecipes.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using HeroicallyRecipes.Models;

    public class HeroicallyRecipesDbContext : IdentityDbContext<User>, IHeroicallyRecipesDbContext
    {
        public HeroicallyRecipesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Recipe> Recipes { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<RecipeImage> RecipeImages { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public static HeroicallyRecipesDbContext Create()
        {
            return new HeroicallyRecipesDbContext();
        }
    }
}
