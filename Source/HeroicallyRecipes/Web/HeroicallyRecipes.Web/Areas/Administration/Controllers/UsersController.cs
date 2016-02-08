using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    public class UsersController : AdminBaseController
    {
        // GET: Administration/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}