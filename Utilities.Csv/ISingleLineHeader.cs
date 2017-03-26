using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Csv
{
    interface ISingleLineHeader
    {
        bool UseSingleLineHeaderInCsv
        {
            get; set;
        }
    }
}
