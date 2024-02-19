using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;

public static class RGaConformalIntersectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement Intersect(this RGaConformalElement element1, RGaConformalElement element2)
    {
        return element2.EncodeIpnsBlade()
            .Op(element1.EncodeIpnsBlade())
            .DecodeIpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Intersect(this RGaConformalElement element1, RGaConformalParametricElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element2.ParameterRange,
            t => element1.Intersect(element2.GetElement(t))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Intersect(this RGaConformalParametricElement element1, RGaConformalElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element1.ParameterRange,
            t => element1.GetElement(t).Intersect(element2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Intersect(this RGaConformalParametricElement element1, RGaConformalParametricElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element1.ParameterRange.Intersect(element2.ParameterRange),
            t => element1.GetElement(t).Intersect(element2.GetElement(t))
        );
    }

        
    /// <summary>
    /// Intersect this EGA Blade with another EGA Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectEGa(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        Debug.Assert(
            blade1.IsEGaBlade() &&
            blade2.IsEGaBlade()
        );

        return blade2.Op(blade1);
    }

    /// <summary>
    /// Intersect this EGA Blade with two other EGA Blades
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectEGa(this RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        Debug.Assert(
            blade1.IsEGaBlade() &&
            blade2.IsEGaBlade() &&
            blade3.IsEGaBlade()
        );

        return blade3.Op(blade2).Op(blade1);
    }
        

    /// <summary>
    /// Intersect this PGA Blade with another PGA Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectPGa(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        Debug.Assert(
            blade1.IsPGaBlade() &&
            blade2.IsPGaBlade()
        );

        return blade2.Op(blade1);
    }

    /// <summary>
    /// Intersect this PGA Blade with two other PGA Blades
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectPGa(this RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        Debug.Assert(
            blade1.IsPGaBlade() &&
            blade2.IsPGaBlade() &&
            blade3.IsPGaBlade()
        );

        return blade3.Op(blade2).Op(blade1);
    }


    /// <summary>
    /// Intersect this CGA OPNS Blade with another CGA OPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectOpns(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return blade2.CGaDual().Lcp(blade1);
    }

    /// <summary>
    /// Intersect this CGA OPNS Blade with two other CGA OPNS Blades
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectOpns(this RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        return blade1.IntersectOpns(blade2).IntersectOpns(blade3);
    }


    /// <summary>
    /// Intersect this CGA IPNS Blade with another CGA IPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectIpns(this RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return blade2.Op(blade1);
    }

    /// <summary>
    /// Intersect this CGA IPNS Blade with two other CGA IPNS Blades
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IntersectIpns(this RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade blade3)
    {
        return blade3.Op(blade2).Op(blade1);
    }
}