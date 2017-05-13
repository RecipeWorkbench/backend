using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.DataTransferObjects
{
    public class IngredientHeaderDto
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }

    public class IngredientDto : IngredientHeaderDto
    {
        public List<ContributionDto> Contributions
        {
            get; set;
        }
    }
}
