using RnD.Workbench.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Services.Assemblers;
using RnD.Database.SQLite;
using System.Linq;

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

        public List<IngredientDto> GetIngredientsStartingWith(string name)
        {
            var ingredients = new List<IngredientDto>();

            using (var context = new FlavorNetworkContext())
            {
                var ingredientsStartingWith = context.Ingredients.Where(i => i.Name.StartsWith(name));

                foreach (var ingredient in ingredientsStartingWith)
                {
                    ingredients.Add(IngredientAssembler.Map(ingredient));
                }
            }

            return ingredients;
        }
    }
}
