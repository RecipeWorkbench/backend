using RnD.Workbench.Services.Interfaces;
using System;
using System.Collections.Generic;
using RnD.Workbench.DataTransferObjects;
using RnD.Database.SQLite;
using RnD.Workbench.Model;
using System.Linq;
using RnD.Workbench.Services.Assemblers;
using Microsoft.EntityFrameworkCore;

namespace RnD.Workbench.Services
{
    public class RecipeServices : IRecipeServices
    {
        #region "Constructor"

        public RecipeServices()
        {
            RecipeAssembler = new RecipeAssembler();
        }

        #endregion

        #region "Properties"

        private RecipeAssembler RecipeAssembler
        {
            get; set;
        }

        #endregion

        #region "IRecipeServices implementation"

        public RecipeDto Create(NewRecipeDto recipeDto)
        {
            RecipeDto newRecipeDto;

            using (var context = new FlavorNetworkContext())
            {
                var recipe = new Recipe();
                recipe.Name = recipeDto.Name;

                foreach (var ingredientDto in recipeDto.Ingredients)
                {
                    var ingredient = new RecipeIngredient();
                    ingredient.Recipe = recipe;
                    ingredient.Ingredient = context.Ingredients.SingleOrDefault(i => i.Id == ingredientDto);

                    recipe.RecipeIngredients.Add(ingredient);
                }

                recipe.Cuisine = context.Cuisines.SingleOrDefault(c => c.Id == recipeDto.Cuisine);
                context.Recipes.Add(recipe);

                context.SaveChanges();

                newRecipeDto = RecipeAssembler.Map(recipe);
            }

            return newRecipeDto;
        }

        public RecipeDto GetRecipe(int id)
        {
            RecipeDto newRecipeDto;

            using (var context = new FlavorNetworkContext())
            {
                var recipe = context.Recipes.SingleOrDefault(r => r.Id == id);
                newRecipeDto = RecipeAssembler.Map(recipe);
            }

            return newRecipeDto;
        }

        public List<RecipeHeaderDto> GetRecipesStartingWith(string name)
        {
            var recipes = new List<RecipeHeaderDto>();

            using (var context = new FlavorNetworkContext())
            {
                var recipesStartingWith = context.Recipes.Include(r => r.Cuisine)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                            .ThenInclude(i => i.IngredientContributions)
                                .ThenInclude(c => c.ContributionMethod)
                    .Where(r => r.Name.StartsWith(name));

                foreach (var recipe in recipesStartingWith)
                {
                    recipes.Add(RecipeAssembler.Map(recipe));
                }
            }

            return recipes;
        }

        public TransformedRecipeDto Transform(TransformRecipeDto task)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
