using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps;

public abstract class SimpleVectorRotation
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64Tuple Rotate(IFloat64Tuple x)
    {
        if (x is Float64DenseTuple xDense)
            return Rotate(xDense);

        return Rotate((Float64SparseTuple) x);
    }

    public abstract Float64DenseTuple Rotate(Float64DenseTuple x);

    public abstract Float64SparseTuple Rotate(Float64SparseTuple x);

    public abstract SimpleVectorRotation GetInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64DenseTuple> Rotate(params Float64DenseTuple[] xList)
    {
        return xList.Select(Rotate).ToImmutableArray();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Float64SparseTuple> Rotate(params Float64SparseTuple[] xList)
    {
        return xList.Select(Rotate).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64DenseTuple> Rotate(IEnumerable<Float64DenseTuple> xList)
    {
        return xList.Select(Rotate);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64SparseTuple> Rotate(IEnumerable<Float64SparseTuple> xList)
    {
        return xList.Select(Rotate);
    }
}