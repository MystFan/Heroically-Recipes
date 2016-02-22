namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;
    using HeroicallyRecipes.Web.Controllers;

    [Authorize]
    public abstract class UsersBaseController : BaseController
    {
    }
}