namespace HeroicallyRecipes.Web.Models.RecipeViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using HeroicallyRecipes.Common.Validation;
    using Ingredients;
    using Infrastructure.CustomAttributes;
    using Tags;

    public class RecipeCreateViewModel
    {
        [Required]
        [StringLength(ModelConstants.RecipeTitleMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.RecipeTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstants.RecipePreparationMinLength)]
        [DataType(DataType.MultilineText)]
        public string Preparation { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<string> Tags { get; set; }

        [ValidateIngradients]
        public IEnumerable<string> Ingredients { get; set; }

        [RecipeImagesValidator]
        public IEnumerable<HttpPostedFileBase> RecipeImages { get; set; }
    }
}
