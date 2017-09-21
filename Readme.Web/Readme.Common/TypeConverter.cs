using System;
using System.Globalization;

namespace Readme.Common
{
    public static class TypeConverter
    {
        public static string ToDateString(this object date, string format)
        {
            string result = null;
            DateTime? dateValue = date as DateTime?;
            if (dateValue.HasValue)
            {
                result = dateValue.Value.ToString(format, CultureInfo.DefaultThreadCurrentCulture);
            }

            return result;
        }
    }
}
