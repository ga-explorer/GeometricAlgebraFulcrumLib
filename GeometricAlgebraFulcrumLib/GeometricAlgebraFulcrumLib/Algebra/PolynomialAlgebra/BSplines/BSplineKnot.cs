﻿using System;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.BSplines;

public sealed record BSplineKnot<T>
{
    public int Index1 { get; }

    public int Index2 
        => Index1 + Multiplicity - 1;

    public int Multiplicity { get; }

    public T Value { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal BSplineKnot(int index1, T value, int multiplicity)
    {
        if (multiplicity < 1)
            throw new ArgumentOutOfRangeException(nameof(multiplicity));

        if (index1 < 0)
            throw new ArgumentOutOfRangeException(nameof(index1));

        Index1 = index1;
        Value = value;
        Multiplicity = multiplicity;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(int index)
    {
        return index >= Index1 && index <= Index2;
    }
}