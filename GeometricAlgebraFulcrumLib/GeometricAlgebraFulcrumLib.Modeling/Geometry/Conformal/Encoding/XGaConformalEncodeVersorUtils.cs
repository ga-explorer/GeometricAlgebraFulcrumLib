using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeVersorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeEGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, Scalar<T> bivectorXy)
    {
        Debug.Assert(conformalSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = bivectorXy.Abs();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeEGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, LinBivector2D<T> bivector)
    {
        Debug.Assert(conformalSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeEGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, Scalar<T> bivectorXy, Scalar<T> bivectorXz, Scalar<T> bivectorYz)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorNorm = (bivectorXy * bivectorXy + bivectorXz * bivectorXz + bivectorYz * bivectorYz).Sqrt();
        var bivectorScalar = halfAngleSin / bivectorNorm;

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivectorXy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivectorXz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivectorYz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeEGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, LinBivector3D<T> bivector)
    {
        Debug.Assert(conformalSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var scalar = halfAngleCos;
        var bivectorScalar = halfAngleSin / bivector.Norm();

        return conformalSpace.ConformalProcessor
            .CreateComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(2, 3, bivectorScalar * bivector.Xy)
            .SetBivectorTerm(2, 4, bivectorScalar * bivector.Xz)
            .SetBivectorTerm(3, 4, bivectorScalar * bivector.Yz)
            .GetSimpleMultivector()
            .ToConformalCGaVersor(conformalSpace);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodePGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        Debug.Assert(conformalSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis = 
            conformalSpace.EncodePGaPoint(egaAxisPoint);

        return new XGaConformalVersor<T>(
            conformalSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodePGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisDirection)
    {
        Debug.Assert(conformalSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis = conformalSpace.EncodePGaLine(egaAxisPoint, egaAxisDirection);

        return new XGaConformalVersor<T>(
            conformalSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        Debug.Assert(conformalSpace.Is4D);

        var bivector = 
            conformalSpace.EncodeIpnsFlatPoint(
                egaAxisPoint
            ).InternalBivector;
            //conformalSpace.EncodeOpnsFlatPoint(
            //    egaAxisPoint
            //).InternalBivector;

        return conformalSpace.EncodeCGaRotation(angle, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        Debug.Assert(conformalSpace.Is5D);

        var bivector = 
            conformalSpace.EncodeIpnsFlatLine(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        return conformalSpace.EncodeCGaRotation(angle, bivector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaRotation<T>(this XGaConformalSpace<T> conformalSpace, LinAngle<T> angle, XGaBivector<T> bivector)
    {
        //var halfAngle = angle.HalfPolarAngle();

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        return (halfAngleCos + halfAngleSin / bivector.Norm() * bivector).ToConformalCGaVersor(conformalSpace);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodePGaTranslation<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaVector));

        return new XGaConformalVersor<T>(
            conformalSpace,
            1 - 0.5d * conformalSpace.EoVector.Op(egaVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaTranslation<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> vectorX, Scalar<T> vectorY)
    {
        Debug.Assert(conformalSpace.Is4D);

        var vector = LinVector2D<T>.Create(vectorX, vectorY);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaTranslation<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> vector)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaTranslation<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> vectorX, Scalar<T> vectorY, Scalar<T> vectorZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        var vector = LinVector3D<T>.Create(vectorX, vectorY, vectorZ);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaTranslation<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> vector)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeCGaTranslation(
            conformalSpace.EncodeEGaVectorAsVector(vector)
            //vector.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaTranslation<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaVector));

        return (1 + 0.5d * conformalSpace.EiVector.Op(egaVector))
            .ToConformalCGaVersor(conformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> EncodeCGaScaling<T>(this XGaConformalSpace<T> conformalSpace, T scalingFactor)
    {
        var scalarProcessor = conformalSpace.ScalarProcessor;
        var g = scalarProcessor.ScalarFromValue(scalingFactor).LogE().Divide(scalarProcessor.TwoValue);
        
        return (g.Cosh() + g.Sinh() * conformalSpace.EoiBivector).ToConformalCGaVersor(conformalSpace);
    }

}