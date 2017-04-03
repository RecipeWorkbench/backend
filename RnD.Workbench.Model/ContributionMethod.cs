using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class ContributionMethod
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public List<IngredientContribution> IngredientContributions
        {
            get; set;
        }
    }
}
