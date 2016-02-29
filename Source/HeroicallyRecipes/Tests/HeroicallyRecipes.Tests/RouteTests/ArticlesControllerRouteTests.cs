namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;

    using HeroicallyRecipes.Web.Areas.Users;
    using MvcRouteTester;
    using MvcRouteUnitTester;
    using NUnit.Framework;

    public class ArticlesControllerRouteTests
    {
        private RouteCollection routeCollection;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            this.routeCollection = new RouteCollection();
        }

        [Test]
        public void ArticlesShouldMapRouteDetailsWithPassedParameterId()
        {
            var tester = new RouteTester<UsersAreaRegistration>();

            tester.WithIncomingRequest("/Users/Articles/Details/5")
                .ShouldMatchRoute("Users", "Articles", "Details", new { id = 5 });
        }

        [Test]
        [ExpectedException]
        public void ArticlesShouldNotMapRouteDetailsWithoutParameterId()
        {
            const string Url = "/Users/Articles/Details";

            this.routeCollection.ShouldMap(Url).ToNonIgnoredRoute();
        }
    }
}
