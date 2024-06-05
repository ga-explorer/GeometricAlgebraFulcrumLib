using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

public static class XGaConformalTranslationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> TranslateBy<T>(this XGaConformalElement<T> element, LinVector2D<T> egaVector)
    {
        return element switch
        {
            XGaConformalDirection<T> el => el,
            XGaConformalTangent<T> el => el.TranslateBy(egaVector),
            XGaConformalFlat<T> el => el.TranslateBy(egaVector),
            XGaConformalRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> TranslateBy<T>(this XGaConformalElement<T> element, LinVector3D<T> egaVector)
    {
        return element switch
        {
            XGaConformalDirection<T> el => el,
            XGaConformalTangent<T> el => el.TranslateBy(egaVector),
            XGaConformalFlat<T> el => el.TranslateBy(egaVector),
            XGaConformalRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> TranslateBy<T>(this XGaConformalElement<T> element, LinVector<T> egaVector)
    {
        return element switch
        {
            XGaConformalDirection<T> el => el,
            XGaConformalTangent<T> el => el.TranslateBy(egaVector),
            XGaConformalFlat<T> el => el.TranslateBy(egaVector),
            XGaConformalRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> TranslateBy<T>(this XGaConformalElement<T> element, XGaVector<T> egaVector)
    {
        return element switch
        {
            XGaConformalDirection<T> el => el,
            XGaConformalTangent<T> el => el.TranslateBy(egaVector),
            XGaConformalFlat<T> el => el.TranslateBy(egaVector),
            XGaConformalRound<T> el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> TranslateBy<T>(this XGaConformalElement<T> element, XGaConformalBlade<T> egaVector)
    {
        return element switch
        {
            XGaConformalDirection<T> el => el,
            XGaConformalTangent<T> el => el.TranslateBy(egaVector),
            XGaConformalFlat<T> el => el.TranslateBy(egaVector),
            XGaConformalRound<T> el => el.TranslateBy(egaVector),
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
    public static XGaConformalTangent<T> TranslateBy<T>(this XGaConformalTangent<T> element, LinVector2D<T> egaVector)
    {
        return new XGaConformalTangent<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> TranslateBy<T>(this XGaConformalTangent<T> element, LinVector3D<T> egaVector)
    {
        return new XGaConformalTangent<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> TranslateBy<T>(this XGaConformalTangent<T> element, LinVector<T> egaVector)
    {
        return new XGaConformalTangent<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> TranslateBy<T>(this XGaConformalTangent<T> element, XGaVector<T> egaVector)
    {
        return new XGaConformalTangent<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> TranslateBy<T>(this XGaConformalTangent<T> element, XGaConformalBlade<T> egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return new XGaConformalTangent<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> TranslateBy<T>(this XGaConformalFlat<T> element, LinVector2D<T> egaVector)
    {
        return new XGaConformalFlat<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> TranslateBy<T>(this XGaConformalFlat<T> element, LinVector3D<T> egaVector)
    {
        return new XGaConformalFlat<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> TranslateBy<T>(this XGaConformalFlat<T> element, LinVector<T> egaVector)
    {
        return new XGaConformalFlat<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> TranslateBy<T>(this XGaConformalFlat<T> element, XGaVector<T> egaVector)
    {
        return new XGaConformalFlat<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> TranslateBy<T>(this XGaConformalFlat<T> element, XGaConformalBlade<T> egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return new XGaConformalFlat<T>(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> TranslateBy<T>(this XGaConformalRound<T> element, LinVector2D<T> egaVector)
    {
        return new XGaConformalRound<T>(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> TranslateBy<T>(this XGaConformalRound<T> element, LinVector3D<T> egaVector)
    {
        return new XGaConformalRound<T>(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> TranslateBy<T>(this XGaConformalRound<T> element, LinVector<T> egaVector)
    {
        return new XGaConformalRound<T>(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> TranslateBy<T>(this XGaConformalRound<T> element, XGaVector<T> egaVector)
    {
        return new XGaConformalRound<T>(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> TranslateBy<T>(this XGaConformalRound<T> element, XGaConformalBlade<T> egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return new XGaConformalRound<T>(
            element.ConformalSpace,
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
    public static XGaConformalBlade<T> TranslateBy<T>(this XGaConformalBlade<T> blade, LinVector2D<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslateBy<T>(this XGaConformalBlade<T> blade, LinVector3D<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslateBy<T>(this XGaConformalBlade<T> blade, LinVector<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslateBy<T>(this XGaConformalBlade<T> blade, XGaVector<T> egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslateBy<T>(this XGaConformalBlade<T> blade, XGaConformalBlade<T> egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsEGaVector()
        );

        var eit =
            0.5d * blade.ConformalSpace.Ei.InternalKVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(blade.InternalKVector).Gp(1 - eit).GetKVectorPart(blade.Grade);

        return new XGaConformalBlade<T>(blade.ConformalSpace, kVector);
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslatePGaBy<T>(this XGaConformalBlade<T> blade, LinVector2D<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslatePGaBy<T>(this XGaConformalBlade<T> blade, LinVector3D<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslatePGaBy<T>(this XGaConformalBlade<T> blade, LinVector<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslatePGaBy<T>(this XGaConformalBlade<T> blade, XGaVector<T> egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> TranslatePGaBy<T>(this XGaConformalBlade<T> blade, XGaConformalBlade<T> egaTranslationVector)
    {
        Debug.Assert(
            blade.IsPGaBlade() &&
            egaTranslationVector.IsEGaBlade()
        );

        var eot = 
            0.5d * blade.ConformalSpace.EoVector.Op(egaTranslationVector.InternalVector);

        var kVector = 
            (1 - eot).Gp(blade.InternalKVector).Gp(1 + eot).GetKVectorPart(blade.InternalKVector.Grade);

        return new XGaConformalBlade<T>(blade.ConformalSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> TranslateBy<T>(this XGaConformalVersor<T> versor, LinVector2D<T> egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> TranslateBy<T>(this XGaConformalVersor<T> versor, LinVector3D<T> egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> TranslateBy<T>(this XGaConformalVersor<T> versor, LinVector<T> egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> TranslateBy<T>(this XGaConformalVersor<T> versor, XGaConformalBlade<T> egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsEGaVector()
        );

        var eit =
            0.5d * versor.ConformalSpace.EiVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(versor.InternalMultivector).Gp(1 - eit);

        return new XGaConformalVersor<T>(versor.ConformalSpace, kVector);
    }
}