using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, Scalar<T> centerX, Scalar<T> centerY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, LinVector2D<T> center)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared, 
            center.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector2D<T> center)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            center.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector2D<T> center)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            center.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, LinVector2D<T> egaCenter, LinBivector2D<T> egaBivector)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radiusSquared, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radiusSquared, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(conformalSpace.Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return conformalSpace.EncodeIpnsRealRoundCircle(
            radius, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(conformalSpace.Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return conformalSpace.EncodeIpnsImaginaryRoundCircle(
            radius, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(conformalSpace.Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radiusSquared, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(conformalSpace.Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return conformalSpace.EncodeIpnsRealRoundCircle(
            radius, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(conformalSpace.Processor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return conformalSpace.EncodeIpnsImaginaryRoundCircle(
            radius, 
            egaCenter.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared, 
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, LinVector3D<T> center)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared, 
            center.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector3D<T> center)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsRealRoundHyperSphere(
            radius, 
            center.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, LinVector3D<T> center)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius, 
            center.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY);

        var pNormSquared = 
            pointX * pointX + 
            pointY * pointY;

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * pNormSquared * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, T pointX, T pointY)
    {
        var scalarProcessor = conformalSpace.ScalarProcessor;

        var p = 
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY);

        var pNormSquared = 
            scalarProcessor.Square(pointX) + 
            scalarProcessor.Square(pointY);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * pNormSquared * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, T pointX, T pointY, T pointZ)
    {
        var scalarProcessor = conformalSpace.ScalarProcessor;

        var p = 
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY, pointZ);

        var pNormSquared = 
            scalarProcessor.Square(pointX) + 
            scalarProcessor.Square(pointY) + 
            scalarProcessor.Square(pointZ);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * pNormSquared * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY, pointZ);

        var pNormSquared = 
            pointX * pointX + 
            pointY * pointY + 
            pointZ * pointZ;

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * pNormSquared * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.ENormSquared() * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint)
    {
        var p = 
            conformalSpace.EncodeEGaVectorAsVector(egaPoint);

        var kVector = 
            conformalSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * conformalSpace.EiVector;

        return new XGaConformalBlade<T>(conformalSpace, kVector);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .EGaDual()
                .GradeInvolution();

        Debug.Assert(radiusSquared is not null);

        return (conformalSpace.Eo - radiusSquared * conformalSpace.Ei / 2)
            .Op(direction)
            .TranslateBy(egaCenter);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundPointPair(
            radius.Square(), 
            egaCenter, 
            egaDirection
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundPointPair(
            radius.NegativeSquare(), 
            egaCenter, 
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .EGaDual()
                .GradeInvolution();
        
        Debug.Assert(radiusSquared is not null);

        return (conformalSpace.Eo - radiusSquared * conformalSpace.Ei / 2)
            .Op(direction)
            .TranslateBy(egaCenter);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radius.Square(), 
            egaCenter, 
            egaDirection
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        return conformalSpace.EncodeIpnsRoundCircle(
            radius.NegativeSquare(), 
            egaCenter, 
            egaDirection
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundUnitHyperSphere<T>(this XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.Eo - conformalSpace.Ei / 2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundUnitHyperSphere<T>(this XGaConformalSpace<T> conformalSpace)
    {
        return conformalSpace.Eo + conformalSpace.Ei / 2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundHyperSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared)
    {
        Debug.Assert(radiusSquared is not null);

        return conformalSpace.Eo - radiusSquared * conformalSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRoundHyperSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radiusSquared, XGaVector<T> egaCenter)
    {
        Debug.Assert(radiusSquared is not null);

        var c = conformalSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - radiusSquared * conformalSpace.Ei / 2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundHyperSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius)
    {
        var radiusSquared = radius.Square();

        return conformalSpace.Eo - radiusSquared * conformalSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsRealRoundHyperSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, XGaVector<T> egaCenter)
    {
        var radiusSquared = radius.Square();

        var c = conformalSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - radiusSquared * conformalSpace.Ei / 2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundHyperSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius)
    {
        var radiusSquared = radius.Square();

        return conformalSpace.Eo + radiusSquared * conformalSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsImaginaryRoundHyperSphere<T>(this XGaConformalSpace<T> conformalSpace, IScalar<T> radius, XGaVector<T> egaCenter)
    {
        var radiusSquared = radius.Square();

        var c = conformalSpace.EncodeIpnsRoundPoint(egaCenter);

        return c + radiusSquared * conformalSpace.Ei / 2;
    }

}