using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching.Patterns
{
    public class Ldc_I4 : IPattern
    {
        public VMOpCode Translates => VMOpCode.Ldc_I4;

        public IList<CilOpCode> Pattern => new List<CilOpCode>
        {
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Ldarg_0,
            CilOpCodes.Ldfld,
            CilOpCodes.Unbox_Any,
            CilOpCodes.Newobj,
            CilOpCodes.Callvirt,
            CilOpCodes.Ret
        };

        public bool Verify(MethodDefinition Method, int index)
        {
            return ((ITypeDescriptor) Method.CilMethodBody.Instructions[index + 4].Operand).Name ==
                   Method.Module.CorLibTypeFactory.Int32.Name;
        }
    }
}