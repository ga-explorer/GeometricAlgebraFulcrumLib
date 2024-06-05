using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeOpnsTangentUtils
{
    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> pointX, Scalar<T> pointY)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            LinVector2D<T>.Create(pointX, pointY).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 2D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            egaPoint.ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            LinVector3D<T>.Create(pointX, pointY, pointZ).ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a 3D Euclidean point into a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            egaPoint.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }
    
    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> egaPoint)
    {
        return conformalSpace.EncodeOpnsTangentPoint(
            egaPoint.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    /// <summary>
    /// Convert a Euclidean point a CGA IPNS flat point
    /// </summary>
    /// <param name="conformalSpace"></param>
    /// <param name="egaPoint"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> distance, Scalar<T> normalX, Scalar<T> normalY)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            LinVector2D<T>.Create(normalX, normalY).ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> distance, LinVector2D<T> egaNormalVector)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            egaNormalVector.ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point, LinVector2D<T> vector)
    {
        return conformalSpace.EncodeOpnsTangentLine(
            point.ToXGaVector(conformalSpace.EuclideanProcessor),
            vector.ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        return conformalSpace.EncodeOpnsTangentLine(
            point1.ToXGaVector(conformalSpace.EuclideanProcessor),
            (point2 - point1).ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaVector)
    {
        return conformalSpace.EncodeOpnsTangentLine(
            egaPoint.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaVector.ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2)
    {
        return conformalSpace.EncodeOpnsTangentLineFromPoints(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLine<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint, XGaVector<T> egaDirection)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> distance, Scalar<T> normalX, Scalar<T> normalY, Scalar<T> normalZ)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            LinVector3D<T>.Create(normalX, normalY, normalZ).ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> distance, LinVector3D<T> egaNormalVector)
    {
        return conformalSpace.EncodeOpnsTangentHyperPlane(
            distance, 
            egaNormalVector.ToXGaVector(conformalSpace.EuclideanProcessor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaVector1, LinVector3D<T> egaVector2)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint, LinVector3D<T> egaNormalVector)
    {
        var egaDirectionBivector = 
            egaNormalVector.NormalToUnitDirection3D();

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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint, LinBivector3D<T> egaBivector)
    {
        return conformalSpace.EncodeOpnsTangentPlane(
            egaPoint.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaBivector.ToXGaBivector(conformalSpace.Processor)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint, XGaBivector<T> egaDirection)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentHyperPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> distance, XGaVector<T> egaNormalVector)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaNormalVector));
        
        return new XGaConformalBlade<T>(
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
    public static XGaConformalBlade<T> EncodeOpnsTangent<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint, XGaKVector<T> egaDirection)
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
    public static XGaConformalBlade<T> EncodeOpnsTangentFromPoints<T>(this XGaConformalSpace<T> conformalSpace, params XGaVector<T>[] egaPointArray)
    {
        var egaPoint1 = 
            egaPointArray[0];

        var egaDirection =
            egaPointArray
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(conformalSpace.Processor);

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
    public static XGaConformalBlade<T> EncodeOpnsTangentFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IReadOnlyList<XGaVector<T>> egaPointList)
    {
        var egaPoint1 = 
            egaPointList[0];

        var egaDirection =
            egaPointList
                .Skip(1)
                .Select(egaPoint2 => egaPoint2 - egaPoint1)
                .Op(conformalSpace.Processor);

        return conformalSpace.EncodeOpnsTangent(
            egaPoint1,
            egaDirection
        );
    }

}