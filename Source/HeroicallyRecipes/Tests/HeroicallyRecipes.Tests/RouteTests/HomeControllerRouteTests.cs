namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Controllers;

    [TestFixture]
    public class HomeControllerRouteTests
    {
        private RouteCollection routeCollection;

        [TestFixtureSetUp]
        public void SetupTests()
        {
            this.routeCollection = new RouteCollection();
        }

        [Test]
        public void HomeShouldMapRouteDefaultIndex()
        {
            const string Url = "/Home/Index";

            RouteConfig.RegisterRoutes(this.routeCollection);

            routeCollection.ShouldMap(Url)
                .To<HomeController>(c => c.Index());
        }

        [Test]
        public void HomeShouldMapRouteGetImage()
        {
            const string Url = "/Home/GetImage/NC4xMjMxMjMxMzEyMw%3d%3d";

            RouteConfig.RegisterRoutes(this.routeCollection);

            routeCollection.ShouldMap(Url)
                .To<HomeController>(c => c.GetImage("NC4xMjMxMjMxMzEyMw%3d%3d"));
        }

        [Test]
        public void HomeShouldMapRouteChildActionGetTopRecipes()
        {
            const string Url = "/Home/GetTopRecipes";

            RouteConfig.RegisterRoutes(this.routeCollection);

            routeCollection.ShouldMap(Url)
                .To<HomeController>(c => c.GetTopRecipes());
        }
    }
}
