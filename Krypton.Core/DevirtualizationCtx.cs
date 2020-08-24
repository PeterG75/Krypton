using AsmResolver.DotNet;
using Krypton.Core.Parser;
using Krypton.Core.PatternMatching;

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
        public PatternMatcher PatternMatcher { get; set; }
    }
}