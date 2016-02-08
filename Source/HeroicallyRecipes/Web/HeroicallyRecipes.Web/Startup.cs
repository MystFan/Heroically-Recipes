using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeroicallyRecipes.Web.Startup))]
namespace HeroicallyRecipes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
