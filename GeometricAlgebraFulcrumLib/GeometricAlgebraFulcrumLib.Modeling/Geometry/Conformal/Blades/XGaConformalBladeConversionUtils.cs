using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

public static class XGaConformalBladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> ToConformalBlade<T>(this XGaKVector<T> cgaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> ScalarPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> VectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaMultivector.GetVectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> VectorPartToConformalEGaBlade<T>(this XGaMultivector<T> cgaMultivector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaMultivector.GetVectorPart().GetVectorPart(i => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> BivectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaMultivector.GetBivectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> KVectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, int grade, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaMultivector.GetKVectorPart(grade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> FirstKVectorPartToConformalBlade<T>(this XGaMultivector<T> cgaMultivector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalBlade<T>(
            conformalSpace, 
            cgaMultivector.GetFirstKVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> OpnsToIpns<T>(this XGaConformalBlade<T> blade)
    {
        return blade.CGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> OpnsToPGa<T>(this XGaConformalBlade<T> blade)
    {
        var kVector = blade.ConformalSpace.MusicalIsomorphism.OmMap(
            blade.CGaDual().InternalKVector
        );

        return new XGaConformalBlade<T>(
            blade.ConformalSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> IpnsToOpns<T>(this XGaConformalBlade<T> blade)
    {
        return blade.CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> IpnsToPGa<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(blade.IsCGaInfBlade());

        var kVector =
            blade.ConformalSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);
        
        return new XGaConformalBlade<T>(
            blade.ConformalSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> PGaToOpns<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector =
            blade.PGaToIpns().CGaUnDual().InternalKVector;

        return new XGaConformalBlade<T>(
            blade.ConformalSpace,
            kVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> PGaToIpns<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(blade.IsPGaBlade());

        var kVector = 
            blade.ConformalSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

        return new XGaConformalBlade<T>(
            blade.ConformalSpace,
            kVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> GetEGaVectorPart<T>(this XGaConformalBlade<T> vector)
    {
        Debug.Assert(vector.IsVector);

        return new XGaConformalBlade<T>(
            vector.ConformalSpace,
            vector.InternalVector.GetVectorPart(i => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EGaVectorToHGaPoint<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );
        
        var conformalSpace = blade.ConformalSpace;

        return conformalSpace.Eo + blade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EGaVectorToPGaPoint<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );
        
        var conformalSpace = blade.ConformalSpace;

        return (conformalSpace.Eo + blade).PGaUnDual();
    }

    public static XGaConformalBlade<T> EGaVectorToOpnsFlatPoint<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(
            blade.IsEGaVector()
        );

        var conformalSpace = blade.ConformalSpace;

        var p = blade.InternalVector;

        var kVector = 
            conformalSpace.EoiBivector +
            p.Op(conformalSpace.EiVector);

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

    public static XGaConformalBlade<T> EGaVectorToIpnsPoint<T>(this XGaConformalBlade<T> blade)
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

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

}