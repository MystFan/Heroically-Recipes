namespace HeroicallyRecipes.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Models.Articles;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;

    public class HomeController : BaseController
    {
        private const int TopRecipesCount = 5;
        private const int NewestArticlesCount = 5;
        private IImagesService images;
        private IRecipesService recipes;
        private IArticlesService articles;

        public HomeController(IImagesService images, IRecipesService recipes, IArticlesService articles)
        {
            this.images = images;
            this.recipes = recipes;
            this.articles = articles;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult GetNewestArticles()
        {
            var newestArticles = this.articles
                .GetNewest(NewestArticlesCount)
                .ProjectTo<ArticleViewModel>()
                .ToList();

            return this.PartialView("_ArticlesListPartial", newestArticles);
        }

        [HttpGet]
        public ActionResult GetImage(string id)
        {
            var img = this.images.GetByRecipeId(id);
            string imgExtension = img.Extension;

            return new FileContentResult(img.Content, "image/" + imgExtension);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public ActionResult GetTopRecipes()
        {
            var allRecipes = this.recipes
                .GetTop(TopRecipesCount)
                .ProjectTo<RecipeHomeViewModel>()
                .ToList();

            return this.PartialView("_SliderPartial", allRecipes);
        }
    }
}
