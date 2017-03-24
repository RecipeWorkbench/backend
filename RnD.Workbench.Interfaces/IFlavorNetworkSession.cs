using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Interfaces
{
    public interface IFlavorNetworkSession
    {
        public IFlavorNetworkContext FlavorNetworkContext
        {
            get; set;
        }
    }
}
