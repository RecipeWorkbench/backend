using System;
using System.Collections.Generic;
using System.Text;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Model;

namespace RnD.Workbench.Services.Assemblers
{
    class CuisineAssembler
    {
        internal CuisineDto Map(Cuisine cuisine)
        {
            var cuisineDto = new CuisineDto();

            cuisineDto.Id = cuisine.Id;
            cuisineDto.Name = cuisine.Name;

            return cuisineDto;
        }
    }
}
