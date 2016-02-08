using HeroicallyRecipes.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HeroicallyRecipes.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUsersService users;

        public HomeController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            return View();
        }


    }
}
