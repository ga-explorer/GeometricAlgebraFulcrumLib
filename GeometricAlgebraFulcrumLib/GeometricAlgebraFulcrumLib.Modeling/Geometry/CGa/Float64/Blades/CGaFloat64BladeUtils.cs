using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

public static class CGaFloat64BladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaVector(this CGaFloat64Blade blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaBivector(this CGaFloat64Blade blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaTrivector(this CGaFloat64Blade blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsTrivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaBlade(this CGaFloat64Blade blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPGaBlade(this CGaFloat64Blade blade)
    {
        return blade.GeometricSpace.IsValidPGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaInfBlade(this CGaFloat64Blade blade)
    {
        return blade.GeometricSpace.IsValidCGaInfElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaDirection(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        return cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() &&
               cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaTangent(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        return !cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() &&
               !cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               blade.SpSquared().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaFlat(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return isZeroEiOpX && !isZeroEiIpX ||
               !isZeroEiOpX && isZeroEiIpX;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOpnsFlat(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return isZeroEiOpX && !isZeroEiIpX;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIpnsFlat(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        return !isZeroEiOpX && isZeroEiIpX;
    }

    public static bool IsIpnsPointOrHyperSphere(this CGaFloat64Blade blade)
    {
        if (!blade.IsVector)
            return false;

        var cgaGeometricSpace = blade.GeometricSpace;

        var eiOpX = blade[0] + blade[1];
        //cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalVector);

        if (eiOpX.IsNearZero())
            return false;

        var eiIpX =
            cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalVector).ScalarValue;

        if (eiIpX.IsNearZero())
            return false;

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCGaRound(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        return !cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero() &&
               !cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero() &&
               !blade.SpSquared().IsNearZero();
    }


    public static CGaFloat64ElementKind GetElementKind(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;

        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return blade.SpSquared().IsNearZero()
                ? CGaFloat64ElementKind.Tangent
                : CGaFloat64ElementKind.Round;

        if (isZeroEiOpX && isZeroEiIpX)
            return CGaFloat64ElementKind.Direction;

        return CGaFloat64ElementKind.Flat;
    }

    public static CGaFloat64ElementSpecs GetElementSpecsOpns(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;
        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // OPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new CGaFloat64ElementSpecs(
                    cgaGeometricSpace,
                    CGaFloat64ElementKind.Tangent,
                    CGaFloat64ElementEncoding.Opns,
                    blade.DecodeOpnsTangent.VGaDirection().Grade
                );

            // OPNS Round
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Round,
                CGaFloat64ElementEncoding.Opns,
                blade.DecodeOpnsRound.VGaDirection().Grade
            );
        }

        // OPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Direction,
                CGaFloat64ElementEncoding.Opns,
                blade.DecodeOpnsDirection.VGaDirection().Grade
            );

        // OPNS Flat
        if (isZeroEiOpX)
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Flat,
                CGaFloat64ElementEncoding.Opns,
                blade.DecodeOpnsFlat.VGaDirection().Grade
            );

        // IPNS Flat
        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            blade.DecodeIpnsFlat.VGaDirection().Grade
        );
    }

    public static CGaFloat64ElementSpecs GetElementSpecsIpns(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;
        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
        {
            // IPNS Tangent
            if (blade.SpSquared().IsNearZero())
                return new CGaFloat64ElementSpecs(
                    cgaGeometricSpace,
                    CGaFloat64ElementKind.Tangent,
                    CGaFloat64ElementEncoding.Ipns,
                    blade.DecodeIpnsTangent.VGaDirection().Grade
                );

            // IPNS Round
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Round,
                CGaFloat64ElementEncoding.Ipns,
                blade.DecodeIpnsRound.VGaDirection().Grade
            );
        }

        // IPNS Direction
        if (isZeroEiOpX && isZeroEiIpX)
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Direction,
                CGaFloat64ElementEncoding.Ipns,
                blade.DecodeIpnsDirection.VGaDirection().Grade
            );

        if (isZeroEiOpX)
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Flat,
                CGaFloat64ElementEncoding.Opns,
                blade.DecodeOpnsFlat.VGaDirection().Grade
            );

        // IPNS Flat
        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Flat,
            CGaFloat64ElementEncoding.Ipns,
            blade.DecodeIpnsFlat.VGaDirection().Grade
        );
    }

    public static CGaFloat64ElementSpecs GetElementSpecs(this CGaFloat64Blade blade)
    {
        var cgaGeometricSpace = blade.GeometricSpace;
        var isZeroEiOpX = cgaGeometricSpace.Ei.InternalVector.Op(blade.InternalKVector).IsNearZero();
        var isZeroEiIpX = cgaGeometricSpace.Ei.InternalVector.Lcp(blade.InternalKVector).IsNearZero();

        if (!isZeroEiOpX && !isZeroEiIpX)
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                blade.SpSquared().IsNearZero()
                    ? CGaFloat64ElementKind.Tangent
                    : CGaFloat64ElementKind.Round,
                CGaFloat64ElementEncoding.OpnsOrIpns
            );

        if (isZeroEiOpX && isZeroEiIpX)
            return new CGaFloat64ElementSpecs(
                cgaGeometricSpace,
                CGaFloat64ElementKind.Direction,
                CGaFloat64ElementEncoding.OpnsOrIpns
            );

        return new CGaFloat64ElementSpecs(
            cgaGeometricSpace,
            CGaFloat64ElementKind.Flat,
            isZeroEiOpX
                ? CGaFloat64ElementEncoding.Opns
                : CGaFloat64ElementEncoding.Ipns
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaFloat64Blade> GetBasisBladesVGa(this CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return (1UL << cgaGeometricSpace.VSpaceDimensions - 2)
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .ConformalProcessor
                    .KVectorTerm(id << 2)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaFloat64Blade> GetBasisBladesPGa(this CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return (1UL << cgaGeometricSpace.VSpaceDimensions - 1)
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade((IndexSet)id)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaFloat64Blade> GetBasisBladesCGa(this CGaFloat64GeometricSpace cgaGeometricSpace)
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
                    .OmMapBasisBlade((IndexSet)id)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<CGaFloat64Blade> GetBasisBladesCGaInf(this CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return (1UL << cgaGeometricSpace.VSpaceDimensions - 1)
            .GetRange(id => id << 1)
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                cgaGeometricSpace
                    .BasisSpecs
                    .BasisMapInverse
                    .OmMapBasisBlade((IndexSet)id)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Gp(this XGaFloat64Multivector mv, CGaFloat64Blade blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade Op(this IEnumerable<CGaFloat64Blade> bladeList)
    {
        return new CGaFloat64Blade(
            bladeList.First().GeometricSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }
}