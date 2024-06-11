using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaLineFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaPlaneFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint, LinVector2D<T> egaDirection)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaLine(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaPoint),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaDirection)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaLine(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaPoint),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaLineFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaPlaneFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaPoint));

        return (cgaGeometricSpace.Eo + egaPoint).PGaUnDual();
        //return (Ie + VGaDual(egaPoint).Op(Eo)).GetKVectorPart(Ie.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint, XGaVector<T> egaDirection)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint) &&
            cgaGeometricSpace.IsValidVGaElement(egaDirection)
        );

        var p1 = cgaGeometricSpace.Eo + egaPoint; //EncodePoint(egaPoint1);
        var p2 = p1 + egaDirection; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1 = cgaGeometricSpace.Eo + egaPoint1; //EncodePoint(egaPoint1);
        var p2 = cgaGeometricSpace.Eo + egaPoint2; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaBisectorLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaBisectorLine(
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaBisectorLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1Dual = cgaGeometricSpace.Eo + egaPoint1;
        var p2Dual = cgaGeometricSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaUnDual();

        var p1 = p1Dual.PGaUnDual();
        var p2 = p2Dual.PGaUnDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToConformalBlade(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint2) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint3)
        );

        var p1 = cgaGeometricSpace.Eo + egaPoint1;
        var p2 = cgaGeometricSpace.Eo + egaPoint2;
        var p3 = cgaGeometricSpace.Eo + egaPoint3;

        return p1.Op(p2).Op(p3).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params XGaVector<T>[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(cgaGeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(egaPoint => cgaGeometricSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodePGaFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IEnumerable<XGaVector<T>> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(cgaGeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(egaPoint => cgaGeometricSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

}