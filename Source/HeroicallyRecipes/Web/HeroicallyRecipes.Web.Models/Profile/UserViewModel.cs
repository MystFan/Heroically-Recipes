namespace HeroicallyRecipes.Web.Models.Profile
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mappings;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string AvatarUrl { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
