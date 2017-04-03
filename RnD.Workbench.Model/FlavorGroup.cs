using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class FlavorGroup
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public List<Flavor> Flavors
        {
            get; set;
        }
    }
}
