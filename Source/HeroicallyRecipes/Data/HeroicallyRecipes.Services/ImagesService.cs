using HeroicallyRecipes.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroicallyRecipes.Models;
using HeroicallyRecipes.Data.Repositories;

namespace HeroicallyRecipes.Services
{
    public class ImagesService : IImagesService
    {
        private IRepository<RecipeImage> recipeImages;

        public ImagesService(IRepository<RecipeImage> recipeImages)
        {
            this.recipeImages = recipeImages;
        }

        public RecipeImage GetByRecipeId(string id)
        {
            return this.recipeImages
                .All()
                .FirstOrDefault(img => img.RecipeId.ToString() == id);
        }
    }
}
