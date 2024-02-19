﻿using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Polyhedra.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;

/// <summary>
/// This represents a planar beam in 3D space
/// </summary>
public sealed class PlanarBeam3Dp
{
    public double OriginX { get; }

    public double OriginY { get; }

    public double OriginZ { get; }


    public double Direction1X { get; }

    public double Direction1Y { get; }

    public double Direction1Z { get; }


    public double Direction2X { get; }

    public double Direction2Y { get; }

    public double Direction2Z { get; }


    public Float64Vector3D Origin
    {
        get { return Float64Vector3D.Create(OriginX, OriginY, OriginZ); }
    }

    public Float64Vector3D Direction1
    {
        get { return Float64Vector3D.Create(Direction1X, Direction1Y, Direction1Z); }
    }

    public Float64Vector3D Direction2
    {
        get { return Float64Vector3D.Create(Direction2X, Direction2Y, Direction2Z); }
    }


    public Line3D Ray1
    {
        get { return new Line3D(OriginX, OriginY, OriginZ, Direction1X, Direction1Y, Direction1Z); }
    }

    public Line3D Ray2
    {
        get { return new Line3D(OriginX, OriginY, OriginZ, Direction2X, Direction2Y, Direction2Z); }
    }


    public Float64Vector3D Normal12
    {
        get { return Direction1.VectorCross(Direction2); }
    }

    public Float64Vector3D UnitNormal12
    {
        get { return Direction1.VectorUnitCross(Direction2); }
    }

    public Float64Vector3D Normal21
    {
        get { return Direction2.VectorCross(Direction1); }
    }

    public Float64Vector3D UnitNormal21
    {
        get { return Direction2.VectorUnitCross(Direction1); }
    }


    public Line3D GetNormalLine12()
    {
        return new Line3D(Origin, Normal12);
    }

    public Line3D GetUnitNormalLine12()
    {
        return new Line3D(Origin, UnitNormal12);
    }

    public Line3D GetNormalLine21()
    {
        return new Line3D(Origin, Normal21);
    }

    public Line3D GetUnitNormalLine21()
    {
        return new Line3D(Origin, UnitNormal21);
    }


    internal PlanarBeam3Dp(double pX, double pY, double pZ, double v1X, double v1Y, double v1Z, double v2X, double v2Y, double v2Z)
    {
        OriginX = pX;
        OriginY = pY;
        OriginZ = pZ;

        Direction1X = v1X;
        Direction1Y = v1Y;
        Direction1Z = v1Z;

        Direction2X = v2X;
        Direction2Y = v2Y;
        Direction2Z = v2Z;
    }


    public Float64Vector3D GetPoint(double t1, double t2)
    {
        return Float64Vector3D.Create(OriginX + t1 * Direction1X + t2 * Direction2X,
            OriginY + t1 * Direction1Y + t2 * Direction2Y,
            OriginZ + t1 * Direction1Z + t2 * Direction2Z);
    }

    public IEnumerable<Float64Vector3D> GetPoints(IEnumerable<Float64Vector2D> tList)
    {
        return tList.Select(t => GetPoint(t.X, t.Y));
    }

    public LineSegment3D GetLineSegment(Float64Vector2D t1, Float64Vector2D t2)
    {
        var point1 = GetPoint(t1.X, t1.Y);
        var point2 = GetPoint(t2.X, t2.Y);

        return new LineSegment3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z
        );
    }

    public Line3D GetRay(Float64Vector2D t1, Float64Vector2D t2)
    {
        var point1 = GetPoint(t1.X, t1.Y);
        var point2 = GetPoint(t2.X, t2.Y);

        return new Line3D(
            point1.X, point1.Y, point1.Z,
            point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z
        );
    }

    public Triangle3D GetTriangle()
    {
        return new Triangle3D(
            OriginX, OriginY, OriginZ,
            OriginX + Direction1X, OriginY + Direction1Y, OriginZ + Direction1Z,
            OriginX + Direction2X, OriginY + Direction2Y, OriginZ + Direction2Z
        );
    }

    public Triangle3D GetTriangle(Float64Vector2D t1, Float64Vector2D t2, Float64Vector2D t3)
    {
        var point1 = GetPoint(t1.X, t1.Y);
        var point2 = GetPoint(t2.X, t2.Y);
        var point3 = GetPoint(t3.X, t3.Y);

        return new Triangle3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z,
            point3.X, point3.Y, point3.Z
        );
    }

    public Tetrahedron3D GetTetrahedron(Float64Vector3D point4)
    {
        return new Tetrahedron3D(
            OriginX, OriginY, OriginZ,
            OriginX + Direction1X, OriginY + Direction1Y, OriginZ + Direction1Z,
            OriginX + Direction2X, OriginY + Direction2Y, OriginZ + Direction2Z,
            point4.X, point4.Y, point4.Z
        );
    }
}