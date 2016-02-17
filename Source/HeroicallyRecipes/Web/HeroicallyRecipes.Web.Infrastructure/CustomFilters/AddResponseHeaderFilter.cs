namespace HeroicallyRecipes.Web.Infrastructure.CustomFilters
{
    using System.Web.Mvc;
    using HeroicallyRecipes.Common.Globals;

    public class AddResponseHeaderFilter : ActionFilterAttribute
    {
        private string key;
        private string value;

        public AddResponseHeaderFilter(string key = "X-Powered-By", string value = GlobalConstants.ApplicationName)
        {
            this.key = key;
            this.value = value;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add(this.key, this.value);
        }
    }
}
