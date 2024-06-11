using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodeVersorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeVGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, Scalar<T> bivectorXy)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = bivectorXy.Abs();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeVGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, LinBivector2D<T> bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeVGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, Scalar<T> bivectorXy, Scalar<T> bivectorXz, Scalar<T> bivectorYz)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = (bivectorXy * bivectorXy + bivectorXz * bivectorXz + bivectorYz * bivectorYz).Sqrt();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivectorXz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivectorYz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeVGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, LinBivector3D<T> bivector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return cgaGeometricSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivector.Xz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivector.Yz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodePGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis =
            cgaGeometricSpace.EncodePGaPoint(egaAxisPoint);

        return new CGaVersor<T>(
            cgaGeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodePGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisDirection)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis = cgaGeometricSpace.EncodePGaLine(egaAxisPoint, egaAxisDirection);

        return new CGaVersor<T>(
            cgaGeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var bivector =
            cgaGeometricSpace.EncodeIpnsFlatPoint(
                egaAxisPoint
            ).InternalBivector;
        //cgaGeometricSpace.EncodeOpnsFlatPoint(
        //    egaAxisPoint
        //).InternalBivector;

        return cgaGeometricSpace.EncodeCGaRotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var bivector =
            cgaGeometricSpace.EncodeIpnsFlatLine(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        return cgaGeometricSpace.EncodeCGaRotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaRotation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinAngle<T> angle, XGaBivector<T> bivector)
    {
        //var halfAngle = angle.HalfPolarAngle();

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        return (halfAngleCos + halfAngleSin / bivector.Norm() * bivector).ToConformalCGaVersor(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodePGaTranslation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaVector));

        return new CGaVersor<T>(
            cgaGeometricSpace,
            1 - 0.5d * cgaGeometricSpace.EoVector.Op(egaVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaTranslation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> vectorX, Scalar<T> vectorY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        var vector = LinVector2D<T>.Create(vectorX, vectorY);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaTranslation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> vector)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaTranslation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        var vector = LinVector3D<T>.Create(vectorX, vectorY, vectorZ);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaTranslation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> vector)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeCGaTranslation(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(vector)
        //vector.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaTranslation<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaVector));

        return (1 + 0.5d * cgaGeometricSpace.EiVector.Op(egaVector))
            .ToConformalCGaVersor(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> EncodeCGaScaling<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T scalingFactor)
    {
        var scalarProcessor = cgaGeometricSpace.ScalarProcessor;
        var g = scalarProcessor.ScalarFromValue(scalingFactor).LogE().Divide(scalarProcessor.TwoValue);

        return (g.Cosh() + g.Sinh() * cgaGeometricSpace.EoiBivector).ToConformalCGaVersor(cgaGeometricSpace);
    }

}