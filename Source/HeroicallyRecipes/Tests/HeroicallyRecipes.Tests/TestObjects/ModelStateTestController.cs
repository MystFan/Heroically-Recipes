namespace HeroicallyRecipes.Tests.TestObjects
{
    using System.Web.Mvc;

    using Moq;

    public class ModelStateTestController : Controller
    {
        public ModelStateTestController()
        {
            this.ControllerContext = (new Mock<ControllerContext>()).Object;
        }

        public bool TestTryValidateModel(object model)
        {
            return this.TryValidateModel(model);
        }
    }
}
