using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;

public static class PGaBladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaVector<T>(this PGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaBivector<T>(this PGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaTrivector<T>(this PGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector) &&
               blade.InternalKVector.Grade == 3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVGaBlade<T>(this PGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidVGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPGaBlade<T>(this PGaBlade<T> blade)
    {
        return blade.GeometricSpace.IsValidElement(blade.InternalKVector);
    }


    public static PGaElementKind GetElementKind<T>(this PGaBlade<T> blade)
    {
        var signature = blade.InternalKVector.SpSquared();

        if (signature.IsZero())
            return PGaElementKind.Ideal;

        if (signature.IsNotZero())
            return PGaElementKind.Euclidean;

        throw new InvalidOperationException();
    }

    public static PGaElementSpecs<T> GetElementSpecs<T>(this PGaBlade<T> blade)
    {
        var pgaGeometricSpace = blade.GeometricSpace;

        var signature = blade.InternalKVector.SpSquared();

        if (signature.IsZero())
            return new PGaElementSpecs<T>(
                pgaGeometricSpace,
                PGaElementKind.Ideal,
                PGaElementEncoding.PGa
            );

        if (signature.IsNotZero())
            return new PGaElementSpecs<T>(
                pgaGeometricSpace,
                PGaElementKind.Euclidean,
                PGaElementEncoding.PGa
            );

        throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<PGaBlade<T>> GetBasisBladesVGa<T>(this PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return (1UL << pgaGeometricSpace.VSpaceDimensions - 1)
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                pgaGeometricSpace
                    .ProjectiveProcessor
                    .KVectorTerm((id << 1).ToUInt64IndexSet())
                    .ToProjectiveBlade(pgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<PGaBlade<T>> GetBasisBladesPGa<T>(this PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace
            .GaSpaceDimensions
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                pgaGeometricSpace
                    .ProjectiveProcessor
                    .KVectorTerm(id.ToUInt64IndexSet())
                    .ToProjectiveBlade(pgaGeometricSpace)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Gp<T>(this XGaMultivector<T> mv, PGaBlade<T> blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> Op<T>(this IEnumerable<PGaBlade<T>> bladeList)
    {
        return new PGaBlade<T>(
            bladeList.First().GeometricSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }
}