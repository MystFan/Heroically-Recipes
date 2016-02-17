namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Controllers;

    [TestFixture]
    public class HomeRouteTests
    {
        [Test]
        public void TestGetImageById()
        {
            const string Url = "/Home/GetImage/NC4xMjMxMjMxMzEyMw%3d%3d";

            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            routeCollection.ShouldMap(Url)
                .To<HomeController>(c => c.GetImage("NC4xMjMxMjMxMzEyMw%3d%3d"));
        }
    }
}
