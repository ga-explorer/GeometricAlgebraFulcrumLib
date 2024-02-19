using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapScale<T> :
    E3DMap<T>
{
    public override IScalarProcessor<T> ScalarProcessor 
        => Origin.ScalarProcessor;

    public T Factor { get; }

    public E3DPoint<T> Origin { get; }

    public E3DVector<T> UnitDirection { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapScale(T factor, E3DPoint<T> point, E3DVector<T> direction)
    {
        Factor = factor;
        Origin = point;
        UnitDirection = direction.GetUnitVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        var s = ScalarProcessor.Subtract(Factor, ScalarProcessor.ScalarOne);

        return vector + s * vector.Dot(UnitDirection) * UnitDirection;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        var s = ScalarProcessor.Subtract(Factor, ScalarProcessor.ScalarOne);

        return point + s * (point - Origin).Dot(UnitDirection) * UnitDirection;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DMap<T> GetInverse()
    {
        var factor = ScalarProcessor.Inverse(Factor);

        return new E3DMapScale<T>(factor, Origin, UnitDirection);
    }
}