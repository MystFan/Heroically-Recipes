namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Models.Categories;

    public class CategoriesController : UsersBaseController
    {
        private const int CategoriesCacheDuration = 10 * 60;
        private const string CategoriesKey = "Categories";

        private ICategoriesService categories;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categories = categoriesService;
        }

        public ActionResult All()
        {
            return PartialView("_CategoriesPartial", GetCategories());
        }

        private IList<CategoryViewModel> GetCategories()
        {
            var allCategories = base.Cache
                .Get(CategoriesKey,
                () =>
                    this.categories
                    .GetAll()
                    .ProjectTo<CategoryViewModel>()
                    .ToList(),
                CategoriesCacheDuration);

            return allCategories;
        }
    }
}