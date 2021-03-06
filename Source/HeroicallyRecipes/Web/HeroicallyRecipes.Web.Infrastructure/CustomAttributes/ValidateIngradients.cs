﻿namespace HeroicallyRecipes.Web.Infrastructure.CustomAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using HeroicallyRecipes.Common.Validation;

    public class ValidateIngradients : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection<string>;
            bool isValid = true;

            if (collection != null)
            {
                if (collection.Count(i => i != string.Empty) < ModelConstants.IngredientMinCount)
                {
                    this.ErrorMessage = "The recipe must contain at least " + ModelConstants.IngredientMinCount + " ingredients!";
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
