using HeroicallyRecipes.Services.Data.Contracts;
using HeroicallyRecipes.Web.Models.RecipeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper.QueryableExtensions;

namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    public class RecipesController : UsersBaseController
    {
        private IRecipesService recipes;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipes = recipesService;
        }

        [HttpGet]
        public ActionResult All(int page = 1)
        {
            int totalRecipes = this.recipes.GetAll().Count();
            int totalPages = (int)Math.Ceiling(totalRecipes / (decimal)3);

            var recipesResult = this.Cache.Get(page.ToString(),
                            () => this.recipes
                                .Get(page)
                                .ProjectTo<RecipeViewModel>()
                                .ToList(),
                            1 * 60);

            RecipeListViewModel viewModel = new RecipeListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Recipes = recipesResult
            };

            return View(viewModel);
        }

        [HttpGet]
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
                this.RedirectToAction("All");
            }

            return View(model);
        }
    }
}