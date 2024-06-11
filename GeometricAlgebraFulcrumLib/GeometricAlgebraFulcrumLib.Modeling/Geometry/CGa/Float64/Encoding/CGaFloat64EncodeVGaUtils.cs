using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodeVGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeVGaVectorAsRGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, double x, double y)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor.Vector(0, 0, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeVGaVectorAsRGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, ILinFloat64Vector2D mv)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor.Vector(0, 0, mv.X, mv.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeVGaVectorAsRGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, double x, double y, double z)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.ConformalProcessor.Vector(0, 0, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeVGaVectorAsRGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, ILinFloat64Vector3D mv)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.ConformalProcessor.Vector(0, 0, mv.X, mv.Y, mv.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeVGaVectorAsRGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector mv)
    {
        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Vector EncodeVGaVectorAsRGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeVGaBivectorAsRGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, double xy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeVGaBivectorAsRGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector2D bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeVGaBivectorAsRGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, double xy, double xz, double yz)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .SetBivectorTerm(2, 4, xz)
            .SetBivectorTerm(3, 4, yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeVGaBivectorAsRGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector3D bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .SetBivectorTerm(2, 4, bivector.Xz)
            .SetBivectorTerm(3, 4, bivector.Yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64Bivector EncodeVGaBivectorAsRGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Bivector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id << 2, scalar);

        return composer.GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector EncodeVGaTrivectorAsRGaKVector(this CGaFloat64GeometricSpace cgaGeometricSpace, double xyzScalar)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return xyzScalar * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector EncodeVGaTrivectorAsRGaKVector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Trivector3D trivector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return trivector.Scalar123 * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaFloat64KVector EncodeVGaKVectorAsRGaKVector(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64KVector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id << 2, scalar);

        return composer.GetKVector(mv.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVectorBlade(this ILinFloat64Vector2D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVectorBlade(this ILinFloat64Vector3D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVectorBlade(this LinFloat64Vector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVectorBlade(this RGaFloat64Vector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivectorBlade(this LinFloat64Bivector2D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivectorBlade(this LinFloat64Bivector3D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivectorBlade(this RGaFloat64Bivector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaTrivectorBlade(this LinFloat64Trivector3D egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBlade(this RGaFloat64KVector egaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBlade(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, double x, double y)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, ILinFloat64Vector2D mv)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, double x, double y, double z)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, ILinFloat64Vector3D mv)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector mv)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaVector(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector mv)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, double xy)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsRGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector2D bivector)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsRGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, double xy, double xz, double yz)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsRGaBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector3D bivector)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsRGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBivector(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Bivector mv)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsRGaBivector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaTrivector(this CGaFloat64GeometricSpace cgaGeometricSpace, double xyzScalar)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaTrivectorAsRGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaTrivector(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Trivector3D trivector)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaTrivectorAsRGaKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeVGaBlade(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64KVector mv)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaKVectorAsRGaKVector(mv)
        );
    }
}