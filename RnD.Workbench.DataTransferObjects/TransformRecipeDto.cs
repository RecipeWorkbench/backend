using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.DataTransferObjects
{
    public class TransformRecipeDto
    {
        public int RecipeId
        {
            get; set;
        }

        public int MethodId
        {
            get; set;
        }

        public int TargetCuisineId
        {
            get; set;
        }

        public List<int> LockedIngredients
        {
            get; set;
        }

        public List<int> IngredientsToConsider
        {
            get; set;
        }
    }
}
