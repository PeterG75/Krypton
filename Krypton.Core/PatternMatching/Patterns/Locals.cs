using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching.Patterns
{
    public class Stloc : IPattern
    {
        public VMOpCode Translates => VMOpCode.Stloc;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Unbox_Any,
            CilOpCodes.Stloc_S,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldloc_S,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Callvirt,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldloc_S,
            CilOpCodes.Callvirt,
            CilOpCodes.Ldfld,
            CilOpCodes.Call,
            CilOpCodes.Stelem_Ref,
            CilOpCodes.Ret
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return true;
        }
    }

    public class Ldloc : IPattern
    {
        public VMOpCode Translates => VMOpCode.Ldloc;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Unbox_Any,
            CilOpCodes.Ldelem_Ref,
            CilOpCodes.Callvirt,
            CilOpCodes.Ret
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return Method.DeclaringType.Fields.IndexOf(
                (FieldDefinition) Method.CilMethodBody.Instructions[index + 3].Operand) == 2;
        }
    }
}