using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

public static class CGaReflectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> ReflectOn<T>(this CGaElement<T> element1, CGaElement<T> element2)
    {
        return element1
            .EncodeOpnsBlade()
            .ReflectOpnsOn(element2.EncodeOpnsBlade())
            .Decode.OpnsElement();
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
    public static CGaBlade<T> ReflectOpnsOn<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            blade1.GeometricSpace,
            blade1.GeometricSpace.ReflectOpnsOnOpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ReflectOpnsOnIpns<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            blade1.GeometricSpace,
            blade1.GeometricSpace.ReflectOpnsOnIpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ReflectIpnsOnOpns<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            blade1.GeometricSpace,
            blade1.GeometricSpace.ReflectIpnsOnOpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ReflectIpnsOn<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return new CGaBlade<T>(
            blade1.GeometricSpace,
            blade1.GeometricSpace.ReflectIpnsOnIpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

}