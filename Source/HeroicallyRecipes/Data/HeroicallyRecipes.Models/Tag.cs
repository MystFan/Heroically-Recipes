namespace HeroicallyRecipes.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Validation;

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.TagMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.TagMinLength)]
        public string Text { get; set; }

        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
