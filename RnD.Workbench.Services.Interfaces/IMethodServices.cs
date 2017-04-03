using RnD.Workbench.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RnD.Workbench.Services.Interfaces
{
    public interface IMethodServices
    {
        MethodDto GetMethod(int id);
    }
}
