namespace HeroicallyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Validation;

    public class Tag : BaseModel<int>
    {
        [Required]
        [StringLength(ModelConstants.TagMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.TagMinLength)]
        public string Text { get; set; }
    }
}
