using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class IngredientContribution
    {
        public int IngredientId
        {
            get; set;
        }

        public int CuisineId
        {
            get; set;
        }

        public double Contribution
        {
            get; set;
        }

        public Ingredient Ingredient
        {
            get; set;
        }

        public Cuisine Cuisine
        {
            get; set;
        }
    }
}
