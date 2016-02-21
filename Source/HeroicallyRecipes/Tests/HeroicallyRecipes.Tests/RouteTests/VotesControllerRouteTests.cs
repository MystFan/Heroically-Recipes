namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;

    using MvcRouteUnitTester;
    using NUnit.Framework;
    using HeroicallyRecipes.Web.Areas.Users;

    public class VotesControllerRouteTests
    {
        private RouteCollection routeCollection;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            this.routeCollection = new RouteCollection();
        }

        [Test]
        public void VotesShouldMapRouteVote()
        {
            var tester = new RouteTester<UsersAreaRegistration>();

            tester.WithIncomingRequest("/Users/Votes/Vote")
                .ShouldMatchRoute("Users", "Votes", "Vote");
        }
    }
}
