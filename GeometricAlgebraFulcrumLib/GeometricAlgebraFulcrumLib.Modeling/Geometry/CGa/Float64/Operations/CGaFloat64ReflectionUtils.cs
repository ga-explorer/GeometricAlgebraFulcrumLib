using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64ReflectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element ReflectOn(this CGaFloat64Element element1, CGaFloat64Element element2)
    {
        return element1
            .EncodeOpnsBlade()
            .ReflectOpnsOn(element2.EncodeOpnsBlade())
            .DecodeOpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ReflectOn(this CGaFloat64Element element1, CGaFloat64ParametricElement element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element2.ParameterRange,
            t => element1.ReflectOn(element2.GetElement(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ReflectOn(this CGaFloat64ParametricElement element1, CGaFloat64Element element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element1.ParameterRange,
            t => element1.GetElement(t).ReflectOn(element2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ReflectOn(this CGaFloat64ParametricElement element1, CGaFloat64ParametricElement element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
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
    public static CGaFloat64Blade ReflectOpnsOn(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
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
    public static CGaFloat64Blade ReflectOpnsOnIpns(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
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
    public static CGaFloat64Blade ReflectIpnsOnOpns(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
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
    public static CGaFloat64Blade ReflectIpnsOn(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
            blade1.GeometricSpace,
            blade1.GeometricSpace.ReflectIpnsOnIpns(blade1.InternalKVector, blade2.InternalKVector)
        );
    }

}