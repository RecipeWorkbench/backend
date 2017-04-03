using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class Flavor
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int FlavorGroupId
        {
            get; set;
        }

        public List<CompoundFlavor> CompoundFlavors
        {
            get; set;
        }

        public FlavorGroup FlavorGroup
        {
            get; set;
        }
    }
}
