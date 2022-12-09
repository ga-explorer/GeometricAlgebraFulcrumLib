using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.SubSpaces;

public sealed record Float64NullSubspace :
    IFloat64Subspace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64NullSubspace Create(int dimensions)
    {
        return new Float64NullSubspace(dimensions);
    }


    public int Dimensions { get; }

    public int SubspaceDimensions 
        => 0;

    public IEnumerable<Float64Tuple> BasisVectors 
        => Enumerable.Empty<Float64Tuple>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64NullSubspace(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        Dimensions = dimensions;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(Float64Tuple vector, double epsilon = 1E-12)
    {
        return vector.IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(IFloat64Subspace subspace, double epsilon = 1E-12)
    {
        return subspace.Dimensions == 0;
    }
}