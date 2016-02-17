namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web;
    using Common.Providers;
    using Web.Controllers;
    using Web.Models.RecipeViewModels;

    [TestFixture]
    public class HomeControllerTests
    {
        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [Test]
        public void GetTopRecipesShouldWorkCorrectly()
        {
            var recipesServiceMock = new Mock<IRecipesService>();
            recipesServiceMock.Setup(x => x.GetAll())
                    .Returns(new List<Recipe>()
                    {
                        new Recipe()
                        {
                            Id = 1,
                            Title = "Tandoori Carrots",
                            Category = new Category() { Name = "Healthy" },
                            CreatedOn = new DateTime(2016, 1, 12, 12, 12, 12),
                            Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image1.png", Extension = ".png"} }
                        },
                        new Recipe()
                        {
                            Id = 2,
                            Title = "Sheetpandinners Chicken",
                            Category = new Category() { Name = "Quick and Easy" },
                            CreatedOn = new DateTime(2016, 1, 12, 11, 11, 11),
                            Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image2.png", Extension = ".png"} }
                        },
                        new Recipe()
                        {
                            Id = 3,
                            Title = "Salad with butter and basted mushrooms",
                            Category = new Category() { Name = "Vegetarian" },
                            CreatedOn = new DateTime(2016, 1, 12, 10, 10, 10),
                            Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image3.png", Extension = ".png"} }
                        }
                    }.AsQueryable());

            var imagesServiceMock = new Mock<IImagesService>();

            var controller = new HomeController(imagesServiceMock.Object, recipesServiceMock.Object);
            IdentifierProvider idProvider = new IdentifierProvider();
            string id = idProvider.EncodeId(1);

            controller.WithCallTo(x => x.GetTopRecipes())
                .ShouldRenderPartialView("_SliderPartial")
                            .WithModel<IEnumerable<RecipeHomeViewModel>>(
                                viewModel =>
                                {
                                    Assert.AreEqual("Tandoori Carrots", viewModel.FirstOrDefault().Title);
                                    Assert.AreEqual("Healthy", viewModel.FirstOrDefault().Category);
                                    Assert.AreEqual(10, viewModel.LastOrDefault().CreatedOn.Second);
                                    Assert.AreEqual(id, viewModel.FirstOrDefault().ViewId);
                                }).AndNoModelErrors();
        }
    }
}
