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
            var UserManager = new UserManager<User>(new UserStore<User>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string roleName = "Admin";
            IdentityResult roleResult;

            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
                var admin = new User
                {
                    Email = "admin@site.com",
                    PasswordHash = hasher.HashPassword("admin"),
                    UserName = "admin@site.com"
                };


                context.Users.Add(admin);
                context.SaveChanges();
                UserManager.UpdateSecurityStamp(admin.Id);

                UserManager.AddToRole(admin.Id, roleName);
            }
        }
    }
}
