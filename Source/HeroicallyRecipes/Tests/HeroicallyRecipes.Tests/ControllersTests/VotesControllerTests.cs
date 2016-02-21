namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;
    using NUnit.Framework;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Services.Web;
    using HeroicallyRecipes.Tests.TestObjects;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Areas.Users.Controllers;

    public class VotesControllerTests
    {
        private IVoteService votes;
        private VotesController controller;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.votes = TestObjectsFactory.GetVoteService();
            this.controller = new VotesController(this.votes);
            this.controller.Cache = new HttpCacheService();
        }

        [Test]
        public void VoteActionWithValidParametersShouldWorkCorectly()
        {

        }

        private void MockIdentity()
        {
            // http://forums.asp.net/t/2028867.aspx?UnitTest+How+to+Mock+User+Identity+GetUserId+
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();

            var identity = new GenericIdentity("dominik.ernst@xyz123.de");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            this.controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), this.controller);
        }
    }
}
