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

public class CGaFloat64OpnsFlatEncoder :
    CGaFloat64EncoderBase
{
    internal CGaFloat64OpnsFlatEncoder(CGaFloat64GeometricSpace geometricSpace) 
        : base(geometricSpace)
    {
    }


    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY)
    {
        return Point(
            LinFloat64Vector2D.Create(pointX, pointY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA OPNS flat point
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector2D egaPoint)
    {
        return Point(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <param name="pointZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY, double pointZ)
    {
        return Point(
            LinFloat64Vector3D.Create(pointX, pointY, pointZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA OPNS flat point
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector3D egaPoint)
    {
        return Point(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector egaPoint)
    {
        return Point(
            egaPoint.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(RGaFloat64Vector egaPoint)
    {
        return GeometricSpace.Eoi.TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a Euclidean point a CGA OPNS flat point
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(CGaFloat64Blade egaPoint)
    {
        return GeometricSpace.Eoi.TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(double distance, double normalX, double normalY)
    {
        return HyperPlane(
            distance,
            LinFloat64Vector2D.Create(normalX, normalY).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(double distance, LinFloat64Vector2D egaNormalVector)
    {
        return HyperPlane(
            distance,
            egaNormalVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point and vector into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="point"></param>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(LinFloat64Vector2D point, LinFloat64Vector2D vector)
    {
        return Line(
            point.ToRGaFloat64Vector(),
            vector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and a direction vector into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector)
    {
        return Line(
            egaPoint.ToRGaFloat64Vector(),
            egaVector.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        var directionOpEi =
            egaDirection
                .EncodeVGaBlade(GeometricSpace)
                .DivideByNorm()
                .Op(GeometricSpace.Ei);

        return GeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(CGaFloat64Blade egaPoint, CGaFloat64Blade egaDirection)
    {
        Debug.Assert(
            egaPoint.IsVGaVector() &&
            egaDirection.IsVGaVector()
        );

        var directionOpEi =
            egaDirection
                .DivideByNorm()
                .Op(GeometricSpace.Ei);

        return GeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert two 2D Euclidean points into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        return Line(
            point1.ToRGaFloat64Vector(),
            (point2 - point1).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points in 3D into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineFromPoints(LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        return LineFromPoints(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA OPNS flat line
    /// </summary>
    
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineFromPoints(RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return Line(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Plane(double distance, double normalX, double normalY, double normalZ)
    {
        return HyperPlane(
            distance,
            LinFloat64Vector3D.Create(normalX, normalY, normalZ).ToRGaFloat64Vector()
        );
    }

    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Plane(double distance, LinFloat64Vector3D egaNormalVector)
    {
        return HyperPlane(
            distance,
            egaNormalVector.EncodeVGaVector(GeometricSpace)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and two direction vectors into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaVector1"></param>
    /// <param name="egaVector2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Plane(LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaVector1, LinFloat64Vector3D egaVector2)
    {
        var egaDirectionBivector =
            egaVector1.Op(egaVector2);

        return Plane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and egaNormalVector direction vector into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Plane(LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaNormalVector)
    {
        var egaDirectionBivector =
            egaNormalVector.NormalToUnitDirection3D();

        return Plane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 2-blade into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaBivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Plane(LinFloat64Vector3D egaPoint, LinFloat64Bivector3D egaBivector)
    {
        return Plane(
            egaPoint.ToRGaFloat64Vector(),
            egaBivector.ToRGaFloat64Bivector()
        );
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points in 3D into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneFromPoints(LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        return Plane(
            egaPoint1,
            egaPoint2 - egaPoint1,
            egaPoint3 - egaPoint1
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA OPNS flat plane
    /// </summary>
    
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Plane(RGaFloat64Vector egaPoint, RGaFloat64Bivector egaDirection)
    {
        var directionOpEi =
            egaDirection
                .EncodeVGaBlade(GeometricSpace)
                .DivideByNorm()
                .Op(GeometricSpace.Ei);

        return GeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points into a CGA OPNS flat
    /// </summary>
    
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneFromPoints(RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return Plane(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA OPNS flat hyper-plane
    /// </summary>
    
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlane(double distance, RGaFloat64Vector egaNormalVector)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaNormalVector));

        var normal =
            egaNormalVector.EncodeVGaBlade(GeometricSpace);

        return normal
            .DivideByNorm()
            .TranslateBy(normal.SetNorm(distance))
            .Negative()
            .IpnsToOpns();
    }

    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA OPNS flat hyper-plane
    /// </summary>
    
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade HyperPlane(double distance, CGaFloat64Blade egaNormalVector)
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
    
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Blade(RGaFloat64Vector egaPoint, RGaFloat64KVector egaDirection)
    {
        var directionOpEi =
            egaDirection
                .EncodeVGaBlade(GeometricSpace)
                .DivideByNorm()
                .Op(GeometricSpace.Ei);

        return GeometricSpace.Eo.Op(directionOpEi).TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA OPNS flat
    /// </summary>
    
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(params RGaFloat64Vector[] egaPointArray)
    {
        var egaPoint1 =
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(GeometricSpace.Processor);

        return Blade(
            egaPoint1,
            egaDirection
        );
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA OPNS flat
    /// </summary>
    
    /// <param name="egaPointList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(IReadOnlyList<RGaFloat64Vector> egaPointList)
    {
        var egaPoint1 =
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(GeometricSpace.Processor);

        return Blade(
            egaPoint1,
            egaDirection
        );
    }

}