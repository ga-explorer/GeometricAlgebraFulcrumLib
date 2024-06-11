using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64MeetUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element Meet(this CGaFloat64Element element1, CGaFloat64Element element2)
    {
        return element2.EncodeIpnsBlade()
            .Op(element1.EncodeIpnsBlade())
            .DecodeIpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Meet(this CGaFloat64Element element1, CGaFloat64ParametricElement element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element2.ParameterRange,
            t => element1.Meet(element2.GetElement(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Meet(this CGaFloat64ParametricElement element1, CGaFloat64Element element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element1.ParameterRange,
            t => element1.GetElement(t).Meet(element2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Meet(this CGaFloat64ParametricElement element1, CGaFloat64ParametricElement element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element1.ParameterRange.Intersect(element2.ParameterRange),
            t => element1.GetElement(t).Meet(element2.GetElement(t))
        );
    }


    /// <summary>
    /// Intersect this EGA Blade with another EGA Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade MeetVGa(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        Debug.Assert(
            blade1.IsVGaBlade() &&
            blade2.IsVGaBlade()
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
    public static CGaFloat64Blade MeetVGa(this CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        Debug.Assert(
            blade1.IsVGaBlade() &&
            blade2.IsVGaBlade() &&
            blade3.IsVGaBlade()
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
    public static CGaFloat64Blade MeetPGa(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
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
    public static CGaFloat64Blade MeetPGa(this CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade blade3)
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
    public static CGaFloat64Blade MeetOpns(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
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
    public static CGaFloat64Blade MeetOpns(this CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        return blade1.MeetOpns(blade2).MeetOpns(blade3);
    }


    /// <summary>
    /// Intersect this CGA IPNS Blade with another CGA IPNS Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade MeetIpns(this CGaFloat64Blade blade1, CGaFloat64Blade blade2)
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
    public static CGaFloat64Blade MeetIpns(this CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        return blade3.Op(blade2).Op(blade1);
    }
}