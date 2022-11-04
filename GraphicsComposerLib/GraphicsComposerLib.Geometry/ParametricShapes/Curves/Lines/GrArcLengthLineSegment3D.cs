using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Lines;

public class GrArcLengthLineSegment3D :
    IGraphicsC2ParametricCurve3D,
    IGraphicsC1ArcLengthCurve3D
{
    public Tuple3D Point1 { get; }

    public Tuple3D Point2 { get; }
    
    public double ParameterValueMin
        => 0d;

    public double ParameterValueMax
        => 1d;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrArcLengthLineSegment3D(ITuple3D point1, ITuple3D point2)
    {
        Point1 = point1.ToTuple3D();
        Point2 = point2.ToTuple3D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetLength()
    {
        return Point1.GetDistanceToPoint(Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ParameterToLength(double parameterValue)
    {
        return parameterValue.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double LengthToParameter(double length)
    {
        var curveLength = GetLength();

        return length.ClampPeriodic(curveLength) / curveLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point1.IsValid() && 
               Point2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetPoint(double parameterValue)
    {
        return parameterValue.Lerp(Point1, Point2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetTangent(double parameterValue)
    {
        return Point2 - Point1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetUnitTangent(double parameterValue)
    {
        return GetTangent(parameterValue).ToUnitVector();
    }

    public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return GrParametricCurveLocalFrame3D.Create(
            parameterValue.ClampPeriodic(1d),
            GetPoint(parameterValue),
            (Point2 - Point1).ToUnitVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetSecondDerivative(double parameterValue)
    {
        return Tuple3D.Zero;
    }
}