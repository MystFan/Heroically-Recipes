﻿namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Http;

    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Services.Web;
    using HeroicallyRecipes.Tests.TestObjects;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Areas.Users.Controllers;

    [TestFixture]
    public class TagsControllerTests
    {
        private ITagsService tags;
        private TagsController controller;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.tags = ServicesObjectFactory.GetTagsService();
            this.controller = new TagsController(this.tags);
            this.controller.Cache = new HttpCacheService();
        }

        [Test]
        public void GetTagsActionShouldReturnAllTags()
        {
            this.controller.WithCallTo(x => x.GetTags())
                .ShouldRenderPartialView("_TagsPartial")
                .WithModel<IEnumerable<string>>(viewModel =>
                    {
                        Assert.AreEqual(5, viewModel.Count());
                        Assert.AreEqual("default", viewModel.FirstOrDefault());
                    }).AndNoModelErrors();
        }

        [Test]
        public void AllActionShouldReturnAllTags()
        {
            HttpRuntime.Cache["TagsList"] = new List<string>()
                {
                    "default",
                    "Tag 1",
                    "Tag 2",
                    "Tag 3",
                    "Tag 4"
                };

            this.controller.WithCallTo(x => x.All())
                .ShouldRenderPartialView("_TagsListPartial")
                .WithModel<IEnumerable<string>>(viewModel =>
                {
                    Assert.AreEqual(5, viewModel.Count());
                    Assert.AreEqual("Tag 4", viewModel.LastOrDefault());
                }).AndNoModelErrors();
        }

        [Test]
        public void AllActionShouldBeChildOnly()
        {
            this.controller.WithCallToChild(c => c.All())
                .ShouldRenderPartialView("_TagsListPartial");
        }

        [Test]
        public void GetTagsActionShouldBeChildOnly()
        {
            this.controller.WithCallToChild(c => c.GetTags())
                .ShouldRenderPartialView("_TagsPartial");
        }

        [Test]
        public void AllActionShouldHaveAllowAnonymousAttribute()
        {
            var type = this.controller.GetType();
            var methodInfo = type.GetMethod("All");
            var attributes = methodInfo.GetCustomAttributes(true).Select(a => a.GetType().Name);
            Assert.IsTrue(attributes.Any(a => a == "AllowAnonymousAttribute"));
        }
    }
}
