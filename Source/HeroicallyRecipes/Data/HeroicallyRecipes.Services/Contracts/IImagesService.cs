using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroicallyRecipes.Services.Contracts
{
    using HeroicallyRecipes.Models;

    public interface IImagesService : IService
    {
        RecipeImage GetByRecipeId(string id);
    }
}
