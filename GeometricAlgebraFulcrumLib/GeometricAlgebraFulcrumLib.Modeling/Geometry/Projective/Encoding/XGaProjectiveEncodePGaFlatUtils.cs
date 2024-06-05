using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Encoding;

public static class XGaProjectiveEncodePGaElementUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.EncodePGaPoint(
            projectiveSpace.EncodeEGaVectorAsVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> point)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.EncodePGaPoint(
            projectiveSpace.EncodeEGaVectorAsVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.EncodePGaLineFromPoints(
            projectiveSpace.EncodeEGaVectorAsVector(point1),
            projectiveSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.EncodePGaPlaneFromPoints(
            projectiveSpace.EncodeEGaVectorAsVector(point1),
            projectiveSpace.EncodeEGaVectorAsVector(point2),
            projectiveSpace.EncodeEGaVectorAsVector(point3)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.EncodePGaPoint(
            projectiveSpace.EncodeEGaVectorAsVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> point)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.EncodePGaPoint(
            projectiveSpace.EncodeEGaVectorAsVector(point)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaLine<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> egaPoint, LinVector2D<T> egaDirection)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.EncodePGaLine(
            projectiveSpace.EncodeEGaVectorAsVector(egaPoint),
            projectiveSpace.EncodeEGaVectorAsVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaLine<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaDirection)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.EncodePGaLine(
            projectiveSpace.EncodeEGaVectorAsVector(egaPoint),
            projectiveSpace.EncodeEGaVectorAsVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.EncodePGaLineFromPoints(
            projectiveSpace.EncodeEGaVectorAsVector(point1),
            projectiveSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        Debug.Assert(projectiveSpace.Is4D);

        return projectiveSpace.EncodePGaPlaneFromPoints(
            projectiveSpace.EncodeEGaVectorAsVector(point1),
            projectiveSpace.EncodeEGaVectorAsVector(point2),
            projectiveSpace.EncodeEGaVectorAsVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> egaPoint)
    {
        Debug.Assert(projectiveSpace.IsValidEGaElement(egaPoint));

        return (projectiveSpace.Eo + egaPoint).PGaDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaLine<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> egaPoint, XGaVector<T> egaDirection)
    {
        Debug.Assert(
            projectiveSpace.IsValidEGaElement(egaPoint) &&
            projectiveSpace.IsValidEGaElement(egaDirection)
        );

        var p1 = projectiveSpace.Eo + egaPoint;
        var p2 = p1 + egaDirection;

        return p1.Op(p2).PGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            projectiveSpace.IsValidEGaElement(egaPoint1) &&
            projectiveSpace.IsValidEGaElement(egaPoint2)
        );

        var p1 = projectiveSpace.Eo + egaPoint1;
        var p2 = projectiveSpace.Eo + egaPoint2;

        return p1.Op(p2).PGaDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaBisectorLine<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(projectiveSpace.Is3D);

        return projectiveSpace.EncodePGaBisectorLine(
            projectiveSpace.EncodeEGaVectorAsVector(point1),
            projectiveSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaBisectorLine<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        Debug.Assert(
            projectiveSpace.IsValidEGaElement(egaPoint1) &&
            projectiveSpace.IsValidEGaElement(egaPoint2)
        );

        var p1Dual = projectiveSpace.Eo + egaPoint1;
        var p2Dual = projectiveSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaDual();

        var p1 = p1Dual.PGaDual();
        var p2 = p2Dual.PGaDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToProjectiveBlade(projectiveSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        Debug.Assert(
            projectiveSpace.IsValidEGaElement(egaPoint1) &&
            projectiveSpace.IsValidEGaElement(egaPoint2) &&
            projectiveSpace.IsValidEGaElement(egaPoint3)
        );

        var p1 = projectiveSpace.Eo + egaPoint1;
        var p2 = projectiveSpace.Eo + egaPoint2;
        var p3 = projectiveSpace.Eo + egaPoint3;

        return p1.Op(p2).Op(p3).PGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, params XGaVector<T>[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(projectiveSpace.IsValidEGaElement)
        );

        return egaPointArray
            .Select(egaPoint => projectiveSpace.Eo + egaPoint)
            .Op()
            .PGaDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> EncodePGaElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, IEnumerable<XGaVector<T>> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(projectiveSpace.IsValidEGaElement)
        );

        return egaPointArray
            .Select(egaPoint => projectiveSpace.Eo + egaPoint)
            .Op()
            .PGaDual();
    }

}