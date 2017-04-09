using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Model;

namespace RnD.Workbench.Services.Assemblers
{
    class RecipeAssembler
    {
        #region "Constructor"

        public RecipeAssembler()
        {
            IngredientAssembler = new IngredientAssembler();
        }

        #endregion

        #region "Properties"

        private IngredientAssembler IngredientAssembler
        {
            get; set;
        }

        #endregion

        internal RecipeDto Map(Recipe recipe)
        {
            var recipeDto = new RecipeDto();

            recipeDto.Name = recipe.Name;
            recipeDto.Id = recipe.Id;
            recipeDto.Ingredients = new List<IngredientDto>();

            foreach (var ingredient in recipe.RecipeIngredients)
            {
                recipeDto.Ingredients.Add(IngredientAssembler.Map(ingredient.Ingredient));
            }

            return recipeDto;
        }
    }
}
