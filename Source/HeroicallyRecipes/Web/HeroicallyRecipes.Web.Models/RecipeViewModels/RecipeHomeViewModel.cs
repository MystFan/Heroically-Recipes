namespace HeroicallyRecipes.Web.Models.RecipeViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using HeroicallyRecipes.Common.Providers;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;

    public class RecipeHomeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ViewId
        {
            get
            {
                IdentifierProvider idProvider = new IdentifierProvider();
                return idProvider.EncodeId(this.Id);
            }
        }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipe, RecipeHomeViewModel>()
                .ForMember(s => s.Category, opt => opt.MapFrom(s => s.Category.Name));
        }
    }
}
