namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Web.Infrastructure.CustomFilters;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;
    using HeroicallyRecipes.Web.Models.Tags;
    using HeroicallyRecipes.Services.Data.Contracts;
    using Common.Validation;
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
            if(model.Tags == null)
            {
                this.ModelState.AddModelError(string.Empty, "The recipe must contain at least " + ModelConstants.TagsMinCount + " tag!");
            }

            if (this.ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                this.recipes.Add(model.Title, model.Preparation, model.CategoryId, userId, model.Ingredients, model.RecipeImages, model.Tags);
                return this.RedirectToAction("All");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var dbRecipe = this.recipes.GetById(id);
            var viewRecipe = Mapper.Map<RecipeViewModel>(dbRecipe);
            viewRecipe.Creator = dbRecipe.Creator.NickName;
            viewRecipe.Category = dbRecipe.Category.Name;
            viewRecipe.Votes = dbRecipe.Votes.Count;

            if(viewRecipe == null)
            {
                return HttpNotFound();
            }
            
            return this.View(viewRecipe);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetRecipePreparation(string id)
        {
            var recipe = this.recipes.GetById(id);

            return Content(recipe.Preparation);
        }
    }
}