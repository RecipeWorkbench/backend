using RnD.Workbench.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Services.Interfaces
{
    public interface IRecipeServices
    {
        RecipeDto Create(NewRecipeDto recipe);

        List<RecipeHeaderDto> GetRecipes(string name, int ingredient, int cuisine, int skip, int take);

        int GetRecipesCount(string name, int ingredient, int cuisine, int skip, int take);

        RecipeDto GetRecipe(int id);

        TransformedRecipeDto Transform(TransformRecipeDto task);
    }
}
