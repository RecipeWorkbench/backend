using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Model;

namespace RnD.Workbench.Services.Assemblers
{
    class MethodAssembler
    {
        internal MethodDto Map(ContributionMethod method)
        {
            return new MethodDto()
            {
                Id = method.Id,
                Name = method.Name
            };
        }
    }
}
