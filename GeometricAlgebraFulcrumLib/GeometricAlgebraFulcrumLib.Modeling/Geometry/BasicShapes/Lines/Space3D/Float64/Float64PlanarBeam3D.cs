using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Polytopes.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

/// <summary>
/// This represents a planar beam in 3D space
/// </summary>
public sealed class Float64PlanarBeam3D
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


    public Float64Line3D Ray1
    {
        get { return new Float64Line3D(OriginX, OriginY, OriginZ, Direction1X, Direction1Y, Direction1Z); }
    }

    public Float64Line3D Ray2
    {
        get { return new Float64Line3D(OriginX, OriginY, OriginZ, Direction2X, Direction2Y, Direction2Z); }
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

    public Float64Line3D GetNormalLine21()
    {
        return new Float64Line3D(Origin, Normal21);
    }

    public Float64Line3D GetUnitNormalLine21()
    {
        return new Float64Line3D(Origin, UnitNormal21);
    }


    internal Float64PlanarBeam3D(double pX, double pY, double pZ, double v1X, double v1Y, double v1Z, double v2X, double v2Y, double v2Z)
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


    public LinFloat64Vector3D GetPoint(double t1, double t2)
    {
        return LinFloat64Vector3D.Create(OriginX + t1 * Direction1X + t2 * Direction2X,
            OriginY + t1 * Direction1Y + t2 * Direction2Y,
            OriginZ + t1 * Direction1Z + t2 * Direction2Z);
    }

    public IEnumerable<LinFloat64Vector3D> GetPoints(IEnumerable<LinFloat64Vector2D> tList)
    {
        return tList.Select(t => GetPoint(t.X, t.Y));
    }

    public Float64LineSegment3D GetLineSegment(LinFloat64Vector2D t1, LinFloat64Vector2D t2)
    {
        var point1 = GetPoint(t1.X, t1.Y);
        var point2 = GetPoint(t2.X, t2.Y);

        return new Float64LineSegment3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z
        );
    }

    public Float64Line3D GetRay(LinFloat64Vector2D t1, LinFloat64Vector2D t2)
    {
        var point1 = GetPoint(t1.X, t1.Y);
        var point2 = GetPoint(t2.X, t2.Y);

        return new Float64Line3D(
            point1.X, point1.Y, point1.Z,
            point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z
        );
    }

    public Float64Triangle3D GetTriangle()
    {
        return new Float64Triangle3D(
            OriginX, OriginY, OriginZ,
            OriginX + Direction1X, OriginY + Direction1Y, OriginZ + Direction1Z,
            OriginX + Direction2X, OriginY + Direction2Y, OriginZ + Direction2Z
        );
    }

    public Float64Triangle3D GetTriangle(LinFloat64Vector2D t1, LinFloat64Vector2D t2, LinFloat64Vector2D t3)
    {
        var point1 = GetPoint(t1.X, t1.Y);
        var point2 = GetPoint(t2.X, t2.Y);
        var point3 = GetPoint(t3.X, t3.Y);

        return new Float64Triangle3D(
            point1.X, point1.Y, point1.Z,
            point2.X, point2.Y, point2.Z,
            point3.X, point3.Y, point3.Z
        );
    }

    public Tetrahedron3D GetTetrahedron(LinFloat64Vector3D point4)
    {
        return new Tetrahedron3D(
            OriginX, OriginY, OriginZ,
            OriginX + Direction1X, OriginY + Direction1Y, OriginZ + Direction1Z,
            OriginX + Direction2X, OriginY + Direction2Y, OriginZ + Direction2Z,
            point4.X, point4.Y, point4.Z
        );
    }
}