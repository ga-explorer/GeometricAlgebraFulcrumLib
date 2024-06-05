using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeOpnsFlatUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY)
    {
        return conformalSpace.EncodeOpnsFlatPoint(
            LinFloat64Vector2D.Create(pointX, pointY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint)
    {
        return conformalSpace.EncodeOpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <param name="pointZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY, double pointZ)
    {
        return conformalSpace.EncodeOpnsFlatPoint(
            LinFloat64Vector3D.Create(pointX, pointY, pointZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint)
    {
        return conformalSpace.EncodeOpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }
    
    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector egaPoint)
    {
        return conformalSpace.EncodeOpnsFlatPoint(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        return conformalSpace.Eoi.TranslateBy(egaPoint);
    }
    
    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPoint(this RGaConformalSpace conformalSpace, RGaConformalBlade egaPoint)
    {
        return conformalSpace.Eoi.TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLine(this RGaConformalSpace conformalSpace, double distance, double normalX, double normalY)
    {
        return conformalSpace.EncodeOpnsFlatHyperPlane(
            distance, 
            LinFloat64Vector2D.Create(normalX, normalY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLine(this RGaConformalSpace conformalSpace, double distance, LinFloat64Vector2D egaNormalVector)
    {
        return conformalSpace.EncodeOpnsFlatHyperPlane(
            distance, 
            egaNormalVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point and vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="point"></param>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLine(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point, LinFloat64Vector2D vector)
    {
        return conformalSpace.EncodeOpnsFlatLine(
            point.ToRGaFloat64Vector(),
            vector.ToRGaFloat64Vector()
        );
    }
    
    /// <summary>
    /// Convert a 3D Euclidean point and a direction vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLine(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector)
    {
        return conformalSpace.EncodeOpnsFlatLine(
            egaPoint.ToRGaFloat64Vector(),
            egaVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        var directionOpEi = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .DivideByNorm()
                .Op(conformalSpace.Ei);

        return conformalSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }
    
    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLine(this RGaConformalSpace conformalSpace, RGaConformalBlade egaPoint, RGaConformalBlade egaDirection)
    {
        Debug.Assert(
            egaPoint.IsEGaVector() &&
            egaDirection.IsEGaVector()
        );

        var directionOpEi = 
            egaDirection
                .DivideByNorm()
                .Op(conformalSpace.Ei);

        return conformalSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert two 2D Euclidean points into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLineFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        return conformalSpace.EncodeOpnsFlatLine(
            point1.ToRGaFloat64Vector(),
            (point2 - point1).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points in 3D into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLineFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        return conformalSpace.EncodeOpnsFlatLineFromPoints(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }
    
    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA OPNS flat line
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatLineFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return conformalSpace.EncodeOpnsFlatLine(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlane(this RGaConformalSpace conformalSpace, double distance, double normalX, double normalY, double normalZ)
    {
        return conformalSpace.EncodeOpnsFlatHyperPlane(
            distance, 
            LinFloat64Vector3D.Create(normalX, normalY, normalZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlane(this RGaConformalSpace conformalSpace, double distance, LinFloat64Vector3D egaNormalVector)
    {
        return conformalSpace.EncodeOpnsFlatHyperPlane(
            distance, 
            egaNormalVector.EncodeEGaVectorBlade(conformalSpace)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and two direction vectors into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector1"></param>
    /// <param name="egaVector2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlane(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector1, LinFloat64Vector3D egaVector2)
    {
        var egaDirectionBivector = 
            egaVector1.Op(egaVector2);

        return conformalSpace.EncodeOpnsFlatPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and egaNormalVector direction vector into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlane(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaNormalVector)
    {
        var egaDirectionBivector = 
            egaNormalVector.NormalToUnitDirection3D();

        return conformalSpace.EncodeOpnsFlatPlane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 2-blade into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaBivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlane(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint, LinFloat64Bivector3D egaBivector)
    {
        return conformalSpace.EncodeOpnsFlatPlane(
            egaPoint.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points in 3D into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlaneFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        return conformalSpace.EncodeOpnsFlatPlane(
            egaPoint1,
            egaPoint2 - egaPoint1,
            egaPoint3 - egaPoint1
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA OPNS flat plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlane(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
    {
        var directionOpEi = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .DivideByNorm()
                .Op(conformalSpace.Ei);

        return conformalSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points into a CGA OPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatPlaneFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return conformalSpace.EncodeOpnsFlatPlane(
            egaPoint1,
            egaDirection
        );
    }

    
    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA OPNS flat hyper-plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatHyperPlane(this RGaConformalSpace conformalSpace, double distance, RGaFloat64Vector egaNormalVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaNormalVector));

        var normal = 
            egaNormalVector.EncodeEGaBlade(conformalSpace);

        return normal
            .DivideByNorm()
            .TranslateBy(normal.SetNorm(distance))
            .Negative()
            .IpnsToOpns();
    }
    
    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA OPNS flat hyper-plane
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatHyperPlane(this RGaConformalSpace conformalSpace, double distance, RGaConformalBlade egaNormalVector)
    {
        Debug.Assert(egaNormalVector.IsEGaVector());
        
        return egaNormalVector
            .DivideByNorm()
            .TranslateBy(egaNormalVector.SetNorm(distance))
            .Negative()
            .IpnsToOpns();
    }


    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA OPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlat(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
    {
        var directionOpEi = 
            egaDirection
                .EncodeEGaBlade(conformalSpace)
                .DivideByNorm()
                .Op(conformalSpace.Ei);

        return conformalSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA OPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatFromPoints(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] egaPointArray)
    {
        var egaPoint1 = 
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(conformalSpace.Processor);

        return conformalSpace.EncodeOpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA OPNS flat
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPointList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsFlatFromPoints(this RGaConformalSpace conformalSpace, IReadOnlyList<RGaFloat64Vector> egaPointList)
    {
        var egaPoint1 = 
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(conformalSpace.Processor);

        return conformalSpace.EncodeOpnsFlat(
            egaPoint1,
            egaDirection
        );
    }

}