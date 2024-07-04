using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        public Dictionary<char, Action<IVirtualMachine>> Commands = new();

        public VirtualMachine(string program, int memorySize)
        {
            Memory = new byte[memorySize];
            Instructions = program;
            InstructionPointer = 0;
            MemoryPointer = 0;
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            Commands.Add(symbol, execute);
        }

        public void Run()
        {
            MemoryPointer = 0;
            for (; InstructionPointer < Instructions.Length;
                InstructionPointer = InstructionPointer + 1)
            {
                char symbol = Instructions[InstructionPointer];
                if (Commands.ContainsKey(symbol))
                    Commands[symbol](this);
            }
        }
    }
}