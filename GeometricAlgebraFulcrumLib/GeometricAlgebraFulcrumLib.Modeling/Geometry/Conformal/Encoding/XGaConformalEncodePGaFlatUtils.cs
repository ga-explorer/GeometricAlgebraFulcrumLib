using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaLineFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPlaneFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2),
            conformalSpace.EncodeEGaVectorAsVector(point3)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(point)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint, LinVector2D<T> egaDirection)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaLine(
            conformalSpace.EncodeEGaVectorAsVector(egaPoint),
            conformalSpace.EncodeEGaVectorAsVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaDirection)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaLine(
            conformalSpace.EncodeEGaVectorAsVector(egaPoint),
            conformalSpace.EncodeEGaVectorAsVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaLineFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPlaneFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2),
            conformalSpace.EncodeEGaVectorAsVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaPoint));

        return (conformalSpace.Eo + egaPoint).PGaUnDual();
        //return (Ie + EGaDual(egaPoint).Op(Eo)).GetKVectorPart(Ie.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaLine<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint, XGaVector<T> egaDirection)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint) &&
            conformalSpace.IsValidEGaElement(egaDirection)
        );

        var p1 = conformalSpace.Eo + egaPoint; //EncodePoint(egaPoint1);
        var p2 = p1 + egaDirection; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2)
        );

        var p1 = conformalSpace.Eo + egaPoint1; //EncodePoint(egaPoint1);
        var p2 = conformalSpace.Eo + egaPoint2; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaBisectorLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaBisectorLine(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaBisectorLine<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2)
        );

        var p1Dual = conformalSpace.Eo + egaPoint1;
        var p2Dual = conformalSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaUnDual();

        var p1 = p1Dual.PGaUnDual();
        var p2 = p2Dual.PGaUnDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToConformalBlade(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2) &&
            conformalSpace.IsValidEGaElement(egaPoint3)
        );

        var p1 = conformalSpace.Eo + egaPoint1;
        var p2 = conformalSpace.Eo + egaPoint2;
        var p3 = conformalSpace.Eo + egaPoint3;

        return p1.Op(p2).Op(p3).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaFlatFromPoints<T>(this XGaConformalSpace<T> conformalSpace, params XGaVector<T>[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(conformalSpace.IsValidEGaElement)
        );

        return egaPointArray
            .Select(egaPoint => conformalSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodePGaFlatFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IEnumerable<XGaVector<T>> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(conformalSpace.IsValidEGaElement)
        );

        return egaPointArray
            .Select(egaPoint => conformalSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

}