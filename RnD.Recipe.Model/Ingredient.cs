using System;

namespace RnD.Recipe.Model
{
    public class Ingredient
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
