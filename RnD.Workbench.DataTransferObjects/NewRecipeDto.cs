using System;
using System.Collections.Generic;

namespace RnD.Workbench.DataTransferObjects
{
    public class NewRecipeDto
    {
        public string Name
        {
            get; set;
        }

        public List<int> Ingredients
        {
            get; set;
        }

        public int Cuisine
        {
            get; set;
        }

        public bool CalculateCuisine
        {
            get; set;
        }
    }
}
