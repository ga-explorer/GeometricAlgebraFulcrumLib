using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Bezier;

public sealed class Float64Bezier2Path3D :
    Float64Path3D
{
    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Point2 { get; }

    public LinFloat64Vector3D Point3 { get; }


    public Float64Bezier2Path3D(bool isPeriodic, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector3D();
        Point2 = point2.ToLinVector3D();
        Point3 = point3.ToLinVector3D();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid() &&
               Point3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bezier1Path3D GetDerivativeCurve()
    {
        return new Float64Bezier1Path3D(
            IsPeriodic,
            2 * (Point2 - Point1),
            2 * (Point3 - Point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        var (p1, p2, p3) = t.BernsteinBasis_2();

        return LinFloat64Vector3D.Create(
            p1 * Point1.X + p2 * Point2.X + p3 * Point3.X,
            p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y,
            p1 * Point1.Z + p2 * Point2.Z + p3 * Point3.Z
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
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        var s = 1 - t;

        var p1 = 2 * s;
        var p2 = 2 * t;

        return LinFloat64Vector3D.Create(
            p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X),
            p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y),
            p1 * (Point2.Z - Point1.Z) + p2 * (Point3.Z - Point2.Z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        throw new InvalidOperationException();
    }
}