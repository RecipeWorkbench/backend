using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Model;

namespace RnD.Workbench.Services.Assemblers
{
    class IngredientAssembler
    {
        #region "Constructor"

        public IngredientAssembler()
        {
            MethodAssembler = new MethodAssembler();
        }

        #endregion

        #region "Properties"

        private MethodAssembler MethodAssembler
        {
            get; set;
        }

        #endregion

        internal IngredientDto Map(Ingredient ingredient)
        {
            var ingredientDto = new IngredientDto();

            ingredientDto.Name = ingredient.Name;
            ingredientDto.Id = ingredient.Id;
            ingredientDto.Contributions = new List<ContributionDto>();

            foreach (var contribution in ingredient.IngredientContributions)
            {
                ingredientDto.Contributions.Add(new ContributionDto
                {
                    Contribution = contribution.Contribution,
                    Method = MethodAssembler.Map(contribution.ContributionMethod)
                });
            }

            return ingredientDto;
        }
    }
}
