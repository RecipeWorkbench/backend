using RnD.Workbench.Interfaces.Calculations;
using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.Interfaces.Dependencies;
using RnD.Workbench.Model.Interfaces;

namespace RnD.Workbench.Core.Calculations
{
    public class ContributionToCuisine : FlavorNetworkSession, IHasSharedCompoundsCalculation, IContributionToCuisine
    {
        public ISharedCompounds SharedCompounds
        {
            get; set;
        }

        public double Calculate(ICuisine cuisine, IIngredient ingredient)
        {
            throw new NotSupportedException();
        }

        
    }
}
