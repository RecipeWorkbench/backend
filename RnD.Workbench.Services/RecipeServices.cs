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
                var recipe = context.Recipes.Include(r => r.Cuisine)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                            .ThenInclude(i => i.IngredientContributions)
                                .ThenInclude(ic => ic.ContributionMethod)
                    .SingleOrDefault(r => r.Id == id);
                newRecipeDto = RecipeAssembler.Map(recipe);
            }

            return newRecipeDto;
        }

        public List<RecipeHeaderDto> GetRecipes(string name, int ingredient, int cuisine, int skip, int take)
        {
            var recipes = new List<RecipeHeaderDto>();

            using (var context = new FlavorNetworkContext())
            {
                var query = GetRecipesQuery(context, name, ingredient, cuisine);

                if (query != null)
                {
                    query = query.OrderBy(r => r.Name)
                        .Skip(skip)
                        .Take(take);

                    foreach (var recipe in query)
                    {
                        recipes.Add(RecipeAssembler.MapToHeader(recipe));
                    }
                }
            }

            return recipes;
        }

        public int GetRecipesCount(string name, int ingredient, int cuisine, int skip, int take)
        {
            var count = 0;

            using (var context = new FlavorNetworkContext())
            {
                var query = GetRecipesQuery(context, name, ingredient, cuisine);

                if (query != null)
                {
                    count = query.OrderBy(r => r.Name).Count();
                }
            }

            return count;
        }

        public TransformedRecipeDto Transform(TransformRecipeDto task)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Private methods"

        private IQueryable<Recipe> GetRecipesQuery(FlavorNetworkContext context, string name, int ingredient, int cuisine)
        {
            IQueryable<Recipe> query = null;

            var inclusionQuery = context.Recipes.Include(r => r.Cuisine)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient);

            if (!string.IsNullOrEmpty(name))
            {
                query = inclusionQuery.Where(r => r.Name.StartsWith(name));
            }

            if (query != null)
            {
                if (ingredient != 0)
                {
                    query = query.Where(r => r.RecipeIngredients.Any(ri => ri.IngredientId == ingredient));
                }
            }
            else
            {
                if (ingredient != 0)
                {
                    query = inclusionQuery.Where(r => r.RecipeIngredients.Any(ri => ri.IngredientId == ingredient));
                }
            }

            if (query != null)
            {
                if (cuisine != 0)
                {
                    query = query.Where(r => r.CuisineId == cuisine);
                }
            }
            else
            {
                if (cuisine != 0)
                {
                    query = inclusionQuery.Where(r => r.CuisineId == cuisine);
                }
            }

            return query;
        }

        #endregion

    }
}
