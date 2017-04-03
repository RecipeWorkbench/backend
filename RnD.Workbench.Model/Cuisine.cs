using RnD.Workbench.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class Cuisine : ICuisine
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int RegionId
        {
            get; set;
        }

        public Region Region
        {
            get; set;
        }

        public List<IngredientContribution> IngredientContributions
        {
            get; set;
        }

        public List<Recipe> Recipes
        {
            get; set;
        }
    }
}
