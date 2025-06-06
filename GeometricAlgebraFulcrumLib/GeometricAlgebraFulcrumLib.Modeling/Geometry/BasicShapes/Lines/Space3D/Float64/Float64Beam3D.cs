﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64Beam3D : 
    IFloat64Beam3D
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


    public double Direction3X { get; }

    public double Direction3Y { get; }

    public double Direction3Z { get; }


    public LinFloat64Vector3D Origin
    {
        get { return LinFloat64Vector3D.Create(OriginX, OriginY, OriginZ); }
    }

    public LinFloat64Vector3D Direction1
    {
        get { return LinFloat64Vector3D.Create(Direction1X, Direction1Y, Direction1Z); }
    }

    public LinFloat64Vector3D Direction2
    {
        get { return LinFloat64Vector3D.Create(Direction2X, Direction2Y, Direction2Z); }
    }

    public LinFloat64Vector3D Direction3
    {
        get { return LinFloat64Vector3D.Create(Direction3X, Direction3Y, Direction3Z); }
    }


    public Float64Line3D Ray1
    {
        get
        {
            return new Float64Line3D(
                OriginX, OriginY, OriginZ,
                Direction1X, Direction1Y, Direction1Z
            );
        }
    }

    public Float64Line3D Ray2
    {
        get
        {
            return new Float64Line3D(
                OriginX, OriginY, OriginZ,
                Direction2X, Direction2Y, Direction2Z
            );
        }
    }

    public Float64Line3D Ray3
    {
        get
        {
            return new Float64Line3D(
                OriginX, OriginY, OriginZ,
                Direction3X, Direction3Y, Direction3Z
            );
        }
    }


    public Float64PlanarBeam3D Beam12
    {
        get
        {
            return new Float64PlanarBeam3D(
                OriginX, OriginY, OriginZ,
                Direction1X, Direction1Y, Direction1Z,
                Direction2X, Direction2Y, Direction2Z
            );
        }
    }

    public Float64PlanarBeam3D Beam21
    {
        get
        {
            return new Float64PlanarBeam3D(
                OriginX, OriginY, OriginZ,
                Direction2X, Direction2Y, Direction2Z,
                Direction1X, Direction1Y, Direction1Z
            );
        }
    }

    public Float64PlanarBeam3D Beam23
    {
        get
        {
            return new Float64PlanarBeam3D(
                OriginX, OriginY, OriginZ,
                Direction2X, Direction2Y, Direction2Z,
                Direction3X, Direction3Y, Direction3Z
            );
        }
    }

    public Float64PlanarBeam3D Beam32
    {
        get
        {
            return new Float64PlanarBeam3D(
                OriginX, OriginY, OriginZ,
                Direction3X, Direction3Y, Direction3Z,
                Direction2X, Direction2Y, Direction2Z
            );
        }
    }

    public Float64PlanarBeam3D Beam31
    {
        get
        {
            return new Float64PlanarBeam3D(
                OriginX, OriginY, OriginZ,
                Direction3X, Direction3Y, Direction3Z,
                Direction1X, Direction1Y, Direction1Z
            );
        }
    }

    public Float64PlanarBeam3D Beam13
    {
        get
        {
            return new Float64PlanarBeam3D(
                OriginX, OriginY, OriginZ,
                Direction1X, Direction1Y, Direction1Z,
                Direction3X, Direction3Y, Direction3Z
            );
        }
    }


    public LinFloat64Vector3D Normal12
    {
        get { return Direction1.VectorCross(Direction2); }
    }

    public LinFloat64Vector3D UnitNormal12
    {
        get { return Direction1.VectorUnitCross(Direction2); }
    }

    public LinFloat64Vector3D Normal21
    {
        get { return Direction2.VectorCross(Direction1); }
    }

    public LinFloat64Vector3D UnitNormal21
    {
        get { return Direction2.VectorUnitCross(Direction1); }
    }


    public Float64Line3D GetNormalLine12()
    {
        return new Float64Line3D(Origin, Normal12);
    }

    public Float64Line3D GetUnitNormalLine12()
    {
        return new Float64Line3D(Origin, UnitNormal12);
    }


    internal Float64Beam3D(double pX, double pY, double pZ, double v1X, double v1Y, double v1Z, double v2X, double v2Y, double v2Z, double v3X, double v3Y, double v3Z)
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

        Direction3X = v3X;
        Direction3Y = v3Y;
        Direction3Z = v3Z;
    }


    public LinFloat64Vector3D GetPointAt(double t1, double t2, double t3)
    {
        return LinFloat64Vector3D.Create(OriginX + t1 * Direction1X + t2 * Direction2X + t3 * Direction3X,
            OriginY + t1 * Direction1Y + t2 * Direction2Y + t3 * Direction3Y,
            OriginZ + t1 * Direction1Z + t2 * Direction2Z + t3 * Direction3Z);
    }

    public IEnumerable<LinFloat64Vector3D> GetPointsAt(IEnumerable<LinFloat64Vector3D> tList)
    {
        return tList.Select(t => GetPointAt(t.X, t.Y, t.Z));
    }

    public Float64LineSegment3D GetLineSegmentAt(LinFloat64Vector3D t1, LinFloat64Vector3D t2)
    {
        var point1 = GetPointAt(t1.X, t1.Y, t1.Z);
        var point2 = GetPointAt(t2.X, t2.Y, t2.Z);

        return new Float64LineSegment3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z
        );
    }

    public Float64Line3D GetRayAt(LinFloat64Vector3D t1, LinFloat64Vector3D t2)
    {
        var point1 = GetPointAt(t1.X, t1.Y, t1.Z);
        var point2 = GetPointAt(t2.X, t2.Y, t2.Z);

        return new Float64Line3D(
            point1.X, point1.Y, point1.Z,
            point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z
        );
    }

    public Float64Triangle3D GetTriangleAt(LinFloat64Vector3D t1, LinFloat64Vector3D t2, LinFloat64Vector3D t3)
    {
        var point1 = GetPointAt(t1.X, t1.Y, t1.Z);
        var point2 = GetPointAt(t2.X, t2.Y, t2.Z);
        var point3 = GetPointAt(t3.X, t3.Y, t3.Z);

        return new Float64Triangle3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z,
            point3.X, point3.Y, point3.Z
        );
    }

    public Tetrahedron3D ToTetrahedron()
    {
        return new Tetrahedron3D(
            OriginX, OriginY, OriginZ,
            OriginX + Direction1X, OriginY + Direction1Y, OriginZ + Direction1Z,
            OriginX + Direction2X, OriginY + Direction2Y, OriginZ + Direction2Z,
            OriginX + Direction3X, OriginY + Direction3Y, OriginZ + Direction3Z
        );
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}