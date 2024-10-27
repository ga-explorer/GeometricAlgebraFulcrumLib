using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public sealed class CGaOpnsRoundEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaOpnsRoundEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(Scalar<T> egaPointX, Scalar<T> egaPointY)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPointX, egaPointY).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(Scalar<T> egaPointX, Scalar<T> egaPointY, Scalar<T> egaPointZ)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPointX, egaPointY, egaPointZ).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector2D<T> egaPoint)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector3D<T> egaPoint)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector<T> egaPoint)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(XGaVector<T> egaPoint)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPoint).CGaUnDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPair(LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return PointPair(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2, LinVector2D<T> egaPoint3)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Circle(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Sphere(LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2, LinVector2D<T> egaPoint3, LinVector2D<T> egaPoint4)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Blade(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint4.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPair(LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return PointPair(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Circle(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Sphere(LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3, LinVector3D<T> egaPoint4)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Blade(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint4.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Sphere(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3, XGaVector<T> egaPoint4)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Blade(
            egaPoint1,
            egaPoint2,
            egaPoint3,
            egaPoint4
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PointPair(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var p1 = GeometricSpace.EncodeIpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.EncodeIpnsRound.Point(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Circle(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var p1 = GeometricSpace.EncodeIpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.EncodeIpnsRound.Point(egaPoint2);
        var p3 = GeometricSpace.EncodeIpnsRound.Point(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaVector<T> egaPoint)
    {
        return GeometricSpace.EncodeIpnsRound.Point(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var p1 = GeometricSpace.EncodeIpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.EncodeIpnsRound.Point(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var p1 = GeometricSpace.EncodeIpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.EncodeIpnsRound.Point(egaPoint2);
        var p3 = GeometricSpace.EncodeIpnsRound.Point(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3, XGaVector<T> egaPoint4)
    {
        var p1 = GeometricSpace.EncodeIpnsRound.Point(egaPoint1);
        var p2 = GeometricSpace.EncodeIpnsRound.Point(egaPoint2);
        var p3 = GeometricSpace.EncodeIpnsRound.Point(egaPoint3);
        var p4 = GeometricSpace.EncodeIpnsRound.Point(egaPoint4);

        return p1.Op(p2).Op(p3).Op(p4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(params XGaVector<T>[] egaPointArray)
    {
        return egaPointArray.Select(GeometricSpace.EncodeIpnsRound.Point).Op();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(IEnumerable<XGaVector<T>> egaPointList)
    {
        return egaPointList.Select(GeometricSpace.EncodeIpnsRound.Point).Op();
    }
}