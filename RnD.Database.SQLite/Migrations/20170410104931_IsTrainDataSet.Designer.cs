using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RnD.Database.SQLite;

namespace RnD.Database.SQLite.Migrations
{
    [DbContext(typeof(FlavorNetworkContext))]
    [Migration("20170410104931_IsTrainDataSet")]
    partial class IsTrainDataSet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("RnD.Workbench.Model.Compound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CasNumber");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Compounds");
                });

            modelBuilder.Entity("RnD.Workbench.Model.CompoundFlavor", b =>
                {
                    b.Property<int>("CompoundId");

                    b.Property<int>("FlavorId");

                    b.HasKey("CompoundId", "FlavorId");

                    b.HasIndex("FlavorId");

                    b.ToTable("CompoundFlavors");
                });

            modelBuilder.Entity("RnD.Workbench.Model.ContributionMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("ContributionMethods");
                });

            modelBuilder.Entity("RnD.Workbench.Model.Cuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("RegionId");

                    b.ToTable("Cuisines");
                });

            modelBuilder.Entity("RnD.Workbench.Model.Flavor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FlavorGroupId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FlavorGroupId");

                    b.HasIndex("Name");

                    b.ToTable("Flavors");
                });

            modelBuilder.Entity("RnD.Workbench.Model.FlavorGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("FlavorGroups");
                });

            modelBuilder.Entity("RnD.Workbench.Model.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IngredientCategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("IngredientCategoryId");

                    b.HasIndex("Name");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RnD.Workbench.Model.IngredientCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("IngredientCategories");
                });

            modelBuilder.Entity("RnD.Workbench.Model.IngredientCompound", b =>
                {
                    b.Property<int>("IngredientId");

                    b.Property<int>("CompoundId");

                    b.HasKey("IngredientId", "CompoundId");

                    b.HasIndex("CompoundId");

                    b.ToTable("IngredientCompounds");
                });

            modelBuilder.Entity("RnD.Workbench.Model.IngredientContribution", b =>
                {
                    b.Property<int>("IngredientId");

                    b.Property<int>("CuisineId");

                    b.Property<int>("ContributionMethodId");

                    b.Property<double>("Contribution");

                    b.HasKey("IngredientId", "CuisineId", "ContributionMethodId");

                    b.HasIndex("ContributionMethodId");

                    b.HasIndex("CuisineId");

                    b.ToTable("IngredientContributions");
                });

            modelBuilder.Entity("RnD.Workbench.Model.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CuisineId");

                    b.Property<bool>("IsTrainData");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CuisineId");

                    b.HasIndex("Name");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RnD.Workbench.Model.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId");

                    b.Property<int>("IngredientId");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("RnD.Workbench.Model.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("RnD.Workbench.Model.CompoundFlavor", b =>
                {
                    b.HasOne("RnD.Workbench.Model.Compound", "Compound")
                        .WithMany("CompoundFlavors")
                        .HasForeignKey("CompoundId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RnD.Workbench.Model.Flavor", "Flavor")
                        .WithMany("CompoundFlavors")
                        .HasForeignKey("FlavorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.Cuisine", b =>
                {
                    b.HasOne("RnD.Workbench.Model.Region", "Region")
                        .WithMany("Cuisines")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.Flavor", b =>
                {
                    b.HasOne("RnD.Workbench.Model.FlavorGroup", "FlavorGroup")
                        .WithMany("Flavors")
                        .HasForeignKey("FlavorGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.Ingredient", b =>
                {
                    b.HasOne("RnD.Workbench.Model.IngredientCategory", "IngredientCategory")
                        .WithMany("Ingredients")
                        .HasForeignKey("IngredientCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.IngredientCompound", b =>
                {
                    b.HasOne("RnD.Workbench.Model.Compound", "Compound")
                        .WithMany("IngredientCompounds")
                        .HasForeignKey("CompoundId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RnD.Workbench.Model.Ingredient", "Ingredient")
                        .WithMany("IngredientCompounds")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.IngredientContribution", b =>
                {
                    b.HasOne("RnD.Workbench.Model.ContributionMethod", "ContributionMethod")
                        .WithMany("IngredientContributions")
                        .HasForeignKey("ContributionMethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RnD.Workbench.Model.Cuisine", "Cuisine")
                        .WithMany("IngredientContributions")
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RnD.Workbench.Model.Ingredient", "Ingredient")
                        .WithMany("IngredientContributions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.Recipe", b =>
                {
                    b.HasOne("RnD.Workbench.Model.Cuisine", "Cuisine")
                        .WithMany("Recipes")
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RnD.Workbench.Model.RecipeIngredient", b =>
                {
                    b.HasOne("RnD.Workbench.Model.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RnD.Workbench.Model.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
