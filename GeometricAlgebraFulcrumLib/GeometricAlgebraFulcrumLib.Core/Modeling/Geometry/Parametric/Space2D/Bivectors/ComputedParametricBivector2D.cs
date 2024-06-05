using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Bivectors;

public class ComputedParametricBivector2D :
    IParametricBivector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Func<double, LinFloat64Bivector2D> getBivectorFunc)
    {
        return new ComputedParametricBivector2D(
            Float64ScalarRange.Infinite, 
            getBivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc)
    {
        return new ComputedParametricBivector2D(
            parameterRange, 
            getBivectorFunc
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Func<double, LinFloat64Bivector2D> getBivectorFunc, Func<double, LinFloat64Bivector2D> getTangentFunc)
    {
        return new ComputedParametricBivector2D(
            Float64ScalarRange.Infinite, 
            getBivectorFunc, 
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc, Func<double, LinFloat64Bivector2D> getTangentFunc)
    {
        return new ComputedParametricBivector2D(
            parameterRange, 
            getBivectorFunc, 
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(DifferentialFunction xyFunc)
    {
        return Create(
            Float64ScalarRange.Infinite, 
            xyFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, DifferentialFunction xyFunc)
    {
        var xyDtFunc = xyFunc.GetDerivative1();
            
        return new ComputedParametricBivector2D(
            parameterRange, 
            t => 
                LinFloat64Bivector2D.Create(
                    xyFunc.GetValue(t)
                ),
            t => 
                LinFloat64Bivector2D.Create(
                    xyDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Func<double, double> xyFunc)
    {
        return Create(
            Float64ScalarRange.Infinite, 
            xyFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, Func<double, double> xyFunc)
    {
        return new ComputedParametricBivector2D(
            parameterRange, 
            t => 
                LinFloat64Bivector2D.Create(
                    xyFunc(t)
                ),
            t => 
                LinFloat64Bivector2D.Create(
                    Differentiate.FirstDerivative(xyFunc, t)
                )
        );
    }


    public Float64ScalarRange ParameterRange { get; }
        
    public Func<double, LinFloat64Bivector2D> GetBivectorFunc { get; }

    public Func<double, LinFloat64Bivector2D> GetTangentFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricBivector2D(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc)
    {
        ParameterRange = parameterRange;
        GetBivectorFunc = getBivectorFunc;
        GetTangentFunc = 
            t =>
            {
                const double epsilon = 1e-7;

                var p1 = getBivectorFunc(t - epsilon);
                var p2 = getBivectorFunc(t + epsilon);

                return (p2 - p1) / (2 * epsilon);
            };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricBivector2D(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc, Func<double, LinFloat64Bivector2D> getTangentFunc)
    {
        ParameterRange = parameterRange;
        GetBivectorFunc = getBivectorFunc;
        GetTangentFunc = getTangentFunc;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetBivector(double parameterValue)
    {
        return GetBivectorFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetDerivative1Bivector(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64ParametricScalar GetDualScalarCurve()
    {
        return ComputedParametricScalar.Create(
            ParameterRange,
            t => GetBivector(t).Dual2D().Scalar.ScalarValue
        );
    }
}