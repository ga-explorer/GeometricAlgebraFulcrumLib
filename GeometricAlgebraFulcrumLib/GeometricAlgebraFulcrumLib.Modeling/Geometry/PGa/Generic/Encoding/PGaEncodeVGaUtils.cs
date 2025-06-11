using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Encoding;

public static class PGaEncodeVGaUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, double x, double y)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        var zero = pgaGeometricSpace.ScalarProcessor.Zero;
        var xScalar = pgaGeometricSpace.ScalarProcessor.ScalarFromNumber(x);
        var yScalar = pgaGeometricSpace.ScalarProcessor.ScalarFromNumber(y);

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, xScalar, yScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> x, IScalar<T> y)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        var zero = pgaGeometricSpace.ScalarProcessor.Zero;

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IPair<Scalar<T>> mv)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        var zero = pgaGeometricSpace.ScalarProcessor.Zero;

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, mv.Item1, mv.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, T x, T y)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        var zero = pgaGeometricSpace.ScalarProcessor.ZeroValue;

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, T x, T y, T z)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        var zero = pgaGeometricSpace.ScalarProcessor.ZeroValue;

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> x, IScalar<T> y, IScalar<T> z)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        var zero = pgaGeometricSpace.ScalarProcessor.Zero;

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, ITriplet<Scalar<T>> mv)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        var zero = pgaGeometricSpace.ScalarProcessor.Zero;

        return pgaGeometricSpace.ProjectiveProcessor.Vector(zero, mv.Item1, mv.Item2, mv.Item3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> mv)
    {
        var composer = pgaGeometricSpace.ProjectiveProcessor.CreateVectorComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 1, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> EncodeVGaVectorAsXGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = pgaGeometricSpace.ProjectiveProcessor.CreateVectorComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 1, scalar);

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, int xy)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, float xy)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, double xy)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, T xy)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> xy)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinBivector2D<T> bivector)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> xy, IScalar<T> xz, IScalar<T> yz)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, xy)
            .SetBivectorTerm(1, 3, xz)
            .SetBivectorTerm(2, 3, yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinBivector3D<T> bivector)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.ProjectiveProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(1, 2, bivector.Xy)
            .SetBivectorTerm(1, 3, bivector.Xz)
            .SetBivectorTerm(2, 3, bivector.Yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> EncodeVGaBivectorAsXGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaBivector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = pgaGeometricSpace.ProjectiveProcessor.CreateBivectorComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(1), scalar);

        return composer.GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, int xyzScalar)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return xyzScalar * pgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, float xyzScalar)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return xyzScalar * pgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, double xyzScalar)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return xyzScalar * pgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, T xyzScalar)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);
        Debug.Assert(xyzScalar is not null);

        return xyzScalar * pgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> xyzScalar)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.Ie.InternalKVector.Times(xyzScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaTrivectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinTrivector3D<T> trivector)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return trivector.Scalar123 * pgaGeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> EncodeVGaKVectorAsXGaKVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaKVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = pgaGeometricSpace.ProjectiveProcessor.CreateKVectorComposer(mv.Grade);

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(1), scalar);

        return composer.GetKVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVectorBlade<T>(this IPair<Scalar<T>> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVectorBlade<T>(this ITriplet<Scalar<T>> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVectorBlade<T>(this LinVector<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVectorBlade<T>(this XGaVector<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaVector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivectorBlade<T>(this LinBivector2D<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivectorBlade<T>(this LinBivector3D<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivectorBlade<T>(this XGaBivector<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaBivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaTrivectorBlade<T>(this LinTrivector3D<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBlade<T>(this XGaKVector<T> egaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return pgaGeometricSpace.EncodeVGaBlade(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> x, IScalar<T> y)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IPair<Scalar<T>> mv)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> x, IScalar<T> y, IScalar<T> z)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, ITriplet<Scalar<T>> mv)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> mv)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaVector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> mv)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, int xy)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> xy)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinBivector2D<T> bivector)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> xy, IScalar<T> xz, IScalar<T> yz)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinBivector3D<T> bivector)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaBivector<T> mv)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaBivectorAsXGaBivector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaTrivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, int xyzScalar)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaTrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaTrivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> xyzScalar)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaTrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaTrivector<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinTrivector3D<T> trivector)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaTrivectorAsXGaKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodeVGaBlade<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaKVector<T> mv)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.EncodeVGaKVectorAsXGaKVector(mv)
        );
    }
}