namespace HeroicallyRecipes.Web.Models.Ingredients
{
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class IngredientViewModels
    {
        [Required]
        [StringLength(ModelConstants.IngredientMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.IngredientMinLength)]
        public string Text { get; set; }
    }
}
