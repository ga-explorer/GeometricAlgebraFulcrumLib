﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.System;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

public readonly struct Float64ScalarRange :
    IAlgebraicElement,
    IPair<double>,
    IEquatable<Float64ScalarRange>
{
    public static Float64ScalarRange Infinite { get; }
        = new Float64ScalarRange(
            double.NegativeInfinity,
            double.PositiveInfinity
        );

    public static Float64ScalarRange ZeroToInfinity { get; }
        = new Float64ScalarRange(
            0d,
            double.PositiveInfinity
        );

    public static Float64ScalarRange InfinityToZero { get; }
        = new Float64ScalarRange(
            double.NegativeInfinity,
            0d
        );
    
    public static Float64ScalarRange ZeroToOne { get; }
        = new Float64ScalarRange(0d, 1d);

    public static Float64ScalarRange ZeroToPi { get; }
        = new Float64ScalarRange(0d, Math.PI);

    public static Float64ScalarRange ZeroToTwoPi { get; }
        = new Float64ScalarRange(0d, 2 * Math.PI);

    public static Float64ScalarRange NegativeOneToZero { get; }
        = new Float64ScalarRange(-1d, 0d);

    public static Float64ScalarRange SymmetricOne { get; }
        = new Float64ScalarRange(-1, 1);
    
    public static Float64ScalarRange SymmetricHalfPi { get; }
        = new Float64ScalarRange(-Math.PI / 2, Math.PI / 2);

    public static Float64ScalarRange SymmetricPi { get; }
        = new Float64ScalarRange(-Math.PI, Math.PI);
    
    public static Float64ScalarRange SymmetricTwoPi { get; }
        = new Float64ScalarRange(-2 * Math.PI, 2 * Math.PI);


    
    public static Float64ScalarRange CreateAroundZero(double delta)
    {
        return delta >= 0
            ? new Float64ScalarRange(-delta, delta)
            : new Float64ScalarRange(delta, -delta);
    }
    
    
    public static Float64ScalarRange CreateAround(double center, double delta)
    {
        return delta >= 0
            ? new Float64ScalarRange(center - delta, center + delta)
            : new Float64ScalarRange(center + delta, center - delta);
    }

    
    public static Float64ScalarRange Create(double value)
    {
        return value > 0d
            ? new Float64ScalarRange(0d, value)
            : new Float64ScalarRange(value, 0d);
    }
    
    
    public static Float64ScalarRange Create(int value1, int value2)
    {
        return value1 <= value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }

    
    public static Float64ScalarRange Create(double value1, double value2)
    {
        return value1 <= value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }
    
    
    public static Float64ScalarRange Create(IPair<double> range)
    {
        var value1 = range.Item1;
        var value2 = range.Item2;

        return value1 < value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }

    
    public static Float64ScalarRange Create(double value1, double value2, double value3)
    {
        var minValue = value1;
        var maxValue = value1;

        if (minValue > value2) minValue = value2;
        if (minValue > value3) minValue = value3;

        if (maxValue < value2) maxValue = value2;
        if (maxValue < value3) maxValue = value3;

        return new Float64ScalarRange(minValue, maxValue);
    }

    public static Float64ScalarRange Create(params double[] valuesList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }
    
    public static Float64ScalarRange Create(IEnumerable<double> valuesList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }
    
    
    public static Float64ScalarRange Create(Float64ScalarRange b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            Math.Min(b1.MinValue, b2.MinValue),
            Math.Max(b1.MaxValue, b2.MaxValue)
        );
    }

    public static Float64ScalarRange Create(params Float64ScalarRange[] rangeList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }

    public static Float64ScalarRange Create(IEnumerable<Float64ScalarRange> rangeList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }

    
    public static Float64ScalarRange CreateFromIntersection(Float64ScalarRange b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            Math.Max(b1.MinValue, b2.MinValue),
            Math.Min(b1.MaxValue, b2.MaxValue)
        );
    }

    
    public static Float64ScalarRange CreateFromOuterBounds(Float64ScalarRange b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            Math.Min(b1.MinValue, b2.MinValue),
            Math.Max(b1.MaxValue, b2.MaxValue)
        );
    }


    
    public static Float64ScalarRange operator -(Float64ScalarRange b1)
    {
        return new Float64ScalarRange(
            -b1.MaxValue,
            -b1.MinValue
        );
    }

    
    public static Float64ScalarRange operator +(Float64ScalarRange b1, double b2)
    {
        return new Float64ScalarRange(
            b1.MinValue + b2,
            b1.MaxValue + b2
        );
    }

    
    public static Float64ScalarRange operator +(double b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            b1 + b2.MinValue,
            b1 + b2.MaxValue
        );
    }

    
    public static Float64ScalarRange operator -(Float64ScalarRange b1, double b2)
    {
        return new Float64ScalarRange(
            b1.MinValue - b2,
            b1.MaxValue - b2
        );
    }

    
    public static Float64ScalarRange operator -(double b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            b1 - b2.MaxValue,
            b1 - b2.MinValue
        );
    }

    
    public static Float64ScalarRange operator *(Float64ScalarRange b1, double b2)
    {
        if (b2 > 0)
            return new Float64ScalarRange(
                b1.MinValue * b2,
                b1.MaxValue * b2
            );

        return new Float64ScalarRange(
            b1.MaxValue * b2,
            b1.MinValue * b2
        );
    }

    
    public static Float64ScalarRange operator *(double b1, Float64ScalarRange b2)
    {
        if (b1 > 0)
            return new Float64ScalarRange(
                b2.MinValue * b1,
                b2.MaxValue * b1
            );

        return new Float64ScalarRange(
            b2.MaxValue * b1,
            b2.MinValue * b1
        );
    }

    
    public static Float64ScalarRange operator /(Float64ScalarRange b1, double b2)
    {
        if (b2 > 0)
            return new Float64ScalarRange(
                b1.MinValue / b2,
                b1.MaxValue / b2
            );

        return new Float64ScalarRange(
            b1.MaxValue / b2,
            b1.MinValue / b2
        );
    }

    
    public static bool operator ==(Float64ScalarRange left, Float64ScalarRange right)
    {
        return left.Equals(right);
    }

    
    public static bool operator !=(Float64ScalarRange left, Float64ScalarRange right)
    {
        return !left.Equals(right);
    }


    public double MinValue { get; }

    public double MaxValue { get; }

    public double MidValue
    {
        get
        {
            if (MinValue.IsFinite() && MaxValue.IsFinite())
                return 0.5 * (MinValue + MaxValue);

            if (MinValue.IsInfinite() && MaxValue.IsInfinite())
                return 0d;

            if (MinValue.IsFinite())
                return double.PositiveInfinity;

            return double.NegativeInfinity;
        }
    }

    public double Length
        => MinValue.IsFinite() && MaxValue.IsFinite() 
            ? MaxValue - MinValue 
            : double.PositiveInfinity;

    public double Item1
        => MinValue;

    public double Item2
        => MaxValue;

    public bool IsZeroLength 
        => MinValue == MaxValue;

    public bool IsFinite
        => MinValue.IsFinite() &&
           MaxValue.IsFinite();

    public bool IsInfinite
        => double.IsInfinity(MinValue) ||
           double.IsInfinity(MaxValue);


    
    private Float64ScalarRange(double minValue, double maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        Debug.Assert(IsValid());
    }


    
    public bool IsValid()
    {
        if (MinValue.IsInfinite() && MaxValue.IsInfinite() && MinValue == MaxValue)
            return false;

        return MinValue.IsValid() &&
               MaxValue.IsValid() &&
               MinValue <= MaxValue;
    }

    
    public void Deconstruct(out double minValue, out double maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }

    
    
    public Float64ScalarRange OuterBoundsUnion(Float64ScalarRange range2)
    {
        return CreateFromOuterBounds(this, range2);
    }
    
    
    public Float64ScalarRange OuterBoundsUnion(Float64ScalarRange range2, Float64ScalarRange range3)
    {
        return CreateFromOuterBounds(
            CreateFromOuterBounds(this, range2), 
            range3
        );
    }

    
    public Float64ScalarRange Intersect(Float64ScalarRange range2)
    {
        return CreateFromIntersection(this, range2);
    }
    
    
    public Float64ScalarRange Intersect(Float64ScalarRange range2, Float64ScalarRange range3)
    {
        return CreateFromIntersection(
            CreateFromIntersection(this, range2), 
            range3
        );
    }

    
    public double AffineMapToZeroOneRange(double value)
    {
        if (!IsFinite)
            throw new InvalidOperationException();

        return (value - MinValue) / (MaxValue - MinValue);
    }

    
    public double AffineMapToRange(double value, double value1, double value2)
    {
        return AffineMapToZeroOneRange(value) * (value2 - value1) + value1;
    }

    
    public double AffineMapToRange(double value, IPair<double> valuePair, bool reverseLimits = false)
    {
        return reverseLimits
            ? AffineMapToZeroOneRange(value) * (valuePair.Item1 - valuePair.Item2) + valuePair.Item2
            : AffineMapToZeroOneRange(value) * (valuePair.Item2 - valuePair.Item1) + valuePair.Item1;
    }

    
    public double AffineMapToRange(double value, Float64ScalarRange range, bool reverseLimits = false)
    {
        if (!range.IsFinite)
            throw new InvalidOperationException();

        return reverseLimits
            ? range.MaxValue - AffineMapToZeroOneRange(value) * range.Length
            : range.MinValue + AffineMapToZeroOneRange(value) * range.Length;
    }

    
    public Float64ScalarRange Negative()
    {
        return new Float64ScalarRange(-MaxValue, -MinValue);
    }

    
    public Float64ScalarRange Plus(double value)
    {
        return new Float64ScalarRange(MinValue + value, MaxValue + value);
    }

    
    public Float64ScalarRange Times(double value)
    {
        return value > 0
            ? new Float64ScalarRange(MinValue * value, MaxValue * value)
            : new Float64ScalarRange(MaxValue * value, MinValue * value);
    }

    
    public Float64ScalarRange Divide(double value)
    {
        return value > 0
            ? new Float64ScalarRange(MinValue / value, MaxValue / value)
            : new Float64ScalarRange(MaxValue / value, MinValue / value);
    }

    
    public Float64ScalarRange ResetMinValue(double minValue)
    {
        return Create(minValue, MaxValue);
    }

    
    public Float64ScalarRange ResetMaxValue(double maxValue)
    {
        return Create(MinValue, maxValue);
    }

    
    public Float64ScalarRange ExpandBy(double delta)
    {
        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    
    public Float64ScalarRange ExpandByFactor(double deltaPercent)
    {
        var delta = deltaPercent * (MaxValue - MinValue);

        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    
    public Float64ScalarRange ExpandToInclude(double value)
    {
        if (value < MinValue)
            return new Float64ScalarRange(value, MaxValue);

        if (value > MaxValue)
            return new Float64ScalarRange(MinValue, value);

        return this;
    }

    
    public Float64ScalarRange ExpandToInclude(double value1, double value2)
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

        return new Float64ScalarRange(minValue, maxValue);
    }

    public Float64ScalarRange ExpandToInclude(params double[] valuesList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }

    public Float64ScalarRange ExpandToInclude(IEnumerable<double> valuesList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }


    
    public Float64ScalarRange ExpandToInclude(IPair<double> range)
    {
        return ExpandToInclude(
            range.Item1,
            range.Item2
        );
    }

    public Float64ScalarRange ExpandToInclude(params Float64ScalarRange[] rangeList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }

    public Float64ScalarRange ExpandToInclude(IEnumerable<Float64ScalarRange> rangeList)
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

        return new Float64ScalarRange(minValue, maxValue);
    }

    
    public double GetOffsetFromMin(double value)
    {
        return value - MinValue;
    }

    
    public double GetOffsetToMin(double value)
    {
        return MinValue - value;
    }

    
    public double GetOffsetFromMax(double value)
    {
        return value - MaxValue;
    }

    
    public double GetOffsetToMax(double value)
    {
        return MaxValue - value;
    }

    
    public double GetRelativeOffset(double value)
    {
        return (value - MinValue) / (MaxValue - MinValue);
    }

    
    public IEnumerable<double> GetLinearSamples(int count, bool assumePeriodic)
    {
        if (assumePeriodic && count < 1 || !assumePeriodic && count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        return MinValue.GetLinearRange(MaxValue, count, assumePeriodic);
    }

    
    public IEnumerable<double> GetLinearPeriodicSamples(int count)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count));

        return MinValue.GetLinearPeriodicRange(MaxValue, count);
    }

    public Float64ScalarRange[] GetSubdivisions(int divisionCount, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var length = Length / divisionCount;

        var minValue = MinValue;

        var minValues =
            Enumerable
                .Range(0, divisionCount)
                .Select(i => i * length + minValue + zeroEpsilon)
                .ToArray();

        var maxValues =
            minValues
                .Select(v => v + length - zeroEpsilon)
                .ToArray();

        var divisions = new Float64ScalarRange[divisionCount];

        for (var i = 0; i < divisionCount; i++)
            divisions[i] = Create(
                minValues[i],
                maxValues[i]
            );

        return divisions;
    }
    
    
    public bool Contains(double value, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return value >= MinValue - zeroEpsilon &&
               value <= MaxValue + zeroEpsilon;
    }
    
    
    public double Clamp(double value, bool isPeriodic)
    {
        return isPeriodic
            ? value.ClampPeriodic(MinValue, MaxValue)
            : value.Clamp(MinValue, MaxValue);
    }

    
    public double Clamp(double value, double zeroEpsilon = 0)
    {
        return value.Clamp(
            MinValue - zeroEpsilon, 
            MaxValue + zeroEpsilon
        );
    }

    
    public double ClampPeriodic(double value)
    {
        if (!IsFinite)
            throw new InvalidOperationException();

        return value.ClampPeriodic(
            MinValue, 
            MaxValue
        );
    }

    
    public bool Contains(Float64ScalarRange box, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return box.MinValue >= MinValue - zeroEpsilon &&
               box.MaxValue <= MaxValue + zeroEpsilon;
    }

    
    public bool ContainsUpperExclusive(double value, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return value >= MinValue - zeroEpsilon &&
               value < MaxValue + zeroEpsilon;
    }

    
    public bool Overlaps(Float64ScalarRange box, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return box.MaxValue >= MinValue - zeroEpsilon &&
               box.MinValue <= MaxValue + zeroEpsilon;
    }


    
    public bool Equals(Float64ScalarRange other)
    {
        return MinValue.Equals(other.MinValue) &&
               MaxValue.Equals(other.MaxValue);
    }
    
    
    public bool Equals(double minValue, double maxValue)
    {
        return MinValue.Equals(minValue) &&
               MaxValue.Equals(maxValue);
    }

    
    public override bool Equals(object? obj)
    {
        return obj is Float64ScalarRange other && Equals(other);
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(MinValue, MaxValue);
    }

    
    public override string ToString()
    {
        return $"[{MinValue}, {MaxValue}]";
    }
}