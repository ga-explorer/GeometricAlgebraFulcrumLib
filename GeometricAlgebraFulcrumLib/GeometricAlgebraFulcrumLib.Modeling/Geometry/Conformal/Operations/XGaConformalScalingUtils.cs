using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

public static class XGaConformalScalingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> ScaleBy<T>(this XGaConformalElement<T> element, T scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsElement();
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ScaleBy<T>(this XGaConformalElement<T> element, IParametricScalar<T> scalingFactor)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        scalingFactor.ParameterRange,
    //        t => element.ScaleBy(scalingFactor.GetValue(t))
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ScaleBy<T>(this XGaConformalParametricElement<T> element, T scalingFactor)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => element.GetElement(t).ScaleBy(scalingFactor)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ScaleBy<T>(this XGaConformalParametricElement<T> element, IParametricScalar<T> scalingFactor)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange.Intersect(scalingFactor.ParameterRange),
    //        t => element.GetElement(t).ScaleBy(scalingFactor.GetValue(t))
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> ScaleBy<T>(this XGaConformalDirection<T> element, T scalingFactor)
    {
        return new XGaConformalDirection<T>(
            element.ConformalSpace, 
            element.Weight, 
            element.Direction.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> ScaleBy<T>(this XGaConformalTangent<T> element, T scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsTangent();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> ScaleBy<T>(this XGaConformalFlat<T> element, T scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsFlat();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> ScaleBy<T>(this XGaConformalRound<T> element, T scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsRound();
    }


    /// <summary>
    /// Apply a uniform scaling to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> ScaleBy<T>(this XGaConformalBlade<T> blade, T scalingFactor)
    {
        var scalarProcessor = blade.ScalarProcessor;

        Debug.Assert(
            scalarProcessor.IsValid(scalingFactor) &&
            scalarProcessor.IsPositive(scalingFactor)
        );

        var g = scalarProcessor.LogE(scalingFactor) / 2;
        var s0 = g.Cosh();
        var s2 = g.Sinh() * blade.ConformalSpace.Eoi.InternalKVector;

        var kVector =
            (s0 + s2).Gp(blade.InternalKVector).Gp(s0 - s2).GetKVectorPart(blade.InternalKVector.Grade);

        return new XGaConformalBlade<T>(blade.ConformalSpace, kVector);
    }
}