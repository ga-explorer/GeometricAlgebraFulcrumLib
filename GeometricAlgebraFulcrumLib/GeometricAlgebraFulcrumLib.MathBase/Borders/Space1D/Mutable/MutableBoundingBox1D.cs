using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Mutable
{
    public sealed class MutableBoundingBox1D : IBoundingBox1D
    {
        public static MutableBoundingBox1D CreateInfinite()
        {
            return new MutableBoundingBox1D(
                double.NegativeInfinity,
                double.PositiveInfinity
            );
        }


        public static MutableBoundingBox1D CreateAround(double center, double delta)
        {
            return delta >= 0
                ? new MutableBoundingBox1D(center - delta, center + delta)
                : new MutableBoundingBox1D(center + delta, center - delta);
        }


        public static MutableBoundingBox1D Create(double value1, double value2)
        {
            return value1 <= value2
                ? new MutableBoundingBox1D(value1, value2)
                : new MutableBoundingBox1D(value2, value1);
        }

        public static MutableBoundingBox1D Create(double value1, double value2, double value3)
        {
            var minValue = value1;
            var maxValue = value1;

            if (minValue > value2) minValue = value2;
            if (minValue > value3) minValue = value3;

            if (maxValue < value2) maxValue = value2;
            if (maxValue < value3) maxValue = value3;

            return new MutableBoundingBox1D(minValue, maxValue);
        }

        public static MutableBoundingBox1D Create(params double[] valuesList)
        {
            var minValue = 0.0d;
            var maxValue = 0.0d;

            var flag = false;
            foreach (var value in valuesList)
            {
                if (!flag)
                {
                    minValue = value;
                    maxValue = value;

                    flag = true;
                    continue;
                }

                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;
            }

            return new MutableBoundingBox1D(minValue, maxValue);
        }

        public static MutableBoundingBox1D Create(IEnumerable<double> valuesList)
        {
            var minValue = 0.0d;
            var maxValue = 0.0d;

            var flag = false;
            foreach (var value in valuesList)
            {
                if (!flag)
                {
                    minValue = value;
                    maxValue = value;

                    flag = true;
                    continue;
                }

                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;
            }

            return new MutableBoundingBox1D(minValue, maxValue);
        }


        public static MutableBoundingBox1D Create(IBoundingBox1D boundingBox)
        {
            return new MutableBoundingBox1D(boundingBox);
        }

        public static MutableBoundingBox1D Create(IBoundingBox1D b1, IBoundingBox1D b2)
        {
            return new MutableBoundingBox1D(
                Math.Min(b1.MinValue, b2.MinValue),
                Math.Max(b1.MaxValue, b2.MaxValue)
            );
        }

        public static MutableBoundingBox1D Create(params IBoundingBox1D[] boundingBoxesList)
        {
            var minValue = 0.0d;
            var maxValue = 0.0d;

            var flag = false;
            foreach (var boundingBox in boundingBoxesList)
            {
                if (!flag)
                {
                    minValue = boundingBox.MinValue;
                    maxValue = boundingBox.MaxValue;

                    flag = true;
                    continue;
                }

                if (minValue > boundingBox.MinValue) minValue = boundingBox.MinValue;
                if (maxValue < boundingBox.MaxValue) maxValue = boundingBox.MaxValue;
            }

            return new MutableBoundingBox1D(minValue, maxValue);
        }

        public static MutableBoundingBox1D Create(IEnumerable<IBoundingBox1D> boundingBoxesList)
        {
            var minValue = 0.0d;
            var maxValue = 0.0d;

            var flag = false;
            foreach (var boundingBox in boundingBoxesList)
            {
                if (!flag)
                {
                    minValue = boundingBox.MinValue;
                    maxValue = boundingBox.MaxValue;

                    flag = true;
                    continue;
                }

                if (minValue > boundingBox.MinValue) minValue = boundingBox.MinValue;
                if (maxValue < boundingBox.MaxValue) maxValue = boundingBox.MaxValue;
            }

            return new MutableBoundingBox1D(minValue, maxValue);
        }

        public static MutableBoundingBox1D CreateFromIntersection(IBoundingBox1D b1, IBoundingBox1D b2)
        {
            return new MutableBoundingBox1D(
                Math.Max(b1.MinValue, b2.MinValue),
                Math.Min(b1.MaxValue, b2.MaxValue)
            );
        }


        public double MinValue { get; private set; }

        public double MaxValue { get; private set; }

        public bool HasNaNComponent
        {
            get
            {
                return double.IsNaN(MinValue) ||
                       double.IsNaN(MaxValue);
            }
        }


        internal MutableBoundingBox1D()
        {
            MinValue = double.NegativeInfinity;
            MaxValue = double.PositiveInfinity;
        }

        internal MutableBoundingBox1D(double minValue, double maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            Debug.Assert(!HasNaNComponent);
        }

        internal MutableBoundingBox1D(IBoundingBox1D boundingBox)
        {
            MinValue = boundingBox.MinValue;
            MaxValue = boundingBox.MaxValue;

            Debug.Assert(!HasNaNComponent);
        }


        public MutableBoundingBox1D Reset(double value)
        {
            MinValue = value;
            MaxValue = value;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D Reset(IBoundingBox1D boundingBox)
        {
            MinValue = boundingBox.MinValue;
            MaxValue = boundingBox.MaxValue;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D Reset(double value1, double value2)
        {
            if (value1 <= value2)
            {
                MinValue = value1;
                MaxValue = value2;
            }
            else
            {
                MinValue = value2;
                MaxValue = value1;
            }

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D RestrictMinValue(double value)
        {
            Debug.Assert(value <= MaxValue);

            if (MinValue < value) MinValue = value;

            return this;
        }

        public MutableBoundingBox1D RestrictMaxValue(double value)
        {
            Debug.Assert(value >= MinValue);

            if (MaxValue > value) MaxValue = value;

            return this;
        }


        public MutableBoundingBox1D ExpandToInfinite()
        {
            MinValue = double.NegativeInfinity;
            MaxValue = double.PositiveInfinity;

            return this;
        }


        public MutableBoundingBox1D ExpandBy(double delta)
        {
            MinValue = MinValue - delta;
            MaxValue = MaxValue + delta;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D ExpandByFactor(double deltaPercent)
        {
            var delta = deltaPercent * (MaxValue - MinValue);

            MinValue = MinValue - delta;
            MaxValue = MaxValue + delta;

            Debug.Assert(!HasNaNComponent);

            return this;
        }


        public MutableBoundingBox1D ExpandToInclude(double value)
        {
            if (MinValue > value) MinValue = value;
            if (MaxValue < value) MaxValue = value;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D ExpandToInclude(double value1, double value2)
        {
            if (MinValue > value1) MinValue = value1;
            if (MinValue > value2) MinValue = value2;

            if (MaxValue < value1) MaxValue = value1;
            if (MaxValue < value2) MaxValue = value2;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D ExpandToInclude(params double[] valuesList)
        {
            foreach (var value in valuesList)
            {
                if (MinValue > value) MinValue = value;
                if (MaxValue < value) MaxValue = value;
            }

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D ExpandToInclude(IEnumerable<double> valuesList)
        {
            foreach (var value in valuesList)
            {
                if (MinValue > value) MinValue = value;
                if (MaxValue < value) MaxValue = value;
            }

            Debug.Assert(!HasNaNComponent);

            return this;
        }


        public MutableBoundingBox1D ExpandToInclude(IBoundingBox1D boundingBox)
        {
            if (MinValue > boundingBox.MinValue) MinValue = boundingBox.MinValue;
            if (MaxValue < boundingBox.MaxValue) MaxValue = boundingBox.MaxValue;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D ExpandToInclude(params IBoundingBox1D[] boundingBoxesList)
        {
            foreach (var boundingBox in boundingBoxesList)
            {
                if (MinValue > boundingBox.MinValue) MinValue = boundingBox.MinValue;
                if (MaxValue < boundingBox.MaxValue) MaxValue = boundingBox.MaxValue;
            }

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableBoundingBox1D ExpandToInclude(IEnumerable<IBoundingBox1D> boundingBoxesList)
        {
            foreach (var boundingBox in boundingBoxesList)
            {
                if (MinValue > boundingBox.MinValue) MinValue = boundingBox.MinValue;
                if (MaxValue < boundingBox.MaxValue) MaxValue = boundingBox.MaxValue;
            }

            Debug.Assert(!HasNaNComponent);

            return this;
        }


        public BoundingBox1D GetBoundingBox()
        {
            return new BoundingBox1D(this);
        }

        public MutableBoundingBox1D GetMutableBoundingBox()
        {
            return new MutableBoundingBox1D(this);
        }
    }
}