namespace HeroicallyRecipes.Web.Infrastructure.CustomAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using HeroicallyRecipes.Common.Validation;

    public class ValidateIngredients : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection<string>;
            bool IsValid = true;

            if (collection != null)
            {
                if (collection.Count(i => i != string.Empty) < ModelConstants.IngredientMinCount)
                {
                    ErrorMessage = "The recipe must contain at least " + ModelConstants.IngredientMinCount + " ingredients!";
                    IsValid = false;
                }
            }

            return IsValid;
        }
    }
}
