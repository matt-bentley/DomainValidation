
namespace System
{
    public static class StringExtensions
    {
        public static string TrimNullable(this string value)
        {
            return (value ?? "").Trim();
        }
    }
}
