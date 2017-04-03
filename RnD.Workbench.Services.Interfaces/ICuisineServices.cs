using RnD.Workbench.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Services.Interfaces
{
    public interface ICuisineServices
    {
        CuisineDto GetCuisine(int id);

        List<CuisineDto> GetCuisines();
    }
}
