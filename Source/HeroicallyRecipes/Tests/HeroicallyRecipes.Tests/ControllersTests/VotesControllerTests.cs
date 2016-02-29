namespace HeroicallyRecipes.Tests.ControllersTests
{
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Services.Web;
    using HeroicallyRecipes.Tests.TestObjects;
    using HeroicallyRecipes.Web;
    using HeroicallyRecipes.Web.Areas.Users.Controllers;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class VotesControllerTests
    {
        private IVoteService votes;
        private VotesController controller;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            AutoMapperConfig.RegisterMappings();

            this.votes = ServicesObjectFactory.GetVoteService();
            this.controller = new VotesController(this.votes);
            this.controller.Cache = new HttpCacheService();
        }

        [Test]
        public void VoteActionWithValidParametersShouldWorkCorectly()
        {
            this.MockIdentity();

            int recipeId = 2;
            int vote = 1;

            this.controller.WithCallTo(x => x.Vote(recipeId, vote))
                .ShouldReturnJson(data =>
                {
                    Assert.AreEqual((object)data.ToString(), "{ Count = 1 }");
                });
        }

        [Test]
        public void VoteActionWithInvalidParameterVoteShouldWorkCorectly()
        {
            this.MockIdentity();

            int recipeId = 1;
            int vote = -2;

            this.controller.WithCallTo(x => x.Vote(recipeId, vote))
                .ShouldReturnJson(data =>
                {
                    Assert.AreEqual((object)data.ToString(), "{ Count = 0 }");
                });
        }

        [Test]
        public void VoteActionShouldHaveAjaxOnlyAttribute()
        {
            var type = this.controller.GetType();
            var methodInfo = type.GetMethod("Vote");
            var attributes = methodInfo.GetCustomAttributes(true).Select(a => a.GetType().Name);
            Assert.IsTrue(attributes.Any(a => a == "AjaxOnlyAttribute"));
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
