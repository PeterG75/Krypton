using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core;
using ModuleDefinition = AsmResolver.DotNet.ModuleDefinition;

namespace Krypton.Pipeline.Stages
{
    public class OpcodeMapping : IStage
    {
        public string Name => nameof(OpcodeMapping);
        public void Run(DevirtualizationCtx Ctx)
        {
            var opcodeHandlerMethod = FindOpCodeMethod(Ctx.Module);
            if (opcodeHandlerMethod == null)
            {
                throw new DevirtualizationException("Could not locate Opcode Handler method.");
            }
            Ctx.Options.Logger.Success($"Found method {opcodeHandlerMethod.Name} that contains Opcode Handlers!");
            
            var switchOpCode = opcodeHandlerMethod.CilMethodBody.Instructions.First(q => q.OpCode == CilOpCodes.Switch);

            var values = (List<ICilLabel>)switchOpCode.Operand;

            for (int i = 0; i < values.Count; i++)
            {
                var instructionLabel = (CilInstructionLabel)values[i];
                //TODO: Pattern Matching
            }
            
        }

        private MethodDefinition FindOpCodeMethod(ModuleDefinition Module)
        {
            foreach (var type in Module.GetAllTypes())
            {
                var method = type.Methods.FirstOrDefault(q =>
                    q.IsIL && q.CilMethodBody != null && q.CilMethodBody.Instructions != null &&
                    q.CilMethodBody.Instructions.Count >= 3200 &&
                    q.CilMethodBody.Instructions.Count(d => d.OpCode == CilOpCodes.Switch) == 1);
                if (method != null)
                {
                    return method;
                }
            }
            return null;
        }
    }
}