namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers;

    [Authorize]
    public abstract class UsersBaseController : BaseController
    {
    }
}