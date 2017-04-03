using RnD.Workbench.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Services.Interfaces
{
    public interface IRecipeServices
    {
        RecipeDto Create(NewRecipeDto recipe);

        List<RecipeHeaderDto> GetRecipesStartingWith(string name);

        RecipeDto GetRecipe(int id);

        TransformedRecipeDto Transform(TransformRecipeDto task);
    }
}
