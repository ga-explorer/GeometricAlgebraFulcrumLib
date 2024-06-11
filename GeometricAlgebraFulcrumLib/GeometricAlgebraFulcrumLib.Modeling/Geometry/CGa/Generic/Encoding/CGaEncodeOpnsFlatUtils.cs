using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public static class CGaEncodeOpnsFlatUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            LinVector2D<T>.Create(pointX, pointY).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            egaPoint.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <param name="pointZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            LinVector3D<T>.Create(pointX, pointY, pointZ).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            egaPoint.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> egaPoint)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            egaPoint.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint)
    {
        return cgaGeometricSpace.Eoi.TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> egaPoint)
    {
        return cgaGeometricSpace.Eoi.TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, Scalar<T> normalX, Scalar<T> normalY)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            LinVector2D<T>.Create(normalX, normalY).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, LinVector2D<T> egaNormalVector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            egaNormalVector.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point and vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="point"></param>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point, LinVector2D<T> vector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLine(
            point.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            vector.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and a direction vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaVector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLine(
            egaPoint.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaVector.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint, XGaVector<T> egaDirection)
    {
        var directionOpEi =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .DivideByNorm()
                .Op(cgaGeometricSpace.Ei);

        return cgaGeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> egaPoint, CGaBlade<T> egaDirection)
    {
        Debug.Assert(
            egaPoint.IsVGaVector() &&
            egaDirection.IsVGaVector()
        );

        var directionOpEi =
            egaDirection
                .DivideByNorm()
                .Op(cgaGeometricSpace.Ei);

        return cgaGeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert two 2D Euclidean points into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLine(
            point1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            (point2 - point1).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points in 3D into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLineFromPoints(
            egaPoint1.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA OPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return cgaGeometricSpace.EncodeOpnsFlatLine(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, Scalar<T> normalX, Scalar<T> normalY, Scalar<T> normalZ)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            LinVector3D<T>.Create(normalX, normalY, normalZ).ToXGaVector(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, LinVector3D<T> egaNormalVector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            egaNormalVector.EncodeVGaVectorBlade(cgaGeometricSpace)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and two direction vectors into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector1"></param>
    /// <param name="egaVector2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaVector1, LinVector3D<T> egaVector2)
    {
        var egaDirectionBivector =
            egaVector1.Op(egaVector2);

        return cgaGeometricSpace.EncodeOpnsFlatPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and egaNormalVector direction vector into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaNormalVector)
    {
        var egaDirectionBivector =
            egaNormalVector.NormalToUnitDirection3D();

        return cgaGeometricSpace.EncodeOpnsFlatPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 2-blade into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaBivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint, LinBivector3D<T> egaBivector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPlane(
            egaPoint.ToXGaVector(cgaGeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(cgaGeometricSpace.Processor)
        );
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points in 3D into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPlane(
            egaPoint1,
            egaPoint2 - egaPoint1,
            egaPoint3 - egaPoint1
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA OPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint, XGaBivector<T> egaDirection)
    {
        var directionOpEi =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .DivideByNorm()
                .Op(cgaGeometricSpace.Ei);

        return cgaGeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points into a CGA OPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return cgaGeometricSpace.EncodeOpnsFlatPlane(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA OPNS flat hyper-plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatHyperPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, XGaVector<T> egaNormalVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaNormalVector));

        var normal =
            egaNormalVector.EncodeVGaBlade(cgaGeometricSpace);

        return normal
            .DivideByNorm()
            .TranslateBy(normal.SetNorm(distance))
            .Negative()
            .IpnsToOpns();
    }

    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA OPNS flat hyper-plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatHyperPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, CGaBlade<T> egaNormalVector)
    {
        Debug.Assert(egaNormalVector.IsVGaVector());

        return egaNormalVector
            .DivideByNorm()
            .TranslateBy(egaNormalVector.SetNorm(distance))
            .Negative()
            .IpnsToOpns();
    }


    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA OPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlat<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> egaPoint, XGaKVector<T> egaDirection)
    {
        var directionOpEi =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .DivideByNorm()
                .Op(cgaGeometricSpace.Ei);

        return cgaGeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA OPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params XGaVector<T>[] egaPointArray)
    {
        var egaPoint1 =
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(cgaGeometricSpace.Processor);

        return cgaGeometricSpace.EncodeOpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA OPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPointList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> EncodeOpnsFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IReadOnlyList<XGaVector<T>> egaPointList)
    {
        var egaPoint1 =
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(cgaGeometricSpace.Processor);

        return cgaGeometricSpace.EncodeOpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

}