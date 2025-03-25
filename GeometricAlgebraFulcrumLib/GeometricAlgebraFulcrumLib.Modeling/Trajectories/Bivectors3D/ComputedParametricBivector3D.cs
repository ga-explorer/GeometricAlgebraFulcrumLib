using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors3D;

public class ComputedParametricBivector3D :
    IParametricBivector3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector3D Create(Func<double, LinFloat64Bivector3D> getBivectorFunc)
    {
        return new ComputedParametricBivector3D(
            Float64ScalarRange.Infinite,
            getBivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector3D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector3D> getBivectorFunc)
    {
        return new ComputedParametricBivector3D(
            parameterRange,
            getBivectorFunc
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ComputedParametricBivector3D Create(Func<double, Float64Bivector3D> getBivectorFunc, Func<double, Float64Bivector3D> getTangentFunc)
    //{
    //    return new ComputedParametricBivector3D(
    //        Float64ScalarRange.Infinite, 
    //        getBivectorFunc, 
    //        getTangentFunc
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ComputedParametricBivector3D Create(Float64ScalarRange parameterRange, Func<double, Float64Bivector3D> getBivectorFunc, Func<double, Float64Bivector3D> getTangentFunc)
    //{
    //    return new ComputedParametricBivector3D(
    //        parameterRange, 
    //        getBivectorFunc, 
    //        getTangentFunc
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector3D Create(DifferentialFunction xyFunc, DifferentialFunction xzFunc, DifferentialFunction yzFunc)
    {
        return Create(
            Float64ScalarRange.Infinite,
            xyFunc,
            xzFunc,
            yzFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector3D Create(Float64ScalarRange parameterRange, DifferentialFunction xyFunc, DifferentialFunction xzFunc, DifferentialFunction yzFunc)
    {
        return new ComputedParametricBivector3D(
            parameterRange,
            t =>
                LinFloat64Bivector3D.Create(
                    xyFunc.GetValue(t),
                    xzFunc.GetValue(t),
                    yzFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector3D Create(Func<double, double> xyFunc, Func<double, double> xzFunc, Func<double, double> yzFunc)
    {
        return Create(
            Float64ScalarRange.Infinite,
            xyFunc,
            xzFunc,
            yzFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector3D Create(Float64ScalarRange parameterRange, Func<double, double> xyFunc, Func<double, double> xzFunc, Func<double, double> yzFunc)
    {
        return new ComputedParametricBivector3D(
            parameterRange,
            t =>
                LinFloat64Bivector3D.Create(
                    xyFunc(t),
                    xzFunc(t),
                    yzFunc(t)
                )
        );
    }


    public Float64ScalarRange TimeRange { get; }

    public Func<double, LinFloat64Bivector3D> GetBivectorFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricBivector3D(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector3D> getBivectorFunc)
    {
        TimeRange = parameterRange;
        GetBivectorFunc = getBivectorFunc;
        //GetTangentFunc = 
        //    t =>
        //    {
        //        const double zeroEpsilon = 1e-7;

        //        var p1 = getBivectorFunc(t - zeroEpsilon);
        //        var p2 = getBivectorFunc(t + zeroEpsilon);

        //        return (p2 - p1) / (2 * zeroEpsilon);
        //    };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private ComputedParametricBivector3D(Float64ScalarRange parameterRange, Func<double, Float64Bivector3D> getBivectorFunc, Func<double, Float64Bivector3D> getTangentFunc)
    //{
    //    ParameterRange = parameterRange;
    //    GetBivectorFunc = getBivectorFunc;
    //    GetTangentFunc = getTangentFunc;
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D GetValue(double parameterValue)
    {
        return GetBivectorFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3D GetNormalVectorCurve(LinFloat64Vector3D? zeroNormal = null)
    {
        return Float64ComputedPath3D.Finite(
            TimeRange,
            t => GetValue(t).DirectionToNormal3D(zeroNormal)
        );
    }
}