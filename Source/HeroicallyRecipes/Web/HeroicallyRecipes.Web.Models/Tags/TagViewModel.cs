namespace HeroicallyRecipes.Web.Models.Tags
{
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class TagViewModel
    {
        [Required]
        [StringLength(ModelConstants.TagMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.TagMinLength)]
        public string Text { get; set; }
    }
}
