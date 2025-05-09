using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Extensions;

public static class StackExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Push<T>(this Stack<T> stack, IEnumerable<T> itemsList)
    {
        foreach (var item in itemsList)
            stack.Push(item);
    }
}