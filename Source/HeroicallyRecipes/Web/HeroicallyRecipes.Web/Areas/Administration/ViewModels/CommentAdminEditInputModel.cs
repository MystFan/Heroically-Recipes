namespace HeroicallyRecipes.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using HeroicallyRecipes.Common.Validation;

    public class CommentAdminEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.CommentContentMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.CommentContentMinLength)]
        public string Content { get; set; }
    }
}