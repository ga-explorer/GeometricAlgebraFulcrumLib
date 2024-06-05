using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.Harmonic;

public class CosWaveParametricScalar :
    IFloat64ParametricC2Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CosWaveParametricScalar Create(Float64ScalarRange parameterRange, double value1, double value2, int cycleCount = 1)
    {
        return new CosWaveParametricScalar(
            parameterRange, 
            value1, 
            value2, 
            cycleCount
        );
    }


    public int CycleCount { get; }

    public Float64Scalar Value1 { get; }

    public Float64Scalar Value2 { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CosWaveParametricScalar(Float64ScalarRange parameterRange, Float64Scalar value1, Float64Scalar value2, int cycleCount)
    {
        Debug.Assert(parameterRange.IsFinite);

        ParameterRange = parameterRange;
        Value1 = value1;
        Value2 = value2;
        CycleCount = cycleCount;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() && ParameterRange.IsFinite &&
               Value1.IsValid() && Value1.IsFinite() &&
               Value2.IsValid() && Value2.IsFinite() &&
               CycleCount > 0;
    }

    public Float64Scalar GetValue(double parameterValue)
    {
        var angle =
            ParameterRange
                .AffineMapToZeroOneRange(parameterValue)
                .Times(CycleCount)
                .ClampPeriodic(1) * LinFloat64DirectedAngle.Angle360;
        
        return Value1 + 0.5d * (1d - angle.Cos()) * (Value2 - Value1);
    }

    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        var angle =
            ParameterRange
                .AffineMapToZeroOneRange(parameterValue)
                .Times(CycleCount)
                .ClampPeriodic(1) * LinFloat64DirectedAngle.Angle360;

        var tFactor = 
            Float64Scalar.TwoPi * CycleCount / ParameterRange.Length;

        return 0.5d * angle.Sin() * (Value2 - Value1) * tFactor;
    }

    public Float64Scalar GetDerivative2Value(double parameterValue)
    {
        var angle =
            ParameterRange
                .AffineMapToZeroOneRange(parameterValue)
                .Times(CycleCount)
                .ClampPeriodic(1) * LinFloat64DirectedAngle.Angle360;

        var tFactor = 
            Float64Scalar.TwoPi * CycleCount / ParameterRange.Length;

        return 0.5d * angle.Cos() * (Value2 - Value1) * tFactor * tFactor;
    }
}