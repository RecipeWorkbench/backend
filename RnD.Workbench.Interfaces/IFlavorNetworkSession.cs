using RnD.Database.Interfaces;

namespace RnD.Workbench.Interfaces
{
    public interface IFlavorNetworkSession
    {
        IFlavorNetworkContext FlavorNetworkContext
        {
            get; set;
        }
    }
}
