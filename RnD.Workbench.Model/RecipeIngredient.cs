using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class RecipeIngredient
    {
        public int RecipeId
        {
            get; set;
        }

        public int IngredientId
        {
            get; set;
        }

        public Recipe Recipe
        {
            get; set;
        }

        public Ingredient Ingredient
        {
            get; set;
        }
    }
}
