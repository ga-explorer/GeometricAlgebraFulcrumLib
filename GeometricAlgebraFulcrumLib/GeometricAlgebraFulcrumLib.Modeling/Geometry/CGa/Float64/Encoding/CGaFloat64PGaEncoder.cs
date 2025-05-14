using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

public class CGaFloat64PGaEncoder :
    CGaFloat64EncoderBase
{
    internal CGaFloat64PGaEncoder(CGaFloat64GeometricSpace geometricSpace) 
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Point(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector2D point)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Point(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return LineFromPoints(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point1),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneFromPoints(LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return PlaneFromPoints(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point1),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point2),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(double pointX, double pointY, double pointZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Point(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(LinFloat64Vector3D point)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Point(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(LinFloat64Vector2D egaPoint, LinFloat64Vector2D egaDirection)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Line(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(egaPoint),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(LinFloat64Vector3D egaPoint, LinFloat64Vector3D egaDirection)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Line(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(egaPoint),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return LineFromPoints(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point1),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneFromPoints(LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return PlaneFromPoints(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point1),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point2),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Point(XGaFloat64Vector egaPoint)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaPoint));

        return (GeometricSpace.Eo + egaPoint).PGaUnDual();
        //return (Ie + VGaDual(egaPoint).Op(Eo)).GetKVectorPart(Ie.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade Line(XGaFloat64Vector egaPoint, XGaFloat64Vector egaDirection)
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(egaPoint) &&
            GeometricSpace.IsValidVGaElement(egaDirection)
        );

        var p1 = GeometricSpace.Eo + egaPoint; //EncodePoint(egaPoint1);
        var p2 = p1 + egaDirection; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade LineFromPoints(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2)
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(egaPoint1) &&
            GeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1 = GeometricSpace.Eo + egaPoint1; //EncodePoint(egaPoint1);
        var p2 = GeometricSpace.Eo + egaPoint2; //EncodePoint(egaPoint2);

        return p1.Op(p2).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BisectorLine(LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return BisectorLine(
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point1),
            GeometricSpace.Encode.VGa.VectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BisectorLine(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2)
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(egaPoint1) &&
            GeometricSpace.IsValidVGaElement(egaPoint2)
        );

        var p1Dual = GeometricSpace.Eo + egaPoint1;
        var p2Dual = GeometricSpace.Eo + egaPoint2;

        var pgaLine = p1Dual.Op(p2Dual).PGaUnDual();

        var p1 = p1Dual.PGaUnDual();
        var p2 = p2Dual.PGaUnDual();

        return (p1 + p2).Gp(pgaLine).VectorPartToConformalBlade(GeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade PlaneFromPoints(XGaFloat64Vector egaPoint1, XGaFloat64Vector egaPoint2, XGaFloat64Vector egaPoint3)
    {
        Debug.Assert(
            GeometricSpace.IsValidVGaElement(egaPoint1) &&
            GeometricSpace.IsValidVGaElement(egaPoint2) &&
            GeometricSpace.IsValidVGaElement(egaPoint3)
        );

        var p1 = GeometricSpace.Eo + egaPoint1;
        var p2 = GeometricSpace.Eo + egaPoint2;
        var p3 = GeometricSpace.Eo + egaPoint3;

        return p1.Op(p2).Op(p3).PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(params XGaFloat64Vector[] egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(GeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(egaPoint => GeometricSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade BladeFromPoints(IEnumerable<XGaFloat64Vector> egaPointArray)
    {
        Debug.Assert(
            egaPointArray.All(GeometricSpace.IsValidVGaElement)
        );

        return egaPointArray
            .Select(egaPoint => GeometricSpace.Eo + egaPoint)
            .Op()
            .PGaUnDual();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Translation(XGaFloat64Vector egaVector)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaVector));

        return new CGaFloat64Versor(
            GeometricSpace,
            1 - 0.5d * GeometricSpace.EoVector.Op(egaVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis =
            Point(egaAxisPoint);

        return new CGaFloat64Versor(
            GeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Versor Rotation(LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisDirection)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();
        var pgaAxis = Line(egaAxisPoint, egaAxisDirection);

        return new CGaFloat64Versor(
            GeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }



}