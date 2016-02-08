namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public abstract class AdminBaseController : Controller
    {
    }
}