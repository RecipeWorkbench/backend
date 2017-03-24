using RnD.Workbench.Interfaces.Calculations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Interfaces.Dependencies
{
    public interface IHasSharedCompoundsCalculation
    {
        ISharedCompounds SharedCompounds
        {
            get; set;
        }
    }
}
