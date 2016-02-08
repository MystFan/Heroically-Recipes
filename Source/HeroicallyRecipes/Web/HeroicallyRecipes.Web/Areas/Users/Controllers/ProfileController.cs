using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    public class ProfileController : UsersBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}