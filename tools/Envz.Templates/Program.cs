using System.IO.Compression;

namespace Envz.Templates;

public class Program
{
    private static readonly string TEMPLATES_ROOT = Path.Combine(AppContext.BaseDirectory, "Templates");

    public static void Main(string[] args)
    {
        string[] templateDirs = GetTemplateDirectories();
        if (templateDirs.Length == 0)
        {
            Console.Error.WriteLine($"No template found in {TEMPLATES_ROOT}.");
            return;
        }

        string[] destinations = args.Length > 0 ? [args[0]] : FindItemTemplatesFolders();
        if (!destinations.Any())
        {
            Console.Error.WriteLine("No folder \"Visual Studio *\\Templates\\ItemTemplates\" found in \"MyDocuments\".");
            return;
        }

        foreach (string templateDir in templateDirs)
        {
            string name = Path.GetFileNameWithoutExtension(Directory.EnumerateFiles(templateDir, "*.vstemplate").First());
            string tempZip = Path.Combine(Path.GetTempPath(), $"{name}-{Guid.NewGuid():N}.zip");

            try
            {
                ZipFile.CreateFromDirectory(templateDir, tempZip, CompressionLevel.Optimal, includeBaseDirectory: false);

                foreach (string destination in destinations)
                {
                    string zipPath = Path.Combine(destination, $"{name}.zip");
                    File.Copy(tempZip, zipPath, overwrite: true);
                    Console.WriteLine($"{name,-20} -> {zipPath}");
                }
            }
            finally
            {
                File.Delete(tempZip);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Restart Visual Studio to view the templates.");
    }

    private static string[] GetTemplateDirectories()
    {
        return Directory.Exists(TEMPLATES_ROOT)
            ? Directory.GetDirectories(TEMPLATES_ROOT)
                .Where(dir => Directory.EnumerateFiles(dir, "*.vstemplate").Any())
                .Order()
                .ToArray()
            : [];
    }

    private static string[] FindItemTemplatesFolders()
    {
        string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!Directory.Exists(documents))
            return [];

        return Directory.GetDirectories(documents, "Visual Studio *")
            .Select(vs => Path.Combine(vs, "Templates", "ItemTemplates"))
            .Where(Directory.Exists)
            .Order()
            .ToArray();
    }
}