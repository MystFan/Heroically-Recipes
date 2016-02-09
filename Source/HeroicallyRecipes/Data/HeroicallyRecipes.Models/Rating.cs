namespace HeroicallyRecipes.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue)]
        public int Value { get; set; }

        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
