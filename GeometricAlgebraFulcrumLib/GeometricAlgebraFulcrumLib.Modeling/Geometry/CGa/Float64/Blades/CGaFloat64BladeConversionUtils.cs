using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

public static class CGaFloat64BladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ToConformalBlade(this RGaFloat64KVector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ScalarPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade VectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade VectorPartToConformalVGaBlade(this RGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetVectorPart((int i) => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade BivectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade KVectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, int grade, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetKVectorPart(grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade FirstKVectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade OpnsToIpns(this CGaFloat64Blade blade)
    {
        return blade.CGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade OpnsToPGa(this CGaFloat64Blade blade)
    {
        var kVector = blade.GeometricSpace.MusicalIsomorphism.OmMap(
            blade.CGaDual().InternalKVector
        );

        return new CGaFloat64Blade(
            blade.GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade IpnsToOpns(this CGaFloat64Blade blade)
    {
        return blade.CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade IpnsToPGa(this CGaFloat64Blade blade)
    {
        Debug.Assert(blade.IsCGaInfBlade());

        var kVector =
            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

        return new CGaFloat64Blade(
            blade.GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade PGaToOpns(this CGaFloat64Blade blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector =
            blade.PGaToIpns().CGaUnDual().InternalKVector;

        return new CGaFloat64Blade(
            blade.GeometricSpace,
            kVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade PGaToIpns(this CGaFloat64Blade blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector =
            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

        return new CGaFloat64Blade(
            blade.GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade GetVGaVectorPart(this CGaFloat64Blade vector)
    {
        Debug.Assert(vector.IsVector);

        return new CGaFloat64Blade(
            vector.GeometricSpace,
            vector.InternalVector.GetVectorPart((int i) => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade VGaVectorToHGaPoint(this CGaFloat64Blade blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        return cgaGeometricSpace.Eo + blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade VGaVectorToPGaPoint(this CGaFloat64Blade blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        return (cgaGeometricSpace.Eo + blade).PGaUnDual();
    }

    public static CGaFloat64Blade VGaVectorToOpnsFlatPoint(this CGaFloat64Blade blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        var p = blade.InternalVector;

        var kVector =
            cgaGeometricSpace.EoiBivector +
            p.Op(cgaGeometricSpace.EiVector);

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    public static CGaFloat64Blade VGaVectorToIpnsPoint(this CGaFloat64Blade blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        var p = blade.InternalVector;

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * p.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

}