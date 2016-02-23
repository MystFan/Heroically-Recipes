namespace HeroicallyRecipes.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Moq;
    using HeroicallyRecipes.Common.Globals;
    using HeroicallyRecipes.Data.Models;
    using HeroicallyRecipes.Services.Data.Contracts;
    using Web.Models.RecipeViewModels;

    public class TestObjectsFactory
    {
        public static MemoryRepository<Recipe> GetRecipeRepositiry(int numberOfRecipes)
        {
            var repo = new MemoryRepository<Recipe>();

            repo.Add(new Recipe()
            {
                Id = 1,
                Title = "Tandoori Carrots",
                Preparation = "Preparation 1, Preparation 1, Preparation 1, Preparation 1, Preparation 1, Preparation 1, Preparation 1, Preparation 1, Preparation 1",
                Category = new Category() { Name = "Healthy" },
                CreatedOn = new DateTime(2016, 1, 12, 12, 12, 12),
                Creator = new User() { NickName = "Creator 1" },
                Tags = new List<Tag>() { new Tag() { Text = "default" } },
                Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image1.png", Extension = ".png" } }
            });

            repo.Add(new Recipe()
            {
                Id = 2,
                Title = "Sheetpandinners Chicken",
                Preparation = "Preparation 2, Preparation 2, Preparation 2, Preparation 2, Preparation 2, Preparation 2, Preparation 2, Preparation 2, Preparation 2",
                Category = new Category() { Name = "Quick and Easy" },
                CreatedOn = new DateTime(2016, 1, 12, 11, 11, 11),
                Creator = new User() { NickName = "Creator 2" },
                Tags = new List<Tag>() { new Tag() { Text = "default" } },
                Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image2.png", Extension = ".png" } }
            });

            repo.Add(new Recipe()
            {
                Id = 3,
                Title = "Salad with butter and basted mushrooms",
                Preparation = "Preparation 3, Preparation 3, Preparation 3, Preparation 3, Preparation 3, Preparation 3, Preparation 3, Preparation 3, Preparation 3",
                Category = new Category() { Name = "Vegetarian" },
                CreatedOn = new DateTime(2016, 1, 12, 10, 10, 10),
                Creator = new User() { NickName = "Creator 3" },
                Tags = new List<Tag>() { new Tag() { Text = "default" } },
                Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image3.png", Extension = ".png" } }
            });

            for (int i = 4; i <= numberOfRecipes + 3; i++)
            {
                var date = DateTime.Now;
                date.AddDays(i);

                repo.Add(new Recipe()
                {
                    Id = i,
                    Title = "Salad with butter and basted mushrooms " + i,
                    Preparation = "Preparation " + i,
                    Category = new Category() { Name = "Vegetarian" },
                    CreatedOn = date,
                    Tags = new List<Tag>() { new Tag() { Text = "default"} },
                    Creator = new User() { NickName = "Creator " + i },
                    Images = new List<RecipeImage>() { new RecipeImage() { OriginalName = "image" + i + ".png", Extension = ".png" } }
                });
            }

            return repo;
        }

        public static MemoryRepository<RecipeImage> GetRecipeImagesRepositiry(int numberOfImages)
        {
            var repo = new MemoryRepository<RecipeImage>();

            for (int i = 1; i <= numberOfImages; i++)
            {
                var date = DateTime.Now;
                date.AddDays(i);

                byte[] bytes = new byte[20];
                new Random().NextBytes(bytes);

                repo.Add(new RecipeImage()
                {
                    Id = i,
                    OriginalName = "OriginalName.png",
                    Extension = ".png",
                    Content = bytes,
                    CreatedOn = date,
                });
            }

            return repo;
        }

        public static MemoryRepository<Article> GetArticlesRepositiry(int numberOfArticles)
        {
            var repo = new MemoryRepository<Article>();

            for (int i = 0; i < numberOfArticles; i++)
            {
                var date = DateTime.Now;
                date.AddDays(i);

                var article = (new Article()
                {
                    Id = i,
                    Title = "Title " + i,
                    Content = "Content " + i,
                    CreatedOn = date,
                    Author = new User() { NickName = "User " + i }
                });

                for (int j = 0; j < 10; j++)
                {
                    article.Comments.Add(new Comment()
                    {
                        Content = "Comment content " + j,
                        CreatedOn = DateTime.Now
                    });
                }

                repo.Add(article);
            }

            return repo;
        }

        public static MemoryRepository<RecipeVote> GetVotesRepositiry(int numberOfVotes)
        {
            var repo = new MemoryRepository<RecipeVote>();

            var recipe = new Recipe()
            {
                Id = 2,
                Title = "Title " + 2,
                Preparation = "Preparation " + 2,
                Ingredients = new List<Ingredient>() { new Ingredient() { Text = "2 cups sugar"} },
                CreatedOn = DateTime.Now,
                Creator = new User() { NickName = "User " + 1 }
            };

            Random rand = new Random();

            for (int i = 0; i < numberOfVotes; i++)
            {
                var date = DateTime.Now;
                date.AddDays(i);

                var vote = new RecipeVote()
                {
                    Id = i + 1,
                    Recipe = recipe,
                    CreatedOn = date,
                    RecipeId = 2,
                    Type = VoteType.Positive
                };

                repo.Add(vote);
            }

            return repo;
        }

        public static RecipeCreateViewModel GetInvalidRecipeCreateViewModel()
        {
            return new RecipeCreateViewModel()
            {
                Title = "Ta",
                Preparation = "Test Preparation",
                CategoryId = 2,
                Ingredients = new List<string>()
                {
                    "garlic cloves, finely grated, divided",
                    "cup plain whole-milk Greek yogurt, divided",
                },
                RecipeImages = new List<HttpPostedFileBase>() { null, null, null }
            };
        }

        public static RecipeCreateViewModel GetValidRecipeCreateViewModel()
        {
            var imageMock = new Mock<HttpPostedFileBase>();
            imageMock.Setup(i => i.FileName).Returns("image1.png");
            imageMock.Setup(i => i.ContentLength).Returns(500000);

            return new RecipeCreateViewModel()
            {
                Title = "Tandoori Carrots",
                Preparation = "Test Preparation,Test Preparation,Test Preparation,Test Preparation,Test Preparation,Test Preparation,Test Preparation,Test Preparation",
                CategoryId = 2,
                Ingredients = new List<string>()
                {
                    "garlic cloves, finely grated, divided",
                    "garlic cloves, finely grated, divided",
                    "cup plain whole-milk Greek yogurt, divided",
                },
                Tags = "butter salamy",
                RecipeImages = new List<HttpPostedFileBase>() { null, null, imageMock.Object }
            };
        }
    }
}
