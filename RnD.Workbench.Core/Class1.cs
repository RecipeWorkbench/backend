using RnD.Workbench.Interfaces;
using RnD.Workbench.Interfaces.Model;
using System;
using System.Collections.Generic;
using RnD.Workbench.Interfaces.Calculations;

namespace RnD.Workbench.Core
{
    public class Class1 : ICuisineIngredientsRelation
    {
        public IPrevalenceInCuisine PrevalenceInCuisine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IContributionToCuisine ContributionToCuisine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void fg(ICollection<IIngredient> ingredients, ICollection<ICuisine> cuisines)
        {
            foreach (IIngredient ingredient in ingredients)
            {
                foreach (ICuisine cuisine in cuisines)
                {
                    ContributionToCuisine.Calculate(cuisine, ingredient);
                    PrevalenceInCuisine.Calculate(cuisine, ingredient);
                }

                foreach (ICuisine cuisine in cuisines)
                {
                    ContributionToCuisine.Calculate(cuisine, ingredient);
                    PrevalenceInCuisine.Calculate(cuisine, ingredient);
                }
            }
            
        }
    }
}
