namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers;

    [Authorize(Roles = "Admin")]
    public abstract class AdminBaseController : BaseController
    {
    }
}