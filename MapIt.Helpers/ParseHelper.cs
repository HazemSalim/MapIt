using System;

namespace MapIt.Helpers
{
    public class ParseHelper
    {
        public static int? GetInt(object input)
        {
            try
            {
                int result = 0;
                if (input != null && int.TryParse(input.ToString(), out result))
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        public static Int64? GetInt64(object input)
        {
            try
            {
                Int64 result = 0;
                if (input != null && Int64.TryParse(input.ToString(), out result))
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        public static double? GetDouble(object input)
        {
            try
            {
                double result = 0;
                if (input != null && double.TryParse(input.ToString(), out result))
                    return result;
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        public static bool GetBool(object input)
        {
            try
            {
                bool result = false;
                if (input != null && bool.TryParse(input.ToString(), out result))
                    return result;
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return false;
            }
        }

        public static DateTime? GetDate(object input, string format, System.Globalization.CultureInfo culInfo)
        {
            try
            {
                DateTime result = DateTime.Now;
                if (input != null)
                    return result = DateTime.ParseExact(input.ToString(), format, culInfo, System.Globalization.DateTimeStyles.AssumeLocal);
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }
    }
}
