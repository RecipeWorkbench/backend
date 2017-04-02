using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

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

            ReadFromFile(countryRegionFile);
            ReadFromFile(compoundsFile);
            ReadFromFile(ingredientsFile);
            ReadFromFile(ingredientsCompoundsFile);
            foreach (var recipeFile in recipesFiles)
            {
                ReadFromFile(recipeFile);
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
    }
}