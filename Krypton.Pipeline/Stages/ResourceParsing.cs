using Krypton.Core;
using Krypton.Core.Parser;

namespace Krypton.Pipeline.Stages
{
    public class ResourceParsing : IStage
    {
        public string Name => nameof(ResourceParsing);

        public void Run(DevirtualizationCtx Ctx)
        {
            Ctx.Parser = new ResourceParser().Parse(Ctx);
            Ctx.Options.Logger.Success("Successfully Parsed Resource!");
            Ctx.Options.Logger.InfoStr("Strings Read", Ctx.Parser.Strings.Length.ToString());
            Ctx.Options.Logger.InfoStr("MethodKeys Read", Ctx.Parser.MethodKeys.Length.ToString());
            Ctx.Options.Logger.InfoStr("Operands Read", Ctx.Parser.Operands.Length.ToString());
        }
    }
}