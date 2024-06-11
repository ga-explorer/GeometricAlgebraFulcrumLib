using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

public static class CGaMeetUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> Meet<T>(this CGaElement<T> element1, CGaElement<T> element2)
    {
        return element2.EncodeIpnsBlade()
            .Op(element1.EncodeIpnsBlade())
            .DecodeIpnsElement();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> Meet<T>(this XGaConformalElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element2.ParameterRange,
    //        t => element1.Meet(element2.GetElement(t))
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> Meet<T>(this XGaConformalParametricElement<T> element1, XGaConformalElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange,
    //        t => element1.GetElement(t).Meet(element2)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> Meet<T>(this XGaConformalParametricElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange.Meet(element2.ParameterRange),
    //        t => element1.GetElement(t).Meet(element2.GetElement(t))
    //    );
    //}


    /// <summary>
    /// Intersect this EGA Blade with another EGA Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> MeetVGa<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
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
    public static CGaBlade<T> MeetVGa<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> blade3)
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
    public static CGaBlade<T> MeetPGa<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
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
    public static CGaBlade<T> MeetPGa<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> blade3)
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
    public static CGaBlade<T> MeetOpns<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
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
    public static CGaBlade<T> MeetOpns<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> blade3)
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
    public static CGaBlade<T> MeetIpns<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2)
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
    public static CGaBlade<T> MeetIpns<T>(this CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> blade3)
    {
        return blade3.Op(blade2).Op(blade1);
    }
}