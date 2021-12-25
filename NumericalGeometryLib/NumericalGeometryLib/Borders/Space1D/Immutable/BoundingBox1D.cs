using System;
using System.Collections.Generic;
using System.Diagnostics;
using NumericalGeometryLib.Borders.Space1D.Mutable;

namespace NumericalGeometryLib.Borders.Space1D.Immutable
{
    public struct BoundingBox1D : IBoundingBox1D
    {
        public static BoundingBox1D Infinite { get; }
            = new BoundingBox1D(
                double.NegativeInfinity, 
                double.PositiveInfinity
            );

        public static BoundingBox1D CreateInfinite()
        {
            return new BoundingBox1D(
                double.NegativeInfinity,
                double.PositiveInfinity
            );
        }


        public static BoundingBox1D CreateAround(double center, double delta)
        {
            return delta >= 0
                ? new BoundingBox1D(center - delta, center + delta)
                : new BoundingBox1D(center + delta, center - delta);
        }


        public static BoundingBox1D Create(double value1, double value2)
        {
            return value1 <= value2
                ? new BoundingBox1D(value1, value2) 
                : new BoundingBox1D(value2, value1);
        }

        public static BoundingBox1D Create(double value1, double value2, double value3)
        {
            var minValue = value1;
            var maxValue = value1;

            if (minValue > value2) minValue = value2;
            if (minValue > value3) minValue = value3;

            if (maxValue < value2) maxValue = value2;
            if (maxValue < value3) maxValue = value3;

            return new BoundingBox1D(minValue, maxValue);
        }

        public static BoundingBox1D Create(params double[] valuesList)
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

            return new BoundingBox1D(minValue, maxValue);
        }

        public static BoundingBox1D Create(IEnumerable<double> valuesList)
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

            return new BoundingBox1D(minValue, maxValue);
        }


        public static BoundingBox1D Create(IBoundingBox1D boundingBox)
        {
            return new BoundingBox1D(boundingBox);
        }

        public static BoundingBox1D Create(IBoundingBox1D b1, IBoundingBox1D b2)
        {
            return new BoundingBox1D(
                Math.Min(b1.MinValue, b2.MinValue),
                Math.Max(b1.MaxValue, b2.MaxValue)
            );
        }

        public static BoundingBox1D Create(params IBoundingBox1D[] boundingBoxesList)
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

            return new BoundingBox1D(minValue, maxValue);
        }

        public static BoundingBox1D Create(IEnumerable<IBoundingBox1D> boundingBoxesList)
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

            return new BoundingBox1D(minValue, maxValue);
        }

        public static BoundingBox1D CreateFromIntersection(IBoundingBox1D b1, IBoundingBox1D b2)
        {
            return new BoundingBox1D(
                Math.Max(b1.MinValue, b2.MinValue),
                Math.Min(b1.MaxValue, b2.MaxValue)
            );
        }


        public double MinValue { get; }

        public double MaxValue { get; }

        public bool HasNaNComponent { get; }


        internal BoundingBox1D(double minValue, double maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            HasNaNComponent =
                double.IsNaN(MinValue) ||
                double.IsNaN(MinValue);

            Debug.Assert(!HasNaNComponent);
        }

        internal BoundingBox1D(IBoundingBox1D boundingBox)
        {
            MinValue = boundingBox.MinValue;
            MaxValue = boundingBox.MaxValue;

            HasNaNComponent =
                double.IsNaN(MinValue) ||
                double.IsNaN(MaxValue);

            Debug.Assert(!HasNaNComponent);
        }


        public BoundingBox1D GetBoundingBox()
        {
            return this;
        }

        public MutableBoundingBox1D GetMutableBoundingBox()
        {
            return new MutableBoundingBox1D(this);
        }
    }
}