using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public class Float64LineSegmentPath3D :
    Float64ArcLengthPath3D
{
    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Point2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64LineSegmentPath3D(bool isPeriodic, ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        Point1 = point1.ToLinVector3D();
        Point2 = point2.ToLinVector3D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return Point1.GetDistanceToPoint(Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double parameterValue)
    {
        return parameterValue.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        var curveLength = GetLength();

        return length.ClampPeriodic(curveLength) / curveLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Point1.IsValid() &&
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        return parameterValue.Lerp(Point1, Point2);
    }

    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        return Point2 - Point1;
    }

    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
    {
        return Float64Path3DLocalFrame.Create(
            parameterValue.ClampPeriodic(1d),
            GetValue(parameterValue),
            (Point2 - Point1).ToUnitLinVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        return LinFloat64Vector3D.Zero;
    }
}