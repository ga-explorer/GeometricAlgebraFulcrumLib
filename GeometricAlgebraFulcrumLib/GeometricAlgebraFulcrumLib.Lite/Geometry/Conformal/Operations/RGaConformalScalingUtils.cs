using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;

public static class RGaConformalScalingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement ScaleBy(this RGaConformalElement element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ScaleBy(this RGaConformalElement element, IParametricScalar scalingFactor)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            scalingFactor.ParameterRange,
            t => element.ScaleBy(scalingFactor.GetValue(t).Value)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ScaleBy(this RGaConformalParametricElement element, double scalingFactor)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => element.GetElement(t).ScaleBy(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ScaleBy(this RGaConformalParametricElement element, IParametricScalar scalingFactor)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange.Intersect(scalingFactor.ParameterRange),
            t => element.GetElement(t).ScaleBy(scalingFactor.GetValue(t).Value)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection ScaleBy(this RGaConformalDirection element, double scalingFactor)
    {
        return new RGaConformalDirection(
            element.ConformalSpace, 
            element.Weight, 
            element.Direction.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent ScaleBy(this RGaConformalTangent element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsTangent();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat ScaleBy(this RGaConformalFlat element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsFlat();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound ScaleBy(this RGaConformalRound element, double scalingFactor)
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
    public static RGaConformalBlade ScaleBy(this RGaConformalBlade blade, double scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = 0.5 * scalingFactor.LogE();
        var s0 = Math.Cosh(g);
        var s2 = Math.Sinh(g) * blade.ConformalSpace.Eoi.InternalKVector;

        var kVector =
            (s0 + s2).Gp(blade.InternalKVector).Gp(s0 - s2).GetKVectorPart(blade.InternalKVector.Grade);

        return new RGaConformalBlade(blade.ConformalSpace, kVector);
    }
}