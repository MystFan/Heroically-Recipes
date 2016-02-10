namespace HeroicallyRecipes.Web.Models.RecipeViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using HeroicallyRecipes.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class RecipeHomeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipe, RecipeHomeViewModel>()
                .ForMember(s => s.Id, opt => opt.MapFrom(s => s.Id.ToString()));

            configuration.CreateMap<Recipe, RecipeHomeViewModel>()
                .ForMember(s => s.Category, opt => opt.MapFrom(s => s.Category.Name));
        }
    }
}
