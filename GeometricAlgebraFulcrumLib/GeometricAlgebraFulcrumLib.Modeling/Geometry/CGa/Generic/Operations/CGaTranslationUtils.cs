using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

public static class CGaTranslationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> TranslateBy<T>(this CGaElement<T> element, LinVector2D<T> egaVector)
    {
        return element switch
        {
            CGaDirection<T> el => el,
            CGaTangent<T> el => el.TranslateBy(egaVector),
            CGaFlat<T> el => el.TranslateBy(egaVector),
            CGaRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> TranslateBy<T>(this CGaElement<T> element, LinVector3D<T> egaVector)
    {
        return element switch
        {
            CGaDirection<T> el => el,
            CGaTangent<T> el => el.TranslateBy(egaVector),
            CGaFlat<T> el => el.TranslateBy(egaVector),
            CGaRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> TranslateBy<T>(this CGaElement<T> element, LinVector<T> egaVector)
    {
        return element switch
        {
            CGaDirection<T> el => el,
            CGaTangent<T> el => el.TranslateBy(egaVector),
            CGaFlat<T> el => el.TranslateBy(egaVector),
            CGaRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> TranslateBy<T>(this CGaElement<T> element, XGaVector<T> egaVector)
    {
        return element switch
        {
            CGaDirection<T> el => el,
            CGaTangent<T> el => el.TranslateBy(egaVector),
            CGaFlat<T> el => el.TranslateBy(egaVector),
            CGaRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> TranslateBy<T>(this CGaElement<T> element, CGaBlade<T> egaVector)
    {
        return element switch
        {
            CGaDirection<T> el => el,
            CGaTangent<T> el => el.TranslateBy(egaVector),
            CGaFlat<T> el => el.TranslateBy(egaVector),
            CGaRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> TranslateBy<T>(this XGaConformalElement<T> element, IParametricCurve3D egaVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        egaVector.ParameterRange,
    //        t => element.TranslateBy(egaVector.GetPoint(t))
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> TranslateBy<T>(this XGaConformalParametricElement<T> element, LinVector2D<T> egaVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => element.GetElement(t).TranslateBy(egaVector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> TranslateBy<T>(this XGaConformalParametricElement<T> element, LinVector3D<T> egaVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => element.GetElement(t).TranslateBy(egaVector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> TranslateBy<T>(this XGaConformalParametricElement<T> element, LinVector<T> egaVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => element.GetElement(t).TranslateBy(egaVector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> TranslateBy<T>(this XGaConformalParametricElement<T> element, XGaVector<T> egaVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => element.GetElement(t).TranslateBy(egaVector)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> TranslateBy<T>(this XGaConformalParametricElement<T> element, IParametricCurve3D egaVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange.Intersect(egaVector.ParameterRange),
    //        t => element.GetElement(t).TranslateBy(egaVector.GetPoint(t))
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> TranslateBy<T>(this CGaTangent<T> element, LinVector2D<T> egaVector)
    {
        return new CGaTangent<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> TranslateBy<T>(this CGaTangent<T> element, LinVector3D<T> egaVector)
    {
        return new CGaTangent<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> TranslateBy<T>(this CGaTangent<T> element, LinVector<T> egaVector)
    {
        return new CGaTangent<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> TranslateBy<T>(this CGaTangent<T> element, XGaVector<T> egaVector)
    {
        return new CGaTangent<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> TranslateBy<T>(this CGaTangent<T> element, CGaBlade<T> egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaTangent<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> TranslateBy<T>(this CGaFlat<T> element, LinVector2D<T> egaVector)
    {
        return new CGaFlat<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> TranslateBy<T>(this CGaFlat<T> element, LinVector3D<T> egaVector)
    {
        return new CGaFlat<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> TranslateBy<T>(this CGaFlat<T> element, LinVector<T> egaVector)
    {
        return new CGaFlat<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> TranslateBy<T>(this CGaFlat<T> element, XGaVector<T> egaVector)
    {
        return new CGaFlat<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> TranslateBy<T>(this CGaFlat<T> element, CGaBlade<T> egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaFlat<T>(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> TranslateBy<T>(this CGaRound<T> element, LinVector2D<T> egaVector)
    {
        return new CGaRound<T>(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> TranslateBy<T>(this CGaRound<T> element, LinVector3D<T> egaVector)
    {
        return new CGaRound<T>(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> TranslateBy<T>(this CGaRound<T> element, LinVector<T> egaVector)
    {
        return new CGaRound<T>(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> TranslateBy<T>(this CGaRound<T> element, XGaVector<T> egaVector)
    {
        return new CGaRound<T>(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVectorBlade(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> TranslateBy<T>(this CGaRound<T> element, CGaBlade<T> egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaRound<T>(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector,
            element.Direction
        );
    }


    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslateBy<T>(this CGaBlade<T> blade, LinVector2D<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslateBy<T>(this CGaBlade<T> blade, LinVector3D<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslateBy<T>(this CGaBlade<T> blade, LinVector<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslateBy<T>(this CGaBlade<T> blade, XGaVector<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslateBy<T>(this CGaBlade<T> blade, CGaBlade<T> egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsVGaVector()
        );

        var eit =
            0.5d * blade.GeometricSpace.Ei.InternalKVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(blade.InternalKVector).Gp(1 - eit).GetKVectorPart(blade.Grade);

        return new CGaBlade<T>(blade.GeometricSpace, kVector);
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslatePGaBy<T>(this CGaBlade<T> blade, LinVector2D<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslatePGaBy<T>(this CGaBlade<T> blade, LinVector3D<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslatePGaBy<T>(this CGaBlade<T> blade, LinVector<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslatePGaBy<T>(this CGaBlade<T> blade, XGaVector<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> TranslatePGaBy<T>(this CGaBlade<T> blade, CGaBlade<T> egaTranslationVector)
    {
        Debug.Assert(
            blade.IsPGaBlade() &&
            egaTranslationVector.IsVGaBlade()
        );

        var eot =
            0.5d * blade.GeometricSpace.EoVector.Op(egaTranslationVector.InternalVector);

        var kVector =
            (1 - eot).Gp(blade.InternalKVector).Gp(1 + eot).GetKVectorPart(blade.InternalKVector.Grade);

        return new CGaBlade<T>(blade.GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> TranslateBy<T>(this CGaVersor<T> versor, LinVector2D<T> egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> TranslateBy<T>(this CGaVersor<T> versor, LinVector3D<T> egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> TranslateBy<T>(this CGaVersor<T> versor, LinVector<T> egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.GeometricSpace.EncodeVGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> TranslateBy<T>(this CGaVersor<T> versor, CGaBlade<T> egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsVGaVector()
        );

        var eit =
            0.5d * versor.GeometricSpace.EiVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(versor.InternalMultivector).Gp(1 - eit);

        return new CGaVersor<T>(versor.GeometricSpace, kVector);
    }
}