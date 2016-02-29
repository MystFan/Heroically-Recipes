namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Models.Profile;
    using Microsoft.AspNet.Identity;

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
            var user = this.Cache
                            .Get(
                            "user",
                            () =>
                                Mapper.Map<UserViewModel>(this.users.GetById(this.User.Identity.GetUserId())),
                            UserProfileCacheDuration);

            return this.View(user);
        }
    }
}