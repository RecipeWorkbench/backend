using RnD.Workbench.Interfaces;
using System;
using System.Collections.Generic;
using RnD.Workbench.Interfaces.Calculations;
using RnD.Workbench.Model.Interfaces;

namespace RnD.Workbench.Core
{
    public class CuisineIngredientsRelation : FlavorNetworkSession, ICuisineIngredientsRelation
    {
        public IPrevalenceInCuisine PrevalenceInCuisine
        {
            get; set;
        }
 
        public IContributionToCuisine ContributionToCuisine
        {
            get; set;
        }

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
