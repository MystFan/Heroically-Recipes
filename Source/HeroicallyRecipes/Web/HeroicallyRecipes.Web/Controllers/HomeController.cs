namespace HeroicallyRecipes.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Services.Data.Contracts;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;

    public class HomeController : BaseController
    {
        private IImagesService images;
        private IRecipesService recipes;

        public HomeController(IImagesService images, IRecipesService recipes)
        {
            this.images = images;
            this.recipes = recipes;
        }

        public ActionResult Index()
        {
            return View();
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
                .GetAll()
                .ProjectTo<RecipeHomeViewModel>()
                .ToList();

            return PartialView("_SliderPartial", allRecipes);
        }
    }
}
