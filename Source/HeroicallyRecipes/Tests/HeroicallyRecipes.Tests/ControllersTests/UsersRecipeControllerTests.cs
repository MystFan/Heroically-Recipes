namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using TestStack.FluentMVCTesting;
    using NUnit.Framework;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Services.Web;
    using HeroicallyRecipes.Tests.TestObjects;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Areas.Users.Controllers;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;

    [TestFixture]
    public class UsersRecipeControllerTests
    {
        private int DefaultPageSize = GlobalConstants.RecipeDefaultPageSize;
        private IRecipesService recipes;
        private RecipesController controller;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.recipes = TestObjectFactory.GetRecipeService();

            controller = new RecipesController(this.recipes);
            controller.Cache = new HttpCacheService();
        }

        [Test]
        public void AllPagingDefaultPageShouldWorkCorrectly()
        {
            int page = 1;

            HttpRuntime.Cache[page.ToString()] = this.recipes.Get(page)
                .ProjectTo<RecipeViewModel>()
                .ToList();

            controller.WithCallTo(x => x.All(page))
                .ShouldRenderView("All")
                            .WithModel<RecipeListViewModel>(
                                viewModel =>
                                {
                                    Assert.AreEqual(page, viewModel.CurrentPage);
                                    Assert.AreEqual((int)Math.Ceiling(13 / (decimal)DefaultPageSize), viewModel.TotalPages);
                                    Assert.AreEqual(3, viewModel.Recipes.Count());
                                    Assert.AreEqual("Tandoori Carrots", viewModel.Recipes.FirstOrDefault().Title);
                                }).AndNoModelErrors();
        }

        [Test]
        public void AllPagingWithRandomPageShouldWorkCorrectly()
        {
            int page = 5;

            HttpRuntime.Cache[page.ToString()] = this.recipes.Get(page)
                .ProjectTo<RecipeViewModel>()
                .ToList();

            controller.WithCallTo(x => x.All(page))
                .ShouldRenderView("All")
                            .WithModel<RecipeListViewModel>(
                                viewModel =>
                                {
                                    Assert.AreEqual(page, viewModel.CurrentPage);
                                    Assert.AreEqual((int)Math.Ceiling(13 / (decimal)DefaultPageSize), viewModel.TotalPages);
                                    Assert.AreEqual(1, viewModel.Recipes.Count());
                                    Assert.AreEqual("Salad with butter and basted mushrooms 13", viewModel.Recipes.FirstOrDefault().Title);
                                }).AndNoModelErrors();
        }


        [Test]
        public void ControllerActionCreateGetShouldReturnView()
        {
            controller.WithCallTo(x => x.Create())
                .ShouldRenderView("Create");
        }

        // TODO: Remake test after Action Create is finished
        [Test]
        public void ControllerActionCreatePostShouldReturnView()
        {
            RecipeCreateViewModel model = new RecipeCreateViewModel()
            {
                Title = "Tandoori Carrots",
                Preparation = "Preparation 1",
                CategoryId = 2,
                Ingredients = new List<string>() { "2 tablespoons vadouvan", "2 garlic cloves, finely grated, divided", "1/2 cup plain whole-milk Greek yogurt, divided" },
                RecipeImages = new List<HttpPostedFileBase>() { null, null, null }
            };

            controller.WithCallTo(a => a.Create(model))
                .ShouldRenderView("Create")
                .WithModel<RecipeCreateViewModel>(m =>
                {
                    
                }).AndModelError("The recipe must contain at least one image!");

            ActionResult result = controller.Create(model) as ActionResult;

            Assert.IsNotNull(result);
        }
    }
}
