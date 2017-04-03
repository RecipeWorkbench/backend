using Microsoft.EntityFrameworkCore;
using RnD.Workbench.Model;
using System;

namespace RnD.Database.Interfaces
{
    public interface IFlavorNetworkContext
    {
        DbSet<Recipe> Recipes { get; set; }

        DbSet<Cuisine> Cuisines { get; set; }

        DbSet<Ingredient> Ingredients { get; set; }

        DbSet<ContributionMethod> ContributionMethods { get; set; }

        DbSet<Compound> Compounds { get; set; }

        DbSet<Flavor> Flavors { get; set; }

        DbSet<FlavorGroup> FlavorGroups { get; set; }

        DbSet<CompoundFlavor> CompoundFlavors { get; set; }

        DbSet<Region> Regions { get; set; }

        DbSet<IngredientCategory> IngredientCategories { get; set; }

        DbSet<IngredientCompound> IngredientCompounds { get; set; }

        DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        DbSet<IngredientContribution> IngredientContributions { get; set; }

        int SaveChanges();

        string ConnectionString { get; set; }
    }
}
