using RnD.Workbench.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace RnD.Workbench.Model
{
    public class Ingredient : IIngredient
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int IngredientCategoryId
        {
            get; set;
        }

        public IngredientCategory IngredientCategory
        {
            get; set;
        }

        public List<IngredientContribution> IngredientContributions
        {
            get; set;
        }

        public List<RecipeIngredient> RecipeIngredients
        {
            get; set;
        }

        public List<IngredientCompound> IngredientCompounds
        {
            get; set;
        }
    }
}
