//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

//namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.PGa.Blades;

//public static class PGaFloat64BladeUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVGaVector(this PGaFloat64Blade blade)
//    {
//        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
//               blade.InternalKVector.IsVector();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVGaBivector(this PGaFloat64Blade blade)
//    {
//        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
//               blade.InternalKVector.IsBivector();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVGaTrivector(this PGaFloat64Blade blade)
//    {
//        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
//               blade.InternalKVector.IsTrivector();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVGaBlade(this PGaFloat64Blade blade)
//    {
//        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsPGaBlade(this PGaFloat64Blade blade)
//    {
//        return blade.GeometricSpace.IsValidPGaElement(blade.InternalKVector);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsPGaInfBlade(this PGaFloat64Blade blade)
//    {
//        return blade.GeometricSpace.IsValidPGaInfElement(blade.InternalKVector);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsPGaDirection(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        return cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
//               cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsPGaTangent(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        return !cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
//               !cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
//               blade.SpSquared().IsNearZero();
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsPGaFlat(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        return (isZeroEiOpX && !isZeroEiIpX) || 
//               (!isZeroEiOpX && isZeroEiIpX);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsOpnsFlat(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        return isZeroEiOpX && !isZeroEiIpX;
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsIpnsFlat(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        return !isZeroEiOpX && isZeroEiIpX;
//    }

//    public static bool IsIpnsPointOrHyperSphere(this PGaFloat64Blade blade)
//    {
//        if (!blade.IsVector)
//            return false;

//        var cgaGeometricSpace = blade.GeometricSpace;

//        var eiOpX = blade[0] + blade[1];
//            //cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalVector);

//        if (eiOpX.IsNearZero()) 
//            return false;

//        var eiIpX = 
//            cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalVector).ScalarValue;

//        if (eiIpX.IsNearZero())
//            return false;
        
//        return true;
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsPGaRound(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        return !cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() && 
//               !cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
//               !blade.SpSquared().IsNearZero();
//    }


//    public static PGaFloat64ElementKind GetElementKind(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
        
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        if (!isZeroEiOpX && !isZeroEiIpX)
//            return blade.SpSquared().IsNearZero()
//                ? PGaFloat64ElementKind.Tangent
//                : PGaFloat64ElementKind.Round;

//        if (isZeroEiOpX && isZeroEiIpX)
//            return PGaFloat64ElementKind.Direction;

//        return PGaFloat64ElementKind.Flat;
//    }
    
//    public static PGaFloat64ElementSpecs GetElementSpecsOpns(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        if (!isZeroEiOpX && !isZeroEiIpX)
//        {
//            // OPNS Tangent
//            if (blade.SpSquared().IsNearZero())
//                return new PGaFloat64ElementSpecs(
//                    cgaGeometricSpace,
//                    PGaFloat64ElementKind.Tangent,
//                    PGaFloat64ElementEncoding.Opns,
//                    blade.DecodeOpnsTangentVGaDirection().Grade
//                );

//            // OPNS Round
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Round,
//                PGaFloat64ElementEncoding.Opns,
//                blade.DecodeOpnsRoundVGaDirection().Grade
//            );
//        }

//        // OPNS Direction
//        if (isZeroEiOpX && isZeroEiIpX)
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Direction,
//                PGaFloat64ElementEncoding.Opns,
//                blade.DecodeOpnsDirectionVGaDirection().Grade
//            );

//        // OPNS Flat
//        if (isZeroEiOpX)
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Flat,
//                PGaFloat64ElementEncoding.Opns,
//                blade.DecodeOpnsFlatVGaDirection().Grade
//            );

//        // IPNS Flat
//        return new PGaFloat64ElementSpecs(
//            cgaGeometricSpace,
//            PGaFloat64ElementKind.Flat,
//            PGaFloat64ElementEncoding.Ipns,
//            blade.DecodeIpnsFlatVGaDirection().Grade
//        );
//    }

//    public static PGaFloat64ElementSpecs GetElementSpecsIpns(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        if (!isZeroEiOpX && !isZeroEiIpX)
//        {
//            // IPNS Tangent
//            if (blade.SpSquared().IsNearZero())
//                return new PGaFloat64ElementSpecs(
//                    cgaGeometricSpace,
//                    PGaFloat64ElementKind.Tangent,
//                    PGaFloat64ElementEncoding.Ipns,
//                    blade.DecodeIpnsTangentVGaDirection().Grade
//                );

//            // IPNS Round
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Round,
//                PGaFloat64ElementEncoding.Ipns,
//                blade.DecodeIpnsRoundVGaDirection().Grade
//            );
//        }

//        // IPNS Direction
//        if (isZeroEiOpX && isZeroEiIpX)
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Direction,
//                PGaFloat64ElementEncoding.Ipns,
//                blade.DecodeIpnsDirectionVGaDirection().Grade
//            );

//        if (isZeroEiOpX)
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Flat,
//                PGaFloat64ElementEncoding.Opns,
//                blade.DecodeOpnsFlatVGaDirection().Grade
//            );

//        // IPNS Flat
//        return new PGaFloat64ElementSpecs(
//            cgaGeometricSpace,
//            PGaFloat64ElementKind.Flat,
//            PGaFloat64ElementEncoding.Ipns,
//            blade.DecodeIpnsFlatVGaDirection().Grade
//        );
//    }

//    public static PGaFloat64ElementSpecs GetElementSpecs(this PGaFloat64Blade blade)
//    {
//        var cgaGeometricSpace = blade.GeometricSpace;
//        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
//        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

//        if (!isZeroEiOpX && !isZeroEiIpX)
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                blade.SpSquared().IsNearZero()
//                    ? PGaFloat64ElementKind.Tangent
//                    : PGaFloat64ElementKind.Round,
//                PGaFloat64ElementEncoding.OpnsOrIpns
//            );

//        if (isZeroEiOpX && isZeroEiIpX)
//            return new PGaFloat64ElementSpecs(
//                cgaGeometricSpace,
//                PGaFloat64ElementKind.Direction,
//                PGaFloat64ElementEncoding.OpnsOrIpns
//            );

//        return new PGaFloat64ElementSpecs(
//            cgaGeometricSpace,
//            PGaFloat64ElementKind.Flat,
//            isZeroEiOpX
//                ? PGaFloat64ElementEncoding.Opns
//                : PGaFloat64ElementEncoding.Ipns
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<PGaFloat64Blade> GetBasisBladesVGa(this PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return (1UL << (cgaGeometricSpace.VSpaceDimensions - 2))
//            .GetRange()
//            .OrderBy(id => id.Grade())
//            .ThenBy(id => id)
//            .Select(id =>
//                cgaGeometricSpace
//                    .ConformalProcessor
//                    .KVectorTerm(id << 2)
//                    .ToConformalBlade(cgaGeometricSpace)
//            );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<PGaFloat64Blade> GetBasisBladesPGa(this PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return (1UL << (cgaGeometricSpace.VSpaceDimensions - 1))
//            .GetRange()
//            .OrderBy(id => id.Grade())
//            .ThenBy(id => id)
//            .Select(id => 
//                cgaGeometricSpace
//                    .BasisSpecs
//                    .BasisMapInverse
//                    .OmMapBasisBlade(id)
//                    .ToConformalBlade(cgaGeometricSpace)
//            );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<PGaFloat64Blade> GetBasisBladesPGa(this PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return cgaGeometricSpace
//            .GaSpaceDimensions
//            .GetRange()
//            .OrderBy(id => id.Grade())
//            .ThenBy(id => id)
//            .Select(id => 
//                cgaGeometricSpace
//                    .BasisSpecs
//                    .BasisMapInverse
//                    .OmMapBasisBlade(id)
//                    .ToConformalBlade(cgaGeometricSpace)
//            );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<PGaFloat64Blade> GetBasisBladesPGaInf(this PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return (1UL << (cgaGeometricSpace.VSpaceDimensions - 1))
//            .GetRange(id => id << 1)
//            .OrderBy(id => id.Grade())
//            .ThenBy(id => id)
//            .Select(id => 
//                cgaGeometricSpace
//                    .BasisSpecs
//                    .BasisMapInverse
//                    .OmMapBasisBlade(id)
//                    .ToConformalBlade(cgaGeometricSpace)
//            );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaFloat64Multivector Gp(this XGaFloat64Multivector mv, PGaFloat64Blade blade)
//    {
//        return mv.Gp(blade.InternalKVector);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade Op(this IEnumerable<PGaFloat64Blade> bladeList)
//    {
//        return new PGaFloat64Blade(
//            bladeList.First().GeometricSpace,
//            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
//        );
//    }
//}