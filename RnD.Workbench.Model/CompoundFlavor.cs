using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class CompoundFlavor
    {
        public int CompoundId
        {
            get; set;
        }

        public int FlavorId
        {
            get; set;
        }

        public Compound Compound
        {
            get; set;
        }

        public Flavor Flavor
        {
            get; set;
        }
    }
}
