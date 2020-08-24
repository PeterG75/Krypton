using System.Collections.Generic;
using Krypton.Core;
using Krypton.Core.Architecture;
using Krypton.Core.Disassembly;

namespace Krypton.Pipeline.Stages
{
    public class MethodDisassembling : IStage
    {
        public string Name => nameof(MethodDisassembling);

        public void Run(DevirtualizationCtx Ctx)
        {
            Ctx.VirtualizedMethods = new List<VMMethod>();
            var disassembler = new VMDisassembler(Ctx);
            for (var i = 0; i < Ctx.Parser.MethodKeys.Length; i++)
            {
                var method = disassembler.DisassembleMethod(Ctx.Parser.MethodKeys[i]);
                Ctx.VirtualizedMethods.Add(method);
            }
        }
    }
}