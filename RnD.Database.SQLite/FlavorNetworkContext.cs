using Microsoft.EntityFrameworkCore;
using RnD.Database.Interfaces;
using RnD.Workbench.Model;
using System;

namespace RnD.Database.SQLite
{
    public class FlavorNetworkContext : DbContext, IFlavorNetworkContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Cuisine> Cuisines { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Compound> Compounds { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<IngredientCategory> IngredientCategories { get; set; }

        public DbSet<IngredientCompound> IngredientCompounds { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<IngredientContribution> IngredientContributions { get; set; }
    }
}
