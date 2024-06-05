using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars;

public readonly struct ScalarRange<T> :
    IAlgebraicElement,
    IScalarAlgebraElement<T>,
    IPair<Scalar<T>>,
    IEquatable<ScalarRange<T>>
{
    public static ScalarRange<T> Infinite(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(
            scalarProcessor.NegativeInfinity,
            scalarProcessor.PositiveInfinity
        );

    public static ScalarRange<T> ZeroToInfinity(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(
            scalarProcessor.Zero,
            scalarProcessor.PositiveInfinity
        );

    public static ScalarRange<T> InfinityToZero(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(
            scalarProcessor.NegativeInfinity,
            scalarProcessor.Zero
        );

    public static ScalarRange<T> ZeroToOne(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(scalarProcessor.Zero, scalarProcessor.One);

    public static ScalarRange<T> ZeroToPi(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(scalarProcessor.Zero, scalarProcessor.Pi);

    public static ScalarRange<T> ZeroToTwoPi(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(scalarProcessor.Zero, scalarProcessor.PiTimes2);

    public static ScalarRange<T> NegativeOneToZero(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(scalarProcessor.MinusOne, scalarProcessor.Zero);

    public static ScalarRange<T> NegativeOneToOne(IScalarProcessor<T> scalarProcessor)
        => new ScalarRange<T>(scalarProcessor.MinusOne, scalarProcessor.One);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> CreateAround(Scalar<T> center, Scalar<T> delta)
    {
        return !delta.IsNegative()
            ? new ScalarRange<T>(center - delta, center + delta)
            : new ScalarRange<T>(center + delta, center - delta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> Create(Scalar<T> value)
    {
        return value.IsPositive()
            ? new ScalarRange<T>(value.ScalarProcessor.Zero, value)
            : new ScalarRange<T>(value, value.ScalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> Create(Scalar<T> value1, Scalar<T> value2)
    {
        return value1 <= value2
            ? new ScalarRange<T>(value1, value2)
            : new ScalarRange<T>(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> Create(IPair<Scalar<T>> range)
    {
        var value1 = range.Item1;
        var value2 = range.Item2;

        return value1 < value2
            ? new ScalarRange<T>(value1, value2)
            : new ScalarRange<T>(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> Create(Scalar<T> value1, Scalar<T> value2, Scalar<T> value3)
    {
        var minValue = value1;
        var maxValue = value1;

        if (minValue > value2) minValue = value2;
        if (minValue > value3) minValue = value3;

        if (maxValue < value2) maxValue = value2;
        if (maxValue < value3) maxValue = value3;

        return new ScalarRange<T>(minValue, maxValue);
    }

    public static ScalarRange<T> Create(params Scalar<T>[] valuesList)
    {
        var scalarProcessor = valuesList[0].ScalarProcessor;

        var minValue = scalarProcessor.Zero;
        var maxValue = scalarProcessor.Zero;

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

        return new ScalarRange<T>(minValue, maxValue);
    }

    public static ScalarRange<T> Create(IReadOnlyList<Scalar<T>> valuesList)
    {
        var scalarProcessor = valuesList[0].ScalarProcessor;

        var minValue = scalarProcessor.Zero;
        var maxValue = scalarProcessor.Zero;

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

        return new ScalarRange<T>(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> Create(ScalarRange<T> b1, ScalarRange<T> b2)
    {
        return new ScalarRange<T>(
            b1.MinValue.Min(b2.MinValue),
            b1.MaxValue.Max(b2.MaxValue)
        );
    }

    public static ScalarRange<T> Create(params ScalarRange<T>[] rangeList)
    {
        var scalarProcessor = rangeList[0].ScalarProcessor;

        var minValue = scalarProcessor.Zero;
        var maxValue = scalarProcessor.Zero;

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

        return new ScalarRange<T>(minValue, maxValue);
    }

    public static ScalarRange<T> Create(IReadOnlyList<ScalarRange<T>> rangeList)
    {
        var scalarProcessor = rangeList[0].ScalarProcessor;

        var minValue = scalarProcessor.Zero;
        var maxValue = scalarProcessor.Zero;

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

        return new ScalarRange<T>(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> CreateFromIntersection(ScalarRange<T> b1, ScalarRange<T> b2)
    {
        return new ScalarRange<T>(
            b1.MinValue.Max(b2.MinValue),
            b1.MaxValue.Min(b2.MaxValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> CreateFromOuterBounds(ScalarRange<T> b1, ScalarRange<T> b2)
    {
        return new ScalarRange<T>(
            b1.MinValue.Min(b2.MinValue),
            b1.MaxValue.Max(b2.MaxValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator -(ScalarRange<T> b1)
    {
        return new ScalarRange<T>(
            -b1.MaxValue,
            -b1.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator +(ScalarRange<T> b1, Scalar<T> b2)
    {
        return new ScalarRange<T>(
            b1.MinValue + b2,
            b1.MaxValue + b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator +(Scalar<T> b1, ScalarRange<T> b2)
    {
        return new ScalarRange<T>(
            b1 + b2.MinValue,
            b1 + b2.MaxValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator -(ScalarRange<T> b1, Scalar<T> b2)
    {
        return new ScalarRange<T>(
            b1.MinValue - b2,
            b1.MaxValue - b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator -(Scalar<T> b1, ScalarRange<T> b2)
    {
        return new ScalarRange<T>(
            b1 - b2.MaxValue,
            b1 - b2.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator *(ScalarRange<T> b1, Scalar<T> b2)
    {
        if (b2.IsPositive())
            return new ScalarRange<T>(
                b1.MinValue * b2,
                b1.MaxValue * b2
            );

        return new ScalarRange<T>(
            b1.MaxValue * b2,
            b1.MinValue * b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator *(Scalar<T> b1, ScalarRange<T> b2)
    {
        if (b1.IsPositive())
            return new ScalarRange<T>(
                b2.MinValue * b1,
                b2.MaxValue * b1
            );

        return new ScalarRange<T>(
            b2.MaxValue * b1,
            b2.MinValue * b1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarRange<T> operator /(ScalarRange<T> b1, Scalar<T> b2)
    {
        if (b2.IsPositive())
            return new ScalarRange<T>(
                b1.MinValue / b2,
                b1.MaxValue / b2
            );

        return new ScalarRange<T>(
            b1.MaxValue / b2,
            b1.MinValue / b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ScalarRange<T> left, ScalarRange<T> right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ScalarRange<T> left, ScalarRange<T> right)
    {
        return !left.Equals(right);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => MinValue.ScalarProcessor;

    public Scalar<T> MinValue { get; }

    public Scalar<T> MaxValue { get; }

    public Scalar<T> MidValue
        => (MinValue + MaxValue) / 2;

    public Scalar<T> Length
        => MaxValue - MinValue;

    public Scalar<T> Item1
        => MinValue;

    public Scalar<T> Item2
        => MaxValue;

    public bool IsFinite
        => MinValue.IsFiniteNumber() &&
           MaxValue.IsFiniteNumber();

    //public bool IsInfinite
    //    => Scalar<T>.IsInfinity(MinValue) ||
    //       Scalar<T>.IsInfinity(MaxValue);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScalarRange(IScalar<T> minValue, IScalar<T> maxValue)
    {
        MinValue = minValue.ToScalar();
        MaxValue = maxValue.ToScalar();

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
    public void Deconstruct(out Scalar<T> minValue, out Scalar<T> maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> Intersect(ScalarRange<T> range2)
    {
        return CreateFromIntersection(this, range2);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> ClampPeriodic(Scalar<T> value)
    //{
    //    if (!IsFinite)
    //        throw new InvalidOperationException();

    //    return value.ClampPeriodic(MinValue, MaxValue);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> AffineMapToZeroOneRange(Scalar<T> value)
    {
        if (!IsFinite)
            throw new InvalidOperationException();

        return (value - MinValue) / (MaxValue - MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> AffineMapToRange(Scalar<T> value, Scalar<T> value1, Scalar<T> value2)
    {
        return AffineMapToZeroOneRange(value) * (value2 - value1) + value1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> AffineMapToRange(Scalar<T> value, IPair<Scalar<T>> valuePair, bool reverseLimits = false)
    {
        return reverseLimits
            ? AffineMapToZeroOneRange(value) * (valuePair.Item1 - valuePair.Item2) + valuePair.Item2
            : AffineMapToZeroOneRange(value) * (valuePair.Item2 - valuePair.Item1) + valuePair.Item1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> AffineMapToRange(Scalar<T> value, ScalarRange<T> range, bool reverseLimits = false)
    {
        if (!range.IsFinite)
            throw new InvalidOperationException();

        return reverseLimits
            ? range.MaxValue - AffineMapToZeroOneRange(value) * range.Length
            : range.MinValue + AffineMapToZeroOneRange(value) * range.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> Negative()
    {
        return new ScalarRange<T>(-MaxValue, -MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> Plus(Scalar<T> value)
    {
        return new ScalarRange<T>(MinValue + value, MaxValue + value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> Times(Scalar<T> value)
    {
        return value > ScalarProcessor.Zero
            ? new ScalarRange<T>(MinValue * value, MaxValue * value)
            : new ScalarRange<T>(MaxValue * value, MinValue * value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> Divide(Scalar<T> value)
    {
        return value > ScalarProcessor.Zero
            ? new ScalarRange<T>(MinValue / value, MaxValue / value)
            : new ScalarRange<T>(MaxValue / value, MinValue / value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ResetMinValue(Scalar<T> minValue)
    {
        return Create(minValue, MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ResetMaxValue(Scalar<T> maxValue)
    {
        return Create(MinValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ExpandBy(Scalar<T> delta)
    {
        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ExpandByFactor(Scalar<T> deltaPercent)
    {
        var delta = deltaPercent * (MaxValue - MinValue);

        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ExpandToInclude(Scalar<T> value)
    {
        if (value < MinValue)
            return new ScalarRange<T>(value, MaxValue);

        if (value > MaxValue)
            return new ScalarRange<T>(MinValue, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ExpandToInclude(Scalar<T> value1, Scalar<T> value2)
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

        return new ScalarRange<T>(minValue, maxValue);
    }

    public ScalarRange<T> ExpandToInclude(params Scalar<T>[] valuesList)
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

        return new ScalarRange<T>(minValue, maxValue);
    }

    public ScalarRange<T> ExpandToInclude(IEnumerable<Scalar<T>> valuesList)
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

        return new ScalarRange<T>(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarRange<T> ExpandToInclude(IPair<Scalar<T>> range)
    {
        return ExpandToInclude(
            range.Item1,
            range.Item2
        );
    }

    public ScalarRange<T> ExpandToInclude(params ScalarRange<T>[] rangeList)
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

        return new ScalarRange<T>(minValue, maxValue);
    }

    public ScalarRange<T> ExpandToInclude(IEnumerable<ScalarRange<T>> rangeList)
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

        return new ScalarRange<T>(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetOffsetFromMin(Scalar<T> value)
    {
        return value - MinValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetOffsetToMin(Scalar<T> value)
    {
        return MinValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetOffsetFromMax(Scalar<T> value)
    {
        return value - MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetOffsetToMax(Scalar<T> value)
    {
        return MaxValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetRelativeOffset(Scalar<T> value)
    {
        return (value - MinValue) / (MaxValue - MinValue);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IEnumerable<Scalar<T>> GetLinearSamples(int count, bool assumePeriodic)
    //{
    //    if (assumePeriodic && count < 1 || !assumePeriodic && count < 2)
    //        throw new ArgumentOutOfRangeException(nameof(count));

    //    return MinValue.GetLinearRange(MaxValue, count, assumePeriodic);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IEnumerable<Scalar<T>> GetLinearPeriodicSamples(int count)
    //{
    //    if (count < 1)
    //        throw new ArgumentOutOfRangeException(nameof(count));

    //    return MinValue.GetLinearPeriodicRange(MaxValue, count);
    //}

    //public ScalarRange<T>[] GetSubdivisions(int divisionCount)
    //{
    //    var length = Length / divisionCount;

    //    var minValue = MinValue;

    //    var minValues =
    //        Enumerable
    //            .Range(ScalarProcessor.Zero, divisionCount)
    //            .Select(i => i * length + minValue + epsilon)
    //            .ToArray();

    //    var maxValues =
    //        minValues
    //            .Select(v => v + length - epsilon)
    //            .ToArray();

    //    var divisions = new ScalarRange<T>[divisionCount];

    //    for (var i = 0; i < divisionCount; i++)
    //        divisions[i] = Create(
    //            minValues[i],
    //            maxValues[i]
    //        );

    //    return divisions;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool Contains(Scalar<T> value)
    //{
    //    return value >= MinValue - epsilon &&
    //           value <= MaxValue + epsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool Contains(ScalarRange<T> box)
    //{
    //    Debug.Assert(epsilon >= ScalarProcessor.Zero);

    //    return box.MinValue >= MinValue - epsilon &&
    //           box.MaxValue <= MaxValue + epsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool ContainsUpperExclusive(Scalar<T> value)
    //{
    //    Debug.Assert(epsilon >= ScalarProcessor.Zero);

    //    return value >= MinValue - epsilon &&
    //           value < MaxValue + epsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool Overlaps(ScalarRange<T> box)
    //{
    //    Debug.Assert(epsilon >= ScalarProcessor.Zero);

    //    return box.MaxValue >= MinValue - epsilon &&
    //           box.MinValue <= MaxValue + epsilon;
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ScalarRange<T> other)
    {
        return MinValue.Equals(other.MinValue) &&
               MaxValue.Equals(other.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is ScalarRange<T> other && Equals(other);
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