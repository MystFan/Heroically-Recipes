namespace HeroicallyRecipes.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq;

    using AutoMapper;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class RecipeAdminEditModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Preparation { get; set; }

        public int Votes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public string Creator { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipe, RecipeAdminEditModel>()
                .ForMember(s => s.Category, opt => opt.MapFrom(s => s.Category.Name));

            configuration.CreateMap<Recipe, RecipeAdminEditModel>()
                .ForMember(s => s.Creator, opt => opt.MapFrom(s => s.Creator.NickName));

            configuration.CreateMap<Recipe, RecipeAdminEditModel>()
                .ForMember(s => s.Votes, opt => opt.MapFrom(s => s.Votes.Any() ? s.Votes.Sum(v => (int)v.Type) : 0));
        }
    }
}
