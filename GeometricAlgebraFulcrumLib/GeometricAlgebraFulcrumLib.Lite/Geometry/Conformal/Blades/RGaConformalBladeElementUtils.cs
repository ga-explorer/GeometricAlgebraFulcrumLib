using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;

public static class RGaConformalBladeElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaVector(this RGaConformalBlade blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaBivector(this RGaConformalBlade blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaTrivector(this RGaConformalBlade blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsTrivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaBlade(this RGaConformalBlade blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPGaBlade(this RGaConformalBlade blade)
    {
        return blade.ConformalSpace.IsValidPGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaInfBlade(this RGaConformalBlade blade)
    {
        return blade.ConformalSpace.IsValidCGaInfElement(blade.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaDirection(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        return conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
               conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaTangent(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        return !conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
               !conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               blade.SpSquared().IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaFlat(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return (isZeroEiOpX && !isZeroEiIpX) || 
               (!isZeroEiOpX && isZeroEiIpX);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOpnsFlat(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return isZeroEiOpX && !isZeroEiIpX;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIpnsFlat(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return !isZeroEiOpX && isZeroEiIpX;
    }

    public static bool IsIpnsPointOrHyperSphere(this RGaConformalBlade blade)
    {
        if (!blade.IsVector)
            return false;

        var conformalSpace = blade.ConformalSpace;

        var eiOpX = blade[0] + blade[1];
            //conformalSpace.Ei.InternalVector.Op(blade.InternalVector);

        if (eiOpX.IsNearZero()) 
            return false;

        var eiIpX = 
            conformalSpace.Ei.InternalVector.Lcp(blade.InternalVector).ScalarValue();

        if (eiIpX.IsNearZero())
            return false;
        
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaRound(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        return !conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
               !conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               !blade.SpSquared().IsNearZero();
    }

    public static RGaConformalElementKind GetElementKind(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return blade.SpSquared().IsNearZero()
                ? RGaConformalElementKind.Tangent
                : RGaConformalElementKind.Round;

        if (isZeroEiOpX && isZeroEiIpX)
            return RGaConformalElementKind.Direction;

        return RGaConformalElementKind.Flat;
    }
    
    public static RGaConformalElementSpecs GetElementSpecsOpns(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // OPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new RGaConformalElementSpecs(
                    conformalSpace,
                    RGaConformalElementKind.Tangent,
                    RGaConformalElementEncoding.Opns,
                    blade.DecodeOpnsTangentEGaDirection().Grade
                );

            // OPNS Round
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Round,
                RGaConformalElementEncoding.Opns,
                blade.DecodeOpnsRoundEGaDirection().Grade
            );
        }

        // OPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Direction,
                RGaConformalElementEncoding.Opns,
                blade.DecodeOpnsDirectionEGaDirection().Grade
            );

        // OPNS Flat
        if (isZeroEiOpX)
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Flat,
                RGaConformalElementEncoding.Opns,
                blade.DecodeOpnsFlatEGaDirection().Grade
            );

        // IPNS Flat
        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            blade.DecodeIpnsFlatEGaDirection().Grade
        );
    }

    public static RGaConformalElementSpecs GetElementSpecsIpns(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // IPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new RGaConformalElementSpecs(
                    conformalSpace,
                    RGaConformalElementKind.Tangent,
                    RGaConformalElementEncoding.Ipns,
                    blade.DecodeIpnsTangentEGaDirection().Grade
                );

            // IPNS Round
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Round,
                RGaConformalElementEncoding.Ipns,
                blade.DecodeIpnsRoundEGaDirection().Grade
            );
        }

        // IPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Direction,
                RGaConformalElementEncoding.Ipns,
                blade.DecodeIpnsDirectionEGaDirection().Grade
            );

        if (isZeroEiOpX)
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Flat,
                RGaConformalElementEncoding.Opns,
                blade.DecodeOpnsFlatEGaDirection().Grade
            );

        // IPNS Flat
        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Flat,
            RGaConformalElementEncoding.Ipns,
            blade.DecodeIpnsFlatEGaDirection().Grade
        );
    }

    public static RGaConformalElementSpecs GetElementSpecs(this RGaConformalBlade blade)
    {
        var conformalSpace = blade.ConformalSpace;
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return new RGaConformalElementSpecs(
                conformalSpace,
                blade.SpSquared().IsNearZero()
                    ? RGaConformalElementKind.Tangent
                    : RGaConformalElementKind.Round,
                RGaConformalElementEncoding.OpnsOrIpns
            );

        if (isZeroEiOpX && isZeroEiIpX)
            return new RGaConformalElementSpecs(
                conformalSpace,
                RGaConformalElementKind.Direction,
                RGaConformalElementEncoding.OpnsOrIpns
            );

        return new RGaConformalElementSpecs(
            conformalSpace,
            RGaConformalElementKind.Flat,
            isZeroEiOpX
                ? RGaConformalElementEncoding.Opns
                : RGaConformalElementEncoding.Ipns
        );
    }

}