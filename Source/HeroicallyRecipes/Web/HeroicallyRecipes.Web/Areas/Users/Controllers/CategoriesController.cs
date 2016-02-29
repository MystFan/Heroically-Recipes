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
            return this.PartialView("_CategoriesPartial", this.GetCategories());
        }

        private IList<CategoryViewModel> GetCategories()
        {
            var allCategories = this.Cache
                .Get(
                CategoriesKey,
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