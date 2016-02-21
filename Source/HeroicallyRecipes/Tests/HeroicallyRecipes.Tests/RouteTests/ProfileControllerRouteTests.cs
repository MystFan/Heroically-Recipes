namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;
    using HeroicallyRecipes.Web.Areas.Users;
    using MvcRouteUnitTester;
    using NUnit.Framework;

    [TestFixture]
    public class ProfileControllerRouteTests
    {
        private RouteCollection routeCollection;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            this.routeCollection = new RouteCollection();
        }

        [Test]
        public void ProfileShouldMapRouteDefaultIndex()
        {
            var tester = new RouteTester<UsersAreaRegistration>();

            tester.WithIncomingRequest("/Users/Profile/Index")
                .ShouldMatchRoute("Users", "Profile", "Index");
        }
    }
}
