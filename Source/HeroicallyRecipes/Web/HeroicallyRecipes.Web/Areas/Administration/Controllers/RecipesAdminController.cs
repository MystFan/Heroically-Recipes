namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Areas.Administration.ViewModels;

    public class RecipesAdminController : AdminBaseController
    {
        private IRecipesService recipes;

        public RecipesAdminController(IRecipesService recipes)
        {
            this.recipes = recipes;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxIndex([DataSourceRequest]DataSourceRequest request)
        {
            var allRecipes = this.recipes
                .GetAll()
                .ProjectTo<RecipeAdminEditModel>()
                .ToList();

            return Json(allRecipes.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, RecipeAdminInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                this.recipes.Update(model.Id, userId, model.Title, model.Preparation, model.Votes);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, RecipeAdminInputModel model)
        {
            if (model != null)
            {
                this.recipes.Delete(model.Id);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}