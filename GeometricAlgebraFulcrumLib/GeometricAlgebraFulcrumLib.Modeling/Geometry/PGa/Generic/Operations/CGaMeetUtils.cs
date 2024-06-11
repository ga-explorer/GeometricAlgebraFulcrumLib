using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Operations;

public static class PGaMeetUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> Meet<T>(this PGaElement<T> element1, PGaElement<T> element2)
    {
        return element1
            .EncodeBlade()
            .Op(element2.EncodeBlade())
            .DecodeElement();
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
    /// Intersect this PGA Blade with another PGA Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> Meet<T>(this PGaBlade<T> blade1, PGaBlade<T> blade2)
    {
        Debug.Assert(
            blade1.IsPGaBlade() &&
            blade2.IsPGaBlade()
        );

        return blade1.Op(blade2);
    }

    /// <summary>
    /// Intersect this PGA Blade with two other PGA Blades
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> Meet<T>(this PGaBlade<T> blade1, PGaBlade<T> blade2, PGaBlade<T> blade3)
    {
        Debug.Assert(
            blade1.IsPGaBlade() &&
            blade2.IsPGaBlade() &&
            blade3.IsPGaBlade()
        );

        return blade1.Op(blade2).Op(blade3);
    }
}