namespace HeroicallyRecipes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Recipe
    {
        private ICollection<Tag> tags;
        private ICollection<Rating> ratings;
        public ICollection<RecipeImage> images;

        public Recipe()
        {
            this.Id = Guid.NewGuid();
            this.tags = new HashSet<Tag>();
            this.ratings = new HashSet<Rating>();
            this.images = new HashSet<RecipeImage>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(ModelConstants.RecipeTitleMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.RecipeTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstants.RecipeContentMinLength)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<RecipeImage> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
