namespace HeroicallyRecipes.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using HeroicallyRecipes.Common.Validation;

    public class RecipeAdminInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.RecipeTitleMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.RecipeTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstants.RecipePreparationMinLength)]
        public string Preparation { get; set; }

        [Required]
        public int Votes { get; set; }
    }
}