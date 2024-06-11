using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

public static class CGaScalingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> ScaleBy<T>(this CGaElement<T> element, T scalingFactor)
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
    public static CGaDirection<T> ScaleBy<T>(this CGaDirection<T> element, T scalingFactor)
    {
        return new CGaDirection<T>(
            element.GeometricSpace,
            element.Weight,
            element.Direction.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> ScaleBy<T>(this CGaTangent<T> element, T scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> ScaleBy<T>(this CGaFlat<T> element, T scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> ScaleBy<T>(this CGaRound<T> element, T scalingFactor)
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
    public static CGaBlade<T> ScaleBy<T>(this CGaBlade<T> blade, T scalingFactor)
    {
        var scalarProcessor = blade.ScalarProcessor;

        Debug.Assert(
            scalarProcessor.IsValid(scalingFactor) &&
            scalarProcessor.IsPositive(scalingFactor)
        );

        var g = scalarProcessor.LogE(scalingFactor) / 2;
        var s0 = g.Cosh();
        var s2 = g.Sinh() * blade.GeometricSpace.Eoi.InternalKVector;

        var kVector =
            (s0 + s2).Gp(blade.InternalKVector).Gp(s0 - s2).GetKVectorPart(blade.InternalKVector.Grade);

        return new CGaBlade<T>(blade.GeometricSpace, kVector);
    }
}