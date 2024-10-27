﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public class CGaFloat64IpnsRoundEncoder :
    CGaFloat64EncoderBase
{
    internal CGaFloat64IpnsRoundEncoder(CGaFloat64GeometricSpace geometricSpace) 
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(double radiusSquared, double centerX, double centerY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return HyperSphere(
            radiusSquared,
            LinFloat64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(double radiusSquared, LinFloat64Vector2D center)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return HyperSphere(
            radiusSquared,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealCircle(double radius, double centerX, double centerY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return RealHyperSphere(
            radius,
            LinFloat64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealCircle(double radius, LinFloat64Vector2D center)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return RealHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryCircle(double radius, double centerX, double centerY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return ImaginaryHyperSphere(
            radius,
            LinFloat64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryCircle(double radius, LinFloat64Vector2D center)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return ImaginaryHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(double radiusSquared, LinFloat64Vector2D egaCenter, LinFloat64Bivector2D egaBivector)
    {
        return Circle(
            radiusSquared,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(double radiusSquared, LinFloat64Vector3D egaCenter, LinFloat64Bivector3D egaBivector)
    {
        return Circle(
            radiusSquared,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealCircle(double radius, LinFloat64Vector3D egaCenter, LinFloat64Bivector3D egaBivector)
    {
        return RealCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryCircle(double radius, LinFloat64Vector3D egaCenter, LinFloat64Bivector3D egaBivector)
    {
        return ImaginaryCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(double radiusSquared, LinFloat64Vector3D egaCenter, LinFloat64Vector3D egaNormalVector)
    {
        return Circle(
            radiusSquared,
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.NormalToUnitDirection3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealCircle(double radius, LinFloat64Vector3D egaCenter, LinFloat64Vector3D egaNormalVector)
    {
        return RealCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.NormalToUnitDirection3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryCircle(double radius, LinFloat64Vector3D egaCenter, LinFloat64Vector3D egaNormalVector)
    {
        return ImaginaryCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.NormalToUnitDirection3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Sphere(double radiusSquared, double centerX, double centerY, double centerZ)
    {
        return HyperSphere(
            radiusSquared,
            LinFloat64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Sphere(double radiusSquared, LinFloat64Vector3D center)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return HyperSphere(
            radiusSquared,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealSphere(double radius, double centerX, double centerY, double centerZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return RealHyperSphere(
            radius,
            LinFloat64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealSphere(double radius, LinFloat64Vector3D center)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return RealHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginarySphere(double radius, double centerX, double centerY, double centerZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return ImaginaryHyperSphere(
            radius,
            LinFloat64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginarySphere(double radius, LinFloat64Vector3D center)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return ImaginaryHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY)
    {
        var p =
            GeometricSpace.Encode.VGa.VectorAsRGaVector(pointX, pointY);

        var pNormSquared =
            pointX * pointX +
            pointY * pointY;

        var kVector =
            GeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * GeometricSpace.EiVector;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector2D egaPoint)
    {
        var p =
            GeometricSpace.Encode.VGa.VectorAsRGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * GeometricSpace.EiVector;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY, double pointZ)
    {
        var p =
            GeometricSpace.Encode.VGa.VectorAsRGaVector(pointX, pointY, pointZ);

        var pNormSquared =
            pointX * pointX +
            pointY * pointY +
            pointZ * pointZ;

        var kVector =
            GeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * GeometricSpace.EiVector;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector3D egaPoint)
    {
        var p =
            GeometricSpace.Encode.VGa.VectorAsRGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared().ScalarValue * GeometricSpace.EiVector;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector egaPoint)
    {
        var p =
            GeometricSpace.Encode.VGa.VectorAsRGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            0.5d * egaPoint.ENormSquared() * GeometricSpace.EiVector;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(RGaFloat64Vector egaPoint)
    {
        var p =
            GeometricSpace.Encode.VGa.VectorAsRGaVector(egaPoint);

        var kVector =
            GeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * GeometricSpace.EiVector;

        return new CGaFloat64Blade(GeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPair(double radiusSquared, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(GeometricSpace)
                .VGaDual()
                .GradeInvolution();

        return (GeometricSpace.Eo - 0.5 * radiusSquared * GeometricSpace.Ei)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealPointPair(double radius, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        return PointPair(
            radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryPointPair(double radius, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        return PointPair(
            -radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(double radiusSquared, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(GeometricSpace)
                .VGaDual()
                .GradeInvolution();

        return (GeometricSpace.Eo - 0.5 * radiusSquared * GeometricSpace.Ei)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealCircle(double radius, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        return Circle(
            radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryCircle(double radius, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        return Circle(
            -radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealUnitHyperSphere()
    {
        return GeometricSpace.Eo - 0.5d * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryUnitHyperSphere()
    {
        return GeometricSpace.Eo + 0.5d * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperSphere(double radiusSquared)
    {
        return GeometricSpace.Eo - 0.5d * radiusSquared * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperSphere(double radiusSquared, RGaFloat64Vector egaCenter)
    {
        var c = Point(egaCenter);

        return c - 0.5d * radiusSquared * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealHyperSphere(double radius)
    {
        return GeometricSpace.Eo - 0.5d * radius * radius * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade RealHyperSphere(double radius, RGaFloat64Vector egaCenter)
    {
        var c = Point(egaCenter);

        return c - 0.5d * radius * radius * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryHyperSphere(double radius)
    {
        return GeometricSpace.Eo + 0.5d * radius * radius * GeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade ImaginaryHyperSphere(double radius, RGaFloat64Vector egaCenter)
    {
        var c = Point(egaCenter);

        return c + 0.5d * radius * radius * GeometricSpace.Ei;
    }

}