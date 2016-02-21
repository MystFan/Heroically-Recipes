namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HeroicallyRecipes.Data;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class TagsController : UsersBaseController
    {
        private ITagsService tags;

        public TagsController(ITagsService tags)
        {
            this.tags = tags;
        }

        public ActionResult GetTags()
        {
            var tags = this.tags
                .GetAll()
                .Select(t => t.Text)
                .ToList();

            return this.PartialView("_TagsPartial", tags);
        }
    }
}