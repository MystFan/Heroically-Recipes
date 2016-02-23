namespace HeroicallyRecipes.Web.Areas.Administration.ViewModels
{
    using System;

    using AutoMapper;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class CommentAdminEditViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentAdminEditViewModel>()
                .ForMember(cv => cv.Author, opt => opt.MapFrom(c => c.Author.NickName));
        }
    }
}