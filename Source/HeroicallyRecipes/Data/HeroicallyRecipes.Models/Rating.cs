namespace HeroicallyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Rating : BaseModel<int>
    {
        [Required]
        [Range(ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue)]
        public int Value { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
