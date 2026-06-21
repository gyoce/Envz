using System.Windows.Media;

namespace Envz.UI.Services;

public interface IIconExtractor
{
    byte[] ExtractPngBytes(string filePath);
    ImageSource? DecodeFromPngBytes(byte[]? bytes);
}