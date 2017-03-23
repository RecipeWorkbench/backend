using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Recipe.Model
{
    public class Recipe
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
    }
}
