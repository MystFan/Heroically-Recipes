namespace HeroicallyRecipes.Data.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Common.Validation;

    public class User : IdentityUser
    {
        private ICollection<Recipe> recipes;
        private ICollection<Article> articles;

        public User()
        {
            this.recipes = new HashSet<Recipe>();
            this.articles = new HashSet<Article>();
        }

        [Required]
        [MaxLength(ModelConstants.UserAvatarPathMaxLength)]
        public string AvatarUrl { get; set; }

        [Required]
        [MaxLength(ModelConstants.UserNickNameMaxLength)]
        public string NickName { get; set; }

        public virtual ICollection<Recipe> Recipes
        {
            get
            {
                return this.recipes;
            }

            set
            {
                this.recipes = value;
            }
        }

        public virtual ICollection<Article> Articles
        {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
