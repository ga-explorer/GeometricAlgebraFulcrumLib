using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;

public static class PGaDecodeVGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScalar<T> DecodeVGaBladeToXGaFloat64Scalar<T>(this XGaScalar<T> cgaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EuclideanProcessor.Scalar(
            cgaKVector.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static LinVector<T> DecodeVGaBladeToFloat64Vector<T>(this XGaVector<T> cgaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer = pgaGeometricSpace.ScalarProcessor.CreateLinVectorComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> DecodeVGaBladeToXGaFloat64Vector<T>(this XGaVector<T> cgaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            pgaGeometricSpace.EuclideanProcessor.CreateVectorComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetVectorTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> DecodeVGaBladeToXGaFloat64Bivector<T>(this XGaBivector<T> cgaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            pgaGeometricSpace.EuclideanProcessor.CreateBivectorComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(-2), scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> DecodeVGaBladeToXGaFloat64KVector<T>(this XGaKVector<T> cgaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            pgaGeometricSpace.EuclideanProcessor.CreateKVectorComposer(cgaKVector.Grade);

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(-2), scalar);

        return composer.GetKVector();
    }


    public static PGaElement<T> DecodeVGaDirection<T>(this PGaBlade<T> blade)
    {
        Debug.Assert(blade.IsVGaBlade());

        var weight = blade.Norm();

        if (weight.IsZero())
            return new PGaElement<T>(
                blade.GeometricSpace,
                blade.GeometricSpace.ScalarProcessor.Zero,
                blade.GeometricSpace.OneScalarBlade
            );

        return new PGaElement<T>(
            blade.GeometricSpace,
            weight,
            blade.Divide(weight)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeVGaVector2D<T>(this PGaBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ProjectiveSpace.Is4D &&
        //    blade.IsVGaVector()
        //);

        return LinVector2D<T>.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeVGaVector3D<T>(this PGaBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ProjectiveSpace.Is5D &&
        //    blade.IsVGaVector()
        //);

        return LinVector3D<T>.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> DecodeVGaVector4D<T>(this PGaBlade<T> blade)
    {
        Debug.Assert(
            blade.VSpaceDimensions == 6 &&
            blade.IsVGaVector()
        );

        return LinVector4D<T>.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4],
            blade.InternalKVector[5]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> DecodeVGaVectorND<T>(this PGaBlade<T> blade)
    {
        return blade.InternalVector.DecodeVGaBladeToFloat64Vector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> DecodeVGaVector<T>(this PGaBlade<T> blade)
    {
        return blade.InternalVector.DecodeVGaBladeToXGaFloat64Vector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> DecodeVGaBivector2D<T>(this PGaBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ProjectiveSpace.Is4D &&
        //    blade.IsVGaBivector()
        //);

        return LinBivector2D<T>.Create(
            blade.InternalKVector[2, 3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> DecodeVGaBivector3D<T>(this PGaBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ProjectiveSpace.Is5D &&
        //    blade.IsVGaBivector()
        //);

        return LinBivector3D<T>.Create(
            blade.InternalKVector[2, 3],
            blade.InternalKVector[2, 4],
            blade.InternalKVector[3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> DecodeVGaBivector<T>(this PGaBlade<T> blade)
    {
        return blade.InternalBivector.DecodeVGaBladeToXGaFloat64Bivector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> DecodeVGaTrivector3D<T>(this PGaBlade<T> blade)
    {
        //Debug.Assert(
        //    blade.ProjectiveSpace.Is5D &&
        //    blade.IsVGaTrivector()
        //);

        return LinTrivector3D<T>.Create(
            blade.InternalKVector[2, 3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> DecodeVGaKVector<T>(this PGaBlade<T> blade)
    {
        return blade.InternalKVector.DecodeVGaBladeToXGaFloat64KVector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector2D<T>> DecodeVGaBladeToVectors2D<T>(this PGaBlade<T> blade)
    {
        return blade.DecodeVGaBladeToVectors().Select(
            v => LinVector2D<T>.Create(v[0], v[1])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector3D<T>> DecodeVGaBladeToVectors3D<T>(this PGaBlade<T> blade)
    {
        return blade.DecodeVGaBladeToVectors().Select(
            v => LinVector3D<T>.Create(v[0], v[1], v[2])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector<T>> DecodeVGaBladeToVectorsND<T>(this PGaBlade<T> blade)
    {
        return blade
            .DecodeVGaKVector()
            .BladeToVectors()
            .SelectToImmutableArray(v => v.ToLinVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<XGaVector<T>> DecodeVGaBladeToVectors<T>(this PGaBlade<T> blade)
    {
        return blade
            .DecodeVGaKVector()
            .BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<PGaBlade<T>> DecodeVGaBladeToVectorVGaBlades<T>(this PGaBlade<T> blade)
    {
        return blade
            .DecodeVGaBladeToVectors()
            .SelectToImmutableArray(blade.GeometricSpace.EncodeVGaVector);
    }

}