namespace HeroicallyRecipes.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.IngredientMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.IngredientMinLength)]
        public string Text { get; set; }

        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
