using RnD.Workbench.Interfaces;
using RnD.Database.Interfaces;

namespace RnD.Workbench.Core
{
    public abstract class FlavorNetworkSession : IFlavorNetworkSession
    {
        public IFlavorNetworkContext FlavorNetworkContext
        {
            get; set;
        }
    }
}
