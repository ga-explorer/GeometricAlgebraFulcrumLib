﻿using System.Diagnostics;
using System.Runtime.CompilerServices;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace NumericalGeometryLib.BasicMath.Tuples
{
    public sealed record Float64Interval :
        IGeometricElement
    {
        public static Float64Interval EmptyInterval { get; }
            = new Float64Interval(
                0d, 
                0d,
                true, 
                true
            );

        public static Float64Interval FullInterval { get; }
            = new Float64Interval(
                double.NegativeInfinity, 
                double.PositiveInfinity, 
                true, 
                true
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Interval Create(double minValue, double maxValue, bool excludeMinValue = false, bool excludeMaxValue = false)
        {
            return new Float64Interval(
                minValue, 
                maxValue, 
                excludeMinValue, 
                excludeMaxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Interval CreateFromInfinity(double maxValue, bool excludeMaxValue = false)
        {
            return new Float64Interval(
                double.NegativeInfinity, 
                maxValue, 
                true, 
                excludeMaxValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Interval CreateToInfinity(double minValue, bool excludeMinValue = false)
        {
            return new Float64Interval(
                minValue, 
                double.PositiveInfinity, 
                excludeMinValue, 
                true
            );
        }


        public bool ExcludeMinValue { get; }

        public bool ExcludeMaxValue { get; }

        public double MinValue { get; }

        public double MaxValue { get; }

        public double Length 
            => MaxValue - MinValue;
        
        public bool IsFullInterval 
            => double.IsNegativeInfinity(MinValue) &&
               double.IsPositiveInfinity(MaxValue);

        public bool IsEmptyInterval 
            => MinValue == MaxValue && 
               ExcludeMinValue && 
               ExcludeMaxValue;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Interval(double minValue, double maxValue, bool excludeMinValue = false, bool excludeMaxValue = false)
        {
            if (minValue > maxValue)
                minValue = maxValue;

            ExcludeMinValue = double.IsFinite(minValue) && excludeMinValue;
            ExcludeMaxValue = double.IsFinite(maxValue) && excludeMaxValue;

            MinValue = minValue;
            MaxValue = maxValue;

            IsValid();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(double value)
        {
            Debug.Assert(!double.IsNaN(value));

            if (IsEmptyInterval)
                return false;

            if (IsFullInterval)
                return double.IsFinite(value);

            if (ExcludeMinValue)
                return ExcludeMinValue
                    ? value > MinValue && value < MaxValue
                    : value > MinValue && value <= MaxValue;

            return ExcludeMinValue 
                ? value > MinValue && value <= MaxValue 
                : value >= MinValue && value <= MaxValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Interval Intersect(Float64Interval interval2)
        {
            if (IsEmptyInterval || interval2.IsEmptyInterval)
                return EmptyInterval;

            var f1 = Contains(interval2.MinValue);
            var f2 = Contains(interval2.MaxValue);

            if (f1) return f2
                ? interval2
                : new Float64Interval(
                    interval2.MinValue,
                    MaxValue,
                    interval2.ExcludeMinValue,
                    ExcludeMaxValue
                );

            return f2
                ? new Float64Interval(
                    MinValue,
                    interval2.MaxValue,
                    ExcludeMinValue,
                    interval2.ExcludeMaxValue
                )
                : EmptyInterval;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return MinValue.IsValid() && MaxValue.IsValid() && MinValue <= MaxValue;
        }
    }
}