using System.Collections.Generic;
using AsmResolver.DotNet;

namespace Krypton.Core.Architecture
{
    public class VMMethod
    {
        public VMMethod(int methodKey)
        {
            MethodKey = methodKey;
            MethodBody = new VMBody(this);
        }

        public MethodDefinition Parent { get; set; }
        public int MethodKey { get; set; }
        public VMBody MethodBody { get; set; }
    }

    public class VMBody
    {
        public VMBody(VMMethod Parent)
        {
            this.Parent = Parent;
            Instructions = new List<VMInstruction>();
            Locals = new List<ITypeDescriptor>();
            ExceptionHandlers = new List<VMExceptionHandler>();
        }

        public VMMethod Parent { get; set; }
        public IList<VMInstruction> Instructions { get; set; }
        public IList<ITypeDescriptor> Locals { get; set; }
        public IList<VMExceptionHandler> ExceptionHandlers { get; set; }
    }
}