using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodeIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, double centerX, double centerY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            LinFloat64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, double centerX, double centerY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            LinFloat64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D center)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, double centerX, double centerY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            LinFloat64Vector2D.Create(centerX, centerY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D center)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D egaCenter, LinFloat64Bivector2D egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radiusSquared,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D egaCenter, LinFloat64Bivector3D egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radiusSquared,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D egaCenter, LinFloat64Bivector3D egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsRealRoundCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D egaCenter, LinFloat64Bivector3D egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsImaginaryRoundCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D egaCenter, LinFloat64Vector3D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radiusSquared,
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.NormalToUnitDirection3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D egaCenter, LinFloat64Vector3D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsRealRoundCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.NormalToUnitDirection3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D egaCenter, LinFloat64Vector3D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsImaginaryRoundCircle(
            radius,
            egaCenter.ToRGaFloat64Vector(),
            egaNormalVector.NormalToUnitDirection3D().ToRGaFloat64Bivector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, double centerX, double centerY, double centerZ)
    {
        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            LinFloat64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsRoundHyperSphere(
            radiusSquared,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, double centerX, double centerY, double centerZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            LinFloat64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D center)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsRealRoundHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, double centerX, double centerY, double centerZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            LinFloat64Vector3D.Create(centerX, centerY, centerZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D center)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeIpnsImaginaryRoundHyperSphere(
            radius,
            center.ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(pointX, pointY);

        var pNormSquared =
            pointX * pointX +
            pointY * pointY;

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY, double pointZ)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(pointX, pointY, pointZ);

        var pNormSquared =
            pointX * pointX +
            pointY * pointY +
            pointZ * pointZ;

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * pNormSquared * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared().ScalarValue * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.ENormSquared() * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        var p =
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaPoint);

        var kVector =
            cgaGeometricSpace.EoVector +
            p +
            0.5d * egaPoint.NormSquared() * cgaGeometricSpace.EiVector;

        return new CGaFloat64Blade(cgaGeometricSpace, kVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .VGaDual()
                .GradeInvolution();

        return (cgaGeometricSpace.Eo - 0.5 * radiusSquared * cgaGeometricSpace.Ei)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPointPair(
            radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Vector egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPointPair(
            -radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .VGaDual()
                .GradeInvolution();

        return (cgaGeometricSpace.Eo - 0.5 * radiusSquared * cgaGeometricSpace.Ei)
            .Op(direction)
            .TranslateBy(egaCenter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector egaCenter, RGaFloat64Bivector egaDirection)
    {
        return cgaGeometricSpace.EncodeIpnsRoundCircle(
            -radius * radius,
            egaCenter,
            egaDirection
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundUnitHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.Eo - 0.5d * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundUnitHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.Eo + 0.5d * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared)
    {
        return cgaGeometricSpace.Eo - 0.5d * radiusSquared * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRoundHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector egaCenter)
    {
        var c = cgaGeometricSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - 0.5d * radiusSquared * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius)
    {
        return cgaGeometricSpace.Eo - 0.5d * radius * radius * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsRealRoundHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector egaCenter)
    {
        var c = cgaGeometricSpace.EncodeIpnsRoundPoint(egaCenter);

        return c - 0.5d * radius * radius * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius)
    {
        return cgaGeometricSpace.Eo + 0.5d * radius * radius * cgaGeometricSpace.Ei;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsImaginaryRoundHyperSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector egaCenter)
    {
        var c = cgaGeometricSpace.EncodeIpnsRoundPoint(egaCenter);

        return c + 0.5d * radius * radius * cgaGeometricSpace.Ei;
    }

}