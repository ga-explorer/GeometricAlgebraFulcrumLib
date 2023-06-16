using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders
{
    public readonly struct Float64Range1D :
        IGeometricElement,
        IPair<double>,
        IEquatable<Float64Range1D>
    {
        public static Float64Range1D Infinite { get; }
            = new Float64Range1D(
                double.NegativeInfinity,
                double.PositiveInfinity
            );

        public static Float64Range1D ZeroToInfinity { get; }
            = new Float64Range1D(
                0d,
                double.PositiveInfinity
            );

        public static Float64Range1D InfinityToZero { get; }
            = new Float64Range1D(
                double.NegativeInfinity,
                0d
            );

        public static Float64Range1D ZeroToOne { get; }
            = new Float64Range1D(0d, 1d);
        
        public static Float64Range1D ZeroToPi { get; }
            = new Float64Range1D(0d, Math.PI);
        
        public static Float64Range1D ZeroToTwoPi { get; }
            = new Float64Range1D(0d, 2d * Math.PI);

        public static Float64Range1D NegativeOneToZero { get; }
            = new Float64Range1D(-1d, 0d);

        public static Float64Range1D NegativeOneToOne { get; }
            = new Float64Range1D(-1d, 1d);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D CreateAround(double center, double delta)
        {
            return delta >= 0
                ? new Float64Range1D(center - delta, center + delta)
                : new Float64Range1D(center + delta, center - delta);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D Create(double value)
        {
            return value > 0d
                ? new Float64Range1D(0, value)
                : new Float64Range1D(value, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D Create(double value1, double value2)
        {
            return value1 <= value2
                ? new Float64Range1D(value1, value2)
                : new Float64Range1D(value2, value1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D Create(IPair<double> range)
        {
            var value1 = range.Item1;
            var value2 = range.Item2;

            return value1 < value2
                ? new Float64Range1D(value1, value2)
                : new Float64Range1D(value2, value1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D Create(double value1, double value2, double value3)
        {
            var minValue = value1;
            var maxValue = value1;

            if (minValue > value2) minValue = value2;
            if (minValue > value3) minValue = value3;

            if (maxValue < value2) maxValue = value2;
            if (maxValue < value3) maxValue = value3;

            return new Float64Range1D(minValue, maxValue);
        }

        public static Float64Range1D Create(params double[] valuesList)
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

            return new Float64Range1D(minValue, maxValue);
        }

        public static Float64Range1D Create(IEnumerable<double> valuesList)
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

            return new Float64Range1D(minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D Create(Float64Range1D b1, Float64Range1D b2)
        {
            return new Float64Range1D(
                Math.Min(b1.MinValue, b2.MinValue),
                Math.Max(b1.MaxValue, b2.MaxValue)
            );
        }

        public static Float64Range1D Create(params Float64Range1D[] rangeList)
        {
            var minValue = 0.0d;
            var maxValue = 0.0d;

            var flag = false;
            foreach (var range in rangeList)
            {
                if (!flag)
                {
                    minValue = range.MinValue;
                    maxValue = range.MaxValue;

                    flag = true;
                    continue;
                }

                if (minValue > range.MinValue) minValue = range.MinValue;
                if (maxValue < range.MaxValue) maxValue = range.MaxValue;
            }

            return new Float64Range1D(minValue, maxValue);
        }

        public static Float64Range1D Create(IEnumerable<Float64Range1D> rangeList)
        {
            var minValue = 0.0d;
            var maxValue = 0.0d;

            var flag = false;
            foreach (var range in rangeList)
            {
                if (!flag)
                {
                    minValue = range.MinValue;
                    maxValue = range.MaxValue;

                    flag = true;
                    continue;
                }

                if (minValue > range.MinValue) minValue = range.MinValue;
                if (maxValue < range.MaxValue) maxValue = range.MaxValue;
            }

            return new Float64Range1D(minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D CreateFromIntersection(Float64Range1D b1, Float64Range1D b2)
        {
            return new Float64Range1D(
                Math.Max(b1.MinValue, b2.MinValue),
                Math.Min(b1.MaxValue, b2.MaxValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator -(Float64Range1D b1)
        {
            return new Float64Range1D(
                -b1.MaxValue,
                -b1.MinValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator +(Float64Range1D b1, double b2)
        {
            return new Float64Range1D(
                b1.MinValue + b2,
                b1.MaxValue + b2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator +(double b1, Float64Range1D b2)
        {
            return new Float64Range1D(
                b1 + b2.MinValue,
                b1 + b2.MaxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator -(Float64Range1D b1, double b2)
        {
            return new Float64Range1D(
                b1.MinValue - b2,
                b1.MaxValue - b2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator -(double b1, Float64Range1D b2)
        {
            return new Float64Range1D(
                b1 - b2.MaxValue,
                b1 - b2.MinValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator *(Float64Range1D b1, double b2)
        {
            if (b2 > 0)
                return new Float64Range1D(
                    b1.MinValue * b2,
                    b1.MaxValue * b2
                );

            return new Float64Range1D(
                b1.MaxValue * b2,
                b1.MinValue * b2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator *(double b1, Float64Range1D b2)
        {
            if (b1 > 0)
                return new Float64Range1D(
                    b2.MinValue * b1,
                    b2.MaxValue * b1
                );

            return new Float64Range1D(
                b2.MaxValue * b1,
                b2.MinValue * b1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Range1D operator /(Float64Range1D b1, double b2)
        {
            if (b2 > 0)
                return new Float64Range1D(
                    b1.MinValue / b2,
                    b1.MaxValue / b2
                );

            return new Float64Range1D(
                b1.MaxValue / b2,
                b1.MinValue / b2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Float64Range1D left, Float64Range1D right)
        {
            return left.Equals(right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Float64Range1D left, Float64Range1D right)
        {
            return !left.Equals(right);
        }


        public double MinValue { get; }

        public double MaxValue { get; }
        
        public double MidValue
            => 0.5 * (MinValue + MaxValue);

        public double Length
            => MaxValue - MinValue;

        public double Item1
            => MinValue;

        public double Item2
            => MaxValue;
        
        public bool IsFinite 
            => !double.IsInfinity(MinValue) &&
               !double.IsInfinity(MaxValue);

        public bool IsInfinite 
            => double.IsInfinity(MinValue) ||
               double.IsInfinity(MaxValue);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Range1D(double minValue, double maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return MinValue.IsValid() &&
                   MaxValue.IsValid() &&
                   MinValue < MaxValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out double minValue, out double maxValue)
        {
            minValue = MinValue;
            maxValue = MaxValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D Negative()
        {
            return new Float64Range1D(-MaxValue, -MinValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D Plus(double value)
        {
            return new Float64Range1D(MinValue + value, MaxValue + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D Times(double value)
        {
            return value > 0
                ? new Float64Range1D(MinValue * value, MaxValue * value)
                : new Float64Range1D(MaxValue * value, MinValue * value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D Divide(double value)
        {
            return value > 0
                ? new Float64Range1D(MinValue / value, MaxValue / value)
                : new Float64Range1D(MaxValue / value, MinValue / value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ResetMinValue(double minValue)
        {
            return Create(minValue, MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ResetMaxValue(double maxValue)
        {
            return Create(MinValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ExpandBy(double delta)
        {
            return Create(
                MinValue - delta,
                MaxValue + delta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ExpandByFactor(double deltaPercent)
        {
            var delta = deltaPercent * (MaxValue - MinValue);

            return Create(
                MinValue - delta,
                MaxValue + delta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ExpandToInclude(double value)
        {
            if (value < MinValue)
                return new Float64Range1D(value, MaxValue);

            if (value > MaxValue)
                return new Float64Range1D(MinValue, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ExpandToInclude(double value1, double value2)
        {
            var minValue = MinValue;
            var maxValue = MaxValue;

            if (value1 < minValue)
                minValue = value1;

            else if (value1 > maxValue)
                maxValue = value1;

            if (value2 < minValue)
                minValue = value1;

            else if (value2 > maxValue)
                maxValue = value1;

            return new Float64Range1D(minValue, maxValue);
        }

        public Float64Range1D ExpandToInclude(params double[] valuesList)
        {
            var minValue = MinValue;
            var maxValue = MaxValue;

            foreach (var value in valuesList)
            {
                if (value < minValue)
                    minValue = value;

                else if (value > MaxValue)
                    maxValue = value;
            }

            return new Float64Range1D(minValue, maxValue);
        }

        public Float64Range1D ExpandToInclude(IEnumerable<double> valuesList)
        {
            var minValue = MinValue;
            var maxValue = MaxValue;

            foreach (var value in valuesList)
            {
                if (value < minValue)
                    minValue = value;

                else if (value > MaxValue)
                    maxValue = value;
            }

            return new Float64Range1D(minValue, maxValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Range1D ExpandToInclude(IPair<double> range)
        {
            return ExpandToInclude(
                range.Item1,
                range.Item2
            );
        }

        public Float64Range1D ExpandToInclude(params Float64Range1D[] rangeList)
        {
            var minValue = MinValue;
            var maxValue = MaxValue;

            foreach (var (value1, value2) in rangeList)
            {
                if (value1 < minValue)
                    minValue = value1;

                else if (value1 > MaxValue)
                    maxValue = value1;

                if (value2 < minValue)
                    minValue = value2;

                else if (value2 > MaxValue)
                    maxValue = value2;
            }

            return new Float64Range1D(minValue, maxValue);
        }

        public Float64Range1D ExpandToInclude(IEnumerable<Float64Range1D> rangeList)
        {
            var minValue = MinValue;
            var maxValue = MaxValue;

            foreach (var (value1, value2) in rangeList)
            {
                if (value1 < minValue)
                    minValue = value1;

                else if (value1 > MaxValue)
                    maxValue = value1;

                if (value2 < minValue)
                    minValue = value2;

                else if (value2 > MaxValue)
                    maxValue = value2;
            }

            return new Float64Range1D(minValue, maxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetOffsetFromMin(double value)
        {
            return value - MinValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetOffsetToMin(double value)
        {
            return MinValue - value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetOffsetFromMax(double value)
        {
            return value - MaxValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetOffsetToMax(double value)
        {
            return MaxValue - value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetRelativeOffset(double value)
        {
            return (value - MinValue) / (MaxValue - MinValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetLinearSamples(int count, bool assumePeriodic)
        {
            if ((assumePeriodic && count < 1) || (!assumePeriodic && count < 2))
                throw new ArgumentOutOfRangeException(nameof(count));

            return MinValue.GetLinearRange(MaxValue, count, assumePeriodic);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetLinearPeriodicSamples(int count)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            return MinValue.GetLinearPeriodicRange(MaxValue, count);
        }

        public Float64Range1D[] GetSubdivisions(int divisionCount, double epsilon = 0d)
        {
            var length = Length / divisionCount;

            var minValue = MinValue;

            var minValues =
                Enumerable
                    .Range(0, divisionCount)
                    .Select(i => i * length + minValue + epsilon)
                    .ToArray();

            var maxValues =
                minValues
                    .Select(v => v + length - epsilon)
                    .ToArray();

            var divisions = new Float64Range1D[divisionCount];

            for (var i = 0; i < divisionCount; i++)
                divisions[i] = Create(
                    minValues[i], 
                    maxValues[i]
                );

            return divisions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(double value, double epsilon = 0d)
        {
            Debug.Assert(epsilon >= 0);

            return value >= MinValue - epsilon &&
                   value <= MaxValue + epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Float64Range1D box, double epsilon = 0d)
        {
            Debug.Assert(epsilon >= 0);

            return box.MinValue >= MinValue - epsilon &&
                   box.MaxValue <= MaxValue + epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsUpperExclusive(double value, double epsilon = 0d)
        {
            Debug.Assert(epsilon >= 0);

            return value >= MinValue - epsilon &&
                   value < MaxValue + epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(Float64Range1D box, double epsilon = 0d)
        {
            Debug.Assert(epsilon >= 0);

            return box.MaxValue >= MinValue - epsilon &&
                   box.MinValue <= MaxValue + epsilon;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Float64Range1D other)
        {
            return MinValue.Equals(other.MinValue) &&
                   MaxValue.Equals(other.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return obj is Float64Range1D other && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(MinValue, MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"[{MinValue}, {MaxValue}]";
        }
    }
}