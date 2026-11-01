namespace Envz.Domain.Extensions;

public static class StringExtensions
{
    extension(string str)
    {
        public bool EqualsIgnoringCase(string other)
        {
            return string.Equals(str, other, StringComparison.OrdinalIgnoreCase);
        }

        public byte[] FromBase64()
        {
            return Convert.FromBase64String(str);
        }
    }
}