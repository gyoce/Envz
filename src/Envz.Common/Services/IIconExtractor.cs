using System.Windows.Media;

namespace Envz.Common.Services;

public interface IIconExtractor
{
    byte[] ExtractPngBytes(string filePath);
    ImageSource? DecodeFromPngBytes(byte[]? bytes);
}