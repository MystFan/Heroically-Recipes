namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Security.Claims;
    using System.Web.Routing;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using TestStack.FluentMVCTesting;
    using NUnit.Framework;
    using Moq;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Common.Validation;
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

            this.recipes = ServicesObjectFactory.GetRecipeService();

            this.controller = new RecipesController(this.recipes);
            this.controller.Cache = new HttpCacheService();
        }

        [Test]
        public void AllPagingDefaultPageShouldWorkCorrectly()
        {
            int page = 1;

            HttpRuntime.Cache["Recipe" + page.ToString()] = this.recipes.Get(page)
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

        [Test]
        public void ControllerActionCreatePostWithValidModelStateShouldReditect()
        {
            var validRecipeCreateViewModel = TestObjectsFactory.GetValidRecipeCreateViewModel();

            MockIdentity();

            var validationController = new ModelStateTestController();
            validationController.TestTryValidateModel(validRecipeCreateViewModel);

            controller.WithCallTo(a => a.Create(validRecipeCreateViewModel))
                    .ShouldRedirectTo<RecipesController>(c => c.All(1));

            var modelState = validationController.ModelState;

            Assert.IsTrue(modelState.IsValid);
        }

        [Test]
        public void ControllerActionCreatePostWithInvalidModelStateShouldReturnViewWithErrors()
        {
            var invalidRecipeCreateViewModel = TestObjectsFactory.GetInvalidRecipeCreateViewModel();

            MockIdentity();

            var validationController = new ModelStateTestController();
            validationController.TestTryValidateModel(invalidRecipeCreateViewModel);

            var errorMessages = GetErrorMessages(validationController.ModelState);

            Assert.AreEqual("The Title must be at least 3 characters long.", errorMessages[0]);
            Assert.AreEqual("The field Preparation must be a string or array type with a minimum length of '100'.", errorMessages[1]);
            Assert.AreEqual("The Tags field is required.", errorMessages[2]);
            Assert.AreEqual("The recipe must contain at least 3 ingredients!", errorMessages[3]);
            Assert.AreEqual("The recipe must contain at least one image!", errorMessages[4]);
        }

        [Test]
        public void SearchByTitleActionShouldHaveAjaxOnlyAttribute()
        {
            var type = this.controller.GetType();
            var methodInfo = type.GetMethod("SearchByTitle");
            var attributes = methodInfo.GetCustomAttributes(true).Select(a => a.GetType().Name);
            Assert.IsTrue(attributes.Any(a => a == "AjaxOnlyAttribute"));
            Assert.IsTrue(attributes.Any(a => a == "ValidateAntiForgeryTokenAttribute"));
        }

        private void MockIdentity()
        {
            // http://forums.asp.net/t/2028867.aspx?UnitTest+How+to+Mock+User+Identity+GetUserId+
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            this.controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), this.controller);
        }

        private List<string> GetErrorMessages(ModelStateDictionary errorsList)
        {
            List<string> messages = new List<string>();
            var errors = errorsList.Values.Select(v => v.Errors);

            foreach (var error in errors)
            {
                messages.Add(error.Select(e => e.ErrorMessage).FirstOrDefault());
            }

            return messages;
        }
    }
}
