using RnD.Workbench.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class Recipe : IRecipe
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int CuisineId
        {
            get; set;
        }

        public Cuisine Cuisine
        {
            get; set;
        }

        public List<RecipeIngredient> RecipeIngredients
        {
            get; set;
        }
    }
}
