using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Recipe.Model
{
    public class Cuisine
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int RegionId
        {
            get; set;
        }

        public Region Region
        {
            get; set;
        }
    }
}
