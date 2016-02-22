namespace HeroicallyRecipes.Common.Validation
{
    public class ModelConstants
    {
        public const int RecipeTitleMaxLength = 100;

        public const int RecipeTitleMinLength = 3;

        public const int RecipePreparationMinLength = 100;

        public const int IngredientMinLength = 5;

        public const int IngredientMaxLength = 500;

        public const int IngredientMinCount = 3;

        public const int TagMinLength = 3;

        public const int TagMaxLength = 100;

        public const int TagsMinCount = 1;

        public const string TagTextPattern = "(#)((?:[A-Za-z0-9-_]{100}))";

        public const string NickNamePattern = "([A-z0-9_]+)";

        public const int RatingMinValue = 2;

        public const int RatingMaxValue = 10;

        public const int ImageNameMinLength = 2;

        public const int ImageNameMaxLength = 100;

        public const int ImageExtensionMaxLength = 10;

        public const int ImageExtensionMinLength = 2;

        public const int RecipeImageMaxContentLength = 1000000;

        public const int CategoryNameMinLength = 2;

        public const int CategoryNameMaxLength = 100;

        public const int UserAvatarPathMaxLength = 250;

        public const int UserNickNameMaxLength = 50;

        public const int UserNickNameMinLength = 5;

        public const int ArticleTitleMinLength = 3;

        public const int ArticleTitleMaxLength = 500;

        public const int CommentContentMinLength = 10;

        public const int CommentContentMaxLength = 2000;
    }
}
