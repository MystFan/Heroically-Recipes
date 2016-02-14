namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using AutoMapper;

    using HeroicallyRecipes.Services.Data.Contracts;
    using Models.Profile;

    public class ProfileController : UsersBaseController
    {
        private const int UserProfileCacheDuration = 1 * 60;
        private IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            var user = base.Cache
                .Get("user",
                () =>
                    Mapper.Map<UserViewModel>(this.users.GetById(this.User.Identity.GetUserId())),
                UserProfileCacheDuration);

            return View(user);
        }
    }
}