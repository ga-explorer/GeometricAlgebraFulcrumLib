using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public class CGaFloat64VGaEncoder :
    CGaFloat64EncoderBase
{
    internal CGaFloat64VGaEncoder(CGaFloat64GeometricSpace geometricSpace) 
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VectorAsXGaVector(double x, double y)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor.Vector(0, 0, x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VectorAsXGaVector(ILinFloat64Vector2D mv)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor.Vector(0, 0, mv.X, mv.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VectorAsXGaVector(double x, double y, double z)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor.Vector(0, 0, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VectorAsXGaVector(ILinFloat64Vector3D mv)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor.Vector(0, 0, mv.X, mv.Y, mv.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VectorAsXGaVector(LinFloat64Vector mv)
    {
        var composer = GeometricSpace.ConformalProcessor.CreateVectorComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector VectorAsXGaVector(XGaFloat64Vector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = GeometricSpace.ConformalProcessor.CreateVectorComposer();

        foreach (var (index, scalar) in mv.IndexScalarPairs)
            composer.SetVectorTerm(index + 2, scalar);

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector BivectorAsXGaBivector(double xy)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(2, 3, xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector BivectorAsXGaBivector(LinFloat64Bivector2D bivector)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return GeometricSpace.ConformalProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector BivectorAsXGaBivector(double xy, double xz, double yz)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(2, 3, xy)
            .SetBivectorTerm(2, 4, xz)
            .SetBivectorTerm(3, 4, yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector BivectorAsXGaBivector(LinFloat64Bivector3D bivector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return GeometricSpace.ConformalProcessor
            .CreateBivectorComposer()
            .SetBivectorTerm(2, 3, bivector.Xy)
            .SetBivectorTerm(2, 4, bivector.Xz)
            .SetBivectorTerm(3, 4, bivector.Yz)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector BivectorAsXGaBivector(XGaFloat64Bivector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = GeometricSpace.ConformalProcessor.CreateBivectorComposer();

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector TrivectorAsXGaKVector(double xyzScalar)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return xyzScalar * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector TrivectorAsXGaKVector(LinFloat64Trivector3D trivector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return trivector.Scalar123 * GeometricSpace.Ie.InternalKVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector KVectorAsXGaKVector(XGaFloat64KVector mv)
    {
        Debug.Assert(mv.Processor.IsEuclidean);

        var composer = GeometricSpace.ConformalProcessor.CreateKVectorComposer(mv.Grade);

        foreach (var (id, scalar) in mv.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(2), scalar);

        return composer.GetKVector();
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(double x, double y)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            VectorAsXGaVector(x, y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(ILinFloat64Vector2D mv)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(double x, double y, double z)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            VectorAsXGaVector(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(ILinFloat64Vector3D mv)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(LinFloat64Vector mv)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Vector(XGaFloat64Vector mv)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            VectorAsXGaVector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(double xy)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            BivectorAsXGaBivector(xy)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(LinFloat64Bivector2D bivector)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            BivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(double xy, double xz, double yz)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            BivectorAsXGaBivector(xy, xz, yz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(LinFloat64Bivector3D bivector)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            BivectorAsXGaBivector(bivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Bivector(XGaFloat64Bivector mv)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            BivectorAsXGaBivector(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Trivector(double xyzScalar)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            TrivectorAsXGaKVector(xyzScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Trivector(LinFloat64Trivector3D trivector)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            TrivectorAsXGaKVector(trivector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Blade(XGaFloat64KVector mv)
    {
        return new CGaFloat64Blade(
            GeometricSpace,
            KVectorAsXGaKVector(mv)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, double bivectorXy = 1d)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = bivectorXy.Abs();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return GeometricSpace.ConformalProcessor
            .CreateMultivectorComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .GetMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, LinFloat64Bivector2D bivector)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return GeometricSpace.ConformalProcessor
            .CreateMultivectorComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .GetMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, double bivectorXy, double bivectorXz, double bivectorYz)
    {
        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = (bivectorXy * bivectorXy + bivectorXz * bivectorXz + bivectorYz * bivectorYz).Sqrt();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return GeometricSpace.ConformalProcessor
            .CreateMultivectorComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivectorXz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivectorYz)
            .GetMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, LinFloat64Bivector3D bivector)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) =
            angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return GeometricSpace.ConformalProcessor
            .CreateMultivectorComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivector.Xz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivector.Yz)
            .GetMultivector()
            .ToConformalCGaVersor(GeometricSpace);
    }


    
}