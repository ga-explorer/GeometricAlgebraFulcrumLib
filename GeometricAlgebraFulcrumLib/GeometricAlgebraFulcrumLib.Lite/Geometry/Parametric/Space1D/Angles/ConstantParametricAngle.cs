using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;

public class ConstantParametricAngle :
    IParametricAngle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricAngle Create(Float64PlanarAngle point)
    {
        return new ConstantParametricAngle(
            point.GetAngleInPositiveRange(),
            Float64PlanarAngle.Angle0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricAngle Create(Float64PlanarAngle point, Float64PlanarAngle tangent)
    {
        return new ConstantParametricAngle(
            point.GetAngleInPositiveRange(),
            tangent.GetAngleInPositiveRange()
        );
    }


    public Float64PlanarAngle Point { get; }

    public Float64PlanarAngle Tangent { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricAngle(Float64PlanarAngle point, Float64PlanarAngle tangent)
    {
        Point = point;
        Tangent = tangent;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetAngle(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetDerivative1Angle(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IParametricScalar ToRadianParametricScalar()
    {
        return ConstantParametricScalar.Create(
            ParameterRange,
            Point.Radians.Value,
            Tangent.Radians.Value
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ParametricCurveLocalFrame4D GetFrame(double parameterValue)
    //{
    //    return ParametricCurveLocalFrame4D.Create(
    //        parameterValue,
    //        Point,
    //        Tangent
    //    );
    //}
}