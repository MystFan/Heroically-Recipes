namespace HeroicallyRecipes.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class RecipeImage
    {
        public RecipeImage()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(ModelConstants.ImageNameMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.ImageNameMinLength)]
        public string OriginalName { get; set; }

        [Required]
        [StringLength(ModelConstants.ImageExtensionMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.ImageExtensionMinLength)]
        public string Extension { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
