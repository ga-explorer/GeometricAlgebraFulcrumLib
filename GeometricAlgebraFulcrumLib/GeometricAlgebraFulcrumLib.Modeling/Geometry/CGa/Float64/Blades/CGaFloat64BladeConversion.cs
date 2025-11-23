using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

public sealed partial record CGaFloat64Blade
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade OpnsToIpns()
    {
        return CGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade OpnsToPGa()
    {
        var kVector = GeometricSpace.MusicalIsomorphism.OmMap(
            CGaDual().InternalKVector
        );

        return new CGaFloat64Blade(
            GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade IpnsToOpns()
    {
        return CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade IpnsToPGa()
    {
        Debug.Assert(IsCGaInfBlade());

        var kVector =
            GeometricSpace.MusicalIsomorphism.OmMap(InternalKVector);

        return new CGaFloat64Blade(
            GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PGaToOpns()
    {
        Debug.Assert(IsPGaBlade());

        var kVector =
            PGaToIpns().CGaUnDual().InternalKVector;

        return new CGaFloat64Blade(
            GeometricSpace,
            kVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PGaToIpns()
    {
        Debug.Assert(IsPGaBlade());

        var kVector =
            GeometricSpace.MusicalIsomorphism.OmMap(InternalKVector);

        return new CGaFloat64Blade(
            GeometricSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade GetVGaVectorPart()
    {
        Debug.Assert(IsVector);

        return new CGaFloat64Blade(
            GeometricSpace,
            InternalVector.GetVectorPart((int i) => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaVectorToHGaPoint()
    {
        Debug.Assert(
            IsVGaVector()
        );

        var cgaGeometricSpace = GeometricSpace;

        return cgaGeometricSpace.Eo + this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaVectorToPGaPoint()
    {
        Debug.Assert(
            IsVGaVector()
        );

        var cgaGeometricSpace = GeometricSpace;

        return (cgaGeometricSpace.Eo + this).PGaUnDual();
    }

    public CGaFloat64Blade VGaVectorToOpnsFlatPoint()
    {
        Debug.Assert(
            IsVGaVector()
        );

        var cgaGeometricSpace = GeometricSpace;

        var p = InternalVector;

        var kVector =
            cgaGeometricSpace.EoiBivector +
            p.Op(cgaGeometricSpace.EiVector);

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    public CGaFloat64Blade VGaVectorToIpnsPoint()
    {
        Debug.Assert(
            IsVGaVector()
        );

        var cgaGeometricSpace = GeometricSpace;

        var p = InternalVector;

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * p.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

}