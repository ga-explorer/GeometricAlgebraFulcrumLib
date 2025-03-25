using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Bezier;

public sealed class Float64Bezier1Path3D :
    Float64Path3D
{
    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Point2 { get; }


    public Float64Bezier1Path3D(bool isPeriodic, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector3D();
        Point2 = point2.ToLinVector3D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bezier0Path3D GetDerivativeCurve()
    {
        return new Float64Bezier0Path3D(IsPeriodic, Point2 - Point1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        var (p1, p2) = parameterValue.BernsteinBasis_1();

        return LinFloat64Vector3D.Create(
            p1 * Point1.X + p2 * Point2.X,
            p1 * Point1.Y + p2 * Point2.Y,
            p1 * Point1.Z + p2 * Point2.Z
        );
    }

    public override Float64Path3D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path3D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        return Point2 - Point1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        return LinFloat64Vector3D.Zero;
    }
}