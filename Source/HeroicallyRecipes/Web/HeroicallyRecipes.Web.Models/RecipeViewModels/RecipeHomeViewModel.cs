namespace HeroicallyRecipes.Web.Models.RecipeViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class RecipeHomeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ViewId { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipe, RecipeHomeViewModel>()
                .ForMember(s => s.ViewId, opt => opt.MapFrom(s => s.ViewId.ToString()));

            configuration.CreateMap<Recipe, RecipeHomeViewModel>()
                .ForMember(s => s.Category, opt => opt.MapFrom(s => s.Category.Name));
        }
    }
}
