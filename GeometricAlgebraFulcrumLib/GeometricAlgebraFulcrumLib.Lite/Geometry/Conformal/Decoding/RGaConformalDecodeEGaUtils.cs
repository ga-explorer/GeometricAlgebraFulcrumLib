using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeEGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Scalar DecodeEGaBladeToRGaFloat64Scalar(this RGaFloat64Scalar cgaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EuclideanProcessor.CreateScalar(
            cgaKVector.ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64Vector DecodeEGaBladeToFloat64Vector(this RGaFloat64Vector cgaKVector, RGaConformalSpace conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer =
            Float64VectorComposer.Create();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector DecodeEGaBladeToRGaFloat64Vector(this RGaFloat64Vector cgaKVector, RGaConformalSpace conformalSpace)
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
    internal static RGaFloat64Bivector DecodeEGaBladeToRGaFloat64Bivector(this RGaFloat64Bivector cgaKVector, RGaConformalSpace conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer =
            conformalSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id >> 2, scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector DecodeEGaBladeToRGaFloat64KVector(this RGaFloat64KVector cgaKVector, RGaConformalSpace conformalSpace)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(cgaKVector)
        );

        var composer =
            conformalSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id >> 2, scalar);

        return composer.GetKVector(cgaKVector.Grade);
    }


    public static RGaConformalDirection DecodeEGaDirection(this RGaConformalBlade blade)
    {
        Debug.Assert(blade.IsEGaBlade());

        var weight = blade.Norm();

        if (weight.IsZero())
            return new RGaConformalDirection(
                blade.ConformalSpace,
                0d,
                blade.ConformalSpace.OneScalarBlade
            );

        return new RGaConformalDirection(
            blade.ConformalSpace,
            weight,
            blade.Divide(weight)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D DecodeEGaVector2D(this RGaConformalBlade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is4D &&
        //    blade.IsEGaVector()
        //);

        return Float64Vector2D.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D DecodeEGaVector3D(this RGaConformalBlade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is5D &&
        //    blade.IsEGaVector()
        //);

        return Float64Vector3D.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D DecodeEGaVector4D(this RGaConformalBlade blade)
    {
        Debug.Assert(
            blade.VSpaceDimensions == 6 &&
            blade.IsEGaVector()
        );

        return Float64Vector4D.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4],
            blade.InternalKVector[5]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector DecodeEGaVectorND(this RGaConformalBlade blade)
    {
        return blade.InternalVector.DecodeEGaBladeToFloat64Vector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector DecodeEGaVector(this RGaConformalBlade blade)
    {
        return blade.InternalVector.DecodeEGaBladeToRGaFloat64Vector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector2D DecodeEGaBivector2D(this RGaConformalBlade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is4D &&
        //    blade.IsEGaBivector()
        //);

        return Float64Bivector2D.Create(
            blade.InternalKVector[2, 3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D DecodeEGaBivector3D(this RGaConformalBlade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is5D &&
        //    blade.IsEGaBivector()
        //);

        return Float64Bivector3D.Create(
            blade.InternalKVector[2, 3],
            blade.InternalKVector[2, 4],
            blade.InternalKVector[3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector DecodeEGaBivector(this RGaConformalBlade blade)
    {
        return blade.InternalBivector.DecodeEGaBladeToRGaFloat64Bivector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Trivector3D DecodeEGaTrivector3D(this RGaConformalBlade blade)
    {
        //Debug.Assert(
        //    blade.ConformalSpace.Is5D &&
        //    blade.IsEGaTrivector()
        //);

        return Float64Trivector3D.Create(
            blade.InternalKVector[2, 3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DecodeEGaKVector(this RGaConformalBlade blade)
    {
        return blade.InternalKVector.DecodeEGaBladeToRGaFloat64KVector(
            blade.ConformalSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Vector2D> DecodeEGaBladeToVectors2D(this RGaConformalBlade blade)
    {
        return blade.DecodeEGaBladeToVectors().Select(
            v => Float64Vector2D.Create(v[0], v[1])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Vector3D> DecodeEGaBladeToVectors3D(this RGaConformalBlade blade)
    {
        return blade.DecodeEGaBladeToVectors().Select(
            v => Float64Vector3D.Create(v[0], v[1], v[2])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Vector> DecodeEGaBladeToVectorsND(this RGaConformalBlade blade)
    {
        return blade
            .DecodeEGaKVector()
            .BladeToVectors()
            .SelectToImmutableArray(v => v.ToLinVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<RGaFloat64Vector> DecodeEGaBladeToVectors(this RGaConformalBlade blade)
    {
        return blade
            .DecodeEGaKVector()
            .BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<RGaConformalBlade> DecodeEGaBladeToVectorEGaBlades(this RGaConformalBlade blade)
    {
        return blade
            .DecodeEGaBladeToVectors()
            .SelectToImmutableArray(blade.ConformalSpace.EncodeEGaVector);
    }

}