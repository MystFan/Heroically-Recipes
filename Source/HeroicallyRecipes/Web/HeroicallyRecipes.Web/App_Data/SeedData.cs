namespace HeroicallyRecipes.Web.App_Data
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using HeroicallyRecipes.Models;
    using System.IO;
    using System.Collections.Generic;
    using System.Web;
    using Data;

    public class SeedData
    {
        public static void SeedUsers(HeroicallyRecipesDbContext context)
        {
            if (context.Users.Count() > 1)
            {
                return;
            }

            string[] usernames = new string[] { "jhonDoe", "batman" };
            string[] passwords = new string[] { "jhonDoe123", "batman123" };
            string[] emails = new string[] { "jhonDoe@site.com", "batman@site.com" };

            PasswordHasher hasher = new PasswordHasher();
            var UserManager = new UserManager<User>(new UserStore<User>(context));

            for (int i = 0; i < usernames.Length; i++)
            {
                var user = new User
                {
                    UserName = emails[i],
                    Email = emails[i],
                    PasswordHash = hasher.HashPassword(passwords[i]),
                };

                context.Users.Add(user);
                context.SaveChanges();
                UserManager.UpdateSecurityStamp(user.Id);
            }
        }

        public static void SeedCategories(HeroicallyRecipesDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            string[] categories = new string[]
            { "Healthy", "Quick and Easy", "Vegetarian", "Soups", "Salads", "Desserts", "Dinner", "Lunch", "Breakfast" };

            for (int i = 0; i < categories.Length; i++)
            {
                var category = new Category
                {
                    Name = categories[i]
                };

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        public static void SeedRecipes(HeroicallyRecipesDbContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            string folderPath = HttpContext.Current.Server.MapPath("~/App_Data/Images");
            string[] filePaths = Directory.GetFiles(folderPath);

            Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>()
            {
                {"Tandoori Carrots",
                    new Recipe()
                    {
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Healthy"),
                        CreatedOn = DateTime.Now,
                        UserId = context.Users.FirstOrDefault(u => u.Email == "jhonDoe@site.com").Id,
                        Preparation = @"Preheat oven to 425°. Mix vadouvan, half of garlic, 1/4 cup yogurt, and 3 tablespoons oil in a large bowl until smooth; season with salt and pepper. Add carrots and toss to coat. Roast on a rimmed baking sheet in a single layer, turning occasionally, until tender and lightly charred in spots, 25-30 minutes.
                                        Meanwhile, heat turmeric and remaining 2 tablespoons oil in a small skillet over medium-low, swirling skillet, until fragrant, about 2 minutes. Remove from heat.
                                        Whisk lemon juice, remaining garlic, and remaining . cup yogurt in a small bowl; season with salt and pepper.
                                        Place carrots (along with crunchy bits on baking sheet) on a platter.Drizzle with yogurt mixture and turmeric oil and top with cilantro.Serve with lemon wedges. ",
                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient() {Text = "2 tablespoons vadouvan" },
                            new Ingredient() {Text = "2 garlic cloves, finely grated, divided" },
                            new Ingredient() {Text = "1/2 cup plain whole-milk Greek yogurt, divided" },
                            new Ingredient() {Text = "5 tablespoons olive oil, divided" },
                            new Ingredient() {Text = "Kosher salt, freshly ground pepper" },
                            new Ingredient() {Text = "1 pound small carrots, tops trimmed, scrubbed" },
                            new Ingredient() {Text = "1/4 teaspoon ground turmeric" },
                            new Ingredient() {Text = "2 tablespoons fresh lemon juice" },
                            new Ingredient() {Text = "Very coarsely chopped cilantro leaves with tender stems and lemon wedges (for serving)" },

                        },
                        Ratings = new List<Rating> { new Rating() { Value = 5}, new Rating() { Value = 7} },
                        Tags = new List<Tag> {new Tag() { Text = "#health"}, new Tag() { Text = "#vegetables" } }
                    }
                },
                {"Sheetpandinners Chicken",
                    new Recipe()
                    {
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Quick and Easy"),
                        CreatedOn = DateTime.Now,
                        UserId = context.Users.FirstOrDefault(u => u.Email == "batman@site.com").Id,
                        Preparation = @"Position rack in upper third of oven and preheat to 425°F. Mix brown sugar, cumin, salt, pepper, and cayenne in a small bowl. Toss squash, fennel, and grapes with oil and half of spice mixture on rimmed baking sheet and arrange in a single layer.
                                        Rub chicken thighs with remaining spice mixture and arrange, skin side up, on top of fruit and vegetables. Roast until skin is browned and an instant-read thermometer inserted into the thickest part of chicken registers 165°F, about 35 minutes; if chicken skin or vegetables start to burn, move pan to a lower rack to finish cooking.
                                        Divide chicken, fruit, and vegetables among 4 plates and top with mint. ",
                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient() {Text = "1 tablespoon brown sugar" },
                            new Ingredient() {Text = "1 tablespoon ground cumin" },
                            new Ingredient() {Text = "1 tablespoon kosher salt" },
                            new Ingredient() {Text = "1 tablespoon freshly ground black pepper" },
                            new Ingredient() {Text = "1/4 teaspoon cayenne pepper" },
                            new Ingredient() {Text = "1 acorn or delicata squash (about 1 1/2 pounds), halved lengthwise, seeded, cut into 1/4\" ,half moons" },
                            new Ingredient() {Text = "1 fennel bulb (about 1/2 pound), cut in half lengthwise, sliced into 1/4\" wedges with core intact" },
                            new Ingredient() {Text = "1/2 pound seedless red grapes (about 1 cup)" },
                            new Ingredient() {Text = "1 tablespoon olive oil" },
                            new Ingredient() {Text = "8 skin-on, bone-in chicken thighs (about 2 pounds)" },
                            new Ingredient() {Text = "1/4 cup torn fresh mint leaves" }
                        },
                        Ratings = new List<Rating> { new Rating() { Value = 5}, new Rating() { Value = 7} },
                        Tags = new List<Tag> {new Tag() { Text = "#chiken"}, new Tag() { Text = "#vegetables" } }
                    }
                },
                {"Salad with butter and basted mushrooms",
                    new Recipe()
                    {
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Vegetarian"),
                        CreatedOn = DateTime.Now,
                        UserId = context.Users.FirstOrDefault(u => u.Email == "batman@site.com").Id,
                        Preparation = @"Cook barley in a medium pot of boiling salted water until tender, 50–60 minutes for hulled or hull-less, 20–30 minutes for pearl. Drain; spread out on a baking sheet and let cool.
                                        Meanwhile, cook shallots in vegetable oil in a small saucepan over medium-high heat, swirling pan occasionally to keep shallots from burning, until golden brown, 5–7 minutes. Using a slotted spoon, transfer shallots to paper towels to drain; season with salt. Let cool. Set shallot cooking oil aside.
                                        Heat olive oil in a large skillet over medium-high until just beginning to smoke. Arrange mushrooms in skillet in a single layer and cook, undisturbed, until undersides are golden brown, about 3 minutes. Season mushrooms with salt and pepper, toss, and continue to cook, tossing often and reducing heat as needed to avoid scorching, until golden brown all over, about 5 minutes longer.
                                        Reduce heat to medium and add thyme sprigs, garlic, and butter to skillet. Tip skillet toward you so butter pools at edge and use a spoon to baste mushrooms with foaming butter; cook until butter smells nutty. Using a slotted spoon, transfer mushrooms to a small bowl, leaving thyme and garlic behind.
                                        Toss cooled barley, cilantro, parsley, lemon juice, 1 1/2 oz. Parmesan, and 2 Tbsp. reserved shallot oil in a large bowl to combine; season with salt and pepper. Add mushrooms; toss again to combine.
                                        Just before serving, top with fried shallots and more shaved Parmesan. 
                                        Do ahead
                                        Barley can be cooked 1 day ahead. Let cool; store airtight and chill. Dish can be made 3 hours ahead; store tightly wrapped at room temperature. ",
                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient() {Text = "1 cup hulled, hull-less, or pearl barley" },
                            new Ingredient() {Text = "Kosher salt" },
                            new Ingredient() {Text = "2 shallots, thinly sliced into rings" },
                            new Ingredient() {Text = "1/3 cup vegetable oil" },
                            new Ingredient() {Text = "2 tablespoons olive oil" },
                            new Ingredient() {Text = "8 oz. mushrooms (such as maitake, chanterelle, and/or oyster), torn or cut into large pieces" },
                            new Ingredient() {Text = "Freshly ground black pepper" },
                            new Ingredient() {Text = "2 sprigs thyme" },
                            new Ingredient() {Text = "1 clove garlic, crushed" },
                            new Ingredient() {Text = "3 tablespoons unsalted butter" },
                            new Ingredient() {Text = "1 cup chopped fresh cilantro" },
                            new Ingredient() {Text = "1 cup chopped fresh parsley" },
                            new Ingredient() {Text = "12 tablespoons fresh lemon juice" },
                            new Ingredient() {Text = "1 1/2 ounces Parmesan, shaved, plus more for serving" }
                        },
                        Ratings = new List<Rating> { new Rating() { Value = 3}, new Rating() { Value = 4} },
                        Tags = new List<Tag> {new Tag() { Text = "#garlic" }, new Tag() { Text = "#mushrooms" } }
                    }
                },
                {"Breakfast bowl with Quinoa and Berries",
                    new Recipe()
                    {
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Breakfast"),
                        CreatedOn = DateTime.Now,
                        UserId = context.Users.FirstOrDefault(u => u.Email == "jhonDoe@site.com").Id,
                        Preparation = @"Divide the berries equally among four bowls. Place the remaining ingredients in another bowl, and toss to combine. Sprinkle the mixture over each of the four bowls and serve. 
                                        Tip:
                                        Add 1 1/2 heaping tablespoons of fat-free Greek yogurt to this dish for more protein.",
                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient() {Text = "4 cups mixed berries (raspberries, strawberries, blueberries, blackberries)" },
                            new Ingredient() {Text = "2 tablespoons hemp hearts (available in the natural section of most supermarkets in a variety of brands)" },
                            new Ingredient() {Text = "20 whole almonds, toasted and chopped" },
                            new Ingredient() {Text = "1/4 cup cooked quinoa" },
                        },
                        Ratings = new List<Rating> { new Rating() { Value = 7}, new Rating() { Value = 9} },
                        Tags = new List<Tag> {new Tag() { Text = "#Berries" }, new Tag() { Text = "#Quinoa" } }
                    }
                },
                {"Crispy potato-leek kugel",
                    new Recipe()
                    {
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Dinner"),
                        CreatedOn = DateTime.Now,
                        UserId = context.Users.FirstOrDefault(u => u.Email == "jhonDoe@site.com").Id,
                        Preparation = "Preheat oven to 375°. Cut 4 potatoes into 1\" chunks and place in a medium pot. Cover with cold water by 1\". Season water generously with salt, bring to a boil over medium-high heat, and cook until potatoes are tender, 10-12 minutes. Drain well, transfer to a large bowl, and mash with a potato masher; set aside." +
                                        "Meanwhile, heat 2 Tbsp. oil in a large skillet over medium until shimmering. Add leeks, 1/4 tsp. salt, and 1/4 tsp. pepper and cook, stirring frequently, until softened and golden, 5-8 minutes. Add garlic and cook until fragrant, 1-2 minutes more. Remove pan from heat and let cool slightly." +
                                        "Grease bottom and sides of an 8x8\" baking pan with 2 Tbsp. oil. Place pan in oven for 10 minutes." +
                                        "Meanwhile, grate 3 potatoes and onion using the large holes of a box grater or a food processor fit with a shredding blade. Wrap potatoes and onion in a clean dishtowel or several layers of paper towels and squeeze out as much liquid as you can; add to the bowl with the mashed potatoes. Stir in sautéed leeks and garlic, eggs, 2 Tbsp.oil, 1 Tbsp.thyme, 2 tsp.salt, and 1 / 4 tsp.pepper; mix until well combined." +
                                        "Thinly slice remaining 2 potatoes and toss with remaining 1 Tbsp.oil, 1 tsp.thyme, 1 / 4 tsp.salt, and 1 / 4 tsp.pepper in a medium bowl; set aside." +
                                        "Carefully remove preheated pan from oven and transfer potato - onion mixture to the pan (it should sizzle when it hits the hot oil). Smooth top with a spatula.Layer potato slices over the top in a shingled fashion.Bake until golden brown and cooked through, 60 - 75 minutes.Heat broiler; broil kugel until crispy crust forms, 1 - 2 minutes, watching carefully so it does not burn.Let cool briefly, then cut into squares to serve.",
                        Ingredients = new List<Ingredient>()
                        {
                            new Ingredient() {Text = "9 medium russet potatoes (about 4 1/2 pounds), peeled" },
                            new Ingredient() {Text = "7 tablespoons vegetable oil, divided" },
                            new Ingredient() {Text = "3 medium leeks, white and pale-green parts only, thinly sliced crosswise" },
                            new Ingredient() {Text = "2 1/2 teaspoons kosher salt, divided, plus more" },
                            new Ingredient() {Text = "3/4 teaspoon freshly ground black pepper, divided" },
                            new Ingredient() {Text = "2 garlic cloves, finely chopped" },
                            new Ingredient() {Text = "1 small onion" },
                            new Ingredient() {Text = "4 large eggs, lightly beaten" },
                            new Ingredient() {Text = "1 tablespoon plus 1 teaspoon fresh thyme leaves, divided" },
                        },
                        Ratings = new List<Rating> { new Rating() { Value = 5}, new Rating() { Value = 9} },
                        Tags = new List<Tag> {new Tag() { Text = "#potatoe" }, new Tag() { Text = "#eggs" } }
                    }
                }
            };

            foreach (var path in filePaths)
            {
                string title = Path.GetFileNameWithoutExtension(path);
                var recipe = recipes[title];
                recipe.Title = title;
                recipe.Images.Add(new RecipeImage
                {
                    OriginalName = title,
                    Extension = Path.GetExtension(path),
                    Content = File.ReadAllBytes(path)
                });

                context.Recipes.Add(recipe);
            }

            context.SaveChanges();
        }
    }
}
