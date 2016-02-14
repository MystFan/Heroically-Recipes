﻿namespace HeroicallyRecipes.Data.Models
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

        public User()
        {
            this.recipes = new HashSet<Recipe>();
        }

        [Required]
        [MaxLength(ModelConstants.UserAvatarPathMaxLength)]
        public string AvatarUrl { get; set; }

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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
