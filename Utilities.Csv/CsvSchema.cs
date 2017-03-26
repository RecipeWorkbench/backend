using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Csv
{
    public class CsvSchema
    {
        private List<SchemaProperty> properties;

        public IList<SchemaProperty> Properties
        {
            get => properties;
        }

        public CsvSchema()
        {
            properties = new List<SchemaProperty>();
        }
    }
}
