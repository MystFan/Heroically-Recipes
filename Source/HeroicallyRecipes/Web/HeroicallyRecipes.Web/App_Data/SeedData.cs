namespace HeroicallyRecipes.Web.App_Data
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using HeroicallyRecipes.Data.Models;
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
                    NickName = emails[i],
                    PasswordHash = hasher.HashPassword(passwords[i]),
                    AvatarUrl = "/images/defaultAvatar1.png"
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
                    Name = categories[i],
                    CreatedOn = DateTime.UtcNow
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
                        Tags = new List<Tag> {new Tag() { Text = "health"}, new Tag() { Text = "vegetables" } }
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
                        Tags = new List<Tag> {new Tag() { Text = "chiken"}, new Tag() { Text = "vegetables" } }
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
                        Tags = new List<Tag> {new Tag() { Text = "garlic" }, new Tag() { Text = "mushrooms" } }
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
                        Tags = new List<Tag> {new Tag() { Text = "Berries" }, new Tag() { Text = "Quinoa" } }
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
                        Tags = new List<Tag> {new Tag() { Text = "potatoe" }, new Tag() { Text = "eggs" } }
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

        public static void SeedArticles(HeroicallyRecipesDbContext context)
        {
            if (!context.Articles.Any())
            {
                var firstUser = context.Users.FirstOrDefault(u => u.Email == "jhonDoe@site.com");
                var secondUser = context.Users.FirstOrDefault(u => u.Email == "batman@site.com");

                context.Articles.Add(new Article()
                {
                    Title = "How to Hot Pot at Home",
                    Content = @"Do you know the kind of dinner party I don’t like? The one where the host is cooking the entire time. A host that's in the kitchen is a host that's stressed out—and possibly stressing everybody else out—while simultaneously ignoring the people who have been invited into his or her home.
                            But what if everyone was cooking?
                            That was my thought when I first considered cooking traditional Chinese hot pot at home. The name says it all: diners gather around a giant pot of flavored broth and take turns dipping in raw ingredients. The broth cooks the ingredients, not unlike the oil in fondue. And just like fondue, hot pot is perfect for groups—and it's not an idea that's stuck in the ‘70s.
                            So here's what I did: I invited eleven friends to come over and gather around the hot pot. Then the hard part came. I had to figure out to pull hot pot at home off.
                            What the Heck Is Hot Pot?
                            The communal activity of gathering around a pot of simmering broth is common all over Asia. But just exactly what kind of broth is in the pot depends on where exactly in Asia you are. In Japan, where the dining ritual is called shabu shabu, the broth is kombu-based, like dashi. Meanwhile, Mongolian hot pot features goji berries and jujubes. And on mainland China, Szechuanese hot pot is packed with lip-numbing peppercorns, chili peppers, and spices. That's the hot pot I wanted at my party.
                            At restaurants specializing in hot pot, the experience goes like this: you order a broth and raw ingredients, the staff fires up a portable hot plate at the table, and once the broth starts simmering, you start cooking the ingredients yourself.
                            To bring hot pot to my home, I had to make a few changes. I couldn't keep the broth simmering on the stovetop, obviously, and I don't own a hot plate. That led me to the crock pot. If it can braise a pork shoulder, surely it can simmer a simple broth—right?
                            When I spoke to Sarah Leung, one of the four writers behind the acclaimed Chinese food blog Woks of Life, she approved my crock pot idea. She also gave me all kinds of other pointers for shopping, preparing the broth, and keeping things moving as smoothly as possible. My most important takeaway? “A hot pot experience is ultimately what you make of it.”
                            Well, I wanted to make it awesome. But first, I had some shopping to do.",
                    Author = firstUser,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Content = "Very good article dude!",
                            AuthorId = secondUser.Id
                        }
                    }
                });

                context.Articles.Add(new Article()
                {
                    Title = "What's the Best Way to Boil an Egg?",
                    Content = @"Putting it to the test
                            To boil eggs with a cold start, I took eggs straight out of the fridge, set them in a pot of cold water, brought them to a boil, then turned them down to the lowest possible setting on the stove. I pulled them out at varying times for soft, medium, and hard boiled (see chart below).
                            To boil eggs with a hot start, I brought a pot of water to a boil, lowered cold eggs straight from the fridge into the boiling water, immediately turned it down to a simmer. I pulled them out at varying times, for soft, medium, and hard boiled.
                            In both cases, I put the eggs straight into an ice bath after cooking, cracked them up a bit by tapping them against each other or the edge of the bowl, and let them sit for at least one minute before peeling.
                            The hot start wins
                            In each stage of doneness, I found that the eggs which started in boiling water were easier to peel. Above all else, this, for me, was the deal breaker. Boiled eggs that are hard to peel are the worst. The ice bath and the cracking of the shell to let a bit of water sneak in around the egg are key parts of the process—our food director Rhoda can peel a whole dozen eggs in 104 seconds using this technique.
                            How long you simmer your eggs after placing them in boiling water all depends on how soft or hard you want them. Me, I like a yolk that's quite runny, while Rhoda likes hers just barely set but still soft and glossy, and Kat likes hers even firmer, with just a bit of golden goo.
                            Once you know the time for your personal perfect style of boiled eggs, you can get the same result using any size pot in any kitchen with any number of eggs in it, as long as you bring water to a boil, lower cold eggs straight into the water, turn it down to a simmer, and start a timer as soon as your eggs are in the hot water.
                            Know your times:
                            The good news? The hot start method works however you like your eggs. Refer to the table below and experiment to find your favorite.
                                4 minutes: eat-it-with-a-spoon-out-of-the-shell soft
                                5 minutes: firm white, runny yolk (my favorite)
                                6 minutes: nice and gooey yolk, starting to set (Rhoda's favorite)
                                8 minutes: fully set yolk, but still sort of gooey and golden
                                10 minutes: firmer pale yolk, a bit soft in the middle (Kat's favorite)
                                12 minutes: almost completely hard-boiled yolk, with a touch of golden goo still in the middle
                                14 minutes: completely hard-boiled crumbly dry pale yolk
                            Don't forget to shock the eggs in ice water after your desired time, and you're good to go!",
                    Author = firstUser,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Content = "Very good article dude!",
                            AuthorId = secondUser.Id
                        }
                    }
                });

                context.Articles.Add(new Article()
                {
                    Title = "In Praise of Brown Food",
                    Content = @"The photo above isn't the sexiest plate of food, I'll admit. But before you start shaking your head at that monochrome, unphotogenic mass, just imagine how this plate of braised beef would taste on a chilly February day. That rich, brown sauce is telling you something. That color is the direct result of hours of slow simmering, caramelized onions, maybe a splash of wine. It promises proverbial rib-sticking richness, and depth of flavor that was earned honestly, from careful cooking.
                            Related
                            Cook 90 Hero / Photos by David Tamarkin
                            The Guilt of Simple Cooking (and How to Get Over It)
                            View article
                            No, you can't Instagram it. Or, rather, you could, but you won't. These days, brown food doesn't get any respect, let alone screen time.
                            Picture it: You're at cool new restaurant, trying to decide between the slow-simmered braised veal and a trendy tumble of rainbow chard, topped with a sunny fried egg and a scattering of black sesame seeds. Which one will you order? Does a little part of you lean towards that multi-colored egg dish, just because of how pretty a picture it'd make?
                            Lookism is rampant in the world of food these days; it's hard to find an all-brown entree on a menu at all. It's even triggered the creation of dishes that have no other reason to exist. Case in point: Smoothie bowls. There's only one reason these cold fruit purees get poured into bowls and topped with perfect rows of chia seeds, cacao nibs, and hemp hearts. And it's not because it tastes any different than if you blended the ingredients together and called it a day.
                            Now, I'm not discounting the role of the visual. First you eat with your eyes is a cliche because it's true. I'm just defending the right for food to look unpretty, too. Especially when you cook it yourself at home. It's hard not to feel self-conscious about sharing the #homefood we cook when no one but our family is looking. A few weeks ago, I made one of the ugliest soups I've ever seen: An army green puree of bone broth, rutabega, and turnip. I didn't have the time or the patience to dress it up with some chopped parsley or a drizzle of herb oil. And you know what? It tasted damned delicious without it.
                            Most brown foods do.And winter is the ideal time to fully embrace the joys of brown food. Cook up a pot of bone broth, but don't just sip it. Use it to make a rib-sticking, umami-packed stew. Then share it on social without shame. Serve it to friends without the merest whisper of parsley. And watch as the room gets quiet as everyone bends over their bowls. Ask yourself: What can brown do for you?",
                    Author = secondUser,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Content = "Very good article dude!",
                            AuthorId = firstUser.Id
                        }
                    }
                });

                context.Articles.Add(new Article()
                {
                    Title = "How to Turn Your Favorite Foods Vegetarian",
                    Content = @"There are lots of logical reasons to eat vegetarian, and there are also lots of evolutionary reasons why we humans crave meat. However, there are creative and tasty ways to staunch your meat cravings and get the protein your body needs without actually eating meat. For those who have recently turned to vegetarianism, or have been vegetarian for a while and are looking for new cooking ideas, here is a run-down of six tasty and easy to prepare meat substitutes that will help turn your favorite foods vegetarian.
                            Meat Substitute #1 - Jackfruit
                            Jackfruit
                            This amazing and relatively unknown fruit from India is high in protein, potassium and vitamin B, making it not only a convincing doppelganger for meat, but providing some of the same nutritional value.
                            How to use it: Pulled pork has been a hot trend in the professional culinary scene for a few years now because, well, it tastes amazing. Vegetarians can get in on the action (without clogging their arteries) by using jackfruit as a substitute in pulled pork dishes.
                            How to prepare it: The most important part of the preparation is finding green jackfruit. It is often sold in cans, a much better option than lugging home the giant, bulbous fruit itself. Go for the jackfruit in water or brine, not syrup.
                                Once you have some green jackfruit rinsed and cut into bite sized pieces, season it with barbecue spices.
                                Saute some onion and jalapeños if you like it spicy, and add the jackfruit to the pan.
                                Add about a cup of vegetable broth, cover, and simmer until the liquid has been absorbed.
                                Remove the jackfruit from the saute pan and spread on a baking sheet, breaking up the fibers with a spatula so that it resembles pulled pork.
                                Bake in a 350-degree oven for about 20 minutes.
                                Remove and toss with vegan barbecue sauce.
                                Add it to a bun with a slaw of your choice and BAM! You've got yourself some vegetarian pulled jackfruit.
                            Where to find it: Asian or Caribbean stores, and some large supermarkets.
                            Meat Substitute #2 - Lentils
                            Lentils
                            Lentils are part of the legume family, which also includes beans and peas. Legumes often mimic meat in their protein levels, texture and tastiness. Lentils, in particular, are a great sub-in for dishes that call for minced meat, and are incredibly low in fat yet high in fiber, iron and protein.
                            How to use it: Lentil burgers (grilled or pan-fried) make a quick, easy and nutritious dinner for the conscious diner.
                            How to prepare it: There are a few different ways to make a veggie burger with legumes, but here's our favorite:
                                Cook lentils in vegetable broth, with 2 cups broth to every cup of lentils.
                                Stir fry some onion and spinach and season with cumin, salt and pepper.
                                Add to the lentils along with about a cup of breadcrumbs and an egg.
                                For a gluten free option, use cornmeal instead of breadcrumbs.
                                If you are going vegan, you can skip the egg, which just helps to bind the mixture a bit better.
                                Let the mixture cool and then form into patties.
                            Where to find it: Lentils are a common staple and found in most grocery stores.
                            Meat Substitute #3 - Marinated Mushrooms
                            Marinated Mushrooms
                            Mushrooms have a meat-like texture when cooked and take on a lovely umami flavor when marinated in soy sauce and rice wine vinegar. They are packed with vitamin D, fiber, potassium, and selenium, a mineral rarely found in fruits and vegetables, but which is essential to healthy liver function. Shiitake mushrooms, in particular, are known for their meaty texture and savory flavor.
                            How to use it: Next time you need to put a little pizazz in your salad, try adding these marinated mushrooms. They are a great stand-in for chicken or other forms of protein typically found in a Cobb, Caesar, or Asian chicken salad.
                            How to prepare it: Mushrooms can be marinated in any combination of oil, vinegar, herbs and spices. Here's our suggestion for Asian-style mushrooms, which use soy sauce, rice wine vinegar, sesame oil, minced garlic and salt.
                                About 2 lbs. of mushrooms will take about a cup of rice wine, 4 tablespoons soy sauce, 2 tablespoons oil and 3 cloves garlic.
                                Mix the marinade first and then add to a container with the mushrooms.
                                The mushrooms can be sliced or, if they are small enough, put whole into the container.
                                It is best to let these marinate over night. Due to the vinegar, these can be stored in the refrigerator for about two weeks (if they don't get gobbled up first!)
                            Where to find it:Though button and crimini mushrooms are easily found, have fun experimenting with different types of mushrooms found in Asian supermarkets and health food stores. Try chaneterelles (known for their golden color) or porcini mushrooms, the smaller cousin to the portabello.",
                    Author = secondUser,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Content = "Very good article dude!",
                            AuthorId = firstUser.Id
                        }
                    }
                });

                context.Articles.Add(new Article()
                {
                    Title = "How to Turn Your Favorite Foods Vegetarian",
                    Content = @"As the green movement enters the mainstream food industry, more customers are looking for choices that reflect higher levels of quality in their food. But for many diners, it's no longer enough to eat ingredients that are local, certified organic, and free from genetic modification, hormones, chemicals and antibiotics.
                            Culinary Arts
                            Culinary Arts (AS)
                            Other Programs:
                                Culinary Arts (D)
                                Baking & Pastry (AS)
                                More...
                            The Art Institutes system of schools
                            Visit www.artinstitutes.edu
                            Matching School Ads
                            Now customers want meat, poultry, eggs, dairy and fish from animals that have been treated humanely--and not just for ethical reasons. They're beginning to link food safety, nutrition and flavor to animal welfare practices. In fact, a 2007 survey conducted for the American Humane Certified (AHC) found that 58 percent of consumers felt the labeling of animal products as humane was more important than organic or natural. But is the food industry ready to meet these standards?
                            Chefs Are On Board
                            The concept of cooking with humane food is not new to professional chefs. In 2007, Wolfgang Puck promised to serve only humane animal products in his restaurants. Some chefs say they prefer humanely raised meat because the animals haven't been sick or stressed during their lives, which can toughen the flesh and degrade the taste. Meat producers are also interested in potential benefits of raising humanely treated animals: a growing market and long-term productivity of the animals.
                            Price can be a concern for both chefs and producers of humane food, since it costs more to keep animals happy and healthy or to provide the space necessary for them to range freely. Some critics say that because consumers have gotten used to the low cost and poor quality of factory meat, convincing them to pay higher prices will be an uphill battle. But proponents of humane food have faith that as consumers make the connection between animal health and human health, they will be willing to pay higher prices. They also argue that as more people demand humane food, the market will adjust and prices will become more competitive.
                            Standards for Humane Food
                            To meet the need for rating and labeling humane food, several independent organizations have emerged. Created by the American Humane Association, the American Humane Certified (AHC) program is a farm animal welfare standards program that provides producers and consumers with independent verification of the ethical treatment of animals.
                            Similarly, Animal Welfare Approved (AWA) is a five-year-old division of the Animal Welfare Institute that certifies and promotes family farms. The Global Animal Partnership is an international group that also performs third-party audits and certifications for farms.
                            These rating systems are based on points for meeting certain criteria for raising animals humanely, including:
                                Cage free
                                No crowding
                                Entire life on one farm,
                                Fed off of mother
                            Similar to the process the food industry went through to create a reliable standard for organic products, there are still some bumps in the road when it comes to creating an industry-wide animal welfare standard that includes all species, provides a way for retailers to easily understand the rating system, and makes it simple for consumers to identify the labels.
                            Cruelty-Free Recipes Support the Cause
                            Chef Alex Seidel of Fruition restaurant in Denver--one of Food and Wine magazine's Top 10 Best New Chefs in 2010--recently served a holiday meal consisting of recipes made solely with ingredients from AHC producers to promote this new standard of animal welfare. Joining the trend, Whole Foods is one of three large food corporations that has agreed to raise their animal welfare standards.
                            Most chefs agree that what they serve has to taste good and be reasonably priced, but with the demand from customers and the support of animal welfare organizations, more and more are willing to take the risk of providing humanely treated products knowing it's better for animals, farms, customers and communities.",
                    Author = secondUser,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Content = "Very good article dude!",
                            AuthorId = firstUser.Id
                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
