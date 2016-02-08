namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HeroicallyRecipes.Services.Contracts;

    public class ProfileController : UsersBaseController
    {
        private IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            var users = this.users.GetAll();
            return View(users);
        }
    }
}