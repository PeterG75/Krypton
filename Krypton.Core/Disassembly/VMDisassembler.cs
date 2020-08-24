using System;
using AsmResolver.DotNet;
using Krypton.Core.Architecture;

namespace Krypton.Core.Disassembly
{
    public class VMDisassembler
    {
        public VMDisassembler(DevirtualizationCtx Ctx)
        {
            this.Ctx = Ctx;
        }

        private DevirtualizationCtx Ctx { get; }

        public VMMethod ReadMethod(int methodKey)
        {
            var method = new VMMethod(methodKey);
            Ctx.Parser.Reader.BaseStream.Position = methodKey;

            var mdToken = Ctx.Parser.ReadEncryptedByte();
            var parentMethod = ((IMethodDescriptor) Ctx.Module.LookupMember(mdToken)).Resolve();
            method.Parent = parentMethod;

            var locals = Ctx.Parser.ReadEncryptedByte();
            var exceptionHandlers = Ctx.Parser.ReadEncryptedByte();
            var instructions = Ctx.Parser.ReadEncryptedByte();

            ReadLocals(method, locals);
            ReadExceptionHandlers(method, exceptionHandlers);
            ReadAllInstructions(method, instructions);

            return method;
        }

        public void ReadLocals(VMMethod method, int locals)
        {
            for (var i = 0; i < locals; i++)
            {
                var token = Ctx.Parser.ReadEncryptedByte();
                method.MethodBody.Locals.Add(Ctx.Module.CorLibTypeFactory.Object);
                //method.MethodBody.Locals.Add((ITypeDescriptor)Ctx.Module.LookupMember(token));
            }
        }

        public void ReadExceptionHandlers(VMMethod method, int exceptionHandlers)
        {
            for (var i = 0; i < exceptionHandlers; i++)
            {
                var EH = new VMExceptionHandler().Read(Ctx.Module, Ctx.Parser);
                method.MethodBody.ExceptionHandlers.Add(EH);
            }
        }

        public void ReadAllInstructions(VMMethod method, int instructions)
        {
            for (var i = 0; i < instructions; i++)
            {
                var instr = new VMInstruction(VMOpCode.Nop, null, i);
                var b = Ctx.Parser.Reader.ReadByte();
                var opCode = Ctx.PatternMatcher.GetOpCodeValue(b);
                if (opCode == VMOpCode.Nop)
                {
                    method.MethodBody.Instructions.Add(instr);
                    continue;
                }

                instr.OpCode = opCode;
                if (b > 173) throw new DevirtualizationException("Disassembling Exception!");

                var operand = Ctx.Parser.Operands[b];
                instr.Operand = operand switch
                {
                    1 => Ctx.Parser.ReadEncryptedByte(),
                    2 => Ctx.Parser.Reader.ReadInt64(),
                    3 => Ctx.Parser.Reader.ReadSingle(),
                    4 => Ctx.Parser.Reader.ReadDouble(),
                    5 => ((Func<object>) (() =>
                    {
                        var num9 = Ctx.Parser.ReadEncryptedByte();
                        var array = new int[num9];
                        for (var m = 0; m < num9; m++) array[m] = Ctx.Parser.ReadEncryptedByte();
                        return array;
                    }))(),
                    _ => null
                };
                method.MethodBody.Instructions.Add(instr);
            }
        }
    }
}