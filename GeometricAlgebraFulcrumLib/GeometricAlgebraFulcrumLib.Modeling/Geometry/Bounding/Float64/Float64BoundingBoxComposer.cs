using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64
{
    /// <summary>
    /// Mutable composer for n-dimensional bounding boxes.
    /// </summary>
    public sealed class Float64BoundingBoxComposer
    {
        private readonly double[] _min;
        private readonly double[] _max;

        public int VSpaceDimensions => _min.Length;

        public double Min(int i) => _min[i];

        public double Max(int i) => _max[i];

        public Float64BoundingBoxComposer(int dimensions)
        {
            _min = Enumerable.Repeat(double.PositiveInfinity, dimensions).ToArray();
            _max = Enumerable.Repeat(double.NegativeInfinity, dimensions).ToArray();
        }

        public Float64BoundingBoxComposer(Float64BoundingBox box)
        {
            _min = box.MinPoint.ToScalarArray(box.VSpaceDimensions);
            _max = box.MaxPoint.ToScalarArray(box.VSpaceDimensions);
        }

        public Float64BoundingBoxComposer SetTo(LinFloat64Vector p)
        {
            Debug.Assert(p.VSpaceDimensions == VSpaceDimensions);
            for (var i = 0; i < VSpaceDimensions; i++)
                _min[i] = _max[i] = p[i];
            return this;
        }

        public Float64BoundingBoxComposer MoveBy(LinFloat64Vector delta)
        {
            Debug.Assert(delta.VSpaceDimensions == VSpaceDimensions);
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                _min[i] += delta[i];
                _max[i] += delta[i];
            }
            return this;
        }

        public Float64BoundingBoxComposer ExpandToInclude(LinFloat64Vector p)
        {
            Debug.Assert(p.VSpaceDimensions == VSpaceDimensions);
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                if (p[i] < _min[i]) _min[i] = p[i];
                if (p[i] > _max[i]) _max[i] = p[i];
            }
            return this;
        }

        public Float64BoundingBoxComposer ExpandToInclude(Float64BoundingBox box)
        {
            Debug.Assert(box.VSpaceDimensions == VSpaceDimensions);
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                if (box.MinPoint[i] < _min[i]) _min[i] = box.MinPoint[i];
                if (box.MaxPoint[i] > _max[i]) _max[i] = box.MaxPoint[i];
            }
            return this;
        }

        public Float64BoundingBoxComposer UpdateSizeBy(double delta)
        {
            for (var i = 0; i < VSpaceDimensions; i++)
            {
                _min[i] -= delta;
                _max[i] += delta;
            }
            return this;
        }

        public Float64BoundingBox GetBoundingBox()
        {
            return Float64BoundingBox.Containing(
                LinFloat64Vector.Create(_min),
                LinFloat64Vector.Create(_max)
            );
        }
    }
}
