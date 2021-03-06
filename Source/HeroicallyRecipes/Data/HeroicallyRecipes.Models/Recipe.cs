﻿namespace HeroicallyRecipes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HeroicallyRecipes.Common.Validation;

    public class Recipe : BaseModel<int>
    {
        private ICollection<Tag> tags;
        private ICollection<RecipeVote> votes;
        private ICollection<Rating> ratings;
        private ICollection<RecipeImage> images;
        private ICollection<Ingredient> ingredients;

        public Recipe()
        {
            this.votes = new HashSet<RecipeVote>();
            this.tags = new HashSet<Tag>();
            this.ratings = new HashSet<Rating>();
            this.images = new HashSet<RecipeImage>();
            this.ingredients = new HashSet<Ingredient>();
        }

        [Required]
        [StringLength(ModelConstants.RecipeTitleMaxLength, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ModelConstants.RecipeTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstants.RecipePreparationMinLength)]
        public string Preparation { get; set; }

        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }

        public string UserId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<RecipeVote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
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

        public virtual ICollection<Ingredient> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }
    }
}
