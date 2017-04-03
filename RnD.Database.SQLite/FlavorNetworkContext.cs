using Microsoft.EntityFrameworkCore;
using RnD.Database.Interfaces;
using RnD.Workbench.Model;
using System;

namespace RnD.Database.SQLite
{
    public class FlavorNetworkContext : DbContext, IFlavorNetworkContext
    {
        public string ConnectionString
        {
            get; set;
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Cuisine> Cuisines { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<ContributionMethod> ContributionMethods { get; set; }

        public DbSet<Compound> Compounds { get; set; }

        public DbSet<Flavor> Flavors { get; set; }

        public DbSet<FlavorGroup> FlavorGroups { get; set; }

        public DbSet<CompoundFlavor> CompoundFlavors { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<IngredientCategory> IngredientCategories { get; set; }

        public DbSet<IngredientCompound> IngredientCompounds { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<IngredientContribution> IngredientContributions { get; set; }

        public FlavorNetworkContext()
        {
            ConnectionString = "flavornetwork.db";
        }

        public FlavorNetworkContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("Data Source={0}", ConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreatePrimaryKeys(modelBuilder);
            CreateIndexes(modelBuilder);
            CreateOneToManyRelations(modelBuilder);
            CreateManyToManyRelations(modelBuilder);
        }

        private void CreateManyToManyRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientContribution>()
                .HasOne(ic => ic.Ingredient)
                .WithMany(i => i.IngredientContributions)
                .HasForeignKey(ic => ic.IngredientId);

            modelBuilder.Entity<IngredientContribution>()
                .HasOne(ic => ic.Cuisine)
                .WithMany(c => c.IngredientContributions)
                .HasForeignKey(ic => ic.CuisineId);

            modelBuilder.Entity<IngredientContribution>()
                .HasOne(ic => ic.ContributionMethod)
                .WithMany(m => m.IngredientContributions)
                .HasForeignKey(ic => ic.ContributionMethodId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<IngredientCompound>()
                .HasOne(ic => ic.Ingredient)
                .WithMany(i => i.IngredientCompounds)
                .HasForeignKey(ic => ic.IngredientId);

            modelBuilder.Entity<IngredientCompound>()
                .HasOne(ic => ic.Compound)
                .WithMany(c => c.IngredientCompounds)
                .HasForeignKey(ic => ic.CompoundId);

            modelBuilder.Entity<CompoundFlavor>()
                .HasOne(cf => cf.Flavor)
                .WithMany(f => f.CompoundFlavors)
                .HasForeignKey(cf => cf.FlavorId);

            modelBuilder.Entity<CompoundFlavor>()
                .HasOne(cf => cf.Compound)
                .WithMany(c => c.CompoundFlavors)
                .HasForeignKey(cf => cf.CompoundId);
        }

        private void CreateOneToManyRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasOne(c => c.IngredientCategory)
                .WithMany(i => i.Ingredients);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Cuisine)
                .WithMany(c => c.Recipes);

            modelBuilder.Entity<Cuisine>()
                .HasOne(c => c.Region)
                .WithMany(r => r.Cuisines);

            modelBuilder.Entity<Flavor>()
                .HasOne(f => f.FlavorGroup)
                .WithMany(fg => fg.Flavors);
        }

        private void CreatePrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Cuisine>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Ingredient>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<ContributionMethod>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<IngredientCategory>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Compound>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Flavor>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<FlavorGroup>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Region>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<CompoundFlavor>()
                .HasKey(t => new { t.CompoundId, t.FlavorId });

            modelBuilder.Entity<IngredientContribution>()
                .HasKey(t => new { t.IngredientId, t.CuisineId, t.ContributionMethodId });

            modelBuilder.Entity<IngredientCompound>()
                .HasKey(t => new { t.IngredientId, t.CompoundId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(t => new { t.RecipeId, t.IngredientId });
        }

        private void CreateIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<Cuisine>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<Ingredient>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<ContributionMethod>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<IngredientCategory>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<Compound>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<Flavor>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<FlavorGroup>()
                .HasIndex(t => t.Name);

            modelBuilder.Entity<Region>()
                .HasIndex(t => t.Name);
        }
    }
}
