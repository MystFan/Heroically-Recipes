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
        public static IRecipesService GetRecipeService()
        {
            var recipesServiceMock = new Mock<IRecipesService>();
            int pageSize = GlobalConstants.RecipeDefaultPageSize;

            recipesServiceMock.Setup(rs => rs.GetAll())
                .Returns(GetRecipeRepositiry(10).All());

            recipesServiceMock.Setup(rs => rs.Get(It.IsIn(1)))
                .Returns(GetRecipeRepositiry(10).All().Skip((1 - 1) * pageSize).Take(pageSize));

            recipesServiceMock.Setup(rs => rs.Get(It.IsIn(5)))
               .Returns(GetRecipeRepositiry(10).All().Skip((5 - 1) * pageSize).Take(pageSize));

            recipesServiceMock.Setup(rs =>
            rs.Add(It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<int>(),
                   It.IsAny<string>(),
                   It.IsAny<IEnumerable<string>>(),
                   It.IsAny<IEnumerable<HttpPostedFileBase>>(),
                   It.IsAny<IEnumerable<string>>()))
                   .Returns(1);

            return recipesServiceMock.Object;
        }

        public static IImagesService GetRecipeImagesService()
        {
            var recipesImagesServiceMock = new Mock<IImagesService>();

            recipesImagesServiceMock.Setup(ri => ri.GetByRecipeId(It.IsAny<string>()))
                .Returns(GetRecipeImagesRepositiry(10).GetById(1));

            return recipesImagesServiceMock.Object;
        }

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
                RecipeImages = new List<HttpPostedFileBase>() { null, null, imageMock.Object }
            };
        }
    }
}
