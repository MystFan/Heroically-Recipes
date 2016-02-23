namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Areas.Administration.ViewModels;

    public class ArticlesAdminController : AdminBaseController
    {
        private IArticlesService articles;

        public ArticlesAdminController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxIndex([DataSourceRequest]DataSourceRequest request)
        {
            var allArticles = this.articles
                .GetAll()
                .ProjectTo<ArticleAdminEditViewModel>()
                .ToList();

            return Json(allArticles.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, ArticleAdminEditInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.articles.Update(model.Id, model.Title, model.Content);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ArticleAdminEditInputModel model)
        {
            if (model != null)
            {
                this.articles.Delete(model.Id);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}