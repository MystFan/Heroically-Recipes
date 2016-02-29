namespace HeroicallyRecipes.Tests.RouteTests
{
    using System.Web.Routing;

    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Controllers;
    using MvcRouteTester;
    using NUnit.Framework;

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
            string url = "/Home/Index";

            RouteConfig.RegisterRoutes(this.routeCollection);

            this.routeCollection.ShouldMap(url)
                .To<HomeController>(c => c.Index());
        }

        [Test]
        public void HomeShouldMapRouteGetImage()
        {
            string url = "/Home/GetImage/NC4xMjMxMjMxMzEyMw%3d%3d";

            RouteConfig.RegisterRoutes(this.routeCollection);

            this.routeCollection.ShouldMap(url)
                .To<HomeController>(c => c.GetImage("NC4xMjMxMjMxMzEyMw%3d%3d"));
        }

        [Test]
        public void HomeShouldMapRouteChildActionGetTopRecipes()
        {
            string url = "/Home/GetTopRecipes";

            RouteConfig.RegisterRoutes(this.routeCollection);

            this.routeCollection.ShouldMap(url)
                .To<HomeController>(c => c.GetTopRecipes());
        }
    }
}
