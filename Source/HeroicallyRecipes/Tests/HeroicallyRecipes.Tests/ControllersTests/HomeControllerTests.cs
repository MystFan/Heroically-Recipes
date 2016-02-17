﻿namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web;
    using Common.Providers;
    using Web.Controllers;
    using Web.Models.RecipeViewModels;
    using TestObjects;

    [TestFixture]
    public class HomeControllerTests
    {
        private IRecipesService recipes;
        private IImagesService images;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.recipes = TestObjectFactory.GetRecipeService();
            this.images = TestObjectFactory.GetRecipeImagesService();
        }

        [Test]
        public void GetTopRecipesShouldWorkCorrectly()
        {
            var recipesServiceMock = this.recipes;
            var imagesServiceMock = this.images;

            var controller = new HomeController(imagesServiceMock, recipesServiceMock);
            IdentifierProvider idProvider = new IdentifierProvider();
            string id = idProvider.EncodeId(1);

            controller.WithCallTo(x => x.GetTopRecipes())
                .ShouldRenderPartialView("_SliderPartial")
                            .WithModel<IEnumerable<RecipeHomeViewModel>>(
                                viewModel =>
                                {
                                    Assert.AreEqual("Tandoori Carrots", viewModel.FirstOrDefault().Title);
                                    Assert.AreEqual("Healthy", viewModel.FirstOrDefault().Category);
                                    Assert.AreEqual(12, viewModel.FirstOrDefault().CreatedOn.Second);
                                    Assert.AreEqual(id, viewModel.FirstOrDefault().ViewId);
                                }).AndNoModelErrors();
        }
    }
}
