using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;

public static class RGaConformalBladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade OpnsToIpns(this RGaConformalBlade blade)
    {
        return blade.CGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade OpnsToPGa(this RGaConformalBlade blade)
    {
        var kVector = blade.ConformalSpace.MusicalIsomorphism.OmMap(
            blade.CGaDual().InternalKVector
        );

        return new RGaConformalBlade(
            blade.ConformalSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IpnsToOpns(this RGaConformalBlade blade)
    {
        return blade.CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade IpnsToPGa(this RGaConformalBlade blade)
    {
        Debug.Assert(blade.IsCGaInfBlade());

        var kVector =
            blade.ConformalSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);
        
        return new RGaConformalBlade(
            blade.ConformalSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade PGaToOpns(this RGaConformalBlade blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector =
            blade.PGaToIpns().CGaUnDual().InternalKVector;

        return new RGaConformalBlade(
            blade.ConformalSpace,
            kVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade PGaToIpns(this RGaConformalBlade blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector = 
            blade.ConformalSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

        return new RGaConformalBlade(
            blade.ConformalSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade GetEGaVectorPart(this RGaConformalBlade vector)
    {
        Debug.Assert(vector.IsVector);

        return new RGaConformalBlade(
            vector.ConformalSpace,
            vector.InternalVector.GetVectorPart((int i) => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EGaVectorToHGaPoint(this RGaConformalBlade blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );
        
        var conformalSpace = blade.ConformalSpace;

        return conformalSpace.Eo + blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EGaVectorToPGaPoint(this RGaConformalBlade blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );
        
        var conformalSpace = blade.ConformalSpace;

        return (conformalSpace.Eo + blade).PGaUnDual();
    }

    public static RGaConformalBlade EGaVectorToOpnsFlatPoint(this RGaConformalBlade blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );

        var conformalSpace = blade.ConformalSpace;

        var p = blade.InternalVector;

        var kVector = 
            conformalSpace.EoiBivector +
            p.Op(conformalSpace.EiVector);

        return new RGaConformalBlade(conformalSpace, kVector);
    }

    public static RGaConformalBlade EGaVectorToIpnsPoint(this RGaConformalBlade blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );

        var conformalSpace = blade.ConformalSpace;

        var p = blade.InternalVector;

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * p.NormSquared() * conformalSpace.EiVector;

        return new RGaConformalBlade(conformalSpace, kVector);
    }

}