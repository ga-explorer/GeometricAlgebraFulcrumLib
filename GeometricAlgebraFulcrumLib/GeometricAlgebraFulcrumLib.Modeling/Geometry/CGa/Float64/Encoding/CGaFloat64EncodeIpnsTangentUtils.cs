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

public static class CGaFloat64EncodeIpnsTangentUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPoint(
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
    public static CGaFloat64Blade EncodeIpnsTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPoint(
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
    public static CGaFloat64Blade EncodeIpnsTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY, double pointZ)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPoint(
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
    public static CGaFloat64Blade EncodeIpnsTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPoint(
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
    public static CGaFloat64Blade EncodeIpnsTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPoint(
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
    public static CGaFloat64Blade EncodeIpnsTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        return cgaGeometricSpace.Eo
            .Op(cgaGeometricSpace.IeInv.GradeInvolution())
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
    public static CGaFloat64Blade EncodeIpnsTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, double normalX, double normalY)
    {
        return cgaGeometricSpace.EncodeIpnsTangentHyperPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector2D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsTangentHyperPlane(
            distance,
            egaNormalVector.ToRGaFloat64Vector()
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
    public static CGaFloat64Blade EncodeIpnsTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point, LinFloat64Vector2D vector)
    {
        return cgaGeometricSpace.EncodeIpnsTangentLine(
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
    public static CGaFloat64Blade EncodeIpnsTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        return cgaGeometricSpace.EncodeIpnsTangentLine(
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
    public static CGaFloat64Blade EncodeIpnsTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector)
    {
        return cgaGeometricSpace.EncodeIpnsTangentLine(
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
    public static CGaFloat64Blade EncodeIpnsTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        return cgaGeometricSpace.EncodeIpnsTangentLineFromPoints(
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
    public static CGaFloat64Blade EncodeIpnsTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .DivideByNorm()
                .VGaDual()
                .GradeInvolution();

        return cgaGeometricSpace.Eo.Op(direction).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return cgaGeometricSpace.EncodeIpnsTangentLine(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, double normalX, double normalY, double normalZ)
    {
        return cgaGeometricSpace.EncodeIpnsTangentHyperPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector3D egaNormalVector)
    {
        return cgaGeometricSpace.EncodeIpnsTangentHyperPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector1, LinFloat64Vector3D egaVector2)
    {
        var egaDirectionBivector =
            egaVector1.Op(egaVector2);

        return cgaGeometricSpace.EncodeIpnsTangentPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaNormalVector)
    {
        var egaDirectionBivector =
            egaNormalVector.NormalToUnitDirection3D();

        return cgaGeometricSpace.EncodeIpnsTangentPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Bivector3D egaBivector)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        return cgaGeometricSpace.EncodeIpnsTangentPlane(
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
    public static CGaFloat64Blade EncodeIpnsTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .DivideByNorm()
                .VGaDual()
                .GradeInvolution();

        return cgaGeometricSpace.Eo.Op(direction).TranslateBy(egaPoint);
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
    public static CGaFloat64Blade EncodeIpnsTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return cgaGeometricSpace.EncodeIpnsTangentPlane(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a egaNormalVector vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsTangentHyperPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, RGaFloat64Vector egaNormalVector)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaNormalVector));

        return new CGaFloat64Blade(
            cgaGeometricSpace,
            egaNormalVector.DivideByNorm() + distance * cgaGeometricSpace.EiVector
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA IPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsTangent(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
    {
        var direction =
            egaDirection
                .EncodeVGaBlade(cgaGeometricSpace)
                .DivideByNorm();

        return cgaGeometricSpace.Eo.Op(direction).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="cgaGeometricSpace"></param>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodeIpnsTangentFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] egaPointArray)
    {
        var egaPoint1 =
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(cgaGeometricSpace.Processor);

        return cgaGeometricSpace.EncodeIpnsTangent(
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
    public static CGaFloat64Blade EncodeIpnsTangentFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IReadOnlyList<RGaFloat64Vector> egaPointList)
    {
        var egaPoint1 =
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(cgaGeometricSpace.Processor);

        return cgaGeometricSpace.EncodeIpnsTangent(
            egaPoint1,
            egaDirection
        );
    }

}