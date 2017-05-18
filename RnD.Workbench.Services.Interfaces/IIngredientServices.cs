using RnD.Workbench.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Services.Interfaces
{
    public interface IIngredientServices
    {
        IngredientDto GetIngredient(int id);

        List<IngredientHeaderDto> GetIngredientsStartingWith(string name);

        List<IngredientHeaderDto> GetIngredientHeaders();

        List<IngredientDto> GetIngredients(string name, int compound, int skip, int take);

        int GetIngredientsCount(string name, int compound, int skip, int take);
    }
}
