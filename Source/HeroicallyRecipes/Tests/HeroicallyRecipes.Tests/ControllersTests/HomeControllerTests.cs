namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Collections.Generic;
    using System.Linq;

    using HeroicallyRecipes.Common.Providers;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Tests.TestObjects;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Controllers;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class HomeControllerTests
    {
        private IRecipesService recipesServiceMock;
        private IImagesService imagesServiceMock;
        private IArticlesService articlesServiceMock;
        private HomeController controller;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.recipesServiceMock = ServicesObjectFactory.GetRecipeService();
            this.imagesServiceMock = ServicesObjectFactory.GetRecipeImagesService();
            this.articlesServiceMock = ServicesObjectFactory.GetArticlesService();
            this.controller = new HomeController(this.imagesServiceMock, this.recipesServiceMock, this.articlesServiceMock);
        }

        [Test]
        public void GetTopRecipesShouldWorkCorrectly()
        {
            IdentifierProvider idProvider = new IdentifierProvider();
            string id = idProvider.EncodeId(1);

            this.controller.WithCallTo(x => x.GetTopRecipes())
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
            this.controller.WithCallToChild(a => a.GetTopRecipes());
        }
    }
}
