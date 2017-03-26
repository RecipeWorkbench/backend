using System.Collections.Generic;
using System.IO;

namespace Utilities.Csv
{
    interface ICsvReader<T> : IHasSchema, ISingleLineHeader, ICsvDelimiter
    {
        ICollection<T> Read(Stream stream);
    }
}
