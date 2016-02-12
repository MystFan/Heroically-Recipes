namespace HeroicallyRecipes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HeroicallyRecipes.Common.Validation;

    public class Category : BaseModel<int>
    {
        private ICollection<Recipe> recipes;

        public Category()
        {
            this.recipes = new HashSet<Recipe>();
        }

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
