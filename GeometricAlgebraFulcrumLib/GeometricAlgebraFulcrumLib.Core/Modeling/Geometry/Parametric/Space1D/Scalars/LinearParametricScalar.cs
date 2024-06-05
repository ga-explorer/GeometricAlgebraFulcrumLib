using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

public class LinearParametricScalar :
    IFloat64ParametricC2Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinearParametricScalar Create(double vector)
    {
        return new LinearParametricScalar(0d, vector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinearParametricScalar Create(double point, double vector)
    {
        return new LinearParametricScalar(point, vector);
    }

        
    public Float64Scalar Point { get; }

    public Float64Scalar Vector { get; }

    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinearParametricScalar(Float64Scalar point, Float64Scalar vector)
    {
        Point = point;
        Vector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Vector.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetValue(double parameterValue)
    {
        return Point + parameterValue * Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetTangent(double parameterValue)
    {
        return Vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetUnitTangent(double parameterValue)
    {
        return Vector.Sign().ToFloat64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative1Value(double parameterValue)
    {
        return Vector;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetDerivative2Value(double parameterValue)
    {
        return Float64Scalar.Zero;
    }
}