﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Model
{
    public class Region
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public List<Cuisine> Cuisines
        {
            get; set;
        }
    }
}
