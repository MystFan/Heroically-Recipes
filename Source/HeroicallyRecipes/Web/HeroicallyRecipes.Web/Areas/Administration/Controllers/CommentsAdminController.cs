namespace HeroicallyRecipes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Areas.Administration.ViewModels;

    public class CommentsAdminController : AdminBaseController
    {
        private ICommentsService comments;

        public CommentsAdminController(ICommentsService comments)
        {
            this.comments = comments;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxIndex([DataSourceRequest]DataSourceRequest request)
        {
            var allComments = this.comments
                .GetAll()
                .ProjectTo<CommentAdminEditViewModel>()
                .ToList();

            return Json(allComments.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, CommentAdminEditInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.comments.Update(model.Id, model.Content);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CommentAdminEditInputModel model)
        {
            if (model != null)
            {
                this.comments.Delete(model.Id);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}