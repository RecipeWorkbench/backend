using RnD.Workbench.Services.Interfaces;
using System;
using System.Collections.Generic;
using RnD.Workbench.DataTransferObjects;
using RnD.Database.SQLite;
using System.Linq;
using RnD.Workbench.Services.Assemblers;

namespace RnD.Workbench.Services
{
    public class CuisineServices : ICuisineServices
    {
        #region "Constructor"

        public CuisineServices()
        {
            CuisineAssembler = new CuisineAssembler();
        }

        #endregion

        #region "Properties"

        private CuisineAssembler CuisineAssembler
        {
            get; set;
        }

        #endregion

        public CuisineDto GetCuisine(int id)
        {
            CuisineDto newCuisineDto;

            using (var context = new FlavorNetworkContext())
            {
                var cuisine = context.Cuisines.SingleOrDefault(c => c.Id == id);
                newCuisineDto = CuisineAssembler.Map(cuisine);
            }

            return newCuisineDto;
        }

        public List<CuisineDto> GetCuisines()
        {
            var cuisines = new List<CuisineDto>();

            using (var context = new FlavorNetworkContext())
            {
                foreach (var cuisine in context.Cuisines)
                {
                    cuisines.Add(CuisineAssembler.Map(cuisine));
                }
            }

            return cuisines;
        }
    }
}
