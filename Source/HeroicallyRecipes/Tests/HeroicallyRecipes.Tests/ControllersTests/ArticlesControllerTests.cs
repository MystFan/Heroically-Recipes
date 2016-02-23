namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using NUnit.Framework;
    using AutoMapper.QueryableExtensions;
    using TestStack.FluentMVCTesting;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Services.Web;
    using HeroicallyRecipes.Tests.TestObjects;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Areas.Users.Controllers;
    using HeroicallyRecipes.Web.Models.Articles;

    [TestFixture]
    public class ArticlesControllerTests
    {
        private IArticlesService articles;
        private ArticlesController controller;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.articles = ServicesObjectFactory.GetArticlesService();
            this.controller = new ArticlesController(this.articles);
            this.controller.Cache = new HttpCacheService();
        }

        [Test]
        public void IndexPagingDefaultPageShouldWorkCorrectly()
        {
            int page = 1;

            HttpRuntime.Cache["Article" + page.ToString()] = this.articles.Get(page)
                .ProjectTo<ArticleViewModel>()
                .ToList();

            controller.WithCallTo(x => x.Index(page))
                .ShouldRenderView("Index")
                            .WithModel<ArticleListViewModel>(
                                viewModel =>
                                {
                                    Assert.AreEqual(page, viewModel.CurrentPage);
                                    Assert.AreEqual((int)Math.Ceiling(10 / (decimal)GlobalConstants.ArticleDefaultPageSize), viewModel.TotalPages);
                                    Assert.AreEqual(3, viewModel.Articles.Count());
                                    Assert.AreEqual("Title 0", viewModel.Articles.FirstOrDefault().Title);
                                }).AndNoModelErrors();
        }

        [Test]
        [ExpectedException]
        public void DetailsActionWithInvalidParameterIdShouldThrow()
        {
            this.controller.WithCallTo(x => x.Details(99))
                .ShouldRenderView("Details")
                            .WithModel<ArticleViewModel>(
                                viewModel =>
                                {
                                });
        }

        [Test]
        public void DetailsActionWithValidParameterIdShouldWorkCorrectly()
        {
            int articleId = 5;
            var article = this.articles.GetById(articleId);
            HttpRuntime.Cache["article" + articleId] = Mapper.Map<ArticleViewModel>(article);

            this.controller.WithCallTo(x => x.Details(articleId))
                .ShouldRenderView("Details")
                            .WithModel<ArticleViewModel>(
                                viewModel =>
                                {
                                    Assert.AreEqual(articleId, viewModel.Id);
                                }).AndNoModelErrors();
        }
    }
}
