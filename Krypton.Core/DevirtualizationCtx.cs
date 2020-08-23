using AsmResolver.DotNet;
using Krypton.Core.Parser;

namespace Krypton.Core
{
    public class DevirtualizationCtx
    {
        public DevirtualizationCtx(DevirtualizationOptions Options)
        {
            this.Options = Options;
            Module = ModuleDefinition.FromFile(Options.FilePath);
        }

        public DevirtualizationOptions Options { get; set; }
        public ModuleDefinition Module { get; set; }
        public ResourceParser Parser { get; set; }
    }
}