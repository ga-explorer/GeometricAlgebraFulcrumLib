using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPoint(this RGaConformalSpace conformalSpace, double egaPointX, double egaPointY)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPoint(this RGaConformalSpace conformalSpace, double egaPointX, double egaPointY, double egaPointZ)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPointX, egaPointY, egaPointZ).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint).CGaUnDual();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPointPair(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundCircle(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2, LinFloat64Vector2D egaPoint3)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundSphere(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint1, LinFloat64Vector2D egaPoint2, LinFloat64Vector2D egaPoint3, LinFloat64Vector2D egaPoint4)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodeOpnsRound(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector(),
            egaPoint4.ToRGaFloat64Vector()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundPointPair(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRoundPointPair(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundCircle(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRoundCircle(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundSphere(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint1, LinFloat64Vector3D egaPoint2, LinFloat64Vector3D egaPoint3, LinFloat64Vector3D egaPoint4)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodeOpnsRound(
            egaPoint1.ToRGaFloat64Vector(),
            egaPoint2.ToRGaFloat64Vector(),
            egaPoint3.ToRGaFloat64Vector(),
            egaPoint4.ToRGaFloat64Vector()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundSphere(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3, RGaFloat64Vector egaPoint4)
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
    public static RGaConformalBlade EncodeOpnsRoundPointPair(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRoundCircle(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = conformalSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRound(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        return conformalSpace.EncodeIpnsRoundPoint(egaPoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRound(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);

        return p1.Op(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRound(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = conformalSpace.EncodeIpnsRoundPoint(egaPoint3);

        return p1.Op(p2).Op(p3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRound(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3, RGaFloat64Vector egaPoint4)
    {
        var p1 = conformalSpace.EncodeIpnsRoundPoint(egaPoint1);
        var p2 = conformalSpace.EncodeIpnsRoundPoint(egaPoint2);
        var p3 = conformalSpace.EncodeIpnsRoundPoint(egaPoint3);
        var p4 = conformalSpace.EncodeIpnsRoundPoint(egaPoint4);

        return p1.Op(p2).Op(p3).Op(p4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRound(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] egaPointArray)
    {
        return egaPointArray.Select(conformalSpace.EncodeIpnsRoundPoint).Op();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeOpnsRound(this RGaConformalSpace conformalSpace, IEnumerable<RGaFloat64Vector> egaPointList)
    {
        return egaPointList.Select(conformalSpace.EncodeIpnsRoundPoint).Op();
    }
}