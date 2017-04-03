using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.DataTransferObjects
{
    public class RecipeHeaderDto
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }

    public class RecipeDto : RecipeHeaderDto
    {
        public List<IngredientDto> Ingredients
        {
            get; set;
        }
    }
}
