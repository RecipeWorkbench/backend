using Microsoft.Extensions.Configuration;
using RnD.Database.SQLite;
using RnD.Workbench.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utilities.Csv;

namespace Utilities.TrainDataImporter
{
    class Program
    {
        public static IConfigurationRoot Configuration
        {
            get; set;
        }

        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            // Create the configuration object that the application will
            // use to retrieve configuration information.
            Configuration = builder.Build();

            BuildInitialDatabase();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void BuildInitialDatabase()
        {
            var connectionStringFile = Configuration["connection-string"];
            var ingredientsFile = Configuration["ingredients-file"];
            var compoundsFile = Configuration["compounds-file"];
            var ingredientsCompoundsFile = Configuration["ingredients-compounds-file"];
            var countryRegionFile = Configuration["country-region-file"];

            var recipesFiles = new List<string>();
            Configuration.GetSection("recipes-files").Bind(recipesFiles);

            Console.WriteLine("APPLICATION ARGUMENTS:");
            Console.WriteLine($"'connection-string' is '{connectionStringFile}'");
            Console.WriteLine($"'ingredients-file' is '{ingredientsFile}'");
            Console.WriteLine($"'compounds-file' is '{compoundsFile}'");
            Console.WriteLine($"'ingredients-compounds-file' is '{ingredientsCompoundsFile}'");
            Console.WriteLine($"'country-region-file' is '{countryRegionFile}'");
            Console.WriteLine();
            foreach (var recipeFile in recipesFiles)
            {
                Console.WriteLine($"'recipe-file' is '{recipeFile}'");
            }

            using (var context = new FlavorNetworkContext())
            {
                //ReadCuisineRegions(countryRegionFile, context);
                //ReadFromFile(countryRegionFile);

                //ReadCompounds(compoundsFile, context);
                //ReadFromFile(compoundsFile);

                //ReadIngredientCategory(ingredientsFile, context);
                //ReadFromFile(ingredientsFile);

                //ReadIngredientCompounds(ingredientsCompoundsFile, context);
                //ReadFromFile(ingredientsCompoundsFile);

                //context.SaveChanges();

                //WriteCompounds(context);
                //WriteContributionMethods(context);
                //WriteCuisines(context);
                //WriteIngredients(context);
                //WriteIngredientCategories(context);
                //WriteIngredientCompounds(context);
                //WriteIngredientContributions(context);
                //WriteRecipes(context);
                //WriteRecipeIngredients(context);
                //WriteRegions(context);
            }

            int recipeId = 1;
            foreach (var recipeFile in recipesFiles)
            {
                recipeId = ReadRecipes(recipeFile, recipeId);
                //ReadFromFile(recipeFile);
            }

            Console.WriteLine($"The last inserted ID: '{recipeId}'.");
        }

        private static void ReadFromFile(string filename)
        {
            Console.WriteLine($"Content of the file '{filename}':");
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(File.OpenRead(filename)))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private static void ReadCuisineRegions(string filename, FlavorNetworkContext context)
        {
            var countryRegionCsvSchema = new CsvSchema();
            countryRegionCsvSchema.Properties.Add(new SchemaProperty("Cuisine"));
            countryRegionCsvSchema.Properties.Add(new SchemaProperty("Region"));

            var countryRegionCsvReader = new CsvReader<CuisineRegion>();
            countryRegionCsvReader.CsvDelimiter = "\t";
            countryRegionCsvReader.UseSingleLineHeaderInCsv = false;
            countryRegionCsvReader.Schema = countryRegionCsvSchema;

            var list = countryRegionCsvReader.Read(File.OpenRead(filename))
                                             .Select(w => NormalizeCuisine(w))
                                             .Distinct(new CuisineEqualityComparer())
                                             .OrderBy(w => w.Cuisine);

            /*foreach (var item in list)
            {
                Console.WriteLine($"C: '{item.Cuisine}', R: '{item.Region}'");
            }*/
            int id = 1;

            foreach (var item in list.Distinct(new RegionEqualityComparer()))
            {
                var region = new Region();
                region.Id = id;
                region.Name = item.Region;
                region.Cuisines = new List<Cuisine>();
                context.Regions.Add(region);

                id++;
            }

            id = 1;

            foreach (var item in list)
            {
                var cuisine = new Cuisine();
                cuisine.Id = id;
                cuisine.Name = item.Cuisine;
                cuisine.IngredientContributions = new List<IngredientContribution>();
                cuisine.Recipes = new List<Recipe>();

                var region = context.Regions.Local.SingleOrDefault(r => r.Name.Equals(item.Region));

                if (region != null)
                {
                    cuisine.Region = region;
                    cuisine.RegionId = region.Id;
                    region.Cuisines.Add(cuisine);
                    context.Cuisines.Add(cuisine);

                    id++;
                }
                else
                {
                    Console.WriteLine($"Region '{item.Region}' not found!!!");
                }
            }
        }

        private class CuisineEqualityComparer : IEqualityComparer<CuisineRegion>
        {
            public bool Equals(CuisineRegion x, CuisineRegion y)
            {
                return x.Cuisine.Equals(y.Cuisine);
            }

            public int GetHashCode(CuisineRegion obj)
            {
                return obj.Cuisine.GetHashCode();
            }
        }

        private class RegionEqualityComparer : IEqualityComparer<CuisineRegion>
        {
            public bool Equals(CuisineRegion x, CuisineRegion y)
            {
                return x.Region.Equals(y.Region);
            }

            public int GetHashCode(CuisineRegion obj)
            {
                return obj.Region.GetHashCode();
            }
        }

        private class CategoryEqualityComparer : IEqualityComparer<IngredientAndCategory>
        {
            public bool Equals(IngredientAndCategory x, IngredientAndCategory y)
            {
                return x.Category.Equals(y.Category);
            }

            public int GetHashCode(IngredientAndCategory obj)
            {
                return obj.Category.GetHashCode();
            }
        }

        private static CuisineRegion NormalizeCuisine(CuisineRegion c)
        {
            c.Cuisine = GetRegionName(c.Cuisine);

            if (string.IsNullOrEmpty(c.Region))
            {
                c.Region = c.Cuisine;
            }

            return c;
        }

        private static string GetRegionName(string region)
        {
            var values = region.Split("_".ToCharArray()).Select(w => w.Substring(0,1).ToUpperInvariant() + w.Substring(1));
            var value = string.Join(" ", values);

            switch (value)
            {
                case "Mexico":
                    return "Mexican";
                case "Japan":
                    return "Japanese";
                case "Italy":
                    return "Italian";
                case "Germany":
                    return "German";
                case "Vietnam":
                    return "Vietnamese";
                case "Korea":
                    return "Korean";
                case "China":
                    return "Chinese";
                case "India":
                    return "Indian";
                case "Israel":
                    return "Jewish";
                case "Thailand":
                    return "Thai";
                case "Scandinavia":
                    return "Scandinavian";
                case "France":
                    return "French";
            }

            return value;
        }

        private static string GetIngredientName(string name)
        {
            var values = name.Split("_".ToCharArray());
            var value = string.Join(" ", values);

            return value;
        }

        private static void ReadIngredientCategory(string filename, FlavorNetworkContext context)
        {
            var ingredientCategoryCsvSchema = new CsvSchema();
            ingredientCategoryCsvSchema.Properties.Add(new SchemaProperty("Id"));
            ingredientCategoryCsvSchema.Properties.Add(new SchemaProperty("Name"));
            ingredientCategoryCsvSchema.Properties.Add(new SchemaProperty("Category"));

            var ingredientCategoryCsvReader = new CsvReader<IngredientAndCategory>();
            ingredientCategoryCsvReader.CsvDelimiter = "\t";
            ingredientCategoryCsvReader.UseSingleLineHeaderInCsv = true;
            ingredientCategoryCsvReader.Schema = ingredientCategoryCsvSchema;

            var list = ingredientCategoryCsvReader.Read(File.OpenRead(filename));

            int id = 1;

            foreach (var item in list.Distinct(new CategoryEqualityComparer()))
            {
                var category = new IngredientCategory();
                category.Id = id;
                category.Name = item.Category;
                category.Ingredients = new List<Ingredient>();

                context.IngredientCategories.Add(category);

                id++;
            }

            foreach (var item in list)
            {
                item.Id = item.Id + 1;

                var ingredient = new Ingredient();
                ingredient.Id = item.Id;
                ingredient.Name = GetIngredientName(item.Name);
                ingredient.IngredientCompounds = new List<IngredientCompound>();
                ingredient.IngredientContributions = new List<IngredientContribution>();
                ingredient.RecipeIngredients = new List<RecipeIngredient>();

                var category = context.IngredientCategories.Local.SingleOrDefault(r => r.Name.Equals(item.Category));

                if (category != null)
                {
                    ingredient.IngredientCategory = category;
                    ingredient.IngredientCategoryId = category.Id;
                    category.Ingredients.Add(ingredient);
                    context.Ingredients.Add(ingredient);
                }
                else
                {
                    Console.WriteLine($"Category '{item.Category}' not found!!!");
                }

                //Console.WriteLine($"Id: '{item.Id}', N: '{item.Name}', C: '{item.Category}'");
            }
        }

        private static void ReadCompounds(string filename, FlavorNetworkContext context)
        {
            var compoundsCsvSchema = new CsvSchema();
            compoundsCsvSchema.Properties.Add(new SchemaProperty("Id"));
            compoundsCsvSchema.Properties.Add(new SchemaProperty("Name"));
            compoundsCsvSchema.Properties.Add(new SchemaProperty("CasNumber"));

            var compoundsCsvReader = new CsvReader<Compound>();
            compoundsCsvReader.CsvDelimiter = "\t";
            compoundsCsvReader.UseSingleLineHeaderInCsv = true;
            compoundsCsvReader.Schema = compoundsCsvSchema;

            var list = compoundsCsvReader.Read(File.OpenRead(filename));

            foreach (var item in list)
            {
                item.Id = item.Id + 1;
                item.CompoundFlavors = new List<CompoundFlavor>();
                item.IngredientCompounds = new List<IngredientCompound>();
                context.Compounds.Add(item);

                //Console.WriteLine($"ID: '{item.Id}', Name: '{item.Name}', CAS: '{item.CasNumber}'");
            }
        }

        private static void ReadIngredientCompounds(string filename, FlavorNetworkContext context)
        {
            var compoundsCsvSchema = new CsvSchema();
            compoundsCsvSchema.Properties.Add(new SchemaProperty("Ingredient"));
            compoundsCsvSchema.Properties.Add(new SchemaProperty("Compound"));

            var compoundsCsvReader = new CsvReader<IngredientAndCompound>();
            compoundsCsvReader.CsvDelimiter = "\t";
            compoundsCsvReader.UseSingleLineHeaderInCsv = true;
            compoundsCsvReader.Schema = compoundsCsvSchema;

            var list = compoundsCsvReader.Read(File.OpenRead(filename));

            foreach (var item in list)
            {
                item.Ingredient = item.Ingredient + 1;
                item.Compound = item.Compound + 1;

                var ingredient = context.Ingredients.Local.SingleOrDefault(i => i.Id == item.Ingredient);
                var compound = context.Compounds.Local.SingleOrDefault(c => c.Id == item.Compound);

                if (ingredient != null)
                {
                    if (compound != null)
                    {
                        var ingredientCompound = new IngredientCompound();
                        ingredientCompound.Ingredient = ingredient;
                        ingredientCompound.IngredientId = ingredient.Id;
                        ingredientCompound.Compound = compound;
                        ingredientCompound.CompoundId = compound.Id;

                        ingredient.IngredientCompounds.Add(ingredientCompound);
                        compound.IngredientCompounds.Add(ingredientCompound);
                        context.IngredientCompounds.Add(ingredientCompound);
                    }
                    else
                    {
                        Console.WriteLine($"Compound with id '{item.Compound}' not found!!!");
                    }
                }
                else
                {
                    Console.WriteLine($"Ingredient with id '{item.Ingredient}' not found!!!");
                }
            }
        }

        private static int ReadRecipes(string filename, int startId)
        {
            int id = startId;
            int lineNumber = 0;
            var context = new FlavorNetworkContext();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader reader = new StreamReader(File.OpenRead(filename)))
                {
                    while (!reader.EndOfStream)
                    {
                        // Read the stream to a string, and write the string to the console.
                        String line = reader.ReadLine();
                        lineNumber++;
                        //Console.WriteLine(line);

                        var data = line.Split("\t".ToCharArray());

                        // We have recipe
                        if (data.Length > 0)
                        {
                            var recipe = new Recipe();
                            recipe.Id = id;
                            recipe.Name = data[0] + " #" + id;
                            recipe.IsTrainData = true;
                            recipe.RecipeIngredients = new List<RecipeIngredient>();

                            var ok = true;

                            var cuisine = context.Cuisines.SingleOrDefault(c => c.Name.Equals(GetRegionName(data[0])));

                            if (cuisine != null)
                            {
                                recipe.Cuisine = cuisine;
                                recipe.CuisineId = cuisine.Id;
                            }
                            else
                            {
                                ok = false;
                                Console.WriteLine($"Cuisine '{data[0]}' not found!!!");
                            }

                            // We have ingredients
                            if (data.Length > 1)
                            {
                                var ingredients = new List<RecipeIngredient>();
                                var ingredientsFromFile = new List<string>(data.Skip(1)).Distinct();
                                foreach (var i in ingredientsFromFile)
                                {
                                    var ing = GetIngredientName(i);

                                    var ingredient = context.Ingredients.SingleOrDefault(ig => ig.Name.Equals(ing));

                                    if (ingredient != null)
                                    {
                                        var recipeIngredient = new RecipeIngredient();
                                        recipeIngredient.Ingredient = ingredient;
                                        recipeIngredient.IngredientId = ingredient.Id;
                                        recipeIngredient.Recipe = recipe;
                                        recipeIngredient.RecipeId = recipe.Id;

                                        ingredients.Add(recipeIngredient);
                                    }
                                    else
                                    {
                                        ok = false;
                                        Console.WriteLine($"Ingredient '{ing}' not found!!!");
                                    }
                                }

                                if (ok)
                                {
                                    context.Recipes.Add(recipe);

                                    foreach (var recipeIngredient in ingredients)
                                    {
                                        //recipe.RecipeIngredients.Add(recipeIngredient);
                                        //context.Entry(recipeIngredient.Ingredient)
                                        //    .Collection(ig => ig.RecipeIngredients)
                                        //    .Load();
                                        //recipeIngredient.Ingredient.RecipeIngredients.Add(recipeIngredient);
                                        context.RecipeIngredients.Add(recipeIngredient);
                                    }

                                    if (id % 500 == 0)
                                    {
                                        Console.Write($"{recipe.Id}, ");
                                    }

                                    id++;
                                }
                            }
                        }

                        if (lineNumber % 100 == 0)
                        {
                            context.SaveChanges();
                            context.Dispose();
                            context = new FlavorNetworkContext();
                        }
                    }

                    context.SaveChanges();
                    context.Dispose();
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return id;
        }

        private static void WriteIngredients(FlavorNetworkContext context)
        {
            foreach (var item in context.Ingredients.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }

        private static void WriteIngredientCategories(FlavorNetworkContext context)
        {
            foreach (var item in context.IngredientCategories.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }

        private static void WriteIngredientCompounds(FlavorNetworkContext context)
        {
            foreach (var item in context.IngredientCompounds.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.IngredientId, item.CompoundId, context.Entry(item).State);
            }
        }

        private static void WriteIngredientContributions(FlavorNetworkContext context)
        {
            foreach (var item in context.IngredientContributions.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.IngredientId, item.ContributionMethodId, context.Entry(item).State);
            }
        }

        private static void WriteRecipes(FlavorNetworkContext context)
        {
            foreach (var item in context.Recipes.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }

        private static void WriteRecipeIngredients(FlavorNetworkContext context)
        {
            foreach (var item in context.RecipeIngredients.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.RecipeId, item.IngredientId, context.Entry(item).State);
            }
        }

        private static void WriteRegions(FlavorNetworkContext context)
        {
            foreach (var item in context.Regions.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }

        private static void WriteCuisines(FlavorNetworkContext context)
        {
            foreach (var item in context.Cuisines.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }

        private static void WriteCompounds(FlavorNetworkContext context)
        {
            foreach (var item in context.Compounds.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }

        private static void WriteContributionMethods(FlavorNetworkContext context)
        {
            foreach (var item in context.ContributionMethods.Local)
            {
                Console.WriteLine("Found {0}: {1} with state {2}", item.Id, item.Name, context.Entry(item).State);
            }
        }
    }

    class CuisineRegion
    {
        public string Cuisine
        {
            get; set;
        }

        public string Region
        {
            get; set;
        }
    }

    class IngredientAndCategory
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }
    }

    class IngredientAndCompound
    {
        public int Ingredient
        {
            get; set;
        }

        public int Compound
        {
            get; set;
        }
    }
}