using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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

            var recipesFiles = new List<string>();
            Configuration.GetSection("recipes-files").Bind(recipesFiles);

            Console.WriteLine("APPLICATION ARGUMENTS:");
            Console.WriteLine($"'connection-string' is '{connectionStringFile}'");
            Console.WriteLine($"'ingredients-file' is '{ingredientsFile}'");
            Console.WriteLine($"'compounds-file' is '{compoundsFile}'");
            Console.WriteLine($"'ingredients-compounds-file' is '{ingredientsCompoundsFile}'");
            Console.WriteLine();
            foreach (var recipeFile in recipesFiles)
            {
                Console.WriteLine($"'recipe-file' is '{recipeFile}'");
            }

            Console.ReadLine();
        }
    }
}