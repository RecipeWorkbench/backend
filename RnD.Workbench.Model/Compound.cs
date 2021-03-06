﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class Compound
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string CasNumber
        {
            get; set;
        }

        public List<CompoundFlavor> CompoundFlavors
        {
            get; set;
        }

        public List<IngredientCompound> IngredientCompounds
        {
            get; set;
        }
    }
}
