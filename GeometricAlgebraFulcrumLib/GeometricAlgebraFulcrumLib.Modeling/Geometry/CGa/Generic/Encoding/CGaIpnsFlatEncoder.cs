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

public sealed class CGaIpnsFlatEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaIpnsFlatEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(Scalar<T> pointX, Scalar<T> pointY)
    {
        return Point(
            LinVector2D<T>.Create(pointX, pointY).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector2D<T> egaPoint)
    {
        return Point(
            egaPoint.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <param name="pointZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        return Point(
            LinVector3D<T>.Create(pointX, pointY, pointZ).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector3D<T> egaPoint)
    {
        return Point(
            egaPoint.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector<T> egaPoint)
    {
        return Point(
            egaPoint.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(XGaVector<T> egaPoint)
    {
        return GeometricSpace.IeInv
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA IPNS flat line
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(Scalar<T> distance, Scalar<T> normalX, Scalar<T> normalY)
    {
        return HyperPlane(
            distance,
            LinVector2D<T>.Create(normalX, normalY).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D egaNormalVector vector and distance to origin into a CGA IPNS flat line
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(Scalar<T> distance, LinVector2D<T> egaNormalVector)
    {
        return HyperPlane(
            distance,
            egaNormalVector.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point and vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="point"></param>
    /// <param name="vector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(LinVector2D<T> point, LinVector2D<T> vector)
    {
        return Line(
            point.ToXGaVector(GeometricSpace.EuclideanProcessor),
            vector.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert two 2D Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> LineFromPoints(LinVector2D<T> point1, LinVector2D<T> point2)
    {
        return Line(
            point1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            (point2 - point1).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and a direction vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(LinVector3D<T> egaPoint, LinVector3D<T> egaVector)
    {
        return Line(
            egaPoint.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaVector.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points in 3D into a CGA IPNS flat line
    /// </summary>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> LineFromPoints(LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2)
    {
        return LineFromPoints(
            egaPoint1.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction vector into a CGA IPNS flat line
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(XGaVector<T> egaPoint, XGaVector<T> egaDirection)
    {
        return egaDirection
            .EncodeVGaVector(GeometricSpace)
            /*.DivideByNorm()*/
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 2 Euclidean points into a CGA IPNS flat line
    /// </summary>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> LineFromPoints(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var egaDirection =
            egaPoint2 - egaPoint1;

        return Line(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA IPNS flat plane
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Plane(Scalar<T> distance, Scalar<T> normalX, Scalar<T> normalY, Scalar<T> normalZ)
    {
        return HyperPlane(
            distance,
            LinVector3D<T>.Create(normalX, normalY, normalZ).ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D egaNormalVector vector and distance to origin into a CGA IPNS flat plane
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Plane(Scalar<T> distance, LinVector3D<T> egaNormalVector)
    {
        return HyperPlane(
            distance,
            egaNormalVector.ToXGaVector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and two direction vectors into a CGA IPNS flat plane
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaVector1"></param>
    /// <param name="egaVector2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Plane(LinVector3D<T> egaPoint, LinVector3D<T> egaVector1, LinVector3D<T> egaVector2)
    {
        var egaDirectionBivector =
            egaVector1.Op(egaVector2);

        return Plane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and egaNormalVector direction vector into a CGA IPNS flat plane
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Plane(LinVector3D<T> egaPoint, LinVector3D<T> egaNormalVector)
    {
        var egaDirectionBivector =
            egaNormalVector.NormalToUnitDirection3D();

        return Plane(
            egaPoint,
            egaDirectionBivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaBivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Plane(LinVector3D<T> egaPoint, LinBivector3D<T> egaBivector)
    {
        return Plane(
            egaPoint.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(GeometricSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points in 3D into a CGA IPNS flat plane
    /// </summary>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PlaneFromPoints(LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3)
    {
        return Plane(
            egaPoint1,
            egaPoint2 - egaPoint1,
            egaPoint3 - egaPoint1
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Plane(XGaVector<T> egaPoint, XGaBivector<T> egaDirection)
    {
        return egaDirection
            .EncodeVGaBivector(GeometricSpace)
            /*.DivideByNorm()*/
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of 3 Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="egaPoint1"></param>
    /// <param name="egaPoint2"></param>
    /// <param name="egaPoint3"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PlaneFromPoints(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var egaDirection =
            (egaPoint2 - egaPoint1).Op(egaPoint3 - egaPoint1);

        return Plane(
            egaPoint1,
            egaDirection
        );
    }


    /// <summary>
    /// Convert a 3D Euclidean point and normal direction scalar into a CGA IPNS flat volume
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaNormal"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Volume(LinVector3D<T> egaPoint, Scalar<T> egaNormal)
    {
        var egaDirectionTrivector =
            LinTrivector3D<T>.Create(1d / egaNormal);

        return Volume(
            egaPoint,
            egaDirectionTrivector
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point and direction 3-blade into a CGA IPNS flat volume
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaTrivector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Volume(LinVector3D<T> egaPoint, LinTrivector3D<T> egaTrivector)
    {
        return Volume(
            egaPoint.ToXGaVector(GeometricSpace.EuclideanProcessor),
            egaTrivector.ToXGaTrivector(GeometricSpace.ConformalProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point and direction 2-blade into a CGA IPNS flat plane
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Volume(XGaVector<T> egaPoint, XGaHigherKVector<T> egaDirection)
    {
        return egaDirection
            .EncodeVGaBlade(GeometricSpace)
            /*.DivideByNorm()*/
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }


    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlane(Scalar<T> distance, XGaVector<T> egaNormalVector)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaNormalVector));

        var normal =
            egaNormalVector.EncodeVGaVector(GeometricSpace);

        return normal
            /*.DivideByNorm()*/
            .TranslateBy(normal.Times(distance))
            .Negative();
    }

    /// <summary>
    /// Convert a normal vector and distance to origin into a CGA IPNS flat hyper-plane
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="egaNormalVector"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> HyperPlane(Scalar<T> distance, CGaBlade<T> egaNormalVector)
    {
        Debug.Assert(egaNormalVector.IsVGaVector());

        return egaNormalVector
            /*.DivideByNorm()*/
            .TranslateBy(egaNormalVector.Times(distance))
            .Negative();
    }


    /// <summary>
    /// Convert a Euclidean point and direction blade into a CGA IPNS flat
    /// </summary>
    /// <param name="egaPoint"></param>
    /// <param name="egaDirection"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Blade(XGaVector<T> egaPoint, XGaKVector<T> egaDirection)
    {
        return egaDirection
            .EncodeVGaBlade(GeometricSpace)
            /*.DivideByNorm()*/
            .VGaDual()
            .GradeInvolution()
            .TranslateBy(egaPoint);
    }

    /// <summary>
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="egaPointArray"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> BladeFromPoints(params XGaVector<T>[] egaPointArray)
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
    /// Convert a set of Euclidean points into a CGA IPNS flat
    /// </summary>
    /// <param name="egaPointList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> BladeFromPoints(IReadOnlyList<XGaVector<T>> egaPointList)
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