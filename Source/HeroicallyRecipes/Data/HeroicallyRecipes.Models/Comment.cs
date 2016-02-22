namespace HeroicallyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HeroicallyRecipes.Common.Validation;

    public class Comment : BaseModel<int>
    {
        [StringLength(ModelConstants.CommentContentMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.CommentContentMinLength)]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}
