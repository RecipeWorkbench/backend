using Microsoft.Extensions.Configuration;
using RnD.Workbench.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

            //ReadCuisineRegions(countryRegionFile);
            //ReadFromFile(countryRegionFile);

            //ReadCompounds(compoundsFile);
            //ReadFromFile(compoundsFile);

            //ReadIngredientCategory(ingredientsFile);
            //ReadFromFile(ingredientsFile);

            ReadFromFile(ingredientsCompoundsFile);
            foreach (var recipeFile in recipesFiles)
            {
                //ReadFromFile(recipeFile);
            }

            Console.ReadLine();
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

        private static void ReadCuisineRegions(string filename)
        {
            var countryRegionCsvSchema = new CsvSchema();
            countryRegionCsvSchema.Properties.Add(new SchemaProperty("Cuisine"));
            countryRegionCsvSchema.Properties.Add(new SchemaProperty("Region"));

            var countryRegionCsvReader = new CsvReader<CuisineRegion>();
            countryRegionCsvReader.CsvDelimiter = "\t";
            countryRegionCsvReader.UseSingleLineHeaderInCsv = true;
            countryRegionCsvReader.Schema = countryRegionCsvSchema;

            var list = countryRegionCsvReader.Read(File.OpenRead(filename));

            foreach (var item in list)
            {
                Console.WriteLine($"C: '{item.Cuisine}', R: '{item.Region}'");
            }
        }

        private static void ReadIngredientCategory(string filename)
        {
            var ingredientCategoryCsvSchema = new CsvSchema();
            ingredientCategoryCsvSchema.Properties.Add(new SchemaProperty("Id"));
            ingredientCategoryCsvSchema.Properties.Add(new SchemaProperty("Name"));
            ingredientCategoryCsvSchema.Properties.Add(new SchemaProperty("Category"));

            var ingredientCategoryCsvReader = new CsvReader<IngredientCategory>();
            ingredientCategoryCsvReader.CsvDelimiter = "\t";
            ingredientCategoryCsvReader.UseSingleLineHeaderInCsv = true;
            ingredientCategoryCsvReader.Schema = ingredientCategoryCsvSchema;

            var list = ingredientCategoryCsvReader.Read(File.OpenRead(filename));

            foreach (var item in list)
            {
                item.Id = item.Id + 1;
                Console.WriteLine($"Id: '{item.Id}', N: '{item.Name}', C: '{item.Category}'");
            }
        }

        private static void ReadCompounds(string filename)
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
                Console.WriteLine($"ID: '{item.Id}', Name: '{item.Name}', CAS: '{item.CasNumber}'");
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

    class IngredientCategory
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
}