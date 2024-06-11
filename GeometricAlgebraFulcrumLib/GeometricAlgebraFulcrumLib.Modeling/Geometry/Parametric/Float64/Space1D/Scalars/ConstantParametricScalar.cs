using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

public class ConstantParametricScalar :
    IFloat64ParametricC2Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricScalar Create(double point)
    {
        return new ConstantParametricScalar(
            Float64ScalarRange.Infinite,
            point,
            0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricScalar Create(Float64ScalarRange parameterRange, double point)
    {
        return new ConstantParametricScalar(
            parameterRange,
            point,
            0d
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricScalar Create(double point, double tangent)
    {
        return new ConstantParametricScalar(
            Float64ScalarRange.Infinite,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricScalar Create(Float64ScalarRange parameterRange, double point, double tangent)
    {
        return new ConstantParametricScalar(
            parameterRange,
            point,
            tangent
        );
    }


    public double Point { get; }

    public double Tangent { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricScalar(Float64ScalarRange parameterRange, double point, double tangent)
    {
        ParameterRange = parameterRange;
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
    public Float64Scalar GetValue(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative2Value(double parameterValue)
    {
        return 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricScalarLocalFrame GetFrame(double parameterValue)
    {
        return ParametricScalarLocalFrame.Create(
            parameterValue,
            Point,
            Tangent
        );
    }
}