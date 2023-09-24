using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeOpnsTangentUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            Float64Vector2D.Create(pointX, pointY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPoint(this RGaConformalSpace conformalSpace, Float64Vector2D egaPoint)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <param name="pointZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY, double pointZ)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            Float64Vector3D.Create(pointX, pointY, pointZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPoint(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }
    
    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPoint(this RGaConformalSpace conformalSpace, Float64Vector egaPoint)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        return conformalSpace.Eo.TranslateBy(egaPoint);
        // This is the same as a IPNS point
    }


    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLine(this RGaConformalSpace conformalSpace, double distance, double normalX, double normalY)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            Float64Vector2D.Create(normalX, normalY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLine(this RGaConformalSpace conformalSpace, double distance, Float64Vector2D egaNormalVector)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            egaNormalVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point and vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="point"></param>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLine(this RGaConformalSpace conformalSpace, Float64Vector2D point, Float64Vector2D vector)
    {
        return conformalSpace.EncodeOpnsTangentLine(
            point.ToRGaFloat64Vector(),
            vector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert two 2D Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2)
    {
        return conformalSpace.EncodeOpnsTangentLine(
            point1.ToRGaFloat64Vector(),
            (point2 - point1).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and a direction vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLine(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Vector3D egaVector)
    {
        return conformalSpace.EncodeOpnsTangentLine(
            egaPoint.ToRGaFloat64Vector(),
            egaVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points in 3D into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint1, Float64Vector3D egaPoint2)
    {
        return conformalSpace.EncodeOpnsTangentLineFromPoints(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .DivideByNorm();

        return conformalSpace.Eo.Op(direction).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentLineFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return conformalSpace.EncodeOpnsTangentLine(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlane(this RGaConformalSpace conformalSpace, double distance, double normalX, double normalY, double normalZ)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            Float64Vector3D.Create(normalX, normalY, normalZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlane(this RGaConformalSpace conformalSpace, double distance, Float64Vector3D egaNormalVector)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            egaNormalVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and two direction vectors into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector1"></param>
    /// <param name="egaVector2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlane(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Vector3D egaVector1, Float64Vector3D egaVector2)
    {
        var egaDirectionBivector = 
            egaVector1.Op(egaVector2);

        return conformalSpace.EncodeOpnsTangentPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and egaNormalVector direction vector into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlane(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Vector3D egaNormalVector)
    {
        var egaDirectionBivector = 
            egaNormalVector.UnDual3D();

        return conformalSpace.EncodeOpnsTangentPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaBivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlane(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Bivector3D egaBivector)
    {
        return conformalSpace.EncodeOpnsTangentPlane(
            egaPoint.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points in 3D into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint1, Float64Vector3D egaPoint2, Float64Vector3D egaPoint3)
    {
        return conformalSpace.EncodeOpnsTangentPlane(
            egaPoint1,
            egaPoint2 - egaPoint1,
            egaPoint3 - egaPoint1
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlane(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .DivideByNorm();

        return conformalSpace.Eo.Op(direction).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return conformalSpace.EncodeOpnsTangentPlane(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a egaNormalVector vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentHyperPlane(this RGaConformalSpace conformalSpace, double distance, RGaFloat64Vector egaNormalVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaNormalVector));
        
        return new RGaConformalBlade(
            conformalSpace,
            egaNormalVector.DivideByNorm() + distance * conformalSpace.EiVector
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA IPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangent(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
    {
        var direction = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .DivideByNorm();

        return conformalSpace.Eo.Op(direction).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentFromPoints(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] egaPointArray)
    {
        var egaPoint1 = 
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op();

        return conformalSpace.EncodeOpnsTangent(
            egaPoint1,
            egaDirection
        );
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPointList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsTangentFromPoints(this RGaConformalSpace conformalSpace, IReadOnlyList<RGaFloat64Vector> egaPointList)
    {
        var egaPoint1 = 
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op();

        return conformalSpace.EncodeOpnsTangent(
            egaPoint1,
            egaDirection
        );
    }

}