namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;

    using HeroicallyRecipes.Web.Areas.Users;
    using MvcRouteUnitTester;
    using NUnit.Framework;

    public class TagsControllerRouteTests
    {
        private RouteCollection routeCollection;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            this.routeCollection = new RouteCollection();
        }

        [Test]
        public void TagsShouldMapRouteGetTags()
        {
            var tester = new RouteTester<UsersAreaRegistration>();

            tester.WithIncomingRequest("/Users/Tags/GetTags")
                .ShouldMatchRoute("Users", "Tags", "GetTags");
        }

        [Test]
        public void TagsShouldMapRouteAll()
        {
            var tester = new RouteTester<UsersAreaRegistration>();

            tester.WithIncomingRequest("/Users/Tags/All")
                .ShouldMatchRoute("Users", "Tags", "All");
        }
    }
}
