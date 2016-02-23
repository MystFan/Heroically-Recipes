namespace HeroicallyRecipes.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using HeroicallyRecipes.Common.Validation;

    public class ArticleAdminEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.ArticleTitleMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}