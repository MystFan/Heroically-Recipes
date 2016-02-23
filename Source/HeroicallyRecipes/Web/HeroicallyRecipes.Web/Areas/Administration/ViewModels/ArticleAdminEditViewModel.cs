namespace HeroicallyRecipes.Web.Areas.Administration.ViewModels
{
    using System;

    using AutoMapper;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class ArticleAdminEditViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleAdminEditViewModel>()
                .ForMember(av => av.Author, opt => opt.MapFrom(a => a.Author.NickName));
        }
    }
}