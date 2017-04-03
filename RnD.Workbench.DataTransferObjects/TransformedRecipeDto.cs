using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.DataTransferObjects
{
    public class TransformedRecipeDto
    {
        public int Id
        {
            get; set;
        }

        public string OriginalName
        {
            get; set;
        }

        public string NewName
        {
            get; set;
        }

        public CuisineDto OriginalCuisine
        {
            get; set;
        }

        public CuisineDto NewCuisine
        {
            get; set;
        }

        public List<IngredientDto> UnmodifiedIngredients
        {
            get; set;
        }

        public List<IngredientDto> AddedIngredients
        {
            get; set;
        }

        public List<ReplacedIngredientDto> ReplacedIngredients
        {
            get; set;
        }
    }

    public class ReplacedIngredientDto
    {
        public IngredientDto Original
        {
            get; set;
        }

        public IngredientDto New
        {
            get; set;
        }
    }
}
