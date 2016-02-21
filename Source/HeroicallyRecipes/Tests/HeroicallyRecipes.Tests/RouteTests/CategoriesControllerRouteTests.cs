namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;

    using NUnit.Framework;
    using MvcRouteUnitTester;
    using HeroicallyRecipes.Web.Areas.Users;

    [TestFixture]
    public class CategoriesControllerRouteTests
    {
        private RouteCollection routeCollection;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            this.routeCollection = new RouteCollection();
        }

        [Test]
        public void CategoriesShouldMapRouteAll()
        {
            var tester = new RouteTester<UsersAreaRegistration>();

            tester.WithIncomingRequest("/Users/Categories/All")
                .ShouldMatchRoute("Users", "Categories", "All");
        }
    }
}
