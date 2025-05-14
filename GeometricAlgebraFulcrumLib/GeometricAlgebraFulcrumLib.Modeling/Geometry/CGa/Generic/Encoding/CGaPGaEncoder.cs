using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

public sealed class CGaPGaEncoder<T> :
    CGaEncoderBase<T>
{
    internal CGaPGaEncoder(CGaGeometricSpace<T> geometricSpace)
        : base(geometricSpace)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(Scalar<T> pointX, Scalar<T> pointY)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Point(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(pointX, pointY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector2D<T> point)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Point(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> LineFromPoints(LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return LineFromPoints(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point1),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PlaneFromPoints(LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return PlaneFromPoints(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point1),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point2),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(Scalar<T> pointX, Scalar<T> pointY, Scalar<T> pointZ)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Point(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(pointX, pointY, pointZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(LinVector3D<T> point)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Point(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(LinVector2D<T> egaPoint, LinVector2D<T> egaDirection)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return Line(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaPoint),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(LinVector3D<T> egaPoint, LinVector3D<T> egaDirection)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return Line(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaPoint),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(egaDirection)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> LineFromPoints(LinVector3D<T> point1, LinVector3D<T> point2)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return LineFromPoints(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point1),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> PlaneFromPoints(LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        Debug.Assert(GeometricSpace.Is5D);

        return PlaneFromPoints(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point1),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point2),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point3)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Point(XGaVector<T> egaPoint)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaPoint));

        return (GeometricSpace.Eo + egaPoint).PGaUnDual();
        //return (Ie + VGaDual(egaPoint).Op(Eo)).GetKVectorPart(Ie.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> Line(XGaVector<T> egaPoint, XGaVector<T> egaDirection)
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
    public CGaBlade<T> LineFromPoints(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
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
    public CGaBlade<T> BisectorLine(LinVector2D<T> point1, LinVector2D<T> point2)
    {
        Debug.Assert(GeometricSpace.Is4D);

        return BisectorLine(
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point1),
            GeometricSpace.EncodeVGa.VectorAsXGaVector(point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> BisectorLine(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2)
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
    public CGaBlade<T> PlaneFromPoints(XGaVector<T> egaPoint1, XGaVector<T> egaPoint2, XGaVector<T> egaPoint3)
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
    public CGaBlade<T> BladeFromPoints(params XGaVector<T>[] egaPointArray)
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
    public CGaBlade<T> BladeFromPoints(IEnumerable<XGaVector<T>> egaPointArray)
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
    public CGaVersor<T> Translation(XGaVector<T> egaVector)
    {
        Debug.Assert(GeometricSpace.IsValidVGaElement(egaVector));

        return new CGaVersor<T>(
            GeometricSpace,
            1 - GeometricSpace.EoVector.Op(egaVector).Divide(2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        Debug.Assert(GeometricSpace.Is4D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis =
            GeometricSpace.EncodePGa.Point(egaAxisPoint);

        return new CGaVersor<T>(
            GeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaVersor<T> Rotation(LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisDirection)
    {
        Debug.Assert(GeometricSpace.Is5D);

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var pgaAxis = GeometricSpace.EncodePGa.Line(egaAxisPoint, egaAxisDirection);

        return new CGaVersor<T>(
            GeometricSpace,
            halfAngleCos + halfAngleSin * pgaAxis.InternalKVector
        );
    }

}