using HeroicallyRecipes.Services.Data.Contracts;
using HeroicallyRecipes.Web.Models.RecipeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    public class RecipesController : UsersBaseController
    {
        private IRecipesService recipes;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipes = recipesService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RecipeCreateViewModel model)
        {        
            if (this.ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                IEnumerable<string> tags = model.Tags.Select(t => t.Text);
                this.recipes.Add(model.Title, model.Preparation, model.CategoryId, userId, model.Ingredients, model.RecipeImages, tags);
            }

            return View(model);
        }
    }
}