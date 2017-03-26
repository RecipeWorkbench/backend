using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Utilities.Csv
{
    public class CsvReader<T> : ICsvReader<T> where T : class
    {
        public CsvSchema Schema
        {
            get; set;
        }

        public string CsvDelimiter
        {
            get; set;
        }
 
        public bool UseSingleLineHeaderInCsv
        {
            get; set;
        }

        public ICollection<T> Read(Stream stream)
        {
            var reader = new StreamReader(stream);
            bool skipFirstLine = UseSingleLineHeaderInCsv;
            var list = new List<T>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(CsvDelimiter.ToCharArray());
                if (skipFirstLine)
                {
                    skipFirstLine = false;
                }
                else
                {
                    var item = (T) Activator.CreateInstance(typeof(T));

                    for (int i = 0; i < values.Length; i++)
                    {
                        var property = item.GetType().GetProperty(Schema.Properties[i].Name);
                        property.SetValue(item, Convert.ChangeType(values[i], property.PropertyType), null);
                    }

                    list.Add(item);
                }

            }

            return list;
        }
    }
}
