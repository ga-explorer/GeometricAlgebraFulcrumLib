using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public sealed class CGaVGaEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaVGaEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(IScalar<T> x, IScalar<T> y)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var zero = GeometricSpace.ScalarProcessor.Zero;

        return GeometricSpace.ConformalProcessor.Vector(zero, zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(ILinVector2D<T> mv)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var zero = GeometricSpace.ScalarProcessor.Zero;

        return GeometricSpace.ConformalProcessor.Vector(zero, zero, mv.X, mv.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(T x, T y)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var zero = GeometricSpace.ScalarProcessor.ZeroValue;

        return GeometricSpace.ConformalProcessor.Vector(zero, zero, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(T x, T y, T z)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var zero = GeometricSpace.ScalarProcessor.ZeroValue;

        return GeometricSpace.ConformalProcessor.Vector(zero, zero, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(double x, double y, double z)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor.Vector(0, 0, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(IScalar<T> x, IScalar<T> y, IScalar<T> z)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var zero = GeometricSpace.ScalarProcessor.Zero;

        return GeometricSpace.ConformalProcessor.Vector(zero, zero, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(ILinVector3D<T> mv)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var zero = GeometricSpace.ScalarProcessor.Zero;

        return GeometricSpace.ConformalProcessor.Vector(zero, zero, mv.X, mv.Y, mv.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(LinVector<T> mv)
    {
        var composer = GeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> VectorAsXGaVector(XGaVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = GeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(int xy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(float xy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(double xy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(T xy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(IScalar<T> xy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(LinBivector2D<T> bivector)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(IScalar<T> xy, IScalar<T> xz, IScalar<T> yz)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, xy)
            .SetBivectorTerm(2, 4, xz)
            .SetBivectorTerm(3, 4, yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(LinBivector3D<T> bivector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .SetBivectorTerm(2, 4, bivector.Xz)
            .SetBivectorTerm(3, 4, bivector.Yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> BivectorAsXGaBivector(XGaBivector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = GeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> TrivectorAsXGaKVector(int xyzScalar)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return xyzScalar * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> TrivectorAsXGaKVector(float xyzScalar)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return xyzScalar * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> TrivectorAsXGaKVector(double xyzScalar)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return xyzScalar * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> TrivectorAsXGaKVector(T xyzScalar)
    {
        Debug.Assert(GeometricSpace.Is5D);
        Debug.Assert(xyzScalar is not null);

        return xyzScalar * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> TrivectorAsXGaKVector(IScalar<T> xyzScalar)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.Ie.InternalKVector.Times(xyzScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> TrivectorAsXGaKVector(LinTrivector3D<T> trivector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return trivector.Scalar123 * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> KVectorAsXGaKVector(XGaKVector<T> mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = GeometricSpace.ConformalProcessor.CreateComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetKVector(mv.Grade);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(IScalar<T> x, IScalar<T> y)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(ILinVector2D<T> mv)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(double x, double y, double z)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(IScalar<T> x, IScalar<T> y, IScalar<T> z)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(ILinVector3D<T> mv)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(LinVector<T> mv)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Vector(XGaVector<T> mv)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(int xy)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            BivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(IScalar<T> xy)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            BivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(LinBivector2D<T> bivector)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            BivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(IScalar<T> xy, IScalar<T> xz, IScalar<T> yz)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            BivectorAsXGaBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(LinBivector3D<T> bivector)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            BivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Bivector(XGaBivector<T> mv)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            BivectorAsXGaBivector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Trivector(int xyzScalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            TrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Trivector(IScalar<T> xyzScalar)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            TrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Trivector(LinTrivector3D<T> trivector)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            TrivectorAsXGaKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaKVector<T> mv)
    {
        return new CGaBlade<T>(
            GeometricSpace,
            KVectorAsXGaKVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, Scalar<T> bivectorXy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = bivectorXy.Abs();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, LinBivector2D<T> bivector)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, Scalar<T> bivectorXy, Scalar<T> bivectorXz, Scalar<T> bivectorYz)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = (bivectorXy * bivectorXy + bivectorXz * bivectorXz + bivectorYz * bivectorYz).Sqrt();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivectorXz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivectorYz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, LinBivector3D<T> bivector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return GeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivector.Xz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivector.Yz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

}