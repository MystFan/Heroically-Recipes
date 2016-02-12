namespace HeroicallyRecipes.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class RecipeImage : BaseModel<int>
    {
        public RecipeImage()
        {
            this.ViewId = Guid.NewGuid();
        }

        public Guid ViewId { get; set; }

        [Required]
        [StringLength(ModelConstants.ImageNameMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.ImageNameMinLength)]
        public string OriginalName { get; set; }

        [Required]
        [StringLength(ModelConstants.ImageExtensionMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.ImageExtensionMinLength)]
        public string Extension { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
