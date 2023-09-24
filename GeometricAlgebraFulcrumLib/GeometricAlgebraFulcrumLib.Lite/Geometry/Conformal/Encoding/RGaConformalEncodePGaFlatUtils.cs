using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPoint(
            Float64Vector2D.Create(pointX, pointY).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, Float64Vector2D point)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPoint(
            point.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaLineFromPoints(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2, Float64Vector2D point3)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPlaneFromPoints(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector(),
            point3.ToRGaFloat64Vector()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY, double pointZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPoint(
            Float64Vector3D.Create(pointX, pointY, pointZ).ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, Float64Vector3D point)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPoint(
            point.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaLineFromPoints(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPlaneFromPoints(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector(),
            point3.ToRGaFloat64Vector()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint)
    {
        Debug.Assert(conformalSpace.IsValidEGaElement(egaPoint));

        return (conformalSpace.Eo + egaPoint).PGaUnDual();
        //return (Ie + EGaDual(egaPoint).Op(Eo)).GetKVectorPart(Ie.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLineFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2)
        );

        var p1 = conformalSpace.Eo + egaPoint1; //EncodePoint(egaPoint1);
        var p2 = conformalSpace.Eo + egaPoint2; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPlaneFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2) &&
            conformalSpace.IsValidEGaElement(egaPoint3)
        );

        var p1 = conformalSpace.Eo + egaPoint1; //EncodePoint(egaPoint1);
        var p2 = conformalSpace.Eo + egaPoint2; //EncodePoint(egaPoint2);
        var p3 = conformalSpace.Eo + egaPoint3; //EncodePoint(egaPoint3);

        return p1.Op(p2).Op(p3).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaFlatFromPoints(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(conformalSpace.IsValidEGaElement)
        );

        return egaPointArray
            .Select(egaPoint => conformalSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaFlatFromPoints(this RGaConformalSpace conformalSpace, IEnumerable<RGaFloat64Vector> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(conformalSpace.IsValidEGaElement)
        );

        return egaPointArray
            .Select(egaPoint => conformalSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

}