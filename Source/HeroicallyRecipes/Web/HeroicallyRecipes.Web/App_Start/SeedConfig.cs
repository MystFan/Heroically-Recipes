namespace HeroicallyRecipes.Web
{
    using App_Data;
    using Data;

    public class SeedConfig
    {
        public static void Seed()
        {
            HeroicallyRecipesDbContext context = new HeroicallyRecipesDbContext();
            SeedData.SeedUsers(context);
            SeedData.SeedCategories(context);
            SeedData.SeedRecipes(context);
        }
    }
}