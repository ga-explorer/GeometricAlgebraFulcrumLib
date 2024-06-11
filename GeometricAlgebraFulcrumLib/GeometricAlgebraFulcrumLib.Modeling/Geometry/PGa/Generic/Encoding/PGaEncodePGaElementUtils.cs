using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Encoding;

public static class PGaEncodePGaElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IPair<Scalar<T>> vgaNormalVector, IScalar<T> distanceToOrigin)
    {
        return pgaGeometricSpace.EncodePGaHyperPlane(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaNormalVector),
            distanceToOrigin
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IPair<Scalar<T>> vgaNormalVector)
    {
        return pgaGeometricSpace.EncodePGaHyperPlane(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaNormalVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, ITriplet<Scalar<T>> vgaNormalVector, IScalar<T> distanceToOrigin)
    {
        return pgaGeometricSpace.EncodePGaHyperPlane(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaNormalVector),
            distanceToOrigin
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, ITriplet<Scalar<T>> vgaNormalVector)
    {
        return pgaGeometricSpace.EncodePGaHyperPlane(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaNormalVector)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaHyperPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> vgaNormalVector, IScalar<T> distanceToOrigin)
    {
        return pgaGeometricSpace.EncodePGaHyperPlane(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaNormalVector),
            distanceToOrigin
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaHyperPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> vgaNormalVector, IScalar<T> distanceToOrigin)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(vgaNormalVector)
        );

        return vgaNormalVector - distanceToOrigin * pgaGeometricSpace.Eo;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaHyperPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> vgaNormalVector)
    {
        return pgaGeometricSpace.EncodePGaHyperPlane(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaNormalVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaHyperPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> vgaNormalVector)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(vgaNormalVector)
        );

        return pgaGeometricSpace.EncodeVGaVector(vgaNormalVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingHyperPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> distanceToOrigin)
    {
        return distanceToOrigin.Negative() * pgaGeometricSpace.Eo;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, double pointX, double pointY)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> pointX, IScalar<T> pointY)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IPair<Scalar<T>> point)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> pointX, IScalar<T> pointY, IScalar<T> pointZ)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, ITriplet<Scalar<T>> point)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> point)
    {
        return pgaGeometricSpace.EncodePGaPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> vgaPoint)
    {
        Debug.Assert(pgaGeometricSpace.IsValidVGaElement(vgaPoint));

        return pgaGeometricSpace.PGaOriginPoint +
               vgaPoint.Gp(pgaGeometricSpace.Ip).GetKVectorPart(pgaGeometricSpace.VSpaceDimensions - 1);

        //return (pgaGeometricSpace.Eo + vgaPoint).PGaDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> pointX, IScalar<T> pointY)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaVanishingPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IPair<Scalar<T>> point)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaVanishingPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IScalar<T> pointX, IScalar<T> pointY, IScalar<T> pointZ)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaVanishingPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, ITriplet<Scalar<T>> point)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaVanishingPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> point)
    {
        return pgaGeometricSpace.EncodePGaVanishingPoint(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaVanishingPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> vgaPoint)
    {
        Debug.Assert(pgaGeometricSpace.IsValidVGaElement(vgaPoint));

        return new PGaBlade<T>(
            pgaGeometricSpace,
            vgaPoint.Gp(pgaGeometricSpace.Ip).GetKVectorPart(pgaGeometricSpace.VSpaceDimensions - 1)
        );

        //return (pgaGeometricSpace.Eo + vgaPoint).PGaDual();
    }







    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaLineFromPoints(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaPlaneFromPoints(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> vgaPoint, LinVector2D<T> egaDirection)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaLine(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaPoint),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> vgaPoint, LinVector3D<T> egaDirection)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaLine(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(vgaPoint),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaLineFromPoints(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        Debug.Assert(pgaGeometricSpace.Is4D);

        return pgaGeometricSpace.EncodePGaPlaneFromPoints(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> vgaPoint, XGaVector<T> egaDirection)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(vgaPoint) &&
            pgaGeometricSpace.IsValidVGaElement(egaDirection)
        );

        var p1 = pgaGeometricSpace.Eo + vgaPoint;
        var p2 = p1 + egaDirection;

        return p1.Op(p2).PGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            pgaGeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1 = pgaGeometricSpace.Eo + egaPoint1;
        var p2 = pgaGeometricSpace.Eo + egaPoint2;

        return p1.Op(p2).PGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaBisectorLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(pgaGeometricSpace.Is3D);

        return pgaGeometricSpace.EncodePGaBisectorLine(
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            pgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaBisectorLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            pgaGeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1Dual = pgaGeometricSpace.Eo + egaPoint1;
        var p2Dual = pgaGeometricSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaDual();

        var p1 = p1Dual.PGaDual();
        var p2 = p2Dual.PGaDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToProjectiveBlade(pgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        Debug.Assert(
            pgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            pgaGeometricSpace.IsValidVGaElement(egaPoint2) &&
            pgaGeometricSpace.IsValidVGaElement(egaPoint3)
        );

        var p1 = pgaGeometricSpace.Eo + egaPoint1;
        var p2 = pgaGeometricSpace.Eo + egaPoint2;
        var p3 = pgaGeometricSpace.Eo + egaPoint3;

        return p1.Op(p2).Op(p3).PGaDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, params XGaVector<T>[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(pgaGeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(vgaPoint => pgaGeometricSpace.Eo + vgaPoint)
            .Op()
            .PGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> EncodePGaElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, IEnumerable<XGaVector<T>> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(pgaGeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(vgaPoint => pgaGeometricSpace.Eo + vgaPoint)
            .Op()
            .PGaDual();
    }

}