namespace HeroicallyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeVote : BaseModel<int>
    {
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public VoteType Type { get; set; }
    }
}
