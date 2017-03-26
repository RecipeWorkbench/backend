using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Csv
{
    interface IHasSchema
    {
        CsvSchema Schema
        {
            get; set;
        }
    }
}
