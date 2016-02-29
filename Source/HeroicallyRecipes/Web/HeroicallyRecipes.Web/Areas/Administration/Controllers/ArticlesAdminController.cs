namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Areas.Administration.ViewModels;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class ArticlesAdminController : AdminBaseController
    {
        private IArticlesService articles;

        public ArticlesAdminController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AjaxIndex([DataSourceRequest]DataSourceRequest request)
        {
            var allArticles = this.articles
                .GetAll()
                .ProjectTo<ArticleAdminEditViewModel>()
                .ToList();

            return this.Json(allArticles.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, ArticleAdminEditInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.articles.Update(model.Id, model.Title, model.Content);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ArticleAdminEditInputModel model)
        {
            if (model != null)
            {
                this.articles.Delete(model.Id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}