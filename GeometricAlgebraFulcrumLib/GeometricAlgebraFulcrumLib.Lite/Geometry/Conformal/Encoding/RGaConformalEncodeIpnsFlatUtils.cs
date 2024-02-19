using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeIpnsFlatUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY)
    {
        return conformalSpace.EncodeIpnsFlatPoint(
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
    public static RGaConformalBlade EncodeIpnsFlatPoint(this RGaConformalSpace conformalSpace, Float64Vector2D egaPoint)
    {
        return conformalSpace.EncodeIpnsFlatPoint(
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
    public static RGaConformalBlade EncodeIpnsFlatPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY, double pointZ)
    {
        return conformalSpace.EncodeIpnsFlatPoint(
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
    public static RGaConformalBlade EncodeIpnsFlatPoint(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint)
    {
        return conformalSpace.EncodeIpnsFlatPoint(
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
    public static RGaConformalBlade EncodeIpnsFlatPoint(this RGaConformalSpace conformalSpace, Float64Vector egaPoint)
    {
        return conformalSpace.EncodeIpnsFlatPoint(
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
    public static RGaConformalBlade EncodeIpnsFlatPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        return conformalSpace.IeInv
            .GradeInvolution()
            .TranslateBy(egaPoint);
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
    public static RGaConformalBlade EncodeIpnsFlatLine(this RGaConformalSpace conformalSpace, double distance, double normalX, double normalY)
    {
        return conformalSpace.EncodeIpnsFlatHyperPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatLine(this RGaConformalSpace conformalSpace, double distance, Float64Vector2D egaNormalVector)
    {
        return conformalSpace.EncodeIpnsFlatHyperPlane(
            distance, 
            Float64Vector2D.Create(egaNormalVector).ToRGaFloat64Vector()
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
    public static RGaConformalBlade EncodeIpnsFlatLine(this RGaConformalSpace conformalSpace, Float64Vector2D point, Float64Vector2D vector)
    {
        return conformalSpace.EncodeIpnsFlatLine(
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
    public static RGaConformalBlade EncodeIpnsFlatLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2)
    {
        return conformalSpace.EncodeIpnsFlatLine(
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
    public static RGaConformalBlade EncodeIpnsFlatLine(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Vector3D egaVector)
    {
        return conformalSpace.EncodeIpnsFlatLine(
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
    public static RGaConformalBlade EncodeIpnsFlatLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint1, Float64Vector3D egaPoint2)
    {
        return conformalSpace.EncodeIpnsFlatLineFromPoints(
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
    public static RGaConformalBlade EncodeIpnsFlatLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        return egaDirection
            .EncodeEGaBlade(conformalSpace)
            .DivideByNorm()
            .EGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatLineFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return conformalSpace.EncodeIpnsFlatLine(
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
    public static RGaConformalBlade EncodeIpnsFlatPlane(this RGaConformalSpace conformalSpace, double distance, double normalX, double normalY, double normalZ)
    {
        return conformalSpace.EncodeIpnsFlatHyperPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatPlane(this RGaConformalSpace conformalSpace, double distance, Float64Vector3D egaNormalVector)
    {
        return conformalSpace.EncodeIpnsFlatHyperPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatPlane(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Vector3D egaVector1, Float64Vector3D egaVector2)
    {
        var egaDirectionBivector = 
            egaVector1.Op(egaVector2);

        return conformalSpace.EncodeIpnsFlatPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatPlane(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Vector3D egaNormalVector)
    {
        var egaDirectionBivector = 
            egaNormalVector.NormalToUnitDirection3D();

        return conformalSpace.EncodeIpnsFlatPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatPlane(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Bivector3D egaBivector)
    {
        return conformalSpace.EncodeIpnsFlatPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint1, Float64Vector3D egaPoint2, Float64Vector3D egaPoint3)
    {
        return conformalSpace.EncodeIpnsFlatPlane(
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
    public static RGaConformalBlade EncodeIpnsFlatPlane(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
    {
        return egaDirection
            .EncodeEGaBlade(conformalSpace)
            .DivideByNorm()
            .EGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
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
    public static RGaConformalBlade EncodeIpnsFlatPlaneFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return conformalSpace.EncodeIpnsFlatPlane(
            egaPoint1,
            egaDirection
        );
    }

    
    /// <summary>
    /// Convert a 3D Euclidean point and normal direction scalar into a CGA IPNS flat volume
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormal"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatVolume(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, double egaNormal)
    {
        var egaDirectionTrivector =
            Float64Trivector3D.Create(1d / egaNormal);

        return conformalSpace.EncodeIpnsFlatVolume(
            egaPoint,
            egaDirectionTrivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 3-blade into a CGA IPNS flat volume
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaTrivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatVolume(this RGaConformalSpace conformalSpace, Float64Vector3D egaPoint, Float64Trivector3D egaTrivector)
    {
        return conformalSpace.EncodeIpnsFlatVolume(
            egaPoint.ToRGaFloat64Vector(),
            egaTrivector.ToRGaFloat64Trivector()
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
    public static RGaConformalBlade EncodeIpnsFlatVolume(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64HigherKVector egaDirection)
    {
        return egaDirection
            .EncodeEGaBlade(conformalSpace)
            .DivideByNorm()
            .EGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatHyperPlane(this RGaConformalSpace conformalSpace, double distance, RGaFloat64Vector egaNormalVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaNormalVector));

        var normal = 
            egaNormalVector.EncodeEGaBlade(conformalSpace);

        return normal
            .DivideByNorm()
            .TranslateBy(normal.SetNorm(distance))
            .Negative();
    }
    
    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatHyperPlane(this RGaConformalSpace conformalSpace, double distance, RGaConformalBlade egaNormalVector)
    {
        Debug.Assert(egaNormalVector.IsEGaVector());
        
        return egaNormalVector
            .DivideByNorm()
            .TranslateBy(egaNormalVector.SetNorm(distance))
            .Negative();
    }


    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA IPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlat(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
    {
        return egaDirection
            .EncodeEGaBlade(conformalSpace)
            .DivideByNorm()
            .EGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsFlatFromPoints(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] egaPointArray)
    {
        var egaPoint1 = 
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(conformalSpace.Processor);

        return conformalSpace.EncodeIpnsFlat(
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
    public static RGaConformalBlade EncodeIpnsFlatFromPoints(this RGaConformalSpace conformalSpace, IReadOnlyList<RGaFloat64Vector> egaPointList)
    {
        var egaPoint1 = 
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(conformalSpace.Processor);

        return conformalSpace.EncodeIpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

}