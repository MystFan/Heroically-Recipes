namespace HeroicallyRecipes.Data.Migrations
{
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
        }
    }
}
