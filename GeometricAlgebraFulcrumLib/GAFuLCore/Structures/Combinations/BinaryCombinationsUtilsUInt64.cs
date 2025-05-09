using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

/// <summary>
/// General utilities for computing Combinations of the form C(n, 2)
/// </summary>
public static class BinaryCombinationsUtilsUInt64
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<ulong> IndexToCombinadic(ulong index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

        return new Pair<ulong>(n1, n2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IndexToCombinadicPattern(ulong index)
    {
        var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
        var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

        return (1UL << (int)n1) | (1UL << (int)n2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong CombinadicToIndex(Pair<ulong> combinadic)
    {
        var (n1, n2) = combinadic;

        Debug.Assert(n1 < n2);

        return n1 + ((n2 * (n2 - 1UL)) >> 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong CombinadicToIndex(int i1, int i2)
    {
        Debug.Assert(i1 >= 0 && i1 < i2);

        var n1 = (ulong)i1;
        var n2 = (ulong)i2;
            
        return n1 + ((n2 * (n2 - 1UL)) >> 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong CombinadicToIndex(ulong n1, ulong n2)
    {
        Debug.Assert(n1 < n2);

        return n1 + ((n2 * (n2 - 1UL)) >> 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong CombinadicPatternToIndex(ulong pattern)
    {
        var n2 = (ulong)Math.Log(pattern, 2);
        var n1 = (ulong)Math.Log(pattern - (1UL << (int)n2), 2);

        return n1 + ((n2 * (n2 - 1UL)) >> 1);
    }
}