using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching.Patterns
{
    public class Pop : IPattern
    {
        public VMOpCode Translates => VMOpCode.Pop;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Callvirt,
            CilOpCodes.Pop
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return true;
        }
    }
}