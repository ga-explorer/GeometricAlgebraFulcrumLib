using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> egaPointX, Scalar<T> egaPointY)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> egaPointX, Scalar<T> egaPointY, Scalar<T> egaPointZ)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY, egaPointZ).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2, LinVector2D<T> egaPoint3)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaPoint1, LinVector2D<T> egaPoint2, LinVector2D<T> egaPoint3, LinVector2D<T> egaPoint4)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeOpnsRound(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint4.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaPoint1, LinVector3D<T> egaPoint2, LinVector3D<T> egaPoint3, LinVector3D<T> egaPoint4)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRound(
            egaPoint1.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint2.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint3.ToXGaVector(conformalSpace.EuclideanProcessor),
            egaPoint4.ToXGaVector(conformalSpace.EuclideanProcessor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3, XGaVector<T> egaPoint4)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRound(
            egaPoint1,
            egaPoint2,
            egaPoint3,
            egaPoint4
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = conformalSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRound<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRound<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRound<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = conformalSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRound<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3, XGaVector<T> egaPoint4)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = conformalSpace.EncodeIpnsRoundPoint(egaPoint3);
        var p4 = conformalSpace.EncodeIpnsRoundPoint(egaPoint4);

        return p1.Op(p2).Op(p3).Op(p4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRound<T>(this XGaConformalSpace<T> conformalSpace, params XGaVector<T>[] egaPointArray)
    {
        return egaPointArray.Select(conformalSpace.EncodeIpnsRoundPoint).Op();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeOpnsRound<T>(this XGaConformalSpace<T> conformalSpace, IEnumerable<XGaVector<T>> egaPointList)
    {
        return egaPointList.Select(conformalSpace.EncodeIpnsRoundPoint).Op();
    }
}