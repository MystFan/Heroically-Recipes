namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Areas.Administration.ViewModels;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class CommentsAdminController : AdminBaseController
    {
        private ICommentsService comments;

        public CommentsAdminController(ICommentsService comments)
        {
            this.comments = comments;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AjaxIndex([DataSourceRequest]DataSourceRequest request)
        {
            var allComments = this.comments
                .GetAll()
                .ProjectTo<CommentAdminEditViewModel>()
                .ToList();

            return this.Json(allComments.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, CommentAdminEditInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.comments.Update(model.Id, model.Content);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CommentAdminEditInputModel model)
        {
            if (model != null)
            {
                this.comments.Delete(model.Id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}