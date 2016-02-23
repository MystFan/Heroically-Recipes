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

    public class ServicesObjectFactory
    {
        public static IRecipesService GetRecipeService()
        {
            var recipesServiceMock = new Mock<IRecipesService>();
            int pageSize = GlobalConstants.RecipeDefaultPageSize;

            recipesServiceMock.Setup(rs => rs.GetAll())
                .Returns(TestObjectsFactory.GetRecipeRepositiry(10).All());

            recipesServiceMock.Setup(rs => rs.Get(It.IsIn(1)))
                .Returns(TestObjectsFactory.GetRecipeRepositiry(10).All().Skip((1 - 1) * pageSize).Take(pageSize));

            recipesServiceMock.Setup(rs => rs.Get(It.IsIn(5)))
               .Returns(TestObjectsFactory.GetRecipeRepositiry(10).All().Skip((5 - 1) * pageSize).Take(pageSize));

            recipesServiceMock.Setup(rs => rs.GetTop(It.IsIn(5)))
                .Returns(TestObjectsFactory.GetRecipeRepositiry(2).All());

            recipesServiceMock.Setup(rs =>
                rs.Add(It.IsAny<string>(),
                       It.IsAny<string>(),
                       It.IsAny<int>(),
                       It.IsAny<string>(),
                       It.IsAny<IEnumerable<string>>(),
                       It.IsAny<IEnumerable<HttpPostedFileBase>>(),
                       It.IsAny<string>()))
                       .Returns(1);

            return recipesServiceMock.Object;
        }

        public static IVoteService GetVoteService()
        {
            var voteServiceMock = new Mock<IVoteService>();
            var voteRepo = TestObjectsFactory.GetVotesRepositiry(10);

            voteServiceMock.Setup(vs => vs.Add(It.Is<int>(d => d.Equals(2)), It.IsAny<string>(), It.IsAny<int>()))
                .Callback(
                () => voteRepo
                .Add(new RecipeVote()
                {
                    RecipeId = 2,
                    CreatedOn = DateTime.Now,
                    AuthorId = "1",
                    Type = VoteType.Positive
                }));

            voteServiceMock.Setup(vs => vs.Add(It.Is<int>(d => d.Equals(1)), It.IsAny<string>(), It.IsAny<int>()))
                .Callback(
                () => voteRepo
                .Add(new RecipeVote()
                {
                    RecipeId = 2,
                    CreatedOn = DateTime.Now,
                    AuthorId = "1",
                    Type = VoteType.Negativ
                }));

            voteServiceMock.Setup(vs => vs.GetTotalRecipeVotes(It.Is<int>(d => d.Equals(2))))
                .Returns(voteRepo.All().Where(v => v.RecipeId == 2).Sum(v => (int)v.Type));

            voteServiceMock.Setup(vs => vs.GetTotalRecipeVotes(It.Is<int>(d => d.Equals(1))))
                .Returns(voteRepo.All().Where(v => v.RecipeId == 2).Sum(v => (int)v.Type));

            return voteServiceMock.Object;
        }

        public static IImagesService GetRecipeImagesService()
        {
            var recipesImagesServiceMock = new Mock<IImagesService>();

            recipesImagesServiceMock.Setup(ri => ri.GetByRecipeId(It.IsAny<string>()))
                .Returns(TestObjectsFactory.GetRecipeImagesRepositiry(10).GetById(1));

            return recipesImagesServiceMock.Object;
        }

        public static IArticlesService GetArticlesService()
        {
            var articlesServiceMock = new Mock<IArticlesService>();

            articlesServiceMock.Setup(a => a.GetById(It.Is<int>(d => d.Equals(99))))
                .Returns((Article)null);

            articlesServiceMock.Setup(a => a.GetById(It.Is<int>(d => d.Equals(5))))
                .Returns(TestObjectsFactory.GetArticlesRepositiry(10).GetById(5));

            return articlesServiceMock.Object;
        }

        public static ITagsService GetTagsService()
        {
            var tagsServiceMock = new Mock<ITagsService>();
            var recipesRepoMock = TestObjectsFactory.GetRecipeRepositiry(10);

            tagsServiceMock.Setup(ts => ts.GetAll())
                .Returns(new List<Tag>()
                {
                    new Tag() { Text = "default" },
                    new Tag() { Text = "Tag 1" },
                    new Tag() { Text = "Tag 2" },
                    new Tag() { Text = "Tag 3" },
                    new Tag() { Text = "Tag 4" }
                }.AsQueryable());

            return tagsServiceMock.Object;
        }
    }
}
