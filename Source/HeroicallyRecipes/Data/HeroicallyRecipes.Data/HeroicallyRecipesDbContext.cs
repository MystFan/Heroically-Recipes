namespace HeroicallyRecipes.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using HeroicallyRecipes.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Contracts;

    public class HeroicallyRecipesDbContext : IdentityDbContext<User>, IHeroicallyRecipesDbContext
    {
        public HeroicallyRecipesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Recipe> Recipes { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Ingredient> Ingredients { get; set; }

        public virtual IDbSet<RecipeImage> RecipeImages { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public static HeroicallyRecipesDbContext Create()
        {
            return new HeroicallyRecipesDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
