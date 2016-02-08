namespace HeroicallyRecipes.Web
{
    using System.Data.Entity;
    using HeroicallyRecipes.Data;
    using HeroicallyRecipes.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HeroicallyRecipesDbContext, Configuration>());
            HeroicallyRecipesDbContext.Create().Database.Initialize(true);
        }
    }
}