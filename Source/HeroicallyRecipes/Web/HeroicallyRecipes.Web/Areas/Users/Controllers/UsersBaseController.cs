namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;
    using Services.Web.Contracts;
    using Web.Controllers;

    [Authorize]
    public abstract class UsersBaseController : BaseController
    {
    }
}