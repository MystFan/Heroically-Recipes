namespace HeroicallyRecipes.Web.Models.Articles
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;
    using HeroicallyRecipes.Web.Models.Comments;

    public class ArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(av => av.Author, opt => opt.MapFrom(a => a.Author.NickName));
        }
    }
}
