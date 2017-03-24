using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class IngredientCompound
    {
        public int IngredientId
        {
            get; set;
        }

        public int CompoundId
        {
            get; set;
        }

        public Ingredient Ingredient
        {
            get; set;
        }

        public Compound Compound
        {
            get; set;
        }
    }
}
