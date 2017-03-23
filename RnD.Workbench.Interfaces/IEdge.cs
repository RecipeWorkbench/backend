using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Interfaces
{
    public interface IEdge
    {
        int SharedCompounds
        {
            get;
        }

        int NodeOne
        {
            get; set;
        }

        int NodeTwo
        {
            get; set;
        }
    }
}
