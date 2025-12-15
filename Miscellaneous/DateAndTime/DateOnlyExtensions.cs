namespace Odin.DateAndTime
{
    /// <summary>
    /// Extensions to DateOnly
    /// </summary>
    public static class DateOnlyExtensions
    {
        /// <summary>
        /// Outputs the date in YYYYMMdd format as a System.Int64
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static long ToLong(this DateOnly theDate)
        {
            return theDate.Year * 10000 + theDate.Month * 100 + theDate.Day;
        }
        
        /// <summary>
        /// Outputs the date in YYYYMMdd format as a System.Int32
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static int ToInt32(this DateOnly theDate)
        {
            return theDate.Year * 10000 + theDate.Month * 100 + theDate.Day;
        }
    }
}