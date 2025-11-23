using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D;

public struct AABB3
{
    public double MinX, MinY, MinZ, MaxX, MaxY, MaxZ;
    public AABB3(double minx, double miny, double minz, double maxx, double maxy, double maxz)
    {
        MinX = minx; MinY = miny; MinZ = minz; MaxX = maxx; MaxY = maxy; MaxZ = maxz;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(in AABB3 other)
    {
        if (MaxX < other.MinX || other.MaxX < MinX) return false;
        if (MaxY < other.MinY || other.MaxY < MinY) return false;
        if (MaxZ < other.MinZ || other.MaxZ < MinZ) return false;
        return true;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Expand(double pad)
    {
        MinX -= pad; MinY -= pad; MinZ -= pad;
        MaxX += pad; MaxY += pad; MaxZ += pad;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (double cx, double cy, double cz) Center()
        => ((MinX + MaxX) * 0.5, (MinY + MaxY) * 0.5, (MinZ + MaxZ) * 0.5);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AABB3 FromSegment(in Pair<LinFloat64Vector3D> s)
    {
        var minx = Math.Min(s.Item1.X, s.Item2.X);
        var miny = Math.Min(s.Item1.Y, s.Item2.Y);
        var minz = Math.Min(s.Item1.Z, s.Item2.Z);
        var maxx = Math.Max(s.Item1.X, s.Item2.X);
        var maxy = Math.Max(s.Item1.Y, s.Item2.Y);
        var maxz = Math.Max(s.Item1.Z, s.Item2.Z);
        return new AABB3(minx, miny, minz, maxx, maxy, maxz);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AABB3 FromTriangle(in Triplet<LinFloat64Vector3D> t)
    {
        var minx = Math.Min(t.Item1.X, Math.Min(t.Item2.X, t.Item3.X));
        var miny = Math.Min(t.Item1.Y, Math.Min(t.Item2.Y, t.Item3.Y));
        var minz = Math.Min(t.Item1.Z, Math.Min(t.Item2.Z, t.Item3.Z));
        var maxx = Math.Max(t.Item1.X, Math.Max(t.Item2.X, t.Item3.X));
        var maxy = Math.Max(t.Item1.Y, Math.Max(t.Item2.Y, t.Item3.Y));
        var maxz = Math.Max(t.Item1.Z, Math.Max(t.Item2.Z, t.Item3.Z));
        return new AABB3(minx, miny, minz, maxx, maxy, maxz);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DistanceSquaredToPoint(double x, double y, double z)
    {
        var dx = 0.0;
        if (x < MinX) dx = MinX - x;
        else if (x > MaxX) dx = x - MaxX;
        var dy = 0.0;
        if (y < MinY) dy = MinY - y;
        else if (y > MaxY) dy = y - MaxY;
        var dz = 0.0;
        if (z < MinZ) dz = MinZ - z;
        else if (z > MaxZ) dz = z - MaxZ;
        return dx * dx + dy * dy + dz * dz;
    }
}