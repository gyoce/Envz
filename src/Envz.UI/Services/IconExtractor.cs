using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Envz.UI.Services;

public class IconExtractor : IIconExtractor
{
    public byte[] ExtractPngBytes(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return [];

        using Icon? icon = Icon.ExtractAssociatedIcon(filePath);
        if (icon is null)
            return [];

        BitmapSource bitmap = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        PngBitmapEncoder encoder = new();
        encoder.Frames.Add(BitmapFrame.Create(bitmap));

        using MemoryStream stream = new();
        encoder.Save(stream);
        return stream.ToArray();
    }

    public ImageSource? DecodeFromPngBytes(byte[]? bytes)
    {
        if (bytes is null || bytes.Length == 0)
            return null;

        using MemoryStream stream = new(bytes);

        BitmapImage bitmap = new();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.StreamSource = stream;
        bitmap.EndInit();
        bitmap.Freeze();

        return bitmap;
    }
}