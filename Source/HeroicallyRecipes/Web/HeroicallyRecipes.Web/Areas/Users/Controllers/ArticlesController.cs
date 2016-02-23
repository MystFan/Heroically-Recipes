namespace HeroicallyRecipes.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Web.Models.Articles;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class ArticlesController : UsersBaseController
    {
        private const string ArticleChacheKey = "Article";
        private const int ArticleCacheDuration = 10 * 60;
        private IArticlesService articles;

        public ArticlesController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index(int page = 1)
        {
            int totalArticles= this.articles.GetAll().Count();
            int totalPages = (int)Math.Ceiling(totalArticles / (decimal)GlobalConstants.ArticleDefaultPageSize);

            var articlesResult = this.Cache.Get(ArticleChacheKey + page.ToString(),
                            () => this.articles
                                .Get(page)
                                .ProjectTo<ArticleViewModel>()
                                .ToList(),
                            1 * 60);

            ArticleListViewModel viewModel = new ArticleListViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Articles = articlesResult
            };

            return this.View(viewModel);
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