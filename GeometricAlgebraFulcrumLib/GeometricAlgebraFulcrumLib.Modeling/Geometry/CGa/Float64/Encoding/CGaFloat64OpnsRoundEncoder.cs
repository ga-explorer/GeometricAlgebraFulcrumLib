using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public class CGaFloat64OpnsRoundEncoder :
    CGaFloat64EncoderBase
{
    internal CGaFloat64OpnsRoundEncoder(CGaFloat64GeometricSpace geometricSpace) 
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double egaPointX, double egaPointY)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPointX, egaPointY).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double egaPointX, double egaPointY, double egaPointZ)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPointX, egaPointY, egaPointZ).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector2D egaPoint)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector3D egaPoint)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector egaPoint)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(XGaFloat64Vector egaPoint)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPoint).CGaUnDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPair(LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return PointPair(
            egaPoint1.ToXGaFloat64Vector(),
            egaPoint2.ToXGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2, LinFloat64Vector2D egaPoint3)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Circle(
            egaPoint1.ToXGaFloat64Vector(),
            egaPoint2.ToXGaFloat64Vector(),
            egaPoint3.ToXGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Sphere(LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2, LinFloat64Vector2D egaPoint3, LinFloat64Vector2D egaPoint4)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return BladeFromPoints(
            egaPoint1.ToXGaFloat64Vector(),
            egaPoint2.ToXGaFloat64Vector(),
            egaPoint3.ToXGaFloat64Vector(),
            egaPoint4.ToXGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPair(LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return PointPair(
            egaPoint1.ToXGaFloat64Vector(),
            egaPoint2.ToXGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Circle(
            egaPoint1.ToXGaFloat64Vector(),
            egaPoint2.ToXGaFloat64Vector(),
            egaPoint3.ToXGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Sphere(LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3, LinFloat64Vector3D egaPoint4)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return BladeFromPoints(
            egaPoint1.ToXGaFloat64Vector(),
            egaPoint2.ToXGaFloat64Vector(),
            egaPoint3.ToXGaFloat64Vector(),
            egaPoint4.ToXGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Sphere(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2, XGaFloat64Vector egaPoint3, XGaFloat64Vector egaPoint4)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return BladeFromPoints(
            egaPoint1,
            egaPoint2,
            egaPoint3,
            egaPoint4
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PointPair(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2)
    {
        var p1 = GeometricSpace.Encode.IpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.Encode.IpnsRound.Point(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Circle(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2, XGaFloat64Vector egaPoint3)
    {
        var p1 = GeometricSpace.Encode.IpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.Encode.IpnsRound.Point(egaPoint2);
        var p3 = GeometricSpace.Encode.IpnsRound.Point(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoint(XGaFloat64Vector egaPoint)
    {
        return GeometricSpace.Encode.IpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2)
    {
        var p1 = GeometricSpace.Encode.IpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.Encode.IpnsRound.Point(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2, XGaFloat64Vector egaPoint3)
    {
        var p1 = GeometricSpace.Encode.IpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.Encode.IpnsRound.Point(egaPoint2);
        var p3 = GeometricSpace.Encode.IpnsRound.Point(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2, XGaFloat64Vector egaPoint3, XGaFloat64Vector egaPoint4)
    {
        var p1 = GeometricSpace.Encode.IpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.Encode.IpnsRound.Point(egaPoint2);
        var p3 = GeometricSpace.Encode.IpnsRound.Point(egaPoint3);
        var p4 = GeometricSpace.Encode.IpnsRound.Point(egaPoint4);

        return p1.Op(p2).Op(p3).Op(p4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(params XGaFloat64Vector[] egaPointArray)
    {
        return egaPointArray.Select(GeometricSpace.Encode.IpnsRound.Point).Op();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(IEnumerable<XGaFloat64Vector> egaPointList)
    {
        return egaPointList.Select(GeometricSpace.Encode.IpnsRound.Point).Op();
    }
}