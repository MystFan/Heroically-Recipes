namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using HeroicallyRecipes.Web.Models.Articles;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class ArticlesController : UsersBaseController
    {
        private const int ArticleCacheDuration = 10 * 60;
        private IArticlesService articles;

        public ArticlesController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Details(int id)
        {
            var article = base.Cache
                .Get("article" + id.ToString(),
                () =>
                    Mapper.Map<ArticleViewModel>(this.articles.GetById(id)),
                ArticleCacheDuration);

            return this.View(article);
        }
    }
}