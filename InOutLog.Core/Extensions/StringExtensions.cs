namespace InOutLog.Core
{
    public static class StringExtensions
    {
        public static string PadLeft02(this int value)
        {
            return value.ToString().PadLeft(2, '0');
        }
    }
}
