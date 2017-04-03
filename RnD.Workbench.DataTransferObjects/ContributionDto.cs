using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.DataTransferObjects
{
    public class ContributionDto
    {
        public double Contribution
        {
            get; set;
        }

        public MethodDto Method
        {
            get; set;
        }
    }
}
