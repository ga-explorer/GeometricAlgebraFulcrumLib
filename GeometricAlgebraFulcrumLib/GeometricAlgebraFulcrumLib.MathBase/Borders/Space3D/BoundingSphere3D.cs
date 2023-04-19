using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D
{
    public sealed class BoundingSphere3D : IBorderSurface3D
    {
        public double Radius { get; }
    
        public double CenterX { get; }

        public double CenterY { get; }

        public double CenterZ { get; }

        public bool IsValid()
        {
            return !double.IsNaN(Radius) &&
                   !double.IsNaN(CenterX) &&
                   !double.IsNaN(CenterY);
        }

        public bool IntersectionTestsEnabled { get; set; } = true;

        public Float64Tuple3D Center
        {
            get { return new Float64Tuple3D(CenterX, CenterY, CenterZ); }
        }


        public BoundingSphere3D(double centerX, double centerY, double centerZ, double radius)
        {
            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;
            Radius = radius;
        }


        public IBorderSurface3D MapUsing(IAffineMap3D affineMap)
        {
            var s1 = affineMap.MapVector(Float64Tuple3D.E1).GetVectorNorm();
            var s2 = affineMap.MapVector(Float64Tuple3D.E2).GetVectorNorm();
            var s3 = affineMap.MapVector(Float64Tuple3D.E3).GetVectorNorm();

            var sMax = s1 > s2 ? s1 : s2;
            if (s3 > sMax) sMax = s3;

            var center = affineMap.MapPoint(Center);

            return new BoundingSphere3D(center.X, center.Y, center.Z, sMax * Radius);
        }

        public BoundingBox3D GetBoundingBox()
        {
            var point1 = new Float64Tuple3D(CenterX - Radius, CenterY - Radius, CenterZ - Radius);
            var point2 = new Float64Tuple3D(CenterX + Radius, CenterY + Radius, CenterZ + Radius);

            return BoundingBox3D.CreateFromPoints(point1, point2);
        }

        public MutableBoundingBox3D GetMutableBoundingBox()
        {
            var point1 = new Float64Tuple3D(CenterX - Radius, CenterY - Radius, CenterZ - Radius);
            var point2 = new Float64Tuple3D(CenterX + Radius, CenterY + Radius, CenterZ + Radius);

            return MutableBoundingBox3D.CreateFromPoints(point1, point2);
        }

        public bool TestLineSegmentIntersection(IFloat64Tuple3D point1, IFloat64Tuple3D point2)
        {
            var d1 = (point1.X - CenterX) * (point1.X - CenterX) +
                     (point1.Y - CenterY) * (point1.Y - CenterY) +
                     (point1.Z - CenterZ) * (point1.Z - CenterZ);

            var d2 = (point2.X - CenterX) * (point2.X - CenterX) +
                     (point2.Y - CenterY) * (point2.Y - CenterY) +
                     (point2.Z - CenterZ) * (point2.Z - CenterZ);

            var r = Radius * Radius;

            return (d1 <= r && d2 >= r) || (d2 <= r && d1 >= r);
        }
    }
}
