using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;


//Credit to https://stackoverflow.com/questions/17489960/nullable-datetime-with-sqldatareader

namespace PrimitiveExtensions
{
    public static class DataReaderExtensions
    {
        public static DateTime? GetNullableDateTime(this IDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? (DateTime?)null : (DateTime?)reader.GetDateTime(columnIndex);
        }
    }
}
