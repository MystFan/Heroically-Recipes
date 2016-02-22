namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using HeroicallyRecipes.Data;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class TagsController : UsersBaseController
    {
        private const string TagsListKey = "TagsList";
        private const int TagsCacheDuration = 30 * 60;
        private ITagsService tags;

        public TagsController(ITagsService tags)
        {
            this.tags = tags;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetTags()
        {
            var tags = this.tags
                .GetAll()
                .Select(t => t.Text)
                .ToList();

            return this.PartialView("_TagsPartial", tags);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult All()
        {
            var allTags = this.Cache
                .Get(TagsListKey,
                    () => this.tags.InRecipe().Select(t => t.Text).ToList(),
                    TagsCacheDuration);

            return this.PartialView("_TagsListPartial", allTags);
        }
    }
}