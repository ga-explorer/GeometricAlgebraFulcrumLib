using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64TranslationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element TranslateBy(this CGaFloat64Element element, LinFloat64Vector2D egaVector)
    {
        return element switch
        {
            CGaFloat64Direction el => el,
            CGaFloat64Tangent el => el.TranslateBy(egaVector),
            CGaFloat64Flat el => el.TranslateBy(egaVector),
            CGaFloat64Round el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).Decode.OpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element TranslateBy(this CGaFloat64Element element, LinFloat64Vector3D egaVector)
    {
        return element switch
        {
            CGaFloat64Direction el => el,
            CGaFloat64Tangent el => el.TranslateBy(egaVector),
            CGaFloat64Flat el => el.TranslateBy(egaVector),
            CGaFloat64Round el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).Decode.OpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element TranslateBy(this CGaFloat64Element element, LinFloat64Vector egaVector)
    {
        return element switch
        {
            CGaFloat64Direction el => el,
            CGaFloat64Tangent el => el.TranslateBy(egaVector),
            CGaFloat64Flat el => el.TranslateBy(egaVector),
            CGaFloat64Round el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).Decode.OpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element TranslateBy(this CGaFloat64Element element, RGaFloat64Vector egaVector)
    {
        return element switch
        {
            CGaFloat64Direction el => el,
            CGaFloat64Tangent el => el.TranslateBy(egaVector),
            CGaFloat64Flat el => el.TranslateBy(egaVector),
            CGaFloat64Round el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).Decode.OpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element TranslateBy(this CGaFloat64Element element, CGaFloat64Blade egaVector)
    {
        return element switch
        {
            CGaFloat64Direction el => el,
            CGaFloat64Tangent el => el.TranslateBy(egaVector),
            CGaFloat64Flat el => el.TranslateBy(egaVector),
            CGaFloat64Round el => el.TranslateBy(egaVector),
            _ => element.EncodeOpnsBlade().TranslateBy(egaVector).Decode.OpnsElement()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement TranslateBy(this CGaFloat64Element element, IParametricCurve3D egaVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            egaVector.ParameterRange,
            t => element.TranslateBy(egaVector.GetPoint(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement TranslateBy(this CGaFloat64ParametricElement element, LinFloat64Vector2D egaVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement TranslateBy(this CGaFloat64ParametricElement element, LinFloat64Vector3D egaVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement TranslateBy(this CGaFloat64ParametricElement element, LinFloat64Vector egaVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement TranslateBy(this CGaFloat64ParametricElement element, RGaFloat64Vector egaVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange,
            t => element.GetElement(t).TranslateBy(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement TranslateBy(this CGaFloat64ParametricElement element, IParametricCurve3D egaVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange.Intersect(egaVector.ParameterRange),
            t => element.GetElement(t).TranslateBy(egaVector.GetPoint(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent TranslateBy(this CGaFloat64Tangent element, LinFloat64Vector2D egaVector)
    {
        return new CGaFloat64Tangent(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent TranslateBy(this CGaFloat64Tangent element, LinFloat64Vector3D egaVector)
    {
        return new CGaFloat64Tangent(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent TranslateBy(this CGaFloat64Tangent element, LinFloat64Vector egaVector)
    {
        return new CGaFloat64Tangent(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent TranslateBy(this CGaFloat64Tangent element, RGaFloat64Vector egaVector)
    {
        return new CGaFloat64Tangent(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent TranslateBy(this CGaFloat64Tangent element, CGaFloat64Blade egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaFloat64Tangent(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat TranslateBy(this CGaFloat64Flat element, LinFloat64Vector2D egaVector)
    {
        return new CGaFloat64Flat(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat TranslateBy(this CGaFloat64Flat element, LinFloat64Vector3D egaVector)
    {
        return new CGaFloat64Flat(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat TranslateBy(this CGaFloat64Flat element, LinFloat64Vector egaVector)
    {
        return new CGaFloat64Flat(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat TranslateBy(this CGaFloat64Flat element, RGaFloat64Vector egaVector)
    {
        return new CGaFloat64Flat(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat TranslateBy(this CGaFloat64Flat element, CGaFloat64Blade egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaFloat64Flat(
            element.GeometricSpace,
            element.Weight,
            element.Position + egaVector,
            element.Direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round TranslateBy(this CGaFloat64Round element, LinFloat64Vector2D egaVector)
    {
        return new CGaFloat64Round(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round TranslateBy(this CGaFloat64Round element, LinFloat64Vector3D egaVector)
    {
        return new CGaFloat64Round(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round TranslateBy(this CGaFloat64Round element, LinFloat64Vector egaVector)
    {
        return new CGaFloat64Round(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round TranslateBy(this CGaFloat64Round element, RGaFloat64Vector egaVector)
    {
        return new CGaFloat64Round(
            element.GeometricSpace,
            element.Weight,
            element.RadiusSquared,
            element.Position + egaVector.EncodeVGaVector(element.GeometricSpace),
            element.Direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round TranslateBy(this CGaFloat64Round element, CGaFloat64Blade egaVector)
    {
        Debug.Assert(egaVector.IsVGaVector());

        return new CGaFloat64Round(
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
    /// <param name="egaTranslationVectorX"></param>
    /// <param name="egaTranslationVectorY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, double egaTranslationVectorX, double egaTranslationVectorY)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGa.Vector(
                egaTranslationVectorX, 
                egaTranslationVectorY
            )
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, LinFloat64Vector2D egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }
    
    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVectorX"></param>
    /// <param name="egaTranslationVectorY"></param>
    /// <param name="egaTranslationVectorZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, double egaTranslationVectorX, double egaTranslationVectorY, double egaTranslationVectorZ)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGa.Vector(
                egaTranslationVectorX, 
                egaTranslationVectorY, 
                egaTranslationVectorZ
            )
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, LinFloat64Vector3D egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, LinFloat64Vector egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, RGaFloat64Vector egaTranslationVector)
    {
        return blade.TranslateBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslateBy(this CGaFloat64Blade blade, CGaFloat64Blade egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsVGaVector()
        );

        var eit =
            0.5d * blade.GeometricSpace.Ei.InternalKVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(blade.InternalKVector).Gp(1 - eit).GetKVectorPart(blade.Grade);

        return new CGaFloat64Blade(blade.GeometricSpace, kVector);
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslatePGaBy(this CGaFloat64Blade blade, LinFloat64Vector2D egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslatePGaBy(this CGaFloat64Blade blade, LinFloat64Vector3D egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslatePGaBy(this CGaFloat64Blade blade, LinFloat64Vector egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslatePGaBy(this CGaFloat64Blade blade, RGaFloat64Vector egaTranslationVector)
    {
        return blade.TranslatePGaBy(
            blade.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade TranslatePGaBy(this CGaFloat64Blade blade, CGaFloat64Blade egaTranslationVector)
    {
        Debug.Assert(
            blade.IsPGaBlade() &&
            egaTranslationVector.IsVGaBlade()
        );

        var eot =
            0.5d * blade.GeometricSpace.EoVector.Op(egaTranslationVector.InternalVector);

        var kVector =
            (1 - eot).Gp(blade.InternalKVector).Gp(1 + eot).GetKVectorPart(blade.InternalKVector.Grade);

        return new CGaFloat64Blade(blade.GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor TranslateBy(this CGaFloat64Versor versor, LinFloat64Vector2D egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor TranslateBy(this CGaFloat64Versor versor, LinFloat64Vector3D egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor TranslateBy(this CGaFloat64Versor versor, LinFloat64Vector egaTranslationVector)
    {
        return versor.TranslateBy(
            versor.GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Versor TranslateBy(this CGaFloat64Versor versor, CGaFloat64Blade egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsVGaVector()
        );

        var eit =
            0.5d * versor.GeometricSpace.EiVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(versor.InternalMultivector).Gp(1 - eit);

        return new CGaFloat64Versor(versor.GeometricSpace, kVector);
    }
}