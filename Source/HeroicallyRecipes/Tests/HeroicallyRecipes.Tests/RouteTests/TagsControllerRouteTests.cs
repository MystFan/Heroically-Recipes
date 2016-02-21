namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;

    using MvcRouteUnitTester;
    using NUnit.Framework;
    using HeroicallyRecipes.Web.Areas.Users;

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
    }
}
