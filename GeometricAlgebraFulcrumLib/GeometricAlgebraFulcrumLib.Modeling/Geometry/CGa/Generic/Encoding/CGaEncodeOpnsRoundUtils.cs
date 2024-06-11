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

public static class CGaEncodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> egaPointX, Scalar<T> egaPointY)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> egaPointX, Scalar<T> egaPointY, Scalar<T> egaPointZ)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY, egaPointZ).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2, LinVector2D<T> egaPoint3)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2, LinVector2D<T> egaPoint3, LinVector2D<T> egaPoint4)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodeOpnsRound(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint4.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3, LinVector3D<T> egaPoint4)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodeOpnsRound(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint4.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3, XGaVector<T> egaPoint4)
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
    public static CGaBlade<T> EncodeOpnsRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3, XGaVector<T> egaPoint4)
    {
        var p1 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint3);
        var p4 = cgaGeometricSpace.EncodeIpnsRoundPoint(egaPoint4);

        return p1.Op(p2).Op(p3).Op(p4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params XGaVector<T>[] egaPointArray)
    {
        return egaPointArray.Select(cgaGeometricSpace.EncodeIpnsRoundPoint).Op();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IEnumerable<XGaVector<T>> egaPointList)
    {
        return egaPointList.Select(cgaGeometricSpace.EncodeIpnsRoundPoint).Op();
    }
}