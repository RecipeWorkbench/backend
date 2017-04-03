using RnD.Workbench.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;

namespace RnD.Workbench.Services
{
    public class RecipeServices : IRecipeServices
    {
        public RecipeDto Create(NewRecipeDto recipe)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipe(int id)
        {
            throw new NotImplementedException();
        }

        public List<RecipeHeaderDto> GetRecipesStartingWith(string name)
        {
            throw new NotImplementedException();
        }

        public TransformedRecipeDto Transform(TransformRecipeDto task)
        {
            throw new NotImplementedException();
        }
    }
}
