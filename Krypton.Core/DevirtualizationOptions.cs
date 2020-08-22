using System.IO;

namespace Krypton.Core
{
    public class DevirtualizationOptions
    {
        public DevirtualizationOptions(string path, ILogger logger)
        {
            FilePath = path;
            Logger = logger;
            OutPath = Path.Combine(Path.GetDirectoryName(path)!,
                Path.GetFileNameWithoutExtension(path) + "-Devirtualized" + Path.GetExtension(path));
        }

        public string FilePath { get; set; }
        public ILogger Logger { get; set; }
        public string OutPath { get; set; }
    }
}