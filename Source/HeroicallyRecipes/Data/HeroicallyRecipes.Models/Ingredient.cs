namespace HeroicallyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Ingredient : BaseModel<int>
    {
        [Required]
        [StringLength(ModelConstants.IngredientMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.IngredientMinLength)]
        public string Text { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
