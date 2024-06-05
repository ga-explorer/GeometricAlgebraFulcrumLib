using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

public static class XGaConformalBladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaVector<T>(this XGaConformalBlade<T> blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaBivector<T>(this XGaConformalBlade<T> blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaTrivector<T>(this XGaConformalBlade<T> blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.Grade == 3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaBlade<T>(this XGaConformalBlade<T> blade)
    {
        return blade.ConformalSpace.IsValidEGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPGaBlade<T>(this XGaConformalBlade<T> blade)
    {
        return blade.ConformalSpace.IsValidPGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaInfBlade<T>(this XGaConformalBlade<T> blade)
    {
        return blade.ConformalSpace.IsValidCGaInfElement(blade.InternalKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaDirection<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        return conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
               conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaTangent<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        return !conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
               !conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               blade.SpSquared().IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaFlat<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return (isZeroEiOpX && !isZeroEiIpX) || 
               (!isZeroEiOpX && isZeroEiIpX);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOpnsFlat<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return isZeroEiOpX && !isZeroEiIpX;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIpnsFlat<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return !isZeroEiOpX && isZeroEiIpX;
    }

    public static bool IsIpnsPointOrHyperSphere<T>(this XGaConformalBlade<T> blade)
    {
        if (!blade.IsVector)
            return false;

        var conformalSpace = blade.ConformalSpace;

        var eiOpX = blade[0] + blade[1];
            //conformalSpace.Ei.InternalVector.Op(blade.InternalVector);

        if (eiOpX.IsNearZero()) 
            return false;

        var eiIpX = 
            conformalSpace.Ei.InternalVector.Lcp(blade.InternalVector).Scalar();

        if (eiIpX.IsNearZero())
            return false;
        
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaRound<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        return !conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
               !conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               !blade.SpSquared().IsNearZero();
    }


    public static XGaConformalElementKind GetElementKind<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return blade.SpSquared().IsNearZero()
                ? XGaConformalElementKind.Tangent
                : XGaConformalElementKind.Round;

        if (isZeroEiOpX && isZeroEiIpX)
            return XGaConformalElementKind.Direction;

        return XGaConformalElementKind.Flat;
    }
    
    public static XGaConformalElementSpecs<T> GetElementSpecsOpns<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // OPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new XGaConformalElementSpecs<T>(
                    conformalSpace,
                    XGaConformalElementKind.Tangent,
                    XGaConformalElementEncoding.Opns,
                    blade.DecodeOpnsTangentEGaDirection().Grade
                );

            // OPNS Round
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Round,
                XGaConformalElementEncoding.Opns,
                blade.DecodeOpnsRoundEGaDirection().Grade
            );
        }

        // OPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Direction,
                XGaConformalElementEncoding.Opns,
                blade.DecodeOpnsDirectionEGaDirection().Grade
            );

        // OPNS Flat
        if (isZeroEiOpX)
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Flat,
                XGaConformalElementEncoding.Opns,
                blade.DecodeOpnsFlatEGaDirection().Grade
            );

        // IPNS Flat
        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            blade.DecodeIpnsFlatEGaDirection().Grade
        );
    }

    public static XGaConformalElementSpecs<T> GetElementSpecsIpns<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // IPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new XGaConformalElementSpecs<T>(
                    conformalSpace,
                    XGaConformalElementKind.Tangent,
                    XGaConformalElementEncoding.Ipns,
                    blade.DecodeIpnsTangentEGaDirection().Grade
                );

            // IPNS Round
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Round,
                XGaConformalElementEncoding.Ipns,
                blade.DecodeIpnsRoundEGaDirection().Grade
            );
        }

        // IPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Direction,
                XGaConformalElementEncoding.Ipns,
                blade.DecodeIpnsDirectionEGaDirection().Grade
            );

        if (isZeroEiOpX)
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Flat,
                XGaConformalElementEncoding.Opns,
                blade.DecodeOpnsFlatEGaDirection().Grade
            );

        // IPNS Flat
        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Flat,
            XGaConformalElementEncoding.Ipns,
            blade.DecodeIpnsFlatEGaDirection().Grade
        );
    }

    public static XGaConformalElementSpecs<T> GetElementSpecs<T>(this XGaConformalBlade<T> blade)
    {
        var conformalSpace = blade.ConformalSpace;
        var isZeroEiOpX = conformalSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = conformalSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                blade.SpSquared().IsNearZero()
                    ? XGaConformalElementKind.Tangent
                    : XGaConformalElementKind.Round,
                XGaConformalElementEncoding.OpnsOrIpns
            );

        if (isZeroEiOpX && isZeroEiIpX)
            return new XGaConformalElementSpecs<T>(
                conformalSpace,
                XGaConformalElementKind.Direction,
                XGaConformalElementEncoding.OpnsOrIpns
            );

        return new XGaConformalElementSpecs<T>(
            conformalSpace,
            XGaConformalElementKind.Flat,
            isZeroEiOpX
                ? XGaConformalElementEncoding.Opns
                : XGaConformalElementEncoding.Ipns
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaConformalBlade<T>> GetBasisBladesEGa<T>(this XGaConformalSpace<T> conformalSpace)
    {
        return (1UL << (conformalSpace.VSpaceDimensions - 2))
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                conformalSpace
                    .ConformalProcessor
                    .KVectorTerm((id << 2).BitPatternToIndexSet())
                    .ToConformalBlade(conformalSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaConformalBlade<T>> GetBasisBladesPGa<T>(this XGaConformalSpace<T> conformalSpace)
    {
        return (1UL << (conformalSpace.VSpaceDimensions - 1))
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                conformalSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade(id.BitPatternToIndexSet())
                    .ToConformalBlade(conformalSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaConformalBlade<T>> GetBasisBladesCGa<T>(this XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace
            .GaSpaceDimensions
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                conformalSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade(id.BitPatternToIndexSet())
                    .ToConformalBlade(conformalSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaConformalBlade<T>> GetBasisBladesCGaInf<T>(this XGaConformalSpace<T> conformalSpace)
    {
        return (1UL << (conformalSpace.VSpaceDimensions - 1))
            .GetRange(id => id << 1)
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                conformalSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade(id.BitPatternToIndexSet())
                    .ToConformalBlade(conformalSpace)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Gp<T>(this XGaMultivector<T> mv, XGaConformalBlade<T> blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> Op<T>(this IEnumerable<XGaConformalBlade<T>> bladeList)
    {
        return new XGaConformalBlade<T>(
            bladeList.First().ConformalSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }
}