using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64
{
    /// <summary>
    /// Immutable axis-aligned bounding box in arbitrary vSpaceDimensions.
    /// Uses LinFloat64Vector for endpoints.
    /// </summary>
    public sealed record Float64BoundingBox :
        IFloat64BoundingBox<Float64BoundingBox, LinFloat64Vector>,
        IFloat64BoundingSurface
    {
        public static Float64BoundingBox Empty(int vSpaveDimensions)
        {
            return new Float64BoundingBox(vSpaveDimensions);
        }

        public static Float64BoundingBox Infinite(int vSpaceDimensions)
        {
            var min = Enumerable.Repeat(double.NegativeInfinity, vSpaceDimensions).ToArray();
            var max = Enumerable.Repeat(double.PositiveInfinity, vSpaceDimensions).ToArray();
            
            return new Float64BoundingBox(
                LinFloat64Vector.Create(min),
                LinFloat64Vector.Create(max)
            );
        }

        public static Float64BoundingBox Around(LinFloat64Vector center, LinFloat64Vector delta)
        {
            var vSpaceDimensions = Math.Max(center.VSpaceDimensions, delta.VSpaceDimensions);

            var min = new double[vSpaceDimensions];
            var max = new double[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
            {
                var c = center[i];
                var d = delta[i];

                min[i] = c - d;
                max[i] = c + d;

                if (min[i] > max[i]) 
                    (min[i], max[i]) = (max[i], min[i]);
            }

            return new Float64BoundingBox(
                LinFloat64Vector.Create(min), 
                LinFloat64Vector.Create(max)
            );
        }

        public static Float64BoundingBox Containing(params LinFloat64Vector[] points)
        {
            Debug.Assert(points.Length > 0);

            var vSpaceDimensions = points.Max(point => point.VSpaceDimensions);
            var min = points[0].ToScalarArray(vSpaceDimensions);
            var max = points[0].ToScalarArray(vSpaceDimensions);

            foreach (var point in points.Skip(1))
            {
                for (var i = 0; i < vSpaceDimensions; i++)
                {
                    var s = point[i];

                    if (s < min[i]) min[i] = s;
                    if (s > max[i]) max[i] = s;
                }
            }

            return new Float64BoundingBox(
                LinFloat64Vector.Create(min),
                LinFloat64Vector.Create(max)
            );
        }

        public static Float64BoundingBox Containing(IEnumerable<Float64BoundingBox> boxes)
        {
            var list = boxes.ToList();
            if (list.Count == 0) 
                throw new ArgumentException("No boxes to contain");

            var composer = new Float64BoundingBoxComposer(list[0]);
            foreach (var b in list.Skip(1))
                composer.ExpandToInclude(b);

            return composer.GetBoundingBox();
        }


        public int VSpaceDimensions { get; }
        
        public bool IsEmpty { get; }

        public LinFloat64Vector MinPoint { get; }

        public LinFloat64Vector MaxPoint { get; }

        public LinFloat64Vector Center { get; }

        public LinFloat64Vector Size { get; }

        public LinFloat64Vector HalfSize 
            => Size / 2.0;

        public double DiagonalLength 
            => Size.L2Norm();
        
        public double HyperVolume
        {
            get
            {
                var vol = 1.0;
                for (var i = 0; i < VSpaceDimensions; i++)
                    vol *= MaxPoint[i] - MinPoint[i];
                return vol;
            }
        }
        
        public bool IntersectionTestsEnabled { get; set; } = true;


        private Float64BoundingBox(int vSpaceDimensions)
        {
            if (vSpaceDimensions < 0)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            IsEmpty = true;
            VSpaceDimensions = vSpaceDimensions;

            MinPoint = LinFloat64Vector.Zero;
            MaxPoint = LinFloat64Vector.Zero;
            Center = LinFloat64Vector.Zero;
            Size = LinFloat64Vector.Zero;
        }

        private Float64BoundingBox(LinFloat64Vector minPoint, LinFloat64Vector maxPoint)
        {
            IsEmpty = false;
            VSpaceDimensions = Math.Max(
                minPoint.VSpaceDimensions, 
                maxPoint.VSpaceDimensions
            );

            MinPoint = minPoint;
            MaxPoint = maxPoint;

            var center = new double[VSpaceDimensions];
            var size = new double[VSpaceDimensions];
            
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                var minValue = minPoint[i];
                var maxValue = maxPoint[i];

                center[i] = 0.5 * (maxValue + minValue);
                size[i] = maxValue - minValue;
            }

            Center = center.CreateLinVector();
            Size = size.CreateLinVector();
        }
        

        public bool IsValid()
        {
            return MinPoint.IsValid() && 
                   MaxPoint.IsValid() && 
                   VSpaceDimensions.GetRange().All(i => MinPoint[i] <= MaxPoint[i]);
        }

        public bool ContainsPoint(LinFloat64Vector point, double tolerance)
        {
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                var s = point[i];

                if (s < MinPoint[i] - tolerance || s > MaxPoint[i] + tolerance)
                    return false;
            }
            
            return true;
        }

        public bool ContainsBox(Float64BoundingBox box, double tolerance = 0.0)
        {
            Debug.Assert(box.VSpaceDimensions == VSpaceDimensions);

            for (var i = 0; i < VSpaceDimensions; i++)
            {
                if (box.MinPoint[i] < MinPoint[i] - tolerance ||
                    box.MaxPoint[i] > MaxPoint[i] + tolerance)
                    return false;
            }

            return true;
        }

        public bool ContainsPoint(LinFloat64Vector point)
        {
            if (point.VSpaceDimensions > VSpaceDimensions)
                return false;

            for (var i = 0; i < VSpaceDimensions; i++)
            {
                var s = point[i];

                if (s < MinPoint[i] || s > MaxPoint[i])
                    return false;
            }

            return true;
        }

        public bool IntersectsBox(Float64BoundingBox box)
        {
            Debug.Assert(box.VSpaceDimensions == VSpaceDimensions);

            for (var i = 0; i < VSpaceDimensions; i++)
                if (MaxPoint[i] < box.MinPoint[i] || MinPoint[i] > box.MaxPoint[i])
                    return false;

            return true;
        }

        public bool IntersectsRay(IFloat64Ray<LinFloat64Vector> ray)
        {
            Debug.Assert(ray.Origin.VSpaceDimensions <= VSpaceDimensions); 
            Debug.Assert(ray.Direction.VSpaceDimensions <= VSpaceDimensions);

            var tMin = double.NegativeInfinity;
            var tMax = double.PositiveInfinity;

            for (var i = 0; i < VSpaceDimensions; i++)
            {
                var origin = ray.Origin[i];
                var invD = 1.0 / ray.Direction[i];
                var t0 = (MinPoint[i] - origin) * invD;
                var t1 = (MaxPoint[i] - origin) * invD;

                if (t0 > t1)
                    (t0, t1) = (t1, t0);

                tMin = Math.Max(tMin, t0);
                tMax = Math.Min(tMax, t1);

                if (tMax < tMin)
                    return false;
            }

            return true;
        }
        
        public bool TryIntersectRay(IFloat64Ray<LinFloat64Vector> ray, out double tMin, out double tMax, double tolerance = Float64Utils.DefaultTolerance, double geometricTolerance = 1e-9)
        {
            tMin = double.NegativeInfinity;
            tMax = double.PositiveInfinity;

            var origin = ray.Origin;
            var dir = ray.Direction;

            for (var i = 0; i < VSpaceDimensions; i++)
            {
                var o = origin[i];
                var d = dir[i];
                var min = MinPoint[i];
                var max = MaxPoint[i];

                if (Math.Abs(d) < tolerance)
                {
                    if (o < min - geometricTolerance || o > max + geometricTolerance) 
                        return false;
                }
                else
                {
                    var invD = 1.0 / d;
                    var t0 = (min - o) * invD;
                    var t1 = (max - o) * invD;

                    if (t0 > t1) (t0, t1) = (t1, t0);

                    tMin = Math.Max(tMin, t0);
                    tMax = Math.Min(tMax, t1);

                    if (tMax < tMin) 
                        return false;
                }
            }

            return true;
        }

        public double DistanceSquaredToPoint(LinFloat64Vector point)
        {
            Debug.Assert(point.VSpaceDimensions <= VSpaceDimensions); 

            var sum = 0d;
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                double diff = 0;

                var s = point[i];
                var sMin = MinPoint[i];
                
                if (s < sMin)
                {
                    diff = sMin - s;
                }
                else
                {
                    var sMax = MaxPoint[i];

                    if (s > sMax) 
                        diff = s - sMax;
                }

                sum += diff * diff;
            }

            return sum;
        }

        /// <summary>
        /// Return one of the 2^Dimension corners.
        /// </summary>
        public LinFloat64Vector GetCorner(int index)
        {
            var coords = new double[VSpaceDimensions];

            for (var i = 0; i < VSpaceDimensions; i++)
                coords[i] = ((index >> i) & 1) == 0 
                    ? MinPoint[i] 
                    : MaxPoint[i];
            
            return LinFloat64Vector.Create(coords);
        }

        public IEnumerable<LinFloat64Vector> GetAllCorners()
        {
            if (VSpaceDimensions > 15)
                throw new InvalidOperationException();

            var count = 1 << VSpaceDimensions;

            for (var i = 0; i < count; i++)
                yield return GetCorner(i);
        }

        public Float64BoundingBoxComposer GetBoundingBoxComposer()
        {
            return new Float64BoundingBoxComposer(this);
        }

        public IFloat64BoundingSurface MapBoundingSurfaceUsing(IFloat64AffineMap affineMap)
        {
            throw new NotImplementedException();
        }

        public Float64BoundingBox GetBoundingBox()
        {
            return this;
        }
    }
}
