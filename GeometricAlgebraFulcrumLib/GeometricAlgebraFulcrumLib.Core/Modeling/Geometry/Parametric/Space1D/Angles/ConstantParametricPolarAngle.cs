using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;

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