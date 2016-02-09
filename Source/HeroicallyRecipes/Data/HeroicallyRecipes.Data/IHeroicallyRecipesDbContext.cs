namespace HeroicallyRecipes.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using HeroicallyRecipes.Models;

    public interface IHeroicallyRecipesDbContext
    {
        IDbSet<Recipe> Recipes { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<RecipeImage> RecipeImages { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}