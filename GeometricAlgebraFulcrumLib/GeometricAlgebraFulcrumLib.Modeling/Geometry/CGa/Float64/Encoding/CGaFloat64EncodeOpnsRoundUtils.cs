using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double egaPointX, double egaPointY)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double egaPointX, double egaPointY, double egaPointZ)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY, egaPointZ).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2, LinFloat64Vector2D egaPoint3)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2, LinFloat64Vector2D egaPoint3, LinFloat64Vector2D egaPoint4)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeOpnsRound(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector(),
            egaPoint4.ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3, LinFloat64Vector3D egaPoint4)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRound(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector(),
            egaPoint4.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3, RGaFloat64Vector egaPoint4)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRound(
            egaPoint1,
            egaPoint2,
            egaPoint3,
            egaPoint4
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRound(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRound(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRound(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRound(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3, RGaFloat64Vector egaPoint4)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint3);
        var p4 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint4);

        return p1.Op(p2).Op(p3).Op(p4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRound(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] egaPointArray)
    {
        return egaPointArray.Select(cgaGeometricSpace.EncodeIpnsRoundPoint).Op();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsRound(this CGaFloat64GeometricSpace cgaGeometricSpace, IEnumerable<RGaFloat64Vector> egaPointList)
    {
        return egaPointList.Select(cgaGeometricSpace.EncodeIpnsRoundPoint).Op();
    }
}