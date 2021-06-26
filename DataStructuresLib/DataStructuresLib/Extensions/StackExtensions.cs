using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLib.Extensions
{
    public static class StackExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Push<T>(this Stack<T> stack, IEnumerable<T> itemsList)
        {
            foreach (var item in itemsList)
                stack.Push(item);
        }
    }
}
