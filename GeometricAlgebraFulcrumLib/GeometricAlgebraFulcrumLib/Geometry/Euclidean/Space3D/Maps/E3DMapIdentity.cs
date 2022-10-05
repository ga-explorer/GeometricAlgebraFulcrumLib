using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapIdentity<T> :
    E3DMap<T>
{
    public override IScalarAlgebraProcessor<T> ScalarProcessor { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapIdentity([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        return point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DMap<T> GetInverse()
    {
        return this;
    }
}