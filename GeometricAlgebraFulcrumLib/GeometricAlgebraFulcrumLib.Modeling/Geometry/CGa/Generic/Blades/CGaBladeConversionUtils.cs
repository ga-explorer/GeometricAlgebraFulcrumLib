using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

public static class CGaBladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ToConformalBlade<T>(this XGaKVector<T> cgaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ScalarPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> VectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaMultivector.GetVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> VectorPartToConformalVGaBlade<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaMultivector.GetVectorPart().GetVectorPart(i => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> BivectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaMultivector.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> KVectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, int grade, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaMultivector.GetKVectorPart(grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> FirstKVectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaMultivector.GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> OpnsToIpns<T>(this CGaBlade<T> blade)
    {
        return blade.CGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> OpnsToPGa<T>(this CGaBlade<T> blade)
    {
        var kVector = blade.GeometricSpace.MusicalIsomorphism.OmMap(
            blade.CGaDual().InternalKVector
        );

        return new CGaBlade<T>(
            blade.GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> IpnsToOpns<T>(this CGaBlade<T> blade)
    {
        return blade.CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> IpnsToPGa<T>(this CGaBlade<T> blade)
    {
        Debug.Assert(blade.IsCGaInfBlade());

        var kVector =
            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

        return new CGaBlade<T>(
            blade.GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> PGaToOpns<T>(this CGaBlade<T> blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector =
            blade.PGaToIpns().CGaUnDual().InternalKVector;

        return new CGaBlade<T>(
            blade.GeometricSpace,
            kVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> PGaToIpns<T>(this CGaBlade<T> blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector =
            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

        return new CGaBlade<T>(
            blade.GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> GetVGaVectorPart<T>(this CGaBlade<T> vector)
    {
        Debug.Assert(vector.IsVector);

        return new CGaBlade<T>(
            vector.GeometricSpace,
            vector.InternalVector.GetVectorPart(i => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> VGaVectorToHGaPoint<T>(this CGaBlade<T> blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        return cgaGeometricSpace.Eo + blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> VGaVectorToPGaPoint<T>(this CGaBlade<T> blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        return (cgaGeometricSpace.Eo + blade).PGaUnDual();
    }

    public static CGaBlade<T> VGaVectorToOpnsFlatPoint<T>(this CGaBlade<T> blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var cgaGeometricSpace = blade.GeometricSpace;

        var p = blade.InternalVector;

        var kVector =
            cgaGeometricSpace.EoiBivector +
            p.Op(cgaGeometricSpace.EiVector);

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    public static CGaBlade<T> VGaVectorToIpnsPoint<T>(this CGaBlade<T> blade)
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

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

}