using RnD.Workbench.Interfaces.Calculations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Interfaces
{
    public interface ICuisineIngredientsRelation
    {
        IPrevalenceInCuisine PrevalenceInCuisine
        {
            get; set;
        }

        IContributionToCuisine ContributionToCuisine
        {
            get; set;
        }
    }
}
