using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            BaseCommands(vm, write);
            Shifts(vm);
            RegisterASCIIsymbols(vm, read);
        }

        private static void RegisterASCIIsymbols(IVirtualMachine vm, Func<int> read)
        {
            vm.RegisterCommand(',', b =>
            {
                int symbolInByte = read();
                b.Memory[b.MemoryPointer] = (byte)symbolInByte;
            });
            RegisterCommandsForRange(vm, (byte)'0', (byte)'9');
            RegisterCommandsForRange(vm, (byte)'A', (byte)'Z');
            RegisterCommandsForRange(vm, (byte)'a', (byte)'z');
        }

        private static void RegisterCommandsForRange(IVirtualMachine vm, byte start, byte end)
        {
            for (byte b = start; b <= end; b++)
            {
                byte b2 = b;
                vm.RegisterCommand((char)b, vm => { vm.Memory[vm.MemoryPointer] = b2; });
            }
        }

        private static void Shifts(IVirtualMachine vm)
        {
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1)
                    b.MemoryPointer = 0;
                else
                    b.MemoryPointer++;
            });
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length - 1;
                else
                    b.MemoryPointer--;
            });
        }

        private static void BaseCommands(IVirtualMachine vm, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] == 255)
                    b.Memory[b.MemoryPointer] = 0;
                else
                    b.Memory[b.MemoryPointer]++;
            });
            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.Memory[b.MemoryPointer] = 255;
                else
                    b.Memory[b.MemoryPointer]--;
            });
        }

    }
}