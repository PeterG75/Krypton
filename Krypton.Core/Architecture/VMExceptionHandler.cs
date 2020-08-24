using AsmResolver.DotNet;
using Krypton.Core.Parser;

namespace Krypton.Core.Architecture
{
    public enum VMExceptionHandlerType
    {
        Catch,
        Filter,
        Finally,
        Fault
    }

    public class VMExceptionHandler
    {
        public ITypeDescriptor CatchType;
        public VMExceptionHandlerType EHType;
        public int Filter;
        public int HandlerEnd;
        public int HandlerStart;
        public int TryEnd;
        public int TryStart;

        public VMExceptionHandler Read(ModuleDefinition Module, ResourceParser Parser)
        {
            TryStart = Parser.ReadEncryptedByte();
            TryEnd = Parser.ReadEncryptedByte();
            HandlerStart = Parser.ReadEncryptedByte();
            HandlerEnd = Parser.ReadEncryptedByte();
            EHType = (VMExceptionHandlerType) Parser.ReadEncryptedByte();
            switch (EHType)
            {
                case VMExceptionHandlerType.Catch:
                    CatchType = (ITypeDescriptor) Module.LookupMember(Parser.ReadEncryptedByte());
                    break;
                case VMExceptionHandlerType.Filter:
                    Filter = Parser.ReadEncryptedByte();
                    break;
                default:
                    Parser.ReadEncryptedByte();
                    break;
            }

            return this;
        }
    }
}