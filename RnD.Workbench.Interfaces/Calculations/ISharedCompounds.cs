using RnD.Workbench.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Interfaces.Calculations
{
    public interface ISharedCompounds
    {
        double MeanNumber(IRecipe recipe);
    }
}
