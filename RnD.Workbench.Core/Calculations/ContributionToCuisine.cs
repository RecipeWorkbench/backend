using RnD.Workbench.Interfaces.Calculations;
using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.Interfaces.Model;
using RnD.Workbench.Interfaces.Dependencies;
using RnD.Database.Interfaces;

namespace RnD.Workbench.Core.Calculations
{
    public class ContributionToCuisine : IHasSharedCompoundsCalculation, IContributionToCuisine
    {
        

        public ISharedCompounds SharedCompounds
        {
            get; set;
        }

        public double Calculate(ICuisine cuisine, IIngredient ingredient)
        {
            
        }

        
    }
}
