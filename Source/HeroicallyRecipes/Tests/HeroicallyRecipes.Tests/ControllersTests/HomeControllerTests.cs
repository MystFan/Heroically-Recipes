namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Common.Providers;
    using HeroicallyRecipes.Web.Controllers;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;
    using HeroicallyRecipes.Tests.TestObjects;

    [TestFixture]
    public class HomeControllerTests
    {
        private IRecipesService recipes;
        private IImagesService images;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.recipes = TestObjectsFactory.GetRecipeService();
            this.images = TestObjectsFactory.GetRecipeImagesService();
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

        [Test]
        public void ControllerShouldHaveChildActionOnlyGetTopRecipes()
        {
            var recipesServiceMock = this.recipes;
            var imagesServiceMock = this.images;

            var controller = new HomeController(imagesServiceMock, recipesServiceMock);

            controller.WithCallToChild(a => a.GetTopRecipes());
        }
    }
}
