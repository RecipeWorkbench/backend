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
            CuisineAssembler = new CuisineAssembler();
        }

        #endregion

        #region "Properties"

        private IngredientAssembler IngredientAssembler
        {
            get; set;
        }

        private CuisineAssembler CuisineAssembler
        {
            get; set;
        }

        #endregion

        internal RecipeHeaderDto MapToHeader(Recipe recipe)
        {
            var recipeDto = new RecipeHeaderDto();

            recipeDto.Name = recipe.Name;
            recipeDto.Id = recipe.Id;
            recipeDto.Cuisine = CuisineAssembler.Map(recipe.Cuisine);

            return recipeDto;
        }

        internal RecipeDto Map(Recipe recipe)
        {
            var recipeDto = new RecipeDto();

            recipeDto.Name = recipe.Name;
            recipeDto.Id = recipe.Id;
            recipeDto.Cuisine = CuisineAssembler.Map(recipe.Cuisine);
            recipeDto.Ingredients = new List<IngredientDto>();

            foreach (var ingredient in recipe.RecipeIngredients)
            {
                recipeDto.Ingredients.Add(IngredientAssembler.Map(ingredient.Ingredient));
            }

            return recipeDto;
        }
    }
}
