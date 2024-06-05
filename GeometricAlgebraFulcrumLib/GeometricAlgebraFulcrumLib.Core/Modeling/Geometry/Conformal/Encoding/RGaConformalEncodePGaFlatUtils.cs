using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;

public static class RGaConformalEncodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLineFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaLineFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPlaneFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaPlaneFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2),
            conformalSpace.EncodeEGaVectorAsVector(point3)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, double pointX, double pointY, double pointZ)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPoint(this RGaConformalSpace conformalSpace, LinFloat64Vector3D point)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPoint(
            conformalSpace.EncodeEGaVectorAsVector(point)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLine(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaPoint, LinFloat64Vector2D egaDirection)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaLine(
            conformalSpace.EncodeEGaVectorAsVector(egaPoint),
            conformalSpace.EncodeEGaVectorAsVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLine(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaDirection)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaLine(
            conformalSpace.EncodeEGaVectorAsVector(egaPoint),
            conformalSpace.EncodeEGaVectorAsVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaLineFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaLineFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPlaneFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        Debug.Assert(conformalSpace.Is5D);

        return conformalSpace.EncodePGaPlaneFromPoints(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2),
            conformalSpace.EncodeEGaVectorAsVector(point3)
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
    public static RGaConformalBlade EncodePGaLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint) &&
            conformalSpace.IsValidEGaElement(egaDirection)
        );

        var p1 = conformalSpace.Eo + egaPoint; //EncodePoint(egaPoint1);
        var p2 = p1 + egaDirection; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
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
    public static RGaConformalBlade EncodePGaBisectorLine(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        Debug.Assert(conformalSpace.Is4D);

        return conformalSpace.EncodePGaBisectorLine(
            conformalSpace.EncodeEGaVectorAsVector(point1),
            conformalSpace.EncodeEGaVectorAsVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaBisectorLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2)
        );

        var p1Dual = conformalSpace.Eo + egaPoint1;
        var p2Dual = conformalSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaUnDual();

        var p1 = p1Dual.PGaUnDual();
        var p2 = p2Dual.PGaUnDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToConformalBlade(conformalSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodePGaPlaneFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        Debug.Assert(
            conformalSpace.IsValidEGaElement(egaPoint1) &&
            conformalSpace.IsValidEGaElement(egaPoint2) &&
            conformalSpace.IsValidEGaElement(egaPoint3)
        );

        var p1 = conformalSpace.Eo + egaPoint1;
        var p2 = conformalSpace.Eo + egaPoint2;
        var p3 = conformalSpace.Eo + egaPoint3;

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