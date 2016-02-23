namespace HeroicallyRecipes.Web.Models.Profile
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Web.Infrastructure.Mappings;
    using HeroicallyRecipes.Web.Models.RecipeViewModels;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string AvatarUrl { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(uv => uv.Recipes, opt => opt.MapFrom(u => u.Recipes));
        }
    }
}
