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

public static class CGaFloat64EncodeOpnsFlatUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            LinFloat64Vector2D.Create(pointX, pointY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY, double pointZ)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            LinFloat64Vector3D.Create(pointX, pointY, pointZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
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
    public static CGaFloat64Blade EncodeOpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade egaPoint)
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
    public static CGaFloat64Blade EncodeOpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, double normalX, double normalY)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            LinFloat64Vector2D.Create(normalX, normalY).ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector2D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            egaNormalVector.ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point, LinFloat64Vector2D vector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLine(
            point.ToRGaFloat64Vector(),
            vector.ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLine(
            egaPoint.ToRGaFloat64Vector(),
            egaVector.ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
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
    public static CGaFloat64Blade EncodeOpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade egaPoint, CGaFloat64Blade egaDirection)
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
    public static CGaFloat64Blade EncodeOpnsFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLine(
            point1.ToRGaFloat64Vector(),
            (point2 - point1).ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        return cgaGeometricSpace.EncodeOpnsFlatLineFromPoints(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
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
    public static CGaFloat64Blade EncodeOpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, double normalX, double normalY, double normalZ)
    {
        return cgaGeometricSpace.EncodeOpnsFlatHyperPlane(
            distance,
            LinFloat64Vector3D.Create(normalX, normalY, normalZ).ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeOpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector3D egaNormalVector)
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
    public static CGaFloat64Blade EncodeOpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector1, LinFloat64Vector3D egaVector2)
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
    public static CGaFloat64Blade EncodeOpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaNormalVector)
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
    public static CGaFloat64Blade EncodeOpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Bivector3D egaBivector)
    {
        return cgaGeometricSpace.EncodeOpnsFlatPlane(
            egaPoint.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
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
    public static CGaFloat64Blade EncodeOpnsFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
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
    public static CGaFloat64Blade EncodeOpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
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
    public static CGaFloat64Blade EncodeOpnsFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
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
    public static CGaFloat64Blade EncodeOpnsFlatHyperPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, RGaFloat64Vector egaNormalVector)
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
    public static CGaFloat64Blade EncodeOpnsFlatHyperPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, CGaFloat64Blade egaNormalVector)
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
    public static CGaFloat64Blade EncodeOpnsFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
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
    public static CGaFloat64Blade EncodeOpnsFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] egaPointArray)
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
    public static CGaFloat64Blade EncodeOpnsFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IReadOnlyList<RGaFloat64Vector> egaPointList)
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