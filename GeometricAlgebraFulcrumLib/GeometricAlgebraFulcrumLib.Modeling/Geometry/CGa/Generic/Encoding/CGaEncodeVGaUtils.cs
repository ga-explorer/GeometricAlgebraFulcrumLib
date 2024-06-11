using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodeVGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> x, Scalar<T> y)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var zero = cgaGeometricSpace.ScalarProcessor.Zero;

        return cgaGeometricSpace.ConformalProcessor.Vector(zero, zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, ILinVector2D<T> mv)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var zero = cgaGeometricSpace.ScalarProcessor.Zero;

        return cgaGeometricSpace.ConformalProcessor.Vector(zero, zero, mv.X, mv.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T x, T y)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var zero = cgaGeometricSpace.ScalarProcessor.ZeroValue;

        return cgaGeometricSpace.ConformalProcessor.Vector(zero, zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T x, T y, T z)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var zero = cgaGeometricSpace.ScalarProcessor.ZeroValue;

        return cgaGeometricSpace.ConformalProcessor.Vector(zero, zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var zero = cgaGeometricSpace.ScalarProcessor.Zero;

        return cgaGeometricSpace.ConformalProcessor.Vector(zero, zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, ILinVector3D<T> mv)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var zero = cgaGeometricSpace.ScalarProcessor.Zero;

        return cgaGeometricSpace.ConformalProcessor.Vector(zero, zero, mv.X, mv.Y, mv.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> mv)
    {
        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, int xy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, float xy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, double xy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T xy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> xy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector2D<T> bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> xy, Scalar<T> xz, Scalar<T> yz)
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
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector3D<T> bivector)
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
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaBivector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, int xyzScalar)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return xyzScalar * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, float xyzScalar)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return xyzScalar * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, double xyzScalar)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return xyzScalar * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T xyzScalar)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);
        Debug.Assert(xyzScalar is not null);

        return xyzScalar * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> xyzScalar)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return xyzScalar * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinTrivector3D<T> trivector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return trivector.Scalar123 * cgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaKVectorAsXGaKVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaKVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = cgaGeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetKVector(mv.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVectorBlade<T>(this ILinVector2D<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVectorBlade<T>(this ILinVector3D<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVectorBlade<T>(this LinVector<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVectorBlade<T>(this XGaVector<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaVector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivectorBlade<T>(this LinBivector2D<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivectorBlade<T>(this LinBivector3D<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivectorBlade<T>(this XGaBivector<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaTrivectorBlade<T>(this LinTrivector3D<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBlade<T>(this XGaKVector<T> egaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EncodeVGaBlade(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> x, Scalar<T> y)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, ILinVector2D<T> mv)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, ILinVector3D<T> mv)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> mv)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaVector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> mv)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, int xy)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> xy)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector2D<T> bivector)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> xy, Scalar<T> xz, Scalar<T> yz)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector3D<T> bivector)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaBivector<T> mv)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaTrivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, int xyzScalar)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaTrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaTrivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> xyzScalar)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaTrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaTrivector<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinTrivector3D<T> trivector)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaTrivectorAsXGaKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeVGaBlade<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaKVector<T> mv)
    {
        return new CGaBlade<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.EncodeVGaKVectorAsXGaKVector(mv)
        );
    }
}