using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

public static class CGaBladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaVector<T>(this CGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaBivector<T>(this CGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaTrivector<T>(this CGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.Grade == 3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaBlade<T>(this CGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPGaBlade<T>(this CGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidPGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaInfBlade<T>(this CGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidCGaInfElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaDirection<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        return cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() &&
               cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaTangent<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        return !cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() &&
               !cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               blade.SpSquared().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaFlat<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return isZeroEiOpX && !isZeroEiIpX ||
               !isZeroEiOpX && isZeroEiIpX;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOpnsFlat<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return isZeroEiOpX && !isZeroEiIpX;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIpnsFlat<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return !isZeroEiOpX && isZeroEiIpX;
    }

    public static bool IsIpnsPointOrHyperSphere<T>(this CGaBlade<T> blade)
    {
        if (!blade.IsVector)
            return false;

        var cgaGeometricSpace = blade.GeometricSpace;

        var eiOpX = blade[0] + blade[1];
        //cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalVector);

        if (eiOpX.IsNearZero())
            return false;

        var eiIpX =
            cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalVector).Scalar();

        if (eiIpX.IsNearZero())
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaRound<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        return !cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() &&
               !cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               !blade.SpSquared().IsNearZero();
    }


    public static CGaElementKind GetElementKind<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return blade.SpSquared().IsNearZero()
                ? CGaElementKind.Tangent
                : CGaElementKind.Round;

        if (isZeroEiOpX && isZeroEiIpX)
            return CGaElementKind.Direction;

        return CGaElementKind.Flat;
    }

    public static CGaElementSpecs<T> GetElementSpecsOpns<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;
        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // OPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new CGaElementSpecs<T>(
                    cgaGeometricSpace,
                    CGaElementKind.Tangent,
                    CGaElementEncoding.Opns,
                    blade.Decode.OpnsTangent.VGaDirection().Grade
                );

            // OPNS Round
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Round,
                CGaElementEncoding.Opns,
                blade.Decode.OpnsRound.VGaDirection().Grade
            );
        }

        // OPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Direction,
                CGaElementEncoding.Opns,
                blade.Decode.OpnsDirection.VGaDirectionAsBlade().Grade
            );

        // OPNS Flat
        if (isZeroEiOpX)
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Flat,
                CGaElementEncoding.Opns,
                blade.Decode.OpnsFlat.VGaDirection().Grade
            );

        // IPNS Flat
        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            blade.Decode.IpnsFlat.VGaDirection().Grade
        );
    }

    public static CGaElementSpecs<T> GetElementSpecsIpns<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;
        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // IPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new CGaElementSpecs<T>(
                    cgaGeometricSpace,
                    CGaElementKind.Tangent,
                    CGaElementEncoding.Ipns,
                    blade.Decode.IpnsTangent.VGaDirection().Grade
                );

            // IPNS Round
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Round,
                CGaElementEncoding.Ipns,
                blade.Decode.IpnsRound.VGaDirection().Grade
            );
        }

        // IPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Direction,
                CGaElementEncoding.Ipns,
                blade.Decode.IpnsDirection.VGaDirectionAsBlade().Grade
            );

        if (isZeroEiOpX)
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Flat,
                CGaElementEncoding.Opns,
                blade.Decode.OpnsFlat.VGaDirection().Grade
            );

        // IPNS Flat
        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Flat,
            CGaElementEncoding.Ipns,
            blade.Decode.IpnsFlat.VGaDirection().Grade
        );
    }

    public static CGaElementSpecs<T> GetElementSpecs<T>(this CGaBlade<T> blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;
        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                blade.SpSquared().IsNearZero()
                    ? CGaElementKind.Tangent
                    : CGaElementKind.Round,
                CGaElementEncoding.OpnsOrIpns
            );

        if (isZeroEiOpX && isZeroEiIpX)
            return new CGaElementSpecs<T>(
                cgaGeometricSpace,
                CGaElementKind.Direction,
                CGaElementEncoding.OpnsOrIpns
            );

        return new CGaElementSpecs<T>(
            cgaGeometricSpace,
            CGaElementKind.Flat,
            isZeroEiOpX
                ? CGaElementEncoding.Opns
                : CGaElementEncoding.Ipns
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaBlade<T>> GetBasisBladesVGa<T>(this CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return (1UL << cgaGeometricSpace.VSpaceDimensions - 2)
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .ConformalProcessor
                    .KVectorTerm((id << 2).ToUInt64IndexSet())
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaBlade<T>> GetBasisBladesPGa<T>(this CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return (1UL << cgaGeometricSpace.VSpaceDimensions - 1)
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade(id.ToUInt64IndexSet())
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaBlade<T>> GetBasisBladesCGa<T>(this CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace
            .GaSpaceDimensions
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade(id.ToUInt64IndexSet())
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaBlade<T>> GetBasisBladesCGaInf<T>(this CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return (1UL << cgaGeometricSpace.VSpaceDimensions - 1)
            .GetRange(id => id << 1)
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade(id.ToUInt64IndexSet())
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Gp<T>(this XGaMultivector<T> mv, CGaBlade<T> blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> Op<T>(this IEnumerable<CGaBlade<T>> bladeList)
    {
        return new CGaBlade<T>(
            bladeList.First().GeometricSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }


    
}