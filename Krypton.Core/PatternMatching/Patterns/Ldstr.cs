using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching.Patterns
{
    public class Ldstr : IPattern
    {
        public VMOpCode Translates => VMOpCode.Ldstr;
        public IList<CilOpCode> Pattern => new List<CilOpCode>()
        {
            CilOpCodes.Ldsfld,
            CilOpCodes.Callvirt,
            CilOpCodes.Brtrue,
            CilOpCodes.Ldtoken,
            CilOpCodes.Call,
            CilOpCodes.Callvirt,
            CilOpCodes.Stloc_S,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldloc_S,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Unbox_Any,
            CilOpCodes.Ldc_I4,
            CilOpCodes.Or,
            CilOpCodes.Callvirt,
            CilOpCodes.Newobj,
            CilOpCodes.Callvirt,
            CilOpCodes.Ret,
        };
        public bool Verify(MethodDefinition Method, int index)
        {
            return true;
        }
    }
}