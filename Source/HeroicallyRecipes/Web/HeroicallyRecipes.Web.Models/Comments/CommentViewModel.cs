namespace HeroicallyRecipes.Web.Models.Comments
{
    using System;

    using AutoMapper;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ArticleId { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(cv => cv.Author, opt => opt.MapFrom(c => c.Author.NickName));
        }
    }
}
