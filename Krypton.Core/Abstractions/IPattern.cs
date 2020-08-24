using System.Collections.Generic;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core
{
    public interface IPattern
    {
        VMOpCode Translates { get; }
        IList<CilOpCode> Pattern { get; }
        bool Verify(MethodDefinition Method, int index);
    }
}