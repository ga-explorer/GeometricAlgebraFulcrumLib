using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64
{
    /// <summary>
    /// Mutable composer for 4D axis-aligned bounding boxes.
    /// Mirrors the 3D composer API.
    /// </summary>
    public sealed class Float64BoundingBoxComposer4D
    {
        public static Float64BoundingBoxComposer4D CreateInfinite()
        {
            return new Float64BoundingBoxComposer4D(
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.NegativeInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity,
                double.PositiveInfinity
            );
        }

        public static Float64BoundingBoxComposer4D CreateFromPoint(double x, double y, double z, double w)
        {
            return new Float64BoundingBoxComposer4D(x, y, z, w);
        }

        public static Float64BoundingBoxComposer4D CreateFromPoints(LinFloat64Vector4D p1, LinFloat64Vector4D p2)
        {
            double minX, minY, minZ, minW, maxX, maxY, maxZ, maxW;

            if (p1.X <= p2.X) { minX = p1.X; maxX = p2.X; } else { minX = p2.X; maxX = p1.X; }
            if (p1.Y <= p2.Y) { minY = p1.Y; maxY = p2.Y; } else { minY = p2.Y; maxY = p1.Y; }
            if (p1.Z <= p2.Z) { minZ = p1.Z; maxZ = p2.Z; } else { minZ = p2.Z; maxZ = p1.Z; }
            if (p1.W <= p2.W) { minW = p1.W; maxW = p2.W; } else { minW = p2.W; maxW = p1.W; }

            return new Float64BoundingBoxComposer4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        public static Float64BoundingBoxComposer4D CreateFromPoints(params LinFloat64Vector4D[] points)
        {
            var p0 = points[0];
            var minX = p0.X; var minY = p0.Y; var minZ = p0.Z; var minW = p0.W;
            var maxX = p0.X; var maxY = p0.Y; var maxZ = p0.Z; var maxW = p0.W;

            foreach (var p in points.Skip(1))
            {
                if (minX > p.X) minX = p.X;
                if (minY > p.Y) minY = p.Y;
                if (minZ > p.Z) minZ = p.Z;
                if (minW > p.W) minW = p.W;

                if (maxX < p.X) maxX = p.X;
                if (maxY < p.Y) maxY = p.Y;
                if (maxZ < p.Z) maxZ = p.Z;
                if (maxW < p.W) maxW = p.W;
            }

            return new Float64BoundingBoxComposer4D(minX, minY, minZ, minW, maxX, maxY, maxZ, maxW);
        }

        public double MinX { get; private set; }
        public double MinY { get; private set; }
        public double MinZ { get; private set; }
        public double MinW { get; private set; }

        public double MaxX { get; private set; }
        public double MaxY { get; private set; }
        public double MaxZ { get; private set; }
        public double MaxW { get; private set; }

        public double MidX => 0.5 * (MinX + MaxX);
        public double MidY => 0.5 * (MinY + MaxY);
        public double MidZ => 0.5 * (MinZ + MaxZ);
        public double MidW => 0.5 * (MinW + MaxW);

        public bool IntersectionTestsEnabled { get; set; } = true;

        internal Float64BoundingBoxComposer4D()
        {
        }

        internal Float64BoundingBoxComposer4D(double x, double y, double z, double w)
        {
            MinX = x; MinY = y; MinZ = z; MinW = w;
            MaxX = x; MaxY = y; MaxZ = z; MaxW = w;
            Debug.Assert(IsValid());
        }

        internal Float64BoundingBoxComposer4D(double minX, double minY, double minZ, double minW, double maxX, double maxY, double maxZ, double maxW)
        {
            MinX = minX; MinY = minY; MinZ = minZ; MinW = minW;
            MaxX = maxX; MaxY = maxY; MaxZ = maxZ; MaxW = maxW;
            Debug.Assert(IsValid());
        }

        internal Float64BoundingBoxComposer4D(Float64BoundingBox4D b)
        {
            MinX = b.MinX; MinY = b.MinY; MinZ = b.MinZ; MinW = b.MinW;
            MaxX = b.MaxX; MaxY = b.MaxY; MaxZ = b.MaxZ; MaxW = b.MaxW;
            Debug.Assert(IsValid());
        }

        public bool IsValid()
        {
            return !double.IsNaN(MinX) && !double.IsNaN(MinY) && !double.IsNaN(MinZ) && !double.IsNaN(MinW)
                && !double.IsNaN(MaxX) && !double.IsNaN(MaxY) && !double.IsNaN(MaxZ) && !double.IsNaN(MaxW);
        }

        private void ValidateValues()
        {
            Debug.Assert(IsValid());
            if (MaxX < MinX) (MaxX, MinX) = (MinX, MaxX);
            if (MaxY < MinY) (MaxY, MinY) = (MinY, MaxY);
            if (MaxZ < MinZ) (MaxZ, MinZ) = (MinZ, MaxZ);
            if (MaxW < MinW) (MaxW, MinW) = (MinW, MaxW);
        }

        public Float64BoundingBoxComposer4D SetTo(double x, double y, double z, double w)
        {
            MinX = x; MinY = y; MinZ = z; MinW = w;
            MaxX = x; MaxY = y; MaxZ = z; MaxW = w;
            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D SetTo(LinFloat64Vector4D p)
        {
            MinX = p.X; MinY = p.Y; MinZ = p.Z; MinW = p.W;
            MaxX = p.X; MaxY = p.Y; MaxZ = p.Z; MaxW = p.W;
            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D SetTo(Float64BoundingBox4D b)
        {
            MinX = b.MinX; MinY = b.MinY; MinZ = b.MinZ; MinW = b.MinW;
            MaxX = b.MaxX; MaxY = b.MaxY; MaxZ = b.MaxZ; MaxW = b.MaxW;
            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D MoveMidPointTo(LinFloat64Vector4D newMid)
        {
            var dx = newMid.X - 0.5 * (MaxX + MinX);
            var dy = newMid.Y - 0.5 * (MaxY + MinY);
            var dz = newMid.Z - 0.5 * (MaxZ + MinZ);
            var dw = newMid.W - 0.5 * (MaxW + MinW);

            MinX += dx; MinY += dy; MinZ += dz; MinW += dw;
            MaxX += dx; MaxY += dy; MaxZ += dz; MaxW += dw;

            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D MoveBy(LinFloat64Vector4D delta)
        {
            MinX += delta.X; MinY += delta.Y; MinZ += delta.Z; MinW += delta.W;
            MaxX += delta.X; MaxY += delta.Y; MaxZ += delta.Z; MaxW += delta.W;
            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D UpdateSizeBy(double delta)
        {
            MinX -= delta; MinY -= delta; MinZ -= delta; MinW -= delta;
            MaxX += delta; MaxY += delta; MaxZ += delta; MaxW += delta;
            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D UpdateSizeBy(double dx, double dy, double dz, double dw)
        {
            MinX -= dx; MinY -= dy; MinZ -= dz; MinW -= dw;
            MaxX += dx; MaxY += dy; MaxZ += dz; MaxW += dw;
            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D UpdateSizeByFactor(double factor)
        {
            var midX = 0.5 * (MaxX + MinX);
            var midY = 0.5 * (MaxY + MinY);
            var midZ = 0.5 * (MaxZ + MinZ);
            var midW = 0.5 * (MaxW + MinW);

            MinX = (MinX - midX) * factor + midX;
            MinY = (MinY - midY) * factor + midY;
            MinZ = (MinZ - midZ) * factor + midZ;
            MinW = (MinW - midW) * factor + midW;

            MaxX = (MaxX - midX) * factor + midX;
            MaxY = (MaxY - midY) * factor + midY;
            MaxZ = (MaxZ - midZ) * factor + midZ;
            MaxW = (MaxW - midW) * factor + midW;

            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D ExpandToInfinite()
        {
            MinX = MinY = MinZ = MinW = double.NegativeInfinity;
            MaxX = MaxY = MaxZ = MaxW = double.PositiveInfinity;
            return this;
        }

        public Float64BoundingBoxComposer4D ExpandToInclude(double x, double y, double z, double w)
        {
            if (MinX > x) MinX = x;
            if (MinY > y) MinY = y;
            if (MinZ > z) MinZ = z;
            if (MinW > w) MinW = w;

            if (MaxX < x) MaxX = x;
            if (MaxY < y) MaxY = y;
            if (MaxZ < z) MaxZ = z;
            if (MaxW < w) MaxW = w;

            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D ExpandToInclude(LinFloat64Vector4D p)
        {
            if (MinX > p.X) MinX = p.X;
            if (MinY > p.Y) MinY = p.Y;
            if (MinZ > p.Z) MinZ = p.Z;
            if (MinW > p.W) MinW = p.W;

            if (MaxX < p.X) MaxX = p.X;
            if (MaxY < p.Y) MaxY = p.Y;
            if (MaxZ < p.Z) MaxZ = p.Z;
            if (MaxW < p.W) MaxW = p.W;

            ValidateValues();
            return this;
        }

        public Float64BoundingBoxComposer4D ExpandToInclude(Float64BoundingBox4D box)
        {
            if (MinX > box.MinX) MinX = box.MinX;
            if (MinY > box.MinY) MinY = box.MinY;
            if (MinZ > box.MinZ) MinZ = box.MinZ;
            if (MinW > box.MinW) MinW = box.MinW;

            if (MaxX < box.MaxX) MaxX = box.MaxX;
            if (MaxY < box.MaxY) MaxY = box.MaxY;
            if (MaxZ < box.MaxZ) MaxZ = box.MaxZ;
            if (MaxW < box.MaxW) MaxW = box.MaxW;

            ValidateValues();
            return this;
        }

        public Float64BoundingBox4D GetBoundingBox()
        {
            return Float64BoundingBox4D.Containing(MinX, MinY, MinZ, MinW, MaxX, MaxY, MaxZ, MaxW);
        }
    }
}
