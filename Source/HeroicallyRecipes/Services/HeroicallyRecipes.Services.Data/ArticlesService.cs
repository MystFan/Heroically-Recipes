namespace HeroicallyRecipes.Services.Data
{
    using System.Linq;

    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Data.Repositories;
    using HeroicallyRecipes.Services.Data.Contracts;

    public class ArticlesService : IArticlesService
    {
        private IDbRepository<Article> articles;

        public ArticlesService(IDbRepository<Article> articles)
        {
            this.articles = articles;
        }

        public IQueryable<Article> Get(int page)
        {
            return this.articles
                .All()
                .OrderBy(a => a.CreatedOn)
                .Skip((page - 1) * GlobalConstants.ArticleDefaultPageSize)
                .Take(GlobalConstants.ArticleDefaultPageSize);
        }

        public IQueryable<Article> GetAll()
        {
            var allArticles = this.articles.All();
            return allArticles;
        }

        public Article GetById(int id)
        {
            var article = this.articles
                .All()
                .FirstOrDefault(a => a.Id == id);

            return article;
        }

        public IQueryable<Article> GetNewest(int count)
        {
            var newestArticles = this.articles
                .All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(count);

            return newestArticles;
        }
    }
}
