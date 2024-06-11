using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeVGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Scalar DecodeVGaBladeToRGaFloat64Scalar(this RGaFloat64Scalar cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EuclideanProcessor.Scalar(
            cgaKVector.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static LinFloat64Vector DecodeVGaBladeToFloat64Vector(this RGaFloat64Vector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            LinFloat64VectorComposer.Create();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector DecodeVGaBladeToRGaFloat64Vector(this RGaFloat64Vector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetVectorTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector DecodeVGaBladeToRGaFloat64Bivector(this RGaFloat64Bivector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id >> 2, scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector DecodeVGaBladeToRGaFloat64KVector(this RGaFloat64KVector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id >> 2, scalar);

        return composer.GetKVector(cgaKVector.Grade);
    }


    public static CGaFloat64Direction DecodeVGaDirection(this CGaFloat64Blade blade)
    {
        Debug.Assert(blade.IsVGaBlade());

        var weight = blade.Norm();

        if (weight.IsZero())
            return new CGaFloat64Direction(
                blade.GeometricSpace,
                0d,
                blade.GeometricSpace.OneScalarBlade
            );

        return new CGaFloat64Direction(
            blade.GeometricSpace,
            weight,
            blade.Divide(weight)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodeVGaVector2D(this CGaFloat64Blade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is4D &&
        //    blade.IsVGaVector()
        //);

        return LinFloat64Vector2D.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodeVGaVector3D(this CGaFloat64Blade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is5D &&
        //    blade.IsVGaVector()
        //);

        return LinFloat64Vector3D.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D DecodeVGaVector4D(this CGaFloat64Blade blade)
    {
        Debug.Assert(
            blade.VSpaceDimensions == 6 &&
            blade.IsVGaVector()
        );

        return LinFloat64Vector4D.Create(
            blade.InternalKVector[2],
            blade.InternalKVector[3],
            blade.InternalKVector[4],
            blade.InternalKVector[5]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector DecodeVGaVectorND(this CGaFloat64Blade blade)
    {
        return blade.InternalVector.DecodeVGaBladeToFloat64Vector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector DecodeVGaVector(this CGaFloat64Blade blade)
    {
        return blade.InternalVector.DecodeVGaBladeToRGaFloat64Vector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D DecodeVGaBivector2D(this CGaFloat64Blade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is4D &&
        //    blade.IsVGaBivector()
        //);

        return LinFloat64Bivector2D.Create(
            blade.InternalKVector[2, 3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D DecodeVGaBivector3D(this CGaFloat64Blade blade)
    {
        //Debug.Assert(
        //    //blade.ConformalSpace.Is5D &&
        //    blade.IsVGaBivector()
        //);

        return LinFloat64Bivector3D.Create(
            blade.InternalKVector[2, 3],
            blade.InternalKVector[2, 4],
            blade.InternalKVector[3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector DecodeVGaBivector(this CGaFloat64Blade blade)
    {
        return blade.InternalBivector.DecodeVGaBladeToRGaFloat64Bivector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D DecodeVGaTrivector3D(this CGaFloat64Blade blade)
    {
        //Debug.Assert(
        //    blade.ConformalSpace.Is5D &&
        //    blade.IsVGaTrivector()
        //);

        return LinFloat64Trivector3D.Create(
            blade.InternalKVector[2, 3, 4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DecodeVGaKVector(this CGaFloat64Blade blade)
    {
        return blade.InternalKVector.DecodeVGaBladeToRGaFloat64KVector(
            blade.GeometricSpace
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64Vector2D> DecodeVGaBladeToVectors2D(this CGaFloat64Blade blade)
    {
        return blade.DecodeVGaBladeToVectors().Select(
            v => LinFloat64Vector2D.Create(v[0], v[1])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64Vector3D> DecodeVGaBladeToVectors3D(this CGaFloat64Blade blade)
    {
        return blade.DecodeVGaBladeToVectors().Select(
            v => LinFloat64Vector3D.Create(v[0], v[1], v[2])
        ).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64Vector> DecodeVGaBladeToVectorsND(this CGaFloat64Blade blade)
    {
        return blade
            .DecodeVGaKVector()
            .BladeToVectors()
            .SelectToImmutableArray(v => v.ToLinVector());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<RGaFloat64Vector> DecodeVGaBladeToVectors(this CGaFloat64Blade blade)
    {
        return blade
            .DecodeVGaKVector()
            .BladeToVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<CGaFloat64Blade> DecodeVGaBladeToVectorVGaBlades(this CGaFloat64Blade blade)
    {
        return blade
            .DecodeVGaBladeToVectors()
            .SelectToImmutableArray(blade.GeometricSpace.EncodeVGaVector);
    }

}