using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;

public class ConstantParametricPolarAngle :
    IParametricPolarAngle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricPolarAngle Create(LinFloat64PolarAngle point)
    {
        return new ConstantParametricPolarAngle(
            point,
            LinFloat64PolarAngle.Angle0
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricPolarAngle Create(LinFloat64PolarAngle point, LinFloat64PolarAngle tangent)
    {
        return new ConstantParametricPolarAngle(
            point,
            tangent
        );
    }


    public LinFloat64PolarAngle Point { get; }

    public LinFloat64PolarAngle Tangent { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricPolarAngle(LinFloat64PolarAngle point, LinFloat64PolarAngle tangent)
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
    public LinFloat64PolarAngle GetAngle(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetDerivative1Angle(double parameterValue)
    {
        return Tangent;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetValue(double parameterValue)
    {
        return GetAngle(parameterValue).Radians;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        return GetDerivative1Angle(parameterValue).Radians;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64ParametricScalar ToRadianParametricScalar()
    {
        return ConstantParametricScalar.Create(
            ParameterRange,
            Point.RadiansValue,
            Tangent.RadiansValue
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