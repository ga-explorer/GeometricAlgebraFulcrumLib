using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, Scalar<T> centerX, Scalar<T> centerY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, LinVector2D<T> center)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            center.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector2D<T> center)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            center.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector2D<T> center)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            center.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, LinVector2D<T> egaCenter, LinBivector2D<T> egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radiusSquared,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radiusSquared,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsRealRoundCircle(
            radius,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsImaginaryRoundCircle(
            radius,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radiusSquared,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsRealRoundCircle(
            radius,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsImaginaryRoundCircle(
            radius,
            egaCenter.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, LinVector3D<T> center)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            center.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector3D<T> center)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            center.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, LinVector3D<T> center)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            center.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY);

        var pNormSquared =
            pointX * pointX +
            pointY * pointY;

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T pointX, T pointY)
    {
        var scalarProcessor = cgaGeometricSpace.ScalarProcessor;

        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY);

        var pNormSquared =
            scalarProcessor.Square(pointX) +
            scalarProcessor.Square(pointY);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, T pointX, T pointY, T pointZ)
    {
        var scalarProcessor = cgaGeometricSpace.ScalarProcessor;

        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY, pointZ);

        var pNormSquared =
            scalarProcessor.Square(pointX) +
            scalarProcessor.Square(pointY) +
            scalarProcessor.Square(pointZ);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY, pointZ);

        var pNormSquared =
            pointX * pointX +
            pointY * pointY +
            pointZ * pointZ;

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.ENormSquared() * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaBlade<T>(cgaGeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .VGaDual()
                .GradeInvolution();

        Debug.Assert(radiusSquared is not null);

        return (cgaGeometricSpace.Eo - radiusSquared * cgaGeometricSpace.Ei / 2)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPointPair(
            radius.Square(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPointPair(
            radius.NegativeSquare(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .VGaDual()
                .GradeInvolution();

        Debug.Assert(radiusSquared is not null);

        return (cgaGeometricSpace.Eo - radiusSquared * cgaGeometricSpace.Ei / 2)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radius.Square(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radius.NegativeSquare(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundUnitHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.Eo - cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundUnitHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.Eo + cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared)
    {
        Debug.Assert(radiusSquared is not null);

        return cgaGeometricSpace.Eo - radiusSquared * cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRoundHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radiusSquared, XGaVector<T> egaCenter)
    {
        Debug.Assert(radiusSquared is not null);

        var c = cgaGeometricSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - radiusSquared * cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius)
    {
        var radiusSquared = radius.Square();

        return cgaGeometricSpace.Eo - radiusSquared * cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsRealRoundHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, XGaVector<T> egaCenter)
    {
        var radiusSquared = radius.Square();

        var c = cgaGeometricSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - radiusSquared * cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius)
    {
        var radiusSquared = radius.Square();

        return cgaGeometricSpace.Eo + radiusSquared * cgaGeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeIpnsImaginaryRoundHyperSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IScalar<T> radius, XGaVector<T> egaCenter)
    {
        var radiusSquared = radius.Square();

        var c = cgaGeometricSpace.EncodeIpnsRoundPoint(egaCenter);

        return c + radiusSquared * cgaGeometricSpace.Ei / 2;
    }

}