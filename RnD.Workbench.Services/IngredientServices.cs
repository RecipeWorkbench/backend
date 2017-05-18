using RnD.Workbench.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Services.Assemblers;
using RnD.Database.SQLite;
using System.Linq;
using RnD.Workbench.Model;
using Microsoft.EntityFrameworkCore;

namespace RnD.Workbench.Services
{
    public class IngredientServices : IIngredientServices
    {
        #region "Constructor"

        public IngredientServices()
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

        public IngredientDto GetIngredient(int id)
        {
            IngredientDto newIngredientDto;

            using (var context = new FlavorNetworkContext())
            {
                var ingredient = context.Ingredients.SingleOrDefault(c => c.Id == id);
                newIngredientDto = IngredientAssembler.Map(ingredient);
            }

            return newIngredientDto;
        }

        public List<IngredientHeaderDto> GetIngredientHeaders()
        {
            var ingredients = new List<IngredientHeaderDto>();

            using (var context = new FlavorNetworkContext())
            {
                var ingredientsStartingWith = context.Ingredients.OrderBy(i => i.Name);

                foreach (var ingredient in ingredientsStartingWith)
                {
                    ingredients.Add(IngredientAssembler.MapToHeader(ingredient));
                }
            }

            return ingredients;
        }

        public List<IngredientDto> GetIngredients(string name, int compound, int skip, int take)
        {
            var ingredients = new List<IngredientDto>();

            using (var context = new FlavorNetworkContext())
            {
                var query = GetIngredientsQuery(context, name, compound);

                if (query != null)
                {
                    query = query.OrderBy(r => r.Name)
                        .Skip(skip)
                        .Take(take);

                    foreach (var ingredient in query)
                    {
                        ingredients.Add(IngredientAssembler.Map(ingredient));
                    }
                }
            }

            return ingredients;
        }

        public int GetIngredientsCount(string name, int compound, int skip, int take)
        {
            var count = 0;

            using (var context = new FlavorNetworkContext())
            {
                var query = GetIngredientsQuery(context, name, compound);

                if (query != null)
                {
                    count = query.OrderBy(r => r.Name).Count();
                }
            }

            return count;
        }

        public List<IngredientHeaderDto> GetIngredientsStartingWith(string name)
        {
            var ingredients = new List<IngredientHeaderDto>();

            using (var context = new FlavorNetworkContext())
            {
                var ingredientsStartingWith = context.Ingredients.Where(i => i.Name.StartsWith(name))
                    .OrderBy(i => i.Name);

                foreach (var ingredient in ingredientsStartingWith)
                {
                    ingredients.Add(IngredientAssembler.MapToHeader(ingredient));
                }
            }

            return ingredients;
        }

        #region "Private methods"

        private IQueryable<Ingredient> GetIngredientsQuery(FlavorNetworkContext context, string name, int compound)
        {
            IQueryable<Ingredient> query = null;

            var inclusionQuery = context.Ingredients
                    .Include(i => i.IngredientCategory)
                    .Include(i => i.IngredientCompounds)
                        .ThenInclude(ic => ic.Compound)
                    .Include(i => i.IngredientContributions)
                        .ThenInclude(ic => ic.ContributionMethod);

            if (!string.IsNullOrEmpty(name))
            {
                query = inclusionQuery.Where(r => r.Name.StartsWith(name));
            }

            if (query != null)
            {
                if (compound != 0)
                {
                    query = query.Where(i => i.IngredientCompounds.Any(ic => ic.CompoundId == compound));
                }
            }
            else
            {
                if (compound != 0)
                {
                    query = inclusionQuery.Where(i => i.IngredientCompounds.Any(ic => ic.CompoundId == compound));
                }
            }

            return query;
        }

        #endregion
    }
}
