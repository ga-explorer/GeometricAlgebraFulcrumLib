using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public sealed class E3DMapRotate<T> :
    E3DMap<T>
{
    public override IScalarAlgebraProcessor<T> ScalarProcessor 
        => Origin.ScalarProcessor;

    public PlanarAngle<T> Angle { get; }

    public E3DPoint<T> Origin { get; }

    public E3DVector<T> UnitDirection { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DMapRotate([NotNull] PlanarAngle<T> angle, [NotNull] E3DPoint<T> point, [NotNull] E3DVector<T> direction)
    {
        Angle = angle;
        Origin = point;
        UnitDirection = direction.GetUnitVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DVector<T> Map(E3DVector<T> vector)
    {
        var angleCos = Angle.Cos();
        var angleSin = Angle.Sin();

        return angleCos * vector + 
               (1 - angleCos) * UnitDirection.Dot(vector) * UnitDirection +
               angleSin * UnitDirection.Cross(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DPoint<T> Map(E3DPoint<T> point)
    {
        return Origin + Map(point - Origin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override E3DMap<T> GetInverse()
    {
        return new E3DMapRotate<T>(-Angle, Origin, UnitDirection);
    }
}