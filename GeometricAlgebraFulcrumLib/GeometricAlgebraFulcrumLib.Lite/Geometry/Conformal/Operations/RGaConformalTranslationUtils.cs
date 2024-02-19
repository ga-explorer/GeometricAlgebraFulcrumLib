using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Versors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;

public static class RGaConformalTranslationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement TranslateBy(this RGaConformalElement element, Float64Vector2D egaVector)
    {
        return element switch
        {
            RGaConformalDirection el => el,
            RGaConformalTangent el => el.TranslateBy(egaVector),
            RGaConformalFlat el => el.TranslateBy(egaVector),
            RGaConformalRound el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement TranslateBy(this RGaConformalElement element, Float64Vector3D egaVector)
    {
        return element switch
        {
            RGaConformalDirection el => el,
            RGaConformalTangent el => el.TranslateBy(egaVector),
            RGaConformalFlat el => el.TranslateBy(egaVector),
            RGaConformalRound el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement TranslateBy(this RGaConformalElement element, Float64Vector egaVector)
    {
        return element switch
        {
            RGaConformalDirection el => el,
            RGaConformalTangent el => el.TranslateBy(egaVector),
            RGaConformalFlat el => el.TranslateBy(egaVector),
            RGaConformalRound el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement TranslateBy(this RGaConformalElement element, RGaFloat64Vector egaVector)
    {
        return element switch
        {
            RGaConformalDirection el => el,
            RGaConformalTangent el => el.TranslateBy(egaVector),
            RGaConformalFlat el => el.TranslateBy(egaVector),
            RGaConformalRound el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement TranslateBy(this RGaConformalElement element, RGaConformalBlade egaVector)
    {
        return element switch
        {
            RGaConformalDirection el => el,
            RGaConformalTangent el => el.TranslateBy(egaVector),
            RGaConformalFlat el => el.TranslateBy(egaVector),
            RGaConformalRound el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).DecodeOpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement TranslateBy(this RGaConformalElement element, IParametricCurve3D egaVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            egaVector.ParameterRange,
            t => element.TranslateBy(egaVector.GetPoint(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement TranslateBy(this RGaConformalParametricElement element, Float64Vector2D egaVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement TranslateBy(this RGaConformalParametricElement element, Float64Vector3D egaVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement TranslateBy(this RGaConformalParametricElement element, Float64Vector egaVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement TranslateBy(this RGaConformalParametricElement element, RGaFloat64Vector egaVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement TranslateBy(this RGaConformalParametricElement element, IParametricCurve3D egaVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange.Intersect(egaVector.ParameterRange),
            t => element.GetElement(t).TranslateBy(egaVector.GetPoint(t))
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent TranslateBy(this RGaConformalTangent element, Float64Vector2D egaVector)
    {
        return new RGaConformalTangent(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent TranslateBy(this RGaConformalTangent element, Float64Vector3D egaVector)
    {
        return new RGaConformalTangent(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent TranslateBy(this RGaConformalTangent element, Float64Vector egaVector)
    {
        return new RGaConformalTangent(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent TranslateBy(this RGaConformalTangent element, RGaFloat64Vector egaVector)
    {
        return new RGaConformalTangent(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent TranslateBy(this RGaConformalTangent element, RGaConformalBlade egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return new RGaConformalTangent(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat TranslateBy(this RGaConformalFlat element, Float64Vector2D egaVector)
    {
        return new RGaConformalFlat(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat TranslateBy(this RGaConformalFlat element, Float64Vector3D egaVector)
    {
        return new RGaConformalFlat(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat TranslateBy(this RGaConformalFlat element, Float64Vector egaVector)
    {
        return new RGaConformalFlat(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat TranslateBy(this RGaConformalFlat element, RGaFloat64Vector egaVector)
    {
        return new RGaConformalFlat(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat TranslateBy(this RGaConformalFlat element, RGaConformalBlade egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return new RGaConformalFlat(
            element.ConformalSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound TranslateBy(this RGaConformalRound element, Float64Vector2D egaVector)
    {
        return new RGaConformalRound(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound TranslateBy(this RGaConformalRound element, Float64Vector3D egaVector)
    {
        return new RGaConformalRound(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound TranslateBy(this RGaConformalRound element, Float64Vector egaVector)
    {
        return new RGaConformalRound(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound TranslateBy(this RGaConformalRound element, RGaFloat64Vector egaVector)
    {
        return new RGaConformalRound(
            element.ConformalSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeEGaVectorBlade(element.ConformalSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound TranslateBy(this RGaConformalRound element, RGaConformalBlade egaVector)
    {
        Debug.Assert(egaVector.IsEGaVector());

        return new RGaConformalRound(
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
    public static RGaConformalBlade TranslateBy(this RGaConformalBlade blade, Float64Vector2D egaTranslationVector)
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
    public static RGaConformalBlade TranslateBy(this RGaConformalBlade blade, Float64Vector3D egaTranslationVector)
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
    public static RGaConformalBlade TranslateBy(this RGaConformalBlade blade, Float64Vector egaTranslationVector)
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
    public static RGaConformalBlade TranslateBy(this RGaConformalBlade blade, RGaFloat64Vector egaTranslationVector)
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
    public static RGaConformalBlade TranslateBy(this RGaConformalBlade blade, RGaConformalBlade egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsEGaVector()
        );

        var eit =
            0.5d * blade.ConformalSpace.Ei.InternalKVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(blade.InternalKVector).Gp(1 - eit).GetKVectorPart(blade.Grade);

        return new RGaConformalBlade(blade.ConformalSpace, kVector);
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade TranslatePGaBy(this RGaConformalBlade blade, Float64Vector2D egaTranslationVector)
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
    public static RGaConformalBlade TranslatePGaBy(this RGaConformalBlade blade, Float64Vector3D egaTranslationVector)
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
    public static RGaConformalBlade TranslatePGaBy(this RGaConformalBlade blade, Float64Vector egaTranslationVector)
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
    public static RGaConformalBlade TranslatePGaBy(this RGaConformalBlade blade, RGaFloat64Vector egaTranslationVector)
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
    public static RGaConformalBlade TranslatePGaBy(this RGaConformalBlade blade, RGaConformalBlade egaTranslationVector)
    {
        Debug.Assert(
            blade.IsPGaBlade() &&
            egaTranslationVector.IsEGaBlade()
        );

        var eot = 
            0.5d * blade.ConformalSpace.EoVector.Op(egaTranslationVector.InternalVector);

        var kVector = 
            (1 - eot).Gp(blade.InternalKVector).Gp(1 + eot).GetKVectorPart(blade.InternalKVector.Grade);

        return new RGaConformalBlade(blade.ConformalSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor TranslateBy(this RGaConformalVersor versor, Float64Vector2D egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor TranslateBy(this RGaConformalVersor versor, Float64Vector3D egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor TranslateBy(this RGaConformalVersor versor, Float64Vector egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.ConformalSpace.EncodeEGaVector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalVersor TranslateBy(this RGaConformalVersor versor, RGaConformalBlade egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsEGaVector()
        );

        var eit =
            0.5d * versor.ConformalSpace.EiVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(versor.InternalMultivector).Gp(1 - eit);

        return new RGaConformalVersor(versor.ConformalSpace, kVector);
    }
}