namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HeroicallyRecipes.Services.Data.Contracts;
    using Services.Web.Contracts;

    public class ProfileController : UsersBaseController
    {
        private IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            var users = base.Cache.Get("users", () => this.users.GetAll().ToList(), 30);
            return View(users);
        }
    }
}