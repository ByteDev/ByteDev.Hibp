namespace ByteDev.Hibp
{
    internal static class StringExtensions
    {
        public static string SafeSubstring(this string source, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (startIndex < 0)
                startIndex = 0;
            else if (startIndex >= source.Length)
                return string.Empty;

            if (length < 1)
                return string.Empty;

            if (source.Length - startIndex <= length) 
                return source.Substring(startIndex);

            return source.Substring(startIndex, length);
        }
    }
}