using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space4D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64
{
    public sealed record Float64BoundingBox4D :
        IFloat64BoundingBox<Float64BoundingBox4D, LinFloat64Vector4D>,
        IFloat64BoundingSurface4D
    {
        public static Float64BoundingBox4D EmptyInstance { get; }
            = new Float64BoundingBox4D();

        public static Float64BoundingBox4D Empty(int vSpaceDimensions = 4)
            => EmptyInstance;

        public static Float64BoundingBox4D Infinite { get; }
            = new Float64BoundingBox4D(
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity
            );

        // A few helpful infinite factories (mirror 3D pattern; add more as needed)
        public static Float64BoundingBox4D InfiniteX(double y1, double z1, double w1, double y2, double z2, double w2)
        {
            if (y1 > y2) (y1, y2) = (y2, y1);
            if (z1 > z2) (z1, z2) = (z2, z1);
            if (w1 > w2) (w1, w2) = (w2, w1);

            return new Float64BoundingBox4D(
                double.NegativeInfinity, y1, z1, w1,
                double.PositiveInfinity, y2, z2, w2
            );
        }

        public static Float64BoundingBox4D Around(double centerX, double centerY, double centerZ, double centerW, double deltaX, double deltaY, double deltaZ, double deltaW)
        {
            var minX = centerX - deltaX;
            var maxX = centerX + deltaX;
            var minY = centerY - deltaY;
            var maxY = centerY + deltaY;
            var minZ = centerZ - deltaZ;
            var maxZ = centerZ + deltaZ;
            var minW = centerW - deltaW;
            var maxW = centerW + deltaW;

            if (deltaX < 0) (maxX, minX) = (minX, maxX);
            if (deltaY < 0) (maxY, minY) = (minY, maxY);
            if (deltaZ < 0) (maxZ, minZ) = (minZ, maxZ);
            if (deltaW < 0) (maxW, minW) = (minW, maxW);

            return new Float64BoundingBox4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        public static Float64BoundingBox4D Containing(IQuad<double> point)
        {
            return new Float64BoundingBox4D(
                point.Item1, point.Item2, point.Item3, point.Item4,
                point.Item1, point.Item2, point.Item3, point.Item4
            );
        }

        public static Float64BoundingBox4D Containing(double p1X, double p1Y, double p1Z, double p1W, double p2X, double p2Y, double p2Z, double p2W)
        {
            double minX, minY, minZ, minW, maxX, maxY, maxZ, maxW;

            if (p1X <= p2X) { minX = p1X; maxX = p2X; } else { minX = p2X; maxX = p1X; }
            if (p1Y <= p2Y) { minY = p1Y; maxY = p2Y; } else { minY = p2Y; maxY = p1Y; }
            if (p1Z <= p2Z) { minZ = p1Z; maxZ = p2Z; } else { minZ = p2Z; maxZ = p1Z; }
            if (p1W <= p2W) { minW = p1W; maxW = p2W; } else { minW = p2W; maxW = p1W; }

            return new Float64BoundingBox4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        public static Float64BoundingBox4D Containing(params IQuad<double>[] pointsList)
        {
            var p1 = pointsList[0];

            var minX = p1.Item1;
            var minY = p1.Item2;
            var minZ = p1.Item3;
            var minW = p1.Item4;

            var maxX = p1.Item1;
            var maxY = p1.Item2;
            var maxZ = p1.Item3;
            var maxW = p1.Item4;

            foreach (var p in pointsList.Skip(1))
            {
                if (minX > p.Item1) minX = p.Item1;
                if (minY > p.Item2) minY = p.Item2;
                if (minZ > p.Item3) minZ = p.Item3;
                if (minW > p.Item4) minW = p.Item4;

                if (maxX < p.Item1) maxX = p.Item1;
                if (maxY < p.Item2) maxY = p.Item2;
                if (maxZ < p.Item3) maxZ = p.Item3;
                if (maxW < p.Item4) maxW = p.Item4;
            }

            return new Float64BoundingBox4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        public static Float64BoundingBox4D Containing(IEnumerable<IQuad<double>> pointsList)
        {
            double minX = 0, minY = 0, minZ = 0, minW = 0;
            double maxX = 0, maxY = 0, maxZ = 0, maxW = 0;

            var flag = false;
            foreach (var p in pointsList)
            {
                if (!flag)
                {
                    minX = p.Item1; minY = p.Item2; minZ = p.Item3; minW = p.Item4;
                    maxX = p.Item1; maxY = p.Item2; maxZ = p.Item3; maxW = p.Item4;
                    flag = true;
                    continue;
                }

                if (minX > p.Item1) minX = p.Item1;
                if (minY > p.Item2) minY = p.Item2;
                if (minZ > p.Item3) minZ = p.Item3;
                if (minW > p.Item4) minW = p.Item4;

                if (maxX < p.Item1) maxX = p.Item1;
                if (maxY < p.Item2) maxY = p.Item2;
                if (maxZ < p.Item3) maxZ = p.Item3;
                if (maxW < p.Item4) maxW = p.Item4;
            }

            return new Float64BoundingBox4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        public static Float64BoundingBox4D Containing(Float64BoundingBox4D b1, Float64BoundingBox4D b2)
        {
            return new Float64BoundingBox4D(
                Math.Min(b1.MinX, b2.MinX),
                Math.Min(b1.MinY, b2.MinY),
                Math.Min(b1.MinZ, b2.MinZ),
                Math.Min(b1.MinW, b2.MinW),
                Math.Max(b1.MaxX, b2.MaxX),
                Math.Max(b1.MaxY, b2.MaxY),
                Math.Max(b1.MaxZ, b2.MaxZ),
                Math.Max(b1.MaxW, b2.MaxW)
            );
        }

        public static Float64BoundingBox4D Containing(params Float64BoundingBox4D[] boxes)
        {
            var composer = new Float64BoundingBoxComposer4D();
            var flag = false;
            foreach (var b in boxes)
            {
                if (!flag) { composer = b.GetBoundingBoxComposer(); flag = true; continue; }
                composer.ExpandToInclude(b);
            }
            return composer.GetBoundingBox();
        }

        public static Float64BoundingBox4D ContainingIntersection(Float64BoundingBox4D b1, Float64BoundingBox4D b2)
        {
            return new Float64BoundingBox4D(
                Math.Max(b1.MinX, b2.MinX),
                Math.Max(b1.MinY, b2.MinY),
                Math.Max(b1.MinZ, b2.MinZ),
                Math.Max(b1.MinW, b2.MinW),
                Math.Min(b1.MaxX, b2.MaxX),
                Math.Min(b1.MaxY, b2.MaxY),
                Math.Min(b1.MaxZ, b2.MaxZ),
                Math.Min(b1.MaxW, b2.MaxW)
            );
        }

        public static Float64BoundingBox4D Containing(IEnumerable<Float64BoundingBox4D> boxes)
        {
            double minX = double.PositiveInfinity, minY = double.PositiveInfinity, minZ = double.PositiveInfinity, minW = double.PositiveInfinity;
            double maxX = double.NegativeInfinity, maxY = double.NegativeInfinity, maxZ = double.NegativeInfinity, maxW = double.NegativeInfinity;

            foreach (var b in boxes)
            {
                if (b.IsEmpty) continue;
                minX = Math.Min(minX, b.MinX); minY = Math.Min(minY, b.MinY);
                minZ = Math.Min(minZ, b.MinZ); minW = Math.Min(minW, b.MinW);
                maxX = Math.Max(maxX, b.MaxX); maxY = Math.Max(maxY, b.MaxY);
                maxZ = Math.Max(maxZ, b.MaxZ); maxW = Math.Max(maxW, b.MaxW);
            }

            return new Float64BoundingBox4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        
        public int VSpaceDimensions 
            => 4;

        public bool IsEmpty { get; }

        public LinFloat64Vector4D MinPoint { get; }

        public LinFloat64Vector4D MaxPoint { get; }

        public LinFloat64Vector4D Center { get; }

        public LinFloat64Vector4D Size { get; }

        public LinFloat64Vector4D HalfSize
            => LinFloat64Vector4D.Create(
                (MaxX - MinX) / 2,
                (MaxY - MinY) / 2,
                (MaxZ - MinZ) / 2,
                (MaxW - MinW) / 2
            );


        public double MinX => MinPoint.X;
        
        public double MinY => MinPoint.Y;
        
        public double MinZ => MinPoint.Z;
        
        public double MinW => MinPoint.W;


        public double MaxX => MaxPoint.X;
        
        public double MaxY => MaxPoint.Y;
        
        public double MaxZ => MaxPoint.Z;
        
        public double MaxW => MaxPoint.W;


        public double CenterX => Center.X;
        
        public double CenterY => Center.Y;
        
        public double CenterZ => Center.Z;
        
        public double CenterW => Center.W;


        public double SizeX => Size.X;
        
        public double SizeY => Size.Y;
        
        public double SizeZ => Size.Z;
        
        public double SizeW => Size.W;


        public Float64ScalarRange RangeX => Float64ScalarRange.Create(MinX, MaxX);
        
        public Float64ScalarRange RangeY => Float64ScalarRange.Create(MinY, MaxY);
        
        public Float64ScalarRange RangeZ => Float64ScalarRange.Create(MinZ, MaxZ);
        
        public Float64ScalarRange RangeW => Float64ScalarRange.Create(MinW, MaxW);


        public bool IntersectionTestsEnabled { get; set; } = true;

        public double DiagonalLength
        {
            get
            {
                var dx = MaxX - MinX;
                var dy = MaxY - MinY;
                var dz = MaxZ - MinZ;
                var dw = MaxW - MinW;
                return Math.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
            }
        }

        public double HyperVolume =>
            (MaxX - MinX) * (MaxY - MinY) * (MaxZ - MinZ) * (MaxW - MinW);

        
        private Float64BoundingBox4D()
        {
            IsEmpty = true;

            MinPoint = LinFloat64Vector4D.Zero;
            MaxPoint = LinFloat64Vector4D.Zero;
            Center = LinFloat64Vector4D.Zero;
            Size = LinFloat64Vector4D.Zero;

            Debug.Assert(IsValid());
        }

        private Float64BoundingBox4D(double minX, double minY, double minZ, double minW, double maxX, double maxY, double maxZ, double maxW)
        {
            IsEmpty = false;

            MinPoint = LinFloat64Vector4D.Create(minX, minY, minZ, minW);
            MaxPoint = LinFloat64Vector4D.Create(maxX, maxY, maxZ, maxW);
            Center = LinFloat64Vector4D.Create((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2, (maxW + minW) / 2);
            Size = LinFloat64Vector4D.Create(maxX - minX, maxY - minY, maxZ - minZ, maxW - minW);

            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            return MinPoint.IsValid() && 
                   MinPoint.IsValid() &&
                   MinX <= MaxX && 
                   MinY <= MaxY && 
                   MinZ <= MaxZ && 
                   MinW <= MaxW;
        }

        public Float64BoundingBox4D GetBoundingBox()
        {
            return this;
        }

        public Float64BoundingBoxComposer4D GetBoundingBoxComposer() => new Float64BoundingBoxComposer4D(this);
        
        public IFloat64BoundingSurface4D MapBoundingSurfaceUsing(IFloat64AffineMap4D affineMap)
        {
            // Map the four axis side vectors
            var oldSide1000 = LinFloat64Vector4D.Create(MaxX - MinX, 0, 0, 0);
            var oldSide0100 = LinFloat64Vector4D.Create(0, MaxY - MinY, 0, 0);
            var oldSide0010 = LinFloat64Vector4D.Create(0, 0, MaxZ - MinZ, 0);
            var oldSide0001 = LinFloat64Vector4D.Create(0, 0, 0, MaxW - MinW);

            var newSide1000 = affineMap.MapVector(oldSide1000);
            var newSide0100 = affineMap.MapVector(oldSide0100);
            var newSide0010 = affineMap.MapVector(oldSide0010);
            var newSide0001 = affineMap.MapVector(oldSide0001);

            var newCorner0000 = affineMap.MapPoint(MinPoint);

            // Generate all 16 corners (binary count for four axes)
            var corners = new List<LinFloat64Vector4D>(16);
            for (var b = 0; b < 16; b++)
            {
                var corner = newCorner0000;
                if ((b & 1) != 0) corner = corner + newSide1000;
                if ((b & 2) != 0) corner = corner + newSide0100;
                if ((b & 4) != 0) corner = corner + newSide0010;
                if ((b & 8) != 0) corner = corner + newSide0001;
                corners.Add(corner);
            }

            return Containing(corners.Select(c => (IQuad<double>)c));
        }

        // Distance to point (euclidean)
        public double DistanceToPoint(LinFloat64Vector4D point)
        {
            return Math.Sqrt(DistanceSquaredToPoint(point));
        }

        public bool IntersectsRay(IFloat64Ray<LinFloat64Vector4D> ray)
        {
            // slab test generalized to 4D
            var tMin = double.NegativeInfinity;
            var tMax = double.PositiveInfinity;

            for (var d = 0; d < 4; d++)
            {
                var invD = 1.0 / ray.Direction[d];
                var t0 = (MinPoint[d] - ray.Origin[d]) * invD;
                var t1 = (MaxPoint[d] - ray.Origin[d]) * invD;
                
                if (t0 > t1) (t0, t1) = (t1, t0);
                
                tMin = Math.Max(tMin, t0);
                tMax = Math.Min(tMax, t1);
                
                if (tMax < tMin) return false;
            }

            return true;
        }

        public double DistanceSquaredToPoint(LinFloat64Vector4D point)
        {
            var dx = Math.Max(0, Math.Max(MinX - point.X, point.X - MaxX));
            var dy = Math.Max(0, Math.Max(MinY - point.Y, point.Y - MaxY));
            var dz = Math.Max(0, Math.Max(MinZ - point.Z, point.Z - MaxZ));
            var dw = Math.Max(0, Math.Max(MinW - point.W, point.W - MaxW));

            return dx * dx + dy * dy + dz * dz + dw * dw;
        }

        public bool ContainsPoint(IQuad<double> point)
        {
            return point.Item1 >= MinX && point.Item1 <= MaxX &&
                   point.Item2 >= MinY && point.Item2 <= MaxY &&
                   point.Item3 >= MinZ && point.Item3 <= MaxZ &&
                   point.Item4 >= MinW && point.Item4 <= MaxW;
        }

        public bool ContainsPoint(LinFloat64Vector4D point)
        {
            return point.X >= MinX && point.X <= MaxX &&
                   point.Y >= MinY && point.Y <= MaxY &&
                   point.Z >= MinZ && point.Z <= MaxZ &&
                   point.W >= MinW && point.W <= MaxW;
        }

        public bool ContainsPoint(double x, double y, double z, double w, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (useMargins)
                return x >= MinX - tolerance && x <= MaxX + tolerance &&
                       y >= MinY - tolerance && y <= MaxY + tolerance &&
                       z >= MinZ - tolerance && z <= MaxZ + tolerance &&
                       w >= MinW - tolerance && w <= MaxW + tolerance;

            return x >= MinX && x <= MaxX &&
                   y >= MinY && y <= MaxY &&
                   z >= MinZ && z <= MaxZ &&
                   w >= MinW && w <= MaxW;
        }

        public bool ContainsBox(Float64BoundingBox4D box, bool useMargins = false, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (useMargins)
                return box.MinX >= MinX - tolerance && box.MaxX <= MaxX + tolerance &&
                       box.MinY >= MinY - tolerance && box.MaxY <= MaxY + tolerance &&
                       box.MinZ >= MinZ - tolerance && box.MaxZ <= MaxZ + tolerance &&
                       box.MinW >= MinW - tolerance && box.MaxW <= MaxW + tolerance;

            return box.MinX >= MinX && box.MaxX <= MaxX &&
                   box.MinY >= MinY && box.MaxY <= MaxY &&
                   box.MinZ >= MinZ && box.MaxZ <= MaxZ &&
                   box.MinW >= MinW && box.MaxW <= MaxW;
        }

        public bool IntersectsBox(Float64BoundingBox4D box)
        {
            return !(MaxX < box.MinX || MinX > box.MaxX ||
                     MaxY < box.MinY || MinY > box.MaxY ||
                     MaxZ < box.MinZ || MinZ > box.MaxZ ||
                     MaxW < box.MinW || MinW > box.MaxW);
        }
        
        public bool TryIntersectRay(IFloat64Ray<LinFloat64Vector4D> ray, out double tMin, out double tMax, double tolerance = Float64Utils.DefaultTolerance, double geometricTolerance = 1e-9)
        {
            tMin = double.NegativeInfinity;
            tMax = double.PositiveInfinity;

            var o = ray.Origin;
            var d = ray.Direction;

            double[] minVals = [MinX, MinY, MinZ, MinW];
            double[] maxVals = [MaxX, MaxY, MaxZ, MaxW];
            double[] oVals = [o.X, o.Y, o.Z, o.W];
            double[] dVals = [d.X, d.Y, d.Z, d.W];

            for (var i = 0; i < 4; i++)
            {
                if (Math.Abs(dVals[i]) < tolerance)
                {
                    if (oVals[i] < minVals[i] - geometricTolerance || oVals[i] > maxVals[i] + geometricTolerance) 
                        return false;
                }
                else
                {
                    var invD = 1.0 / dVals[i];
                    var t0 = (minVals[i] - oVals[i]) * invD;
                    var t1 = (maxVals[i] - oVals[i]) * invD;
                    
                    if (t0 > t1) (t0, t1) = (t1, t0);

                    tMin = Math.Max(tMin, t0);
                    tMax = Math.Min(tMax, t1);

                    if (tMax < tMin) 
                        return false;
                }
            }

            return true;
        }

        // Intersect line segment using the slab method (adapted from 3D routine)
        public bool IntersectsLineSegment(IPair<IQuad<double>> startEndPair)
        {
            return IntersectsLineSegment(startEndPair.Item1, startEndPair.Item2);
        }

        public bool IntersectsLineSegment(IQuad<double> start, IQuad<double> end, double tolerance = Float64Utils.DefaultTolerance)
        {
            // Quick rejection: both points strictly on same side of any hyper-face
            if ((start.Item1 < MinX && end.Item1 < MinX) || (start.Item1 > MaxX && end.Item1 > MaxX) ||
                (start.Item2 < MinY && end.Item2 < MinY) || (start.Item2 > MaxY && end.Item2 > MaxY) ||
                (start.Item3 < MinZ && end.Item3 < MinZ) || (start.Item3 > MaxZ && end.Item3 > MaxZ) ||
                (start.Item4 < MinW && end.Item4 < MinW) || (start.Item4 > MaxW && end.Item4 > MaxW))
                return false;

            if (ContainsPoint(start) || ContainsPoint(end)) return true;

            var tMin = 0.0;
            var tMax = 1.0;

            var dirX = end.Item1 - start.Item1;
            var dirY = end.Item2 - start.Item2;
            var dirZ = end.Item3 - start.Item3;
            var dirW = end.Item4 - start.Item4;

            if (!UpdateSlabIntersection(start.Item1, dirX, MinX, MaxX, ref tMin, ref tMax)) return false;
            if (!UpdateSlabIntersection(start.Item2, dirY, MinY, MaxY, ref tMin, ref tMax)) return false;
            if (!UpdateSlabIntersection(start.Item3, dirZ, MinZ, MaxZ, ref tMin, ref tMax)) return false;
            if (!UpdateSlabIntersection(start.Item4, dirW, MinW, MaxW, ref tMin, ref tMax)) return false;

            return true;

            bool UpdateSlabIntersection(double axisOrigin, double axisDirection, double minBound, double maxBound, ref double tMin, ref double tMax)
            {
                if (Math.Abs(axisDirection) < tolerance)
                {
                    return !(axisOrigin < minBound) && !(axisOrigin > maxBound);
                }

                var t1 = (minBound - axisOrigin) / axisDirection;
                var t2 = (maxBound - axisOrigin) / axisDirection;
                if (t1 > t2) (t1, t2) = (t2, t1);

                if (t1 > tMin) tMin = t1;
                if (t2 < tMax) tMax = t2;

                return tMin <= tMax;
            }
        }

        // Get a parametric point inside box using tx,ty,tz,tw in [0,1]
        public LinFloat64Vector4D GetPointAt(double tx, double ty, double tz, double tw)
        {
            return LinFloat64Vector4D.Create(
                (1.0 - tx) * MinX + tx * MaxX,
                (1.0 - ty) * MinY + ty * MaxY,
                (1.0 - tz) * MinZ + tz * MaxZ,
                (1.0 - tw) * MinW + tw * MaxW
            );
        }

        // Get corner by index 0..15 (wraps using Mod(16) if provided)
        public LinFloat64Vector4D GetCorner(int cornerIndex)
        {
            cornerIndex = cornerIndex.Mod(16);

            // Map binary digits to min/max choices
            var x = ((cornerIndex & 1) != 0) ? MaxX : MinX;
            var y = ((cornerIndex & 2) != 0) ? MaxY : MinY;
            var z = ((cornerIndex & 4) != 0) ? MaxZ : MinZ;
            var w = ((cornerIndex & 8) != 0) ? MaxW : MinW;

            return LinFloat64Vector4D.Create(x, y, z, w);
        }

        public double GetSideMinValue(int axisIndex)
        {
            axisIndex = axisIndex.Mod(4);
            return axisIndex switch
            {
                0 => MinX,
                1 => MinY,
                2 => MinZ,
                _ => MinW
            };
        }

        public double GetSideMaxValue(int axisIndex)
        {
            axisIndex = axisIndex.Mod(4);
            return axisIndex switch
            {
                0 => MaxX,
                1 => MaxY,
                2 => MaxZ,
                _ => MaxW
            };
        }

        public double GetSideMidValue(int axisIndex)
        {
            axisIndex = axisIndex.Mod(4);
            return axisIndex switch
            {
                0 => 0.5 * (MinX + MaxX),
                1 => 0.5 * (MinY + MaxY),
                2 => 0.5 * (MinZ + MaxZ),
                _ => 0.5 * (MinW + MaxW)
            };
        }

        public double GetSideLength(int axisIndex)
        {
            axisIndex = axisIndex.Mod(4);
            return axisIndex switch
            {
                0 => MaxX - MinX,
                1 => MaxY - MinY,
                2 => MaxZ - MinZ,
                _ => MaxW - MinW
            };
        }

        public Float64ScalarRange GetSideRange(int axisIndex)
        {
            axisIndex = axisIndex.Mod(4);
            return axisIndex switch
            {
                0 => Float64ScalarRange.Create(MinX, MaxX),
                1 => Float64ScalarRange.Create(MinY, MaxY),
                2 => Float64ScalarRange.Create(MinZ, MaxZ),
                _ => Float64ScalarRange.Create(MinW, MaxW)
            };
        }

        public IEnumerable<double> GetComponents(bool byCorner = true)
        {
            if (byCorner)
            {
                yield return MinX;
                yield return MinY;
                yield return MinZ;
                yield return MinW;

                yield return MaxX;
                yield return MaxY;
                yield return MaxZ;
                yield return MaxW;
            }
            else
            {
                yield return MinX; yield return MaxX;
                yield return MinY; yield return MaxY;
                yield return MinZ; yield return MaxZ;
                yield return MinW; yield return MaxW;
            }
        }

        
        public override string ToString()
        {
            return new StringBuilder()
                .Append("AABB: ")
                .Append(MinPoint.ToTupleString())
                .Append(" -> ")
                .Append(MaxPoint.ToTupleString())
                .ToString();
        }
    }
}
