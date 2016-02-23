namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Common.Validation;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Infrastructure.CustomFilters;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;

    public class RecipesController : UsersBaseController
    {
        private const string RecipeChacheKey = "Recipe";
        private const int TagCacheDuration = 10 * 60;
        private const int NicknameCacheDuration = 10 * 60;
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

            var recipesResult = this.Cache.Get(RecipeChacheKey + page.ToString(),
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

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Duration = 5 * 60, VaryByParam = "titleQuery")]
        public ActionResult SearchByTitle(string titleQuery)
        {
            var resultRecipes = this.recipes
                .GetByTitle(titleQuery)
                .ProjectTo<RecipeViewModel>()
                .ToList();

            return PartialView("_RecipesListPartial", resultRecipes);
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
            var recipe = this.recipes.GetById(id);
            var viewRecipe = Mapper.Map<RecipeViewModel>(recipe);
            viewRecipe.Creator = recipe.Creator.NickName;
            viewRecipe.Category = recipe.Category.Name;
            viewRecipe.Votes = recipe.Votes.Count;
            
            return this.View(viewRecipe);
        }

        [HttpGet]
        public ActionResult GetByTagName(string tagName = GlobalConstants.DefaultTagName)
        {
            var viewRecipes = this.Cache.Get(
                tagName, 
                () => this.recipes.
                        GetByTagName(tagName)
                        .ProjectTo<RecipeViewModel>()
                        .ToList(),
                TagCacheDuration);

            return this.View(viewRecipes);
        }

        [HttpGet]
        public ActionResult GetByNickname(string nickname)
        {
            var viewRecipes = this.Cache.Get(
                nickname,
                () => this.recipes.
                        GetByNickname(nickname)
                        .ProjectTo<RecipeViewModel>()
                        .ToList(),
                NicknameCacheDuration);

            return this.PartialView("_UserRecipesListPartial", viewRecipes);
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