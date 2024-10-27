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

public sealed class CGaIpnsRoundEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaIpnsRoundEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(IScalar<T> radiusSquared, IScalar<T> centerX, IScalar<T> centerY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return HyperSphere(
            radiusSquared,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(IScalar<T> radiusSquared, LinVector2D<T> center)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return HyperSphere(
            radiusSquared,
            center.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealCircle(IScalar<T> radius, IScalar<T> centerX, IScalar<T> centerY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return RealHyperSphere(
            radius,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealCircle(IScalar<T> radius, LinVector2D<T> center)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return RealHyperSphere(
            radius,
            center.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryCircle(IScalar<T> radius, IScalar<T> centerX, IScalar<T> centerY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return ImaginaryHyperSphere(
            radius,
            LinVector2D<T>.Create(centerX, centerY).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryCircle(IScalar<T> radius, LinVector2D<T> center)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return ImaginaryHyperSphere(
            radius,
            center.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(IScalar<T> radiusSquared, LinVector2D<T> egaCenter, LinBivector2D<T> egaBivector)
    {
        return Circle(
            radiusSquared,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(IScalar<T> radiusSquared, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return Circle(
            radiusSquared,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealCircle(IScalar<T> radius, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return RealCircle(
            radius,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryCircle(IScalar<T> radius, LinVector3D<T> egaCenter, LinBivector3D<T> egaBivector)
    {
        return ImaginaryCircle(
            radius,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(IScalar<T> radiusSquared, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return Circle(
            radiusSquared,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealCircle(IScalar<T> radius, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return RealCircle(
            radius,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryCircle(IScalar<T> radius, LinVector3D<T> egaCenter, LinVector3D<T> egaNormalVector)
    {
        return ImaginaryCircle(
            radius,
            egaCenter.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaNormalVector.NormalToUnitDirection3D().ToXGaBivector(GeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Sphere(IScalar<T> radiusSquared, IScalar<T> centerX, IScalar<T> centerY, IScalar<T> centerZ)
    {
        return HyperSphere(
            radiusSquared,
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Sphere(IScalar<T> radiusSquared, LinVector3D<T> center)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return HyperSphere(
            radiusSquared,
            center.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealSphere(string radius, string centerX, string centerY, string centerZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var sp = ScalarProcessor;

        return RealHyperSphere(
            sp.ScalarFromText(radius),
            LinVector3D<T>.Create(
                sp.ScalarFromText(centerX), 
                sp.ScalarFromText(centerY), 
                sp.ScalarFromText(centerZ)
            ).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealSphere(IScalar<T> radius, IScalar<T> centerX, IScalar<T> centerY, IScalar<T> centerZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return RealHyperSphere(
            radius,
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealSphere(IScalar<T> radius, LinVector3D<T> center)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return RealHyperSphere(
            radius,
            center.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginarySphere(IScalar<T> radius, IScalar<T> centerX, IScalar<T> centerY, IScalar<T> centerZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return ImaginaryHyperSphere(
            radius,
            LinVector3D<T>.Create(centerX, centerY, centerZ).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginarySphere(IScalar<T> radius, LinVector3D<T> center)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return ImaginaryHyperSphere(
            radius,
            center.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(T pointX, T pointY)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;

        return Point(
            scalarProcessor.ScalarFromValue(pointX),
            scalarProcessor.ScalarFromValue(pointY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(double pointX, double pointY)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;

        return Point(
            scalarProcessor.ScalarFromNumber(pointX),
            scalarProcessor.ScalarFromNumber(pointY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(string pointX, string pointY)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;

        return Point(
            scalarProcessor.ScalarFromText(pointX),
            scalarProcessor.ScalarFromText(pointY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(IScalar<T> pointX, IScalar<T> pointY)
    {
        var p =
            GeometricSpace.EncodeVGa.VectorAsXGaVector(pointX, pointY);

        var pNormSquared =
            pointX.Square() + pointY.Square();

        var kVector =
            GeometricSpace.EoVector +
            p +
            pNormSquared / 2 * GeometricSpace.EiVector;

        return new CGaBlade<T>(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(T pointX, T pointY, T pointZ)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;

        return Point(
            scalarProcessor.ScalarFromValue(pointX),
            scalarProcessor.ScalarFromValue(pointY),
            scalarProcessor.ScalarFromValue(pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(double pointX, double pointY, double pointZ)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;

        return Point(
            scalarProcessor.ScalarFromNumber(pointX),
            scalarProcessor.ScalarFromNumber(pointY),
            scalarProcessor.ScalarFromNumber(pointZ)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(string pointX, string pointY, string pointZ)
    {
        var scalarProcessor = GeometricSpace.ScalarProcessor;

        return Point(
            scalarProcessor.ScalarFromText(pointX),
            scalarProcessor.ScalarFromText(pointY),
            scalarProcessor.ScalarFromText(pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(IScalar<T> pointX, IScalar<T> pointY, IScalar<T> pointZ)
    {
        var p =
            GeometricSpace.EncodeVGa.VectorAsXGaVector(pointX, pointY, pointZ);

        var pNormSquared =
            pointX.Square() + pointY.Square() + pointZ.Square();

        var kVector =
            GeometricSpace.EoVector +
            p +
            pNormSquared / 2 * GeometricSpace.EiVector;

        return new CGaBlade<T>(GeometricSpace, kVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector2D<T> egaPoint)
    {
        var p =
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            egaPoint.NormSquared() / 2 * GeometricSpace.EiVector;

        return new CGaBlade<T>(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector3D<T> egaPoint)
    {
        var p =
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            egaPoint.NormSquared() / 2 * GeometricSpace.EiVector;

        return new CGaBlade<T>(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector<T> egaPoint)
    {
        var p =
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            egaPoint.ENormSquared() / 2 * GeometricSpace.EiVector;

        return new CGaBlade<T>(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(XGaVector<T> egaPoint)
    {
        var p =
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            egaPoint.NormSquared() / 2 * GeometricSpace.EiVector;

        return new CGaBlade<T>(GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPair(IScalar<T> radiusSquared, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaVector(GeometricSpace)
                .VGaDual()
                .GradeInvolution();

        Debug.Assert(radiusSquared is not null);

        return (GeometricSpace.Eo - radiusSquared * GeometricSpace.Ei / 2)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealPointPair(IScalar<T> radius, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        return PointPair(
            radius.Square(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryPointPair(IScalar<T> radius, XGaVector<T> egaCenter, XGaVector<T> egaDirection)
    {
        return PointPair(
            radius.NegativeSquare(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(IScalar<T> radiusSquared, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBivector(GeometricSpace)
                .VGaDual()
                .GradeInvolution();

        Debug.Assert(radiusSquared is not null);

        return (GeometricSpace.Eo - radiusSquared * GeometricSpace.Ei / 2)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealCircle(IScalar<T> radius, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        return Circle(
            radius.Square(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryCircle(IScalar<T> radius, XGaVector<T> egaCenter, XGaBivector<T> egaDirection)
    {
        return Circle(
            radius.NegativeSquare(),
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealUnitHyperSphere()
    {
        return GeometricSpace.Eo - GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryUnitHyperSphere()
    {
        return GeometricSpace.Eo + GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperSphere(IScalar<T> radiusSquared)
    {
        Debug.Assert(radiusSquared is not null);

        return GeometricSpace.Eo - radiusSquared * GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperSphere(IScalar<T> radiusSquared, XGaVector<T> egaCenter)
    {
        Debug.Assert(radiusSquared is not null);

        var c = Point(egaCenter);

        return c - radiusSquared * GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealHyperSphere(IScalar<T> radius)
    {
        var radiusSquared = radius.Square();

        return GeometricSpace.Eo - radiusSquared * GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> RealHyperSphere(IScalar<T> radius, XGaVector<T> egaCenter)
    {
        var radiusSquared = radius.Square();

        var c = Point(egaCenter);

        return c - radiusSquared * GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryHyperSphere(IScalar<T> radius)
    {
        var radiusSquared = radius.Square();

        return GeometricSpace.Eo + radiusSquared * GeometricSpace.Ei / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> ImaginaryHyperSphere(IScalar<T> radius, XGaVector<T> egaCenter)
    {
        var radiusSquared = radius.Square();

        var c = Point(egaCenter);

        return c + radiusSquared * GeometricSpace.Ei / 2;
    }

}