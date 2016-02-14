namespace HeroicallyRecipes.Common.Validation
{
    public class ModelConstants
    {
        public const int RecipeTitleMaxLength = 100;

        public const int RecipeTitleMinLength = 3;

        public const int RecipePreparationMinLength = 100;

        public const int IngredientMinLength = 5;

        public const int IngredientMaxLength = 500;

        public const int TagMinLength = 3;

        public const int TagMaxLength = 100;

        public const string TagTextPattern = "(#)((?:[A-Za-z0-9-_]{100}))";

        public const int RatingMinValue = 2;

        public const int RatingMaxValue = 10;

        public const int ImageNameMinLength = 2;

        public const int ImageNameMaxLength = 100;

        public const int ImageExtensionMaxLength = 10;

        public const int ImageExtensionMinLength = 2;

        public const int CategoryNameMinLength = 2;

        public const int CategoryNameMaxLength = 100;

        public const int UserAvatarPathMaxLength = 250;
    }
}
