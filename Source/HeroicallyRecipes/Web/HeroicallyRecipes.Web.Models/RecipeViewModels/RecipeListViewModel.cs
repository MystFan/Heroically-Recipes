namespace HeroicallyRecipes.Web.Models.RecipeViewModels
{
    using System.Collections.Generic;

    public class RecipeListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
