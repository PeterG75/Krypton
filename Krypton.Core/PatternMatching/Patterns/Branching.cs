using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching.Patterns
{
    public class Br : IPattern
    {
        public VMOpCode Translates => VMOpCode.Br;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Unbox_Any,
            CilOpCodes.Ldc_I4_1,
            CilOpCodes.Sub,
            CilOpCodes.Stfld,
            CilOpCodes.Ret
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return true;
        }
    }

    public class BrFalse : IPattern
    {
        public VMOpCode Translates => VMOpCode.BrFalse;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Callvirt,
            CilOpCodes.Callvirt,
            CilOpCodes.Ldc_I4_0,
            CilOpCodes.Ceq,
            CilOpCodes.Stloc_S,
            CilOpCodes.Ldloc_S,
            CilOpCodes.Brfalse,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Unbox_Any,
            CilOpCodes.Ldc_I4_1,
            CilOpCodes.Sub,
            CilOpCodes.Stfld,
            CilOpCodes.Ret
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return true;
        }
    }
}