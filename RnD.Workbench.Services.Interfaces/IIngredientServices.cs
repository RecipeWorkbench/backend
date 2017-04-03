using RnD.Workbench.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Services.Interfaces
{
    public interface IIngredientServices
    {
        IngredientDto GetIngredient(int id);

        List<IngredientDto> GetIngredientsStartingWith(string name);
    }
}
