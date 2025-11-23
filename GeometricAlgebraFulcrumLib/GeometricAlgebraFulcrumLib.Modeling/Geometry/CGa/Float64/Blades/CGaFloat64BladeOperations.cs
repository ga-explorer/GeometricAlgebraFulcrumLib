using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

public sealed partial record CGaFloat64Blade
{
    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVectorX"></param>
    /// <param name="egaTranslationVectorY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(double egaTranslationVectorX, double egaTranslationVectorY)
    {
        return TranslateBy(
            GeometricSpace.EncodeVGa.Vector(
                egaTranslationVectorX, 
                egaTranslationVectorY
            )
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(LinFloat64Vector2D egaTranslationVector)
    {
        return TranslateBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }
    
    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVectorX"></param>
    /// <param name="egaTranslationVectorY"></param>
    /// <param name="egaTranslationVectorZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(double egaTranslationVectorX, double egaTranslationVectorY, double egaTranslationVectorZ)
    {
        return TranslateBy(
            GeometricSpace.EncodeVGa.Vector(
                egaTranslationVectorX, 
                egaTranslationVectorY, 
                egaTranslationVectorZ
            )
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(LinFloat64Vector3D egaTranslationVector)
    {
        return TranslateBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(LinFloat64Vector egaTranslationVector)
    {
        return TranslateBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(XGaFloat64Vector egaTranslationVector)
    {
        return TranslateBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a CGA translation to this CGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslateBy(CGaFloat64Blade egaTranslationVector)
    {
        Debug.Assert(
            egaTranslationVector.IsVGaVector()
        );

        var eit =
            0.5d * GeometricSpace.Ei.InternalKVector.Op(egaTranslationVector.InternalKVector);

        var kVector =
            (1 + eit).Gp(InternalKVector).Gp(1 - eit).GetKVectorPart(Grade);

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslatePGaBy(LinFloat64Vector2D egaTranslationVector)
    {
        return TranslatePGaBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslatePGaBy(LinFloat64Vector3D egaTranslationVector)
    {
        return TranslatePGaBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslatePGaBy(LinFloat64Vector egaTranslationVector)
    {
        return TranslatePGaBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslatePGaBy(XGaFloat64Vector egaTranslationVector)
    {
        return TranslatePGaBy(
            GeometricSpace.EncodeVGa.Vector(egaTranslationVector)
        );
    }

    /// <summary>
    /// Apply a PGA translation to this PGA blade
    /// </summary>
    /// <param name="egaTranslationVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade TranslatePGaBy(CGaFloat64Blade egaTranslationVector)
    {
        Debug.Assert(
            IsPGaBlade() &&
            egaTranslationVector.IsVGaBlade()
        );

        var eot =
            0.5d * GeometricSpace.EoVector.Op(egaTranslationVector.InternalVector);

        var kVector =
            (1 - eot).Gp(InternalKVector).Gp(1 + eot).GetKVectorPart(InternalKVector.Grade);

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    
    /// <summary>
    /// Apply a uniform scaling to this CGA blade
    /// </summary>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ScaleBy(double scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = 0.5 * scalingFactor.LogE();
        var s0 = Math.Cosh(g);
        var s2 = Math.Sinh(g) * GeometricSpace.Eoi.InternalKVector;

        var kVector =
            (s0 + s2).Gp(InternalKVector).Gp(s0 - s2).GetKVectorPart(InternalKVector.Grade);

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }


    public CGaFloat64Blade RotateUsing(LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        var bivector =
            GeometricSpace.EncodeIpnsFlat.Point(
                egaAxisPoint
            ).InternalBivector;

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var s0 = halfAngleCos;
        var s2 = halfAngleSin / bivector.Norm();

        var mv = s0 + s2 * bivector;
        var mvInv = s0 - s2 * bivector;

        return mv
            .Gp(InternalKVector)
            .Gp(mvInv)
            .KVectorPartToConformalBlade(Grade, GeometricSpace);
    }

    public CGaFloat64Blade RotateUsing(LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        var bivector =
            GeometricSpace.EncodeIpnsFlat.Line3D(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var s0 = halfAngleCos;
        var s2 = halfAngleSin / bivector.Norm();

        var mv = s0 + s2 * bivector;
        var mvInv = s0 - s2 * bivector;

        return mv
            .Gp(InternalKVector)
            .Gp(mvInv)
            .KVectorPartToConformalBlade(Grade, GeometricSpace);
    }


    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ReflectOpnsOn(CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            GeometricSpace.ReflectOpnsOnOpns(InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA OPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ReflectOpnsOnIpns(CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            GeometricSpace.ReflectOpnsOnIpns(InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ReflectIpnsOnOpns(CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            GeometricSpace.ReflectIpnsOnOpns(InternalKVector, blade2.InternalKVector)
        );
    }

    /// <summary>
    /// Reflect this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ReflectIpnsOn(CGaFloat64Blade blade2)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            GeometricSpace.ReflectIpnsOnIpns(InternalKVector, blade2.InternalKVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CGaFloat64Blade ProjectOn(XGaFloat64KVector subspace)
    {
        var projectedBlade =
            InternalKVector
                .Fdp(subspace)
                .Gp(subspace)
                .GetKVectorPart(Grade);

        var scalarFactor = subspace.SpSquared();

        var kVector = scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    /// <summary>
    /// Project this EGA Blade on another EGA Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ProjectVGaOn(CGaFloat64Blade subspace)
    {
        Debug.Assert(
            IsVGaBlade() &&
            subspace.IsVGaBlade()
        );

        return ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this PGA Blade on another PGA Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ProjectPGaOn(CGaFloat64Blade subspace)
    {
        Debug.Assert(
            IsPGaBlade() &&
            subspace.IsPGaBlade()
        );

        return ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ProjectOpnsOn(CGaFloat64Blade subspace)
    {
        return ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ProjectIpnsOn(CGaFloat64Blade subspace)
    {
        return ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this EGA Point on an IPNS Blade
    /// </summary>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ProjectVGaPointOnIpns(CGaFloat64Blade subspace)
    {
        Debug.Assert(
            IsVGaBlade()
        );

        return VGaVectorToIpnsPoint().ProjectIpnsOn(subspace);
    }


    /// <summary>
    /// Intersect this EGA Blade with another EGA Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetVGa(CGaFloat64Blade blade2)
    {
        Debug.Assert(
            IsVGaBlade() &&
            blade2.IsVGaBlade()
        );

        return blade2.Op(this);
    }

    /// <summary>
    /// Intersect this EGA Blade with two other EGA Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetVGa(CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        Debug.Assert(
            IsVGaBlade() &&
            blade2.IsVGaBlade() &&
            blade3.IsVGaBlade()
        );

        return blade3.Op(blade2).Op(this);
    }


    /// <summary>
    /// Intersect this PGA Blade with another PGA Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetPGa(CGaFloat64Blade blade2)
    {
        Debug.Assert(
            IsPGaBlade() &&
            blade2.IsPGaBlade()
        );

        return blade2.Op(this);
    }

    /// <summary>
    /// Intersect this PGA Blade with two other PGA Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetPGa(CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        Debug.Assert(
            IsPGaBlade() &&
            blade2.IsPGaBlade() &&
            blade3.IsPGaBlade()
        );

        return blade3.Op(blade2).Op(this);
    }


    /// <summary>
    /// Intersect this CGA OPNS Blade with another CGA OPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetOpns(CGaFloat64Blade blade2)
    {
        return blade2.CGaDual().Lcp(this);
    }

    /// <summary>
    /// Intersect this CGA OPNS Blade with two other CGA OPNS Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetOpns(CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        return MeetOpns(blade2).MeetOpns(blade3);
    }


    /// <summary>
    /// Intersect this CGA IPNS Blade with another CGA IPNS Blade
    /// </summary>
    /// <param name="blade2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetIpns(CGaFloat64Blade blade2)
    {
        return blade2.Op(this);
    }

    /// <summary>
    /// Intersect this CGA IPNS Blade with two other CGA IPNS Blades
    /// </summary>
    /// <param name="blade2"></param>
    /// <param name="blade3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MeetIpns(CGaFloat64Blade blade2, CGaFloat64Blade blade3)
    {
        return blade3.Op(blade2).Op(this);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MapUsingMusicalIsomorphism()
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            GeometricSpace.MusicalIsomorphism.OmMap(InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MapUsing(IXGaFloat64Outermorphism outerMorphism)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            outerMorphism.OmMap(InternalKVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade MapUsing(CGaFloat64Versor versor)
    {
        return versor.MapBlade(InternalKVector);
    }



}