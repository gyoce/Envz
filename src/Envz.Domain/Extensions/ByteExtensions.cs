namespace Envz.Domain.Extensions;

public static class ByteExtensions
{
    extension(byte[] bytes)
    {
        public string ToBase64()
        {
            return Convert.ToBase64String(bytes);
        }
    }
}