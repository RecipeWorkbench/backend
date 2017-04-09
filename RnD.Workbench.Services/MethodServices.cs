using RnD.Workbench.Services.Interfaces;
using RnD.Workbench.DataTransferObjects;
using RnD.Workbench.Services.Assemblers;
using RnD.Database.SQLite;
using System.Linq;
using System.Collections.Generic;

namespace RnD.Workbench.Services
{
    public class MethodServices : IMethodServices
    {
        #region "Constructor"

        public MethodServices()
        {
            MethodAssembler = new MethodAssembler();
        }

        #endregion

        #region "Properties"

        private MethodAssembler MethodAssembler
        {
            get; set;
        }

        #endregion

        public MethodDto GetMethod(int id)
        {
            MethodDto newMethodDto;

            using (var context = new FlavorNetworkContext())
            {
                var Method = context.ContributionMethods.SingleOrDefault(c => c.Id == id);
                newMethodDto = MethodAssembler.Map(Method);
            }

            return newMethodDto;
        }

        public List<MethodDto> GetMethods()
        {
            var methods = new List<MethodDto>();

            using (var context = new FlavorNetworkContext())
            {
                foreach (var method in context.ContributionMethods)
                {
                    methods.Add(MethodAssembler.Map(method));
                }
            }

            return methods;
        }
    }
}
