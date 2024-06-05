using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

public static class XGaConformalReflectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> ReflectOn<T>(this XGaConformalElement<T> element1, XGaConformalElement<T> element2)
    {
        return element1
            .EncodeOpnsBlade()
            .ReflectOpnsOn(element2.EncodeOpnsBlade())
            .DecodeOpnsElement();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ReflectOn<T>(this XGaConformalElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element2.ParameterRange,
    //        t => element1.ReflectOn(element2.GetElement(t))
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ReflectOn<T>(this XGaConformalParametricElement<T> element1, XGaConformalElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange,
    //        t => element1.GetElement(t).ReflectOn(element2)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ReflectOn<T>(this XGaConformalParametricElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange.Intersect(element2.ParameterRange),
    //        t => element1.GetElement(t).ReflectOn(element2.GetElement(t))
    //    );
    //}


    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> ReflectOpnsOn<T>(this XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
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
    public static XGaConformalBlade<T> ReflectOpnsOnIpns<T>(this XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
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
    public static XGaConformalBlade<T> ReflectIpnsOnOpns<T>(this XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
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
    public static XGaConformalBlade<T> ReflectIpnsOn<T>(this XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return new XGaConformalBlade<T>(
            blade1.ConformalSpace,
            blade1.ConformalSpace.ReflectIpnsOnIpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

}