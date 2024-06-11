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

public static class CGaFloat64EncodeIpnsFlatUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPoint(
            LinFloat64Vector2D.Create(pointX, pointY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <param name="pointZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY, double pointZ)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPoint(
            LinFloat64Vector3D.Create(pointX, pointY, pointZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.IeInv
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, double normalX, double normalY)
    {
        return cgaGeometricSpace.EncodeIpnsFlatHyperPlane(
            distance,
            LinFloat64Vector2D.Create(normalX, normalY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector2D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsFlatHyperPlane(
            distance,
            LinFloat64Vector2D.Create(egaNormalVector).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point and vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="point"></param>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point, LinFloat64Vector2D vector)
    {
        return cgaGeometricSpace.EncodeIpnsFlatLine(
            point.ToRGaFloat64Vector(),
            vector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert two 2D Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        return cgaGeometricSpace.EncodeIpnsFlatLine(
            point1.ToRGaFloat64Vector(),
            (point2 - point1).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and a direction vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector)
    {
        return cgaGeometricSpace.EncodeIpnsFlatLine(
            egaPoint.ToRGaFloat64Vector(),
            egaVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points in 3D into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        return cgaGeometricSpace.EncodeIpnsFlatLineFromPoints(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        return egaDirection
            .EncodeVGaBlade(cgaGeometricSpace)
            .DivideByNorm()
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return cgaGeometricSpace.EncodeIpnsFlatLine(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, double normalX, double normalY, double normalZ)
    {
        return cgaGeometricSpace.EncodeIpnsFlatHyperPlane(
            distance,
            LinFloat64Vector3D.Create(normalX, normalY, normalZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector3D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsFlatHyperPlane(
            distance,
            egaNormalVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and two direction vectors into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector1"></param>
    /// <param name="egaVector2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector1, LinFloat64Vector3D egaVector2)
    {
        var egaDirectionBivector =
            egaVector1.Op(egaVector2);

        return cgaGeometricSpace.EncodeIpnsFlatPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and egaNormalVector direction vector into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaNormalVector)
    {
        var egaDirectionBivector =
            egaNormalVector.NormalToUnitDirection3D();

        return cgaGeometricSpace.EncodeIpnsFlatPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaBivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Bivector3D egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPlane(
            egaPoint.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points in 3D into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        return cgaGeometricSpace.EncodeIpnsFlatPlane(
            egaPoint1,
            egaPoint2 - egaPoint1,
            egaPoint3 - egaPoint1
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
    {
        return egaDirection
            .EncodeVGaBlade(cgaGeometricSpace)
            .DivideByNorm()
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return cgaGeometricSpace.EncodeIpnsFlatPlane(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D Euclidean point and normal direction scalar into a CGA IPNS flat volume
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormal"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, double egaNormal)
    {
        var egaDirectionTrivector =
            LinFloat64Trivector3D.Create(1d / egaNormal);

        return cgaGeometricSpace.EncodeIpnsFlatVolume(
            egaPoint,
            egaDirectionTrivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 3-blade into a CGA IPNS flat volume
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaTrivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Trivector3D egaTrivector)
    {
        return cgaGeometricSpace.EncodeIpnsFlatVolume(
            egaPoint.ToRGaFloat64Vector(),
            egaTrivector.ToRGaFloat64Trivector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64HigherKVector egaDirection)
    {
        return egaDirection
            .EncodeVGaBlade(cgaGeometricSpace)
            .DivideByNorm()
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatHyperPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, RGaFloat64Vector egaNormalVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaNormalVector));

        var normal =
            egaNormalVector.EncodeVGaBlade(cgaGeometricSpace);

        return normal
            .DivideByNorm()
            .TranslateBy(normal.SetNorm(distance))
            .Negative();
    }

    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatHyperPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, CGaFloat64Blade egaNormalVector)
    {
        Debug.Assert(egaNormalVector.IsVGaVector());

        return egaNormalVector
            .DivideByNorm()
            .TranslateBy(egaNormalVector.SetNorm(distance))
            .Negative();
    }


    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA IPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
    {
        return egaDirection
            .EncodeVGaBlade(cgaGeometricSpace)
            .DivideByNorm()
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] egaPointArray)
    {
        var egaPoint1 =
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(cgaGeometricSpace.Processor);

        return cgaGeometricSpace.EncodeIpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPointList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IReadOnlyList<RGaFloat64Vector> egaPointList)
    {
        var egaPoint1 =
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(cgaGeometricSpace.Processor);

        return cgaGeometricSpace.EncodeIpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

}