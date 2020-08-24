namespace Krypton.Core.Architecture
{
    public class VMInstruction
    {
        public VMInstruction() : this(VMOpCode.Nop)
        {
        }

        public VMInstruction(VMOpCode OpCode) : this(VMOpCode.Nop, null)
        {
        }

        public VMInstruction(VMOpCode opcode, object operand) : this(opcode, operand, 0)
        {
        }

        public VMInstruction(VMOpCode opcode, object operand, int offset)
        {
            OpCode = opcode;
            Operand = operand;
            Offset = offset;
        }

        public VMOpCode OpCode { get; set; }
        public object Operand { get; set; }
        public int Offset { get; set; }

        public override string ToString()
        {
            if (Operand != null) return $"[ {Offset} ] - [ {OpCode} ] - [ {Operand} ]";
            return $"[ {Offset} ] - [ {OpCode} ]";
        }
    }
}