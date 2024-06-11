using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public static class CGaFloat64EncodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaLineFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaPlaneFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point2),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double pointX, double pointY, double pointZ)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaPoint(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D egaPoint, LinFloat64Vector2D egaDirection)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaLine(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaPoint),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaDirection)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaLine(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaPoint),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaLineFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        Debug.Assert(cgaGeometricSpace.Is5D);

        return cgaGeometricSpace.EncodePGaPlaneFromPoints(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point2),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint)
    {
        Debug.Assert(cgaGeometricSpace.IsValidVGaElement(egaPoint));

        return (cgaGeometricSpace.Eo + egaPoint).PGaUnDual();
        //return (Ie + VGaDual(egaPoint).Op(Eo)).GetKVectorPart(Ie.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint, RGaFloat64Vector egaDirection)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint) &&
            cgaGeometricSpace.IsValidVGaElement(egaDirection)
        );

        var p1 = cgaGeometricSpace.Eo + egaPoint; //EncodePoint(egaPoint1);
        var p2 = p1 + egaDirection; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1 = cgaGeometricSpace.Eo + egaPoint1; //EncodePoint(egaPoint1);
        var p2 = cgaGeometricSpace.Eo + egaPoint2; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaBisectorLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        Debug.Assert(cgaGeometricSpace.Is4D);

        return cgaGeometricSpace.EncodePGaBisectorLine(
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point1),
            cgaGeometricSpace.EncodeVGaVectorAsRGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaBisectorLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1Dual = cgaGeometricSpace.Eo + egaPoint1;
        var p2Dual = cgaGeometricSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaUnDual();

        var p1 = p1Dual.PGaUnDual();
        var p2 = p2Dual.PGaUnDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToConformalBlade(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector egaPoint1, RGaFloat64Vector egaPoint2, RGaFloat64Vector egaPoint3)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(egaPoint1) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint2) &&
            cgaGeometricSpace.IsValidVGaElement(egaPoint3)
        );

        var p1 = cgaGeometricSpace.Eo + egaPoint1;
        var p2 = cgaGeometricSpace.Eo + egaPoint2;
        var p3 = cgaGeometricSpace.Eo + egaPoint3;

        return p1.Op(p2).Op(p3).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(cgaGeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(egaPoint => cgaGeometricSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade EncodePGaFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IEnumerable<RGaFloat64Vector> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(cgaGeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(egaPoint => cgaGeometricSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

}