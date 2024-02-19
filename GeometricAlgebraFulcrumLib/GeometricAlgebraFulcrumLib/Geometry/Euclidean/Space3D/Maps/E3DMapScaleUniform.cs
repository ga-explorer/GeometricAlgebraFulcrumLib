using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapScaleUniform<T> :
    E3DMap<T>
{
    public override IScalarProcessor<T> ScalarProcessor 
        => Origin.ScalarProcessor;

    public T Factor { get; }

    public E3DPoint<T> Origin { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapScaleUniform(T factor, E3DPoint<T> point)
    {
        Factor = factor;
        Origin = point;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        return Factor * vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        var s = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, Factor);

        return Factor * point + s * Origin;
    }

    public override E3DMap<T> GetInverse()
    {
        var factor = ScalarProcessor.Inverse(Factor);

        return new E3DMapScaleUniform<T>(factor, Origin);
    }
}