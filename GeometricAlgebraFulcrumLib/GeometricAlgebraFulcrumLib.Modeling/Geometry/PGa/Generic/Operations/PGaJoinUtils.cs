using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Operations;

public static class PGaJoinUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> Join<T>(this PGaElement<T> element1, PGaElement<T> element2)
    {
        return element1
            .EncodeBlade()
            .PGaDual()
            .Op(element2.EncodeBlade().PGaDual())
            .PGaUnDual()
            .DecodeElement();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> Join<T>(this XGaConformalElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element2.ParameterRange,
    //        t => element1.Join(element2.GetElement(t))
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> Join<T>(this XGaConformalParametricElement<T> element1, XGaConformalElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange,
    //        t => element1.GetElement(t).Join(element2)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> Join<T>(this XGaConformalParametricElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange.Join(element2.ParameterRange),
    //        t => element1.GetElement(t).Join(element2.GetElement(t))
    //    );
    //}


    /// <summary>
    /// Intersect this PGA Blade with another PGA Blade
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> Join<T>(this PGaBlade<T> blade1, PGaBlade<T> blade2)
    {
        Debug.Assert(
            blade1.IsPGaBlade() &&
            blade2.IsPGaBlade()
        );

        return blade1.PGaDual().Op(blade2.PGaDual()).PGaUnDual();
    }

    /// <summary>
    /// Intersect this PGA Blade with two other PGA Blades
    /// </summary>
    /// <param name="blade1"></param>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> Join<T>(this PGaBlade<T> blade1, PGaBlade<T> blade2, PGaBlade<T> blade3)
    {
        Debug.Assert(
            blade1.IsPGaBlade() &&
            blade2.IsPGaBlade() &&
            blade3.IsPGaBlade()
        );

        return blade1.PGaDual().Op(blade2.PGaDual()).Op(blade3.PGaDual()).PGaUnDual();
    }
}