using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.DataTransferObjects
{
    public class IngredientDto
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public List<ContributionDto> Contributions
        {
            get; set;
        }
    }
}
