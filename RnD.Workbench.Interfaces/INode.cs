using System;

namespace RnD.Workbench.Interfaces
{
    public interface INode
    {
        double Prevalence
        {
            get;
        }

        IRelatedNodes RelatedNodes
        {
            get;
        }
    }
}
