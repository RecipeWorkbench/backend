using RnD.Workbench.Model.Interfaces;
using System;

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
    }
}
