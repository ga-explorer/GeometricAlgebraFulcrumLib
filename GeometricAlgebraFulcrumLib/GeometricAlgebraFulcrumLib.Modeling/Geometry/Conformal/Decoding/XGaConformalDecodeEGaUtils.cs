using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeEGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScalar<T> DecodeEGaBladeToXGaFloat64Scalar<T>(this XGaScalar<T> cgaKVector, XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.EuclideanProcessor.Scalar(
            cgaKVector.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static LinVector<T> DecodeEGaBladeToFloat64Vector<T>(this XGaVector<T> cgaKVector, XGaConformalSpace<T> conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer = conformalSpace.ScalarProcessor.CreateLinVectorComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> DecodeEGaBladeToXGaFloat64Vector<T>(this XGaVector<T> cgaKVector, XGaConformalSpace<T> conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer =
            conformalSpace.EuclideanProcessor.CreateComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetVectorTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> DecodeEGaBladeToXGaFloat64Bivector<T>(this XGaBivector<T> cgaKVector, XGaConformalSpace<T> conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer =
            conformalSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(-2), scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> DecodeEGaBladeToXGaFloat64KVector<T>(this XGaKVector<T> cgaKVector, XGaConformalSpace<T> conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer =
            conformalSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(-2), scalar);

        return composer.GetKVector(cgaKVector.Grade);
    }


    public static XGaConformalDirection<T> DecodeEGaDirection<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(blade.IsEGaBlade());

        var weight = blade.Norm();

        if (weight.IsZero())
            return new XGaConformalDirection<T>(
                blade.ConformalSpace,
                blade.ConformalSpace.ScalarProcessor.Zero,
                blade.ConformalSpace.OneScalarBlade
            );

        return new XGaConformalDirection<T>(
            blade.ConformalSpace,
            weight,
            blade.Divide(weight)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodeEGaVector2D<T>(this XGaConformalBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is4D &&
        //    blade.IsEGaVector()
        //);

        return LinVector2D<T>.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodeEGaVector3D<T>(this XGaConformalBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is5D &&
        //    blade.IsEGaVector()
        //);

        return LinVector3D<T>.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> DecodeEGaVector4D<T>(this XGaConformalBlade<T> blade)
    {
        Debug.Assert(
            blade.VSpaceDimensions == 6 &&
            blade.IsEGaVector()
        );

        return LinVector4D<T>.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4],
            blade.InternalKVector[5]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> DecodeEGaVectorND<T>(this XGaConformalBlade<T> blade)
    {
        return blade.InternalVector.DecodeEGaBladeToFloat64Vector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> DecodeEGaVector<T>(this XGaConformalBlade<T> blade)
    {
        return blade.InternalVector.DecodeEGaBladeToXGaFloat64Vector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> DecodeEGaBivector2D<T>(this XGaConformalBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is4D &&
        //    blade.IsEGaBivector()
        //);

        return LinBivector2D<T>.Create(
            blade.InternalKVector[2, 3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> DecodeEGaBivector3D<T>(this XGaConformalBlade<T> blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is5D &&
        //    blade.IsEGaBivector()
        //);

        return LinBivector3D<T>.Create(
            blade.InternalKVector[2, 3],
            blade.InternalKVector[2, 4],
            blade.InternalKVector[3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> DecodeEGaBivector<T>(this XGaConformalBlade<T> blade)
    {
        return blade.InternalBivector.DecodeEGaBladeToXGaFloat64Bivector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> DecodeEGaTrivector3D<T>(this XGaConformalBlade<T> blade)
    {
        //Debug.Assert(
        //    blade.ConformalSpace.Is5D &&
        //    blade.IsEGaTrivector()
        //);

        return LinTrivector3D<T>.Create(
            blade.InternalKVector[2, 3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> DecodeEGaKVector<T>(this XGaConformalBlade<T> blade)
    {
        return blade.InternalKVector.DecodeEGaBladeToXGaFloat64KVector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector2D<T>> DecodeEGaBladeToVectors2D<T>(this XGaConformalBlade<T> blade)
    {
        return blade.DecodeEGaBladeToVectors().Select(
            v => LinVector2D<T>.Create(v[0], v[1])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector3D<T>> DecodeEGaBladeToVectors3D<T>(this XGaConformalBlade<T> blade)
    {
        return blade.DecodeEGaBladeToVectors().Select(
            v => LinVector3D<T>.Create(v[0], v[1], v[2])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector<T>> DecodeEGaBladeToVectorsND<T>(this XGaConformalBlade<T> blade)
    {
        return blade
            .DecodeEGaKVector()
            .BladeToVectors()
            .SelectToImmutableArray(v => v.ToLinVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<XGaVector<T>> DecodeEGaBladeToVectors<T>(this XGaConformalBlade<T> blade)
    {
        return blade
            .DecodeEGaKVector()
            .BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<XGaConformalBlade<T>> DecodeEGaBladeToVectorEGaBlades<T>(this XGaConformalBlade<T> blade)
    {
        return blade
            .DecodeEGaBladeToVectors()
            .SelectToImmutableArray(blade.ConformalSpace.EncodeEGaVector);
    }

}