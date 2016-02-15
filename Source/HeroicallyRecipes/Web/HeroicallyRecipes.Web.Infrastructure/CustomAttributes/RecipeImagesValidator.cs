namespace HeroicallyRecipes.Web.Infrastructure.CustomAttributes
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HeroicallyRecipes.Common.Validation;

    public class RecipeImagesValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var images = value as IEnumerable<HttpPostedFileBase>;
            bool IsValid = true;

            if (images != null)
            {
                if (!images.Any(i => i != null))
                {
                    this.ErrorMessage = "The recipe must contain at least one image!";
                    IsValid = false;
                    return IsValid;
                }

                foreach (var image in images)
                {
                    if (image == null)
                    {
                        continue;
                    }

                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string fileExtension = Path.GetExtension(image.FileName);

                    if (fileName.Length <= ModelConstants.ImageNameMinLength)
                    {
                        IsValid = false;
                        this.ErrorMessage = string.Format("The recipe image must be at least {0} characters long.", ModelConstants.ImageNameMinLength);
                        break;
                    }

                    if (fileName.Length > ModelConstants.ImageNameMaxLength)
                    {
                        IsValid = false;
                        this.ErrorMessage = string.Format("Recipe image cannot be longer than {0} characters.", ModelConstants.ImageNameMaxLength);
                        break;
                    }

                    if (fileExtension.Length <= ModelConstants.ImageExtensionMinLength)
                    {
                        IsValid = false;
                        this.ErrorMessage = string.Format("The recipe image extension must be at least {0} characters long.", ModelConstants.ImageExtensionMinLength);
                        break;
                    }

                    if (fileExtension.Length > ModelConstants.ImageExtensionMaxLength)
                    {
                        IsValid = false;
                        this.ErrorMessage = string.Format("Recipe image extension cannot be longer than {0} characters.", ModelConstants.ImageExtensionMaxLength);
                        break;
                    }

                    if (image.ContentLength == 0 || image.ContentLength > ModelConstants.RecipeImageMaxContentLength)
                    {
                        IsValid = false;
                        this.ErrorMessage = string.Format("Recipe image size must be between {0} and {1} MB.", 0, (decimal)ModelConstants.RecipeImageMaxContentLength / 1000000);
                        break;
                    }
                }
            }

            return IsValid;
        }
    }
}
