using System;
using Krypton.Core;
using Krypton.Pipeline;
using Console = Colorful.Console;

namespace Krypton
{
    internal class Program
    {
        public static Version CurrentVersion = new Version("1.0.0");

        private static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            Console.Title = $"Krypton - {CurrentVersion}";
            var opts = new DevirtualizationOptions(args[0], logger);
            var ctx = new DevirtualizationCtx(opts);
            var devirtualizer = new Devirtualizer(ctx);
            devirtualizer.Devirtualize();
            devirtualizer.Save();
            Console.ReadLine();
        }
    }
}