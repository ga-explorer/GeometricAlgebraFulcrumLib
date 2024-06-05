using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;

public static class RGaConformalReflectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement ReflectOn(this RGaConformalElement element1, RGaConformalElement element2)
    {
        return element1
            .EncodeOpnsBlade()
            .ReflectOpnsOn(element2.EncodeOpnsBlade())
            .DecodeOpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ReflectOn(this RGaConformalElement element1, RGaConformalParametricElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element2.ParameterRange,
            t => element1.ReflectOn(element2.GetElement(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ReflectOn(this RGaConformalParametricElement element1, RGaConformalElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element1.ParameterRange,
            t => element1.GetElement(t).ReflectOn(element2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ReflectOn(this RGaConformalParametricElement element1, RGaConformalParametricElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element1.ParameterRange.Intersect(element2.ParameterRange),
            t => element1.GetElement(t).ReflectOn(element2.GetElement(t))
        );
    }


    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ReflectOpnsOn(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            blade1.ConformalSpace,
            blade1.ConformalSpace.ReflectOpnsOnOpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ReflectOpnsOnIpns(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            blade1.ConformalSpace,
            blade1.ConformalSpace.ReflectOpnsOnIpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ReflectIpnsOnOpns(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            blade1.ConformalSpace,
            blade1.ConformalSpace.ReflectIpnsOnOpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ReflectIpnsOn(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return new RGaConformalBlade(
            blade1.ConformalSpace,
            blade1.ConformalSpace.ReflectIpnsOnIpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

}