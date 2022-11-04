using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D;

public abstract class SimpleVectorRotation3D
{
    public abstract Tuple3D Rotate(ITuple3D x);

    public abstract SimpleVectorRotation3D GetInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<Tuple3D> Rotate(params ITuple3D[] xList)
    {
        return xList.Select(Rotate).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple3D> Rotate(IEnumerable<ITuple3D> xList)
    {
        return xList.Select(Rotate);
    }
}