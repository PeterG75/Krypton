using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching.Patterns
{
    public class Call : IPattern
    {
        public VMOpCode Translates => VMOpCode.Call;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldc_I4_1,
            CilOpCodes.Call,
            CilOpCodes.Ret
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return true;
        }
    }
}