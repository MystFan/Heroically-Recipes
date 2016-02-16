namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Services.Web;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Areas.Users.Controllers;
    using HeroicallyRecipes.Web.Models.Categories;

    [TestFixture]
    public class CategoriesControllerTests
    {
        [Test]
        public void GetCategoriesShouldWorkCorrectly()
        {
            AutoMapperConfig.RegisterMappings();

            var categoriesServiceMock = new Mock<ICategoriesService>();
            categoriesServiceMock.Setup(x => x.GetAll())
                    .Returns((new List<Category>()
                    {
                        new Category() { Name = "Category1" },
                        new Category() { Name = "Category2" },
                        new Category() { Name = "Category3" },
                        new Category() { Name = "Category4" },
                        new Category() { Name = "Category5" },
                    })
                    .AsQueryable());


            var controller = new CategoriesController(categoriesServiceMock.Object);
            controller.Cache = new HttpCacheService();
            HttpRuntime.Cache["Categories"] = categoriesServiceMock.Object.GetAll().Select(c => new CategoryViewModel { Name = c.Name }).ToList();

            controller.WithCallTo(x => x.All())
                .ShouldRenderPartialView("_CategoriesPartial")
                            .WithModel<IEnumerable<CategoryViewModel>>(
                                viewModel =>
                                {
                                    Assert.AreEqual(5, viewModel.Count());
                                    Assert.AreEqual("Category5", viewModel.LastOrDefault().Name);
                                }).AndNoModelErrors();
        }

    }
}
