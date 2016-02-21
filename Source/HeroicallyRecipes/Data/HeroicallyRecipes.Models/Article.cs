namespace HeroicallyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Article : BaseModel<int>
    {
        [Required]
        [StringLength(ModelConstants.ArticleTitleMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User Author { get; set; }
    }
}
