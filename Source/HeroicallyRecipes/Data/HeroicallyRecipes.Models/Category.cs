namespace HeroicallyRecipes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Category
    {
        private ICollection<Recipe> recipes;

        public Category()
        {
            this.recipes = new HashSet<Recipe>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(ModelConstants.CategoryNameMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.CategoryNameMinLength)]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { this.recipes = value; }
        }
    }
}
