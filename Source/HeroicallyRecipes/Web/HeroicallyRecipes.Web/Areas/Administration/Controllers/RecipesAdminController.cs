namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Areas.Administration.ViewModels;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;

    public class RecipesAdminController : AdminBaseController
    {
        private IRecipesService recipes;

        public RecipesAdminController(IRecipesService recipes)
        {
            this.recipes = recipes;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AjaxIndex([DataSourceRequest]DataSourceRequest request)
        {
            var allRecipes = this.recipes
                .GetAll()
                .ProjectTo<RecipeAdminEditModel>()
                .ToList();

            return this.Json(allRecipes.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, RecipeAdminInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                this.recipes.Update(model.Id, userId, model.Title, model.Preparation, model.Votes);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, RecipeAdminInputModel model)
        {
            if (model != null)
            {
                this.recipes.Delete(model.Id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}