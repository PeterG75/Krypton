using System;
using System.Collections.Generic;
using System.Linq;
using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Cil;
using Krypton.Core.Architecture;

namespace Krypton.Core.PatternMatching
{
    public class PatternMatcher
    {
        public PatternMatcher()
        {
            OpCodes = new Dictionary<int, VMOpCode>();
            Patterns = new List<IPattern>();
            foreach (var type in typeof(PatternMatcher).Assembly.GetTypes())
                if (type.GetInterface(nameof(IPattern)) != null)
                    if (Activator.CreateInstance(type) is IPattern instance)
                        Patterns.Add(instance);
        }

        private Dictionary<int, VMOpCode> OpCodes { get; }
        private List<IPattern> Patterns { get; }

        public void SetOpCodeValue(VMOpCode opCode, int value)
        {
            OpCodes[value] = opCode;
        }

        public VMOpCode GetOpCodeValue(int value)
        {
            if (OpCodes.TryGetValue(value, out var opc)) return opc;
            return VMOpCode.Nop;
        }

        public VMOpCode FindOpCode(MethodDefinition Method, int index)
        {
            var instructions = Method.CilMethodBody.Instructions.ToList();
            foreach (var pat in Patterns)
                if (MatchesPattern(pat, instructions, index) && pat.Verify(Method, index))
                {
                    Patterns.Remove(pat);
                    return pat.Translates;
                }

            return VMOpCode.Nop;
        }

        private bool MatchesPattern(IPattern pattern, List<CilInstruction> instructions, int index)
        {
            var pat = pattern.Pattern;
            if (index + pat.Count > instructions.Count)
                return false;
            for (var i = 0; i < pat.Count; i++)
            {
                if (pat[i] == CilOpCodes.Nop)
                    continue;
                if (instructions[i + index].OpCode != pat[i])
                    return false;
            }

            return true;
        }
    }
}