namespace HeroicallyRecipes.Data.Migrations
{
    using System.Linq;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<HeroicallyRecipesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HeroicallyRecipesDbContext context)
        {
            PasswordHasher hasher = new PasswordHasher();
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string roleName = "Admin";
            IdentityResult roleResult;

            if (!roleManager.RoleExists(roleName))
            {
                roleResult = roleManager.Create(new IdentityRole(roleName));
                var admin = new User
                {
                    Email = "admin@site.com",
                    PasswordHash = hasher.HashPassword("admin"),
                    UserName = "admin@site.com",
                    NickName = "admin@site.com",
                    AvatarUrl = "/images/defaultAvatar3.png"
                };

                context.Users.Add(admin);
                context.SaveChanges();
                userManager.UpdateSecurityStamp(admin.Id);

                userManager.AddToRole(admin.Id, roleName);
            }

            if (!context.Tags.Any())
            {
                string[] tags = new string[]
                {
                    "Appetizers",
                    "Baking",
                    "Beef",
                    "Breakfast",
                    "Chicken",
                    "Desserts",
                    "Drinks",
                    "Fish",
                    "Pasta",
                    "Pizza",
                    "Pork",
                    "Sides",
                    "Apple",
                    "Artichoke",
                    "Arugula",
                    "Asparagus",
                    "Avocado",
                    "Bamboo Shoots",
                    "Bean Sprouts",
                    "Beans- see Bean List",
                    "Beet",
                    "Belgian Endive",
                    "Bell Pepper",
                    "Broccoli",
                    "Brussels Sprouts",
                    "Burdock Root/Gobo",
                    "Cabbage",
                    "Calabash",
                    "Capers",
                    "Carrot",
                    "Cassava/Yuca",
                    "Cauliflower",
                    "Celery",
                    "Celtuce",
                    "Chayote",
                    "Corn/Maize",
                    "Cucumber",
                    "Daikon Radish",
                    "Edamame",
                    "Eggplant/Aubergine",
                    "Elephant Garlic",
                    "Endive",
                    "Fennel",
                    "Fiddlehead",
                    "Galangal",
                    "Garlic",
                    "Ginger",
                    "Grape Leaves",
                    "Green Beans",
                    "Greens",
                    "Hearts of Palm",
                    "Horseradish",
                    "Kale",
                    "Kohlrabi",
                    "Leeks",
                    "Lemongrass",
                    "Lettuce",
                    "Lotus Root",
                    "Lotus Seed",
                    "Mushrooms- see Mushroom List",
                    "Napa Cabbage",
                    "Nopales",
                    "Okra",
                    "Olive",
                    "Onion",
                    "Parsley",
                    "Parsley Root",
                    "Parsnip",
                    "Peas",
                    "Peppers",
                    "Plantain",
                    "Potato",
                    "Pumpkin",
                    "Purslane",
                    "Radicchio",
                    "Radish",
                    "Rutabaga",
                    "Sea Vegetables- see Sea Vegetable List",
                    "Shallots",
                    "Spinach",
                    "Squash- see Squash List",
                    "Sweet Potato",
                    "Swiss Chard",
                    "Taro",
                    "Tomatillo",
                    "Tomato",
                    "Turnip",
                    "Water Chestnut",
                    "Water Spinach",
                    "Watercress",
                    "Winter Melon",
                    "Yams",
                    "Zucchini",
                };

                for (int i = 0; i < tags.Length; i++)
                {
                    context.Tags.Add(new Tag()
                    {
                        Text = tags[i]
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
