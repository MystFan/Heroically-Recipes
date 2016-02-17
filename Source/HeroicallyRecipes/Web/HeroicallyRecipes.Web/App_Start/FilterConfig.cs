namespace HeroicallyRecipes.Web
{
    using System.Web.Mvc;
    using HeroicallyRecipes.Web.Infrastructure.CustomFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AddResponseHeaderFilter());
        }
    }
}
