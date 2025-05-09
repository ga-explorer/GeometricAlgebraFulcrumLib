using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64
{
    public static class Float64ScalarSignalUtils
    {
        /// <summary>
        /// Regular search for time of minimum value of given function.
        /// Can be used for initial solution search for a later more accurate stage of search
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static double GetSampledMinValueTime(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            var tValues = 
                timeRange.GetLinearSamples(sampleCount, false);
            
            var minValueTime = timeRange.MinValue;
            var minValue = timeToValueMap(timeRange.MinValue);

            foreach (var t in tValues)
            {
                var v = timeToValueMap(t);

                if (minValue > v)
                {
                    minValue = v;
                    minValueTime = t;
                }
            }

            return minValueTime;
        }
        
        /// <summary>
        /// Regular search for time of maximum value of given function.
        /// Can be used for initial solution search for a later more accurate stage of search
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static double GetSampledMaxValueTime(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            var tValues = 
                timeRange.GetLinearSamples(sampleCount, false);
        
            var maxValueTime = timeRange.MinValue;
            var maxValue = timeToValueMap(timeRange.MinValue);

            foreach (var t in tValues)
            {
                var v = timeToValueMap(t);

                if (maxValue < v)
                {
                    maxValue = v;
                    maxValueTime = t;
                }
            }

            return maxValueTime;
        }
    
        /// <summary>
        /// Regular search for times of minimum and maximum values of given function.
        /// Can be used for initial solution search for a later more accurate stage of search
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static Pair<double> GetSampledMinMaxValueTime(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            var tValues = 
                timeRange.GetLinearSamples(sampleCount, false);
        
            var minValueTime = timeRange.MinValue;
            var minValue = timeToValueMap(timeRange.MinValue);

            var maxValueTime = timeRange.MinValue;
            var maxValue = minValue;

            foreach (var t in tValues)
            {
                var v = timeToValueMap(t);

                if (minValue > v)
                {
                    minValue = v;
                    minValueTime = t;
                }

                if (maxValue < v)
                {
                    maxValue = v;
                    maxValueTime = t;
                }
            }

            return new Pair<double>(minValueTime, maxValueTime);
        }
        
        /// <summary>
        /// Find time of minimum value of given function.
        /// Initial solution is found using regular search.
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static double FindMinValueTime(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            //return MathNet.Numerics.FindMinimum.OfScalarFunctionConstrained(
            //    timeToValueMap,
            //    timeRange.MinValue,
            //    timeRange.MaxValue,
            //    1e-12,
            //    10000
            //);

            var initialTime =
                GetSampledMinValueTime(timeRange, timeToValueMap, sampleCount);

            double time;

            try
            {
                time = MathNet.Numerics.FindMinimum.OfScalarFunction(
                    timeToValueMap,
                    initialTime,
                    1e-12,
                    10000
                );
            }
            catch (Exception)
            {
                time = initialTime;
            }

            return time;
        }
        
        /// <summary>
        /// Find time of maximum value of given function.
        /// Initial solution is found using regular search.
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static double FindMaxValueTime(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            //return MathNet.Numerics.FindMinimum.OfScalarFunctionConstrained(
            //    x => -timeToValueMap(x),
            //    timeRange.MinValue,
            //    timeRange.MaxValue,
            //    1e-12,
            //    10000
            //);

            var initialTime =
                GetSampledMaxValueTime(timeRange, timeToValueMap, sampleCount);

            double time;

            try
            {
                time = MathNet.Numerics.FindMinimum.OfScalarFunction(
                    t => -timeToValueMap(t),
                    initialTime,
                    1e-12,
                    10000
                );
            }
            catch (Exception)
            {
                time = initialTime;
            }

            return time;
        }
        
        //private static double GoldenSectionMinSearch(Func<double, double> timeToValueMap, double minTime, double maxTime, double tolerance = 1e-5, int maxIterations = 10000) 
        //{
        //    var midTime = Split(minTime, maxTime);
        //    var midTimeValue = timeToValueMap(midTime);

        //    while ((maxTime - minTime) > tolerance && maxIterations > 0) 
        //    {
        //        var time = Split(minTime, midTime);
        //        var value = timeToValueMap(time);

        //        if (value < midTimeValue) 
        //        {
        //            midTimeValue = value;
        //            maxTime = midTime;
        //            midTime = time;
        //        }
        //        else 
        //        {
        //            minTime = maxTime;
        //            maxTime = time;
        //        }

        //        maxIterations--;
        //    }

        //    return midTime;

        //    double Split(double x1, double x2)
        //    {
        //        return x1 + 0.6180339887498949 * (x2 - x1);
        //    }
        //}

        /// <summary>
        /// Find times of minimum and maximum values of given function.
        /// Initial solutions are found using regular search.
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static Pair<double> FindMinMaxValueTime(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            //var tMin = MathNet.Numerics.FindMinimum.OfScalarFunctionConstrained(
            //    timeToValueMap,
            //    timeRange.MinValue,
            //    timeRange.MaxValue,
            //    1e-5,
            //    10000
            //);

            //var tMax = MathNet.Numerics.FindMinimum.OfScalarFunctionConstrained(
            //    x => -timeToValueMap(x),
            //    timeRange.MinValue,
            //    timeRange.MaxValue,
            //    1e-5,
            //    10000
            //);

            var (initialMinTime, initialMaxTime) =
                GetSampledMinMaxValueTime(timeRange, timeToValueMap, sampleCount);

            double tMin;
            double tMax;

            try
            {
                tMin = MathNet.Numerics.FindMinimum.OfScalarFunction(
                    timeToValueMap,
                    initialMinTime,
                    1e-12,
                    10000
                );
            }
            catch (Exception)
            {
                tMin = initialMinTime;
            }

            try
            {
                tMax = MathNet.Numerics.FindMinimum.OfScalarFunction(
                    x => -timeToValueMap(x),
                    initialMaxTime,
                    1e-12,
                    10000
                );
            }
            catch (Exception)
            {
                tMax = initialMaxTime;
            }

            //Console.WriteLine($"({timeToValueMap(tMin)}, {timeToValueMap(tMax)})");

            return new Pair<double>(tMin, tMax);
        }
        
        /// <summary>
        /// Find minimum and maximum values of given function.
        /// Initial solutions are found using regular search.
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public static Float64ScalarRange FindValueRange(this Float64ScalarRange timeRange, Func<double, double> timeToValueMap, int sampleCount = 1024)
        {
            if (timeRange.Length.IsZero())
            {
                var value = timeToValueMap(timeRange.MinValue);
                return Float64ScalarRange.Create(value, value);
            }

            var (tMin, tMax) = 
                timeRange.FindMinMaxValueTime(timeToValueMap, sampleCount);

            return Float64ScalarRange.Create(
                timeToValueMap(tMin),
                timeToValueMap(tMax)
            );
        }
        
        /// <summary>
        /// Find minimum and maximum values of given function.
        /// Initial solutions are found using regular search.
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="timeToValueMap"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarRange FindValueRange(this Func<double, double> timeToValueMap, Float64ScalarRange timeRange, int sampleCount = 1024)
        {
            return timeRange.FindValueRange(timeToValueMap, sampleCount);
        }

        /// <summary>
        /// Find minimum and maximum values of given function.
        /// Initial solutions are found using regular search.
        /// </summary>
        /// <param name="timeToValueMap"></param>
        /// <param name="t2"></param>
        /// <param name="sampleCount"></param>
        /// <param name="t1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarRange FindValueRange(this Func<double, double> timeToValueMap, double t1, double t2, int sampleCount = 1024)
        {
            if (t1 < t2)
                return Float64ScalarRange.Create(t1, t2).FindValueRange(timeToValueMap, sampleCount);

            if (t1 > t2)
                return Float64ScalarRange.Create(t2, t1).FindValueRange(timeToValueMap, sampleCount);

            var v = timeToValueMap(t1);
            return Float64ScalarRange.Create(v, v);
        }
        
        /// <summary>
        /// Find minimum and maximum values of given function using regular search.
        /// </summary>
        /// <param name="timeValues"></param>
        /// <param name="timeToValueMap"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarRange FindValueRange(this IEnumerable<double> timeValues, Func<double, double> timeToValueMap)
        {
            var minValue = double.PositiveInfinity;
            var maxValue = double.NegativeInfinity;

            foreach (var value in timeValues.Select(timeToValueMap))
            {
                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;
            }

            return Float64ScalarRange.Create(minValue, maxValue);
        }
        
        /// <summary>
        /// Find minimum and maximum values of given function using regular search.
        /// </summary>
        /// <param name="valueList"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarRange FindValueRange(this IEnumerable<double> valueList)
        {
            var minValue = double.PositiveInfinity;
            var maxValue = double.NegativeInfinity;

            foreach (var value in valueList)
            {
                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;
            }

            return Float64ScalarRange.Create(minValue, maxValue);
        }

        /// <summary>
        /// Find minimum and maximum values of given function using regular search.
        /// </summary>
        /// <param name="timeValues"></param>
        /// <param name="timeToValueMap"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarRange FindValueRange(this Func<double, double> timeToValueMap, IEnumerable<double> timeValues)
        {
            var minValue = double.PositiveInfinity;
            var maxValue = double.NegativeInfinity;

            foreach (var value in timeValues.Select(timeToValueMap))
            {
                if (minValue > value) minValue = value;
                if (maxValue < value) maxValue = value;
            }

            return Float64ScalarRange.Create(minValue, maxValue);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToTimeSignal(this int value, Float64ScalarRange timeRange)
        {
            return Float64ScalarSignal.FiniteConstant(timeRange, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToTimeSignal(this double value, Float64ScalarRange timeRange)
        {
            return Float64ScalarSignal.FiniteConstant(timeRange, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToTimeSignal(this IFloat64Scalar value, Float64ScalarRange timeRange)
        {
            return Float64ScalarSignal.FiniteConstant(timeRange, value.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToTimeSignal(this int value, double minTime, double maxTime)
        {
            return Float64ScalarSignal.FiniteConstant(minTime, maxTime, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToTimeSignal(this double value, double minTime, double maxTime)
        {
            return Float64ScalarSignal.FiniteConstant(minTime, maxTime, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToTimeSignal(this IFloat64Scalar value, double minTime, double maxTime)
        {
            return Float64ScalarSignal.FiniteConstant(minTime, maxTime, value.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValue(this Float64ScalarSignal baseSignal, int t)
        {
            return baseSignal.GetValue(t);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValue(this Float64ScalarSignal baseSignal, Float64Scalar t)
        {
            return baseSignal.GetValue(t.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValue(this Float64ScalarSignal baseSignal, IFloat64Scalar t)
        {
            return baseSignal.GetValue(t.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarDerivativeSignal Derivative(this Float64ScalarSignal baseSignal)
        {
            return Float64ScalarDerivativeSignal.Create(baseSignal);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSegmentSignal Segment(this Float64ScalarSignal baseSignal, Float64ScalarRange timeRange)
        {
            return Float64ScalarSegmentSignal.Finite(timeRange, baseSignal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSegmentSignal Segment(this Float64ScalarSignal baseSignal, Float64Scalar timeMin, Float64Scalar timeMax)
        {
            return Float64ScalarSegmentSignal.Finite(timeMin, timeMax, baseSignal);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSegmentSignal SegmentFromRelativeTime(this Float64ScalarSignal baseSignal, Float64Scalar timeMin, Float64Scalar timeMax)
        {
            return Float64ScalarSegmentSignal.FiniteFromRelativeTime(baseSignal, timeMin, timeMax);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal Repeat(this Float64ScalarSignal baseSignal, int count)
        {
            return count == 1
                ? baseSignal
                : Float64ScalarRepeatedSignal.Create(baseSignal, count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal Repeat(this Float64ScalarSignal baseSignal, int count, double time1, double time2)
        {
            return baseSignal
                .Repeat(count)
                .MapTimeRangeTo(time1, time2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal Repeat(this Float64ScalarSignal baseSignal, int count, double time1, double time2, double value1, double value2)
        {
            return baseSignal
                .Repeat(count)
                .MapTimeRangeTo(time1, time2)
                .MapValueRangeTo(value1, value2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarListSignal Concat(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
        {
            return Float64ScalarListSignal.Finite(baseSignal1, baseSignal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarListSignal Concat(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2, params Float64ScalarSignal[] scalarList)
        {
            return Float64ScalarListSignal.Finite(baseSignal1, baseSignal2, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarListSignal Concat(this IEnumerable<Float64ScalarSignal> scalarList)
        {
            return Float64ScalarListSignal.Finite(scalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSmoothBlendSignal Blend(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2, double blendTimeMin, double blendTimeMax)
        {
            return Float64ScalarSmoothBlendSignal.Finite(blendTimeMin, blendTimeMax, baseSignal1, baseSignal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSmoothBlendSignal ConcatBlend(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2, double blendTimeLength)
        {
            var scalar2 = baseSignal2.OffsetTimeMinTo(baseSignal1.MaxTime - blendTimeLength);

            return Float64ScalarSmoothBlendSignal.Finite(baseSignal1.MaxTime - blendTimeLength, 
                scalar2.MinTime + blendTimeLength, baseSignal1, scalar2);
        }
        
        public static Float64ScalarListSignal ConcatBlend(this IReadOnlyList<Float64ScalarSignal> baseSignalList, double blendTimeLength)
        {
            if (blendTimeLength <= 0)
                return baseSignalList.Concat();

            if (baseSignalList.Count < 2)
                throw new InvalidOperationException();

            if (baseSignalList.Any(s => s.TimeRangeLength <= blendTimeLength))
                throw new InvalidOperationException();

            var scalarList = new List<Float64ScalarSignal>(
                baseSignalList.Count * 2 - 1
            )
            {
                baseSignalList[0].Segment(
                    baseSignalList[0].MinTime,
                    baseSignalList[0].MaxTime - blendTimeLength
                )
            };

            var baseSignal1 = baseSignalList[0];
            for (var i = 1; i < baseSignalList.Count; i++)
            {
                var baseSignal2 = baseSignalList[i].OffsetTimeMinTo(baseSignal1.MaxTime);

                scalarList.Add(
                    baseSignal1.Blend(
                        baseSignal2, 
                        baseSignal1.MaxTime - blendTimeLength,
                        baseSignal2.MinTime + blendTimeLength
                    )
                );

                var s = i == baseSignalList.Count - 1 ? 0 : blendTimeLength;

                scalarList.Add(
                    baseSignal2.Segment(
                        baseSignal2.MinTime + blendTimeLength,
                        baseSignal2.MaxTime - s
                    )
                );

                baseSignal1 = baseSignal2;
            }

            return Float64ScalarListSignal.Finite(scalarList);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarPlusSignal Plus(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
        {
            return Float64ScalarPlusSignal.Finite(baseSignal1, baseSignal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarPlusSignal Plus(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2, params Float64ScalarSignal[] scalarList)
        {
            return Float64ScalarPlusSignal.Finite(baseSignal1, baseSignal2, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarPlusSignal Plus(this IEnumerable<Float64ScalarSignal> scalarList)
        {
            return Float64ScalarPlusSignal.Finite(scalarList);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarTimesSignal Times(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2)
        {
            return Float64ScalarTimesSignal.Finite(baseSignal1, baseSignal2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarTimesSignal Times(this Float64ScalarSignal baseSignal1, Float64ScalarSignal baseSignal2, params Float64ScalarSignal[] scalarList)
        {
            return Float64ScalarTimesSignal.Finite(baseSignal1, baseSignal2, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarTimesSignal Times(this IEnumerable<Float64ScalarSignal> scalarList)
        {
            return Float64ScalarTimesSignal.Finite(scalarList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal NegativeTime(this Float64ScalarSignal baseSignal)
        {
            var timeMap = Float64AffineMap1D.CreateScale(-1d);

            return baseSignal.MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ScaleTimeBy(this Float64ScalarSignal baseSignal, double timeScaling)
        {
            var timeMap = Float64AffineMap1D.CreateScale(timeScaling);

            return baseSignal.MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ScaleTimeUsing(this Float64ScalarSignal baseSignal, double inTime, double outTime)
        {
            return baseSignal.ScaleTimeBy(outTime / inTime);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetTimeBy(this Float64ScalarSignal baseSignal, double timeOffset)
        {
            return baseSignal.MapTimeUsing(
                Float64AffineMap1D.CreateTranslate(timeOffset)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetTimeUsing(this Float64ScalarSignal baseSignal, double inTime, double outTime)
        {
            return baseSignal.OffsetTimeBy(outTime - inTime);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetTimeMinTo(this Float64ScalarSignal baseSignal, double outTimeMin)
        {
            return baseSignal.OffsetTimeBy(outTimeMin - baseSignal.MinTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetTimeMidTo(this Float64ScalarSignal baseSignal, double outTimeMid)
        {
            return baseSignal.OffsetTimeBy(outTimeMid - baseSignal.MidTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetTimeMaxTo(this Float64ScalarSignal baseSignal, double outTimeMax)
        {
            return baseSignal.OffsetTimeBy(outTimeMax - baseSignal.MaxTime);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapTimeUsing(this Float64ScalarSignal baseSignal, Float64AffineMap1D affineMap)
        {
            if (affineMap.IsIdentity())
                return baseSignal;

            if (baseSignal is Float64ScalarAffineMappedTimeSignal affineScalar)
                return Float64ScalarAffineMappedSignal.Create(
                    affineScalar.BaseSignal,
                    affineMap * affineScalar.AffineMap
                );

            return Float64ScalarAffineMappedTimeSignal.Create(
                baseSignal,
                affineMap
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapTimeUsing(this Float64ScalarSignal baseSignal, Float64ScalarRange inTimeRange, Float64ScalarRange outTimeRange)
        {
            var timeMap = Float64AffineMap1D.CreateFromRanges(
                inTimeRange, 
                outTimeRange
            );

            return baseSignal.MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapTimeUsing(this Float64ScalarSignal baseSignal, double inTime1, double inTime2, double outTime1, double outTime2)
        {
            var timeMap = Float64AffineMap1D.CreateFromRanges(
                inTime1, 
                inTime2, 
                outTime1, 
                outTime2
            );

            return baseSignal.MapTimeUsing(timeMap);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapTimeRangeTo(this Float64ScalarSignal baseSignal, Float64ScalarRange timeRange)
        {
            if (baseSignal.TimeRange.Equals(timeRange))
                return baseSignal;

            return baseSignal.MapTimeUsing(
                baseSignal.TimeRange,
                timeRange
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapTimeRangeTo(this Float64ScalarSignal baseSignal, double outTime1, double outTime2)
        {
            if (baseSignal.TimeRange.Equals(outTime1, outTime2))
                return baseSignal;

            return baseSignal.MapTimeUsing(
                baseSignal.MinTime,
                baseSignal.MaxTime,
                outTime1,
                outTime2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FlipTimeRange(this Float64ScalarSignal baseSignal)
        {
            return baseSignal.MapTimeUsing(
                baseSignal.MinTime,
                baseSignal.MaxTime,
                baseSignal.MaxTime,
                baseSignal.MinTime
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FlipTimeRange(this Float64ScalarSignal baseSignal, double time1, double time2)
        {
            return baseSignal.MapTimeUsing(
                time1,
                time2,
                time2,
                time1
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal NegativeValue(this Float64ScalarSignal baseSignal)
        {
            var valueMap = Float64AffineMap1D.CreateScale(-1d);

            return baseSignal.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ScaleValueBy(this Float64ScalarSignal baseSignal, double valueScaling)
        {
            var valueMap = Float64AffineMap1D.CreateScale(valueScaling);

            return baseSignal.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ScaleValueUsing(this Float64ScalarSignal baseSignal, double inValue, double outValue)
        {
            return baseSignal.ScaleValueBy(outValue / inValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetValueBy(this Float64ScalarSignal baseSignal, double valueOffset)
        {
            var valueMap = Float64AffineMap1D.CreateTranslate(valueOffset);

            return baseSignal.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetValueUsing(this Float64ScalarSignal baseSignal, double inValue, double outValue)
        {
            return baseSignal.OffsetValueBy(outValue - inValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetValueMinTo(this Float64ScalarSignal baseSignal, double outValueMin)
        {
            return baseSignal.OffsetValueBy(outValueMin - baseSignal.GetValueRange().MinValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetValueMidTo(this Float64ScalarSignal baseSignal, double outValueMid)
        {
            return baseSignal.OffsetValueBy(outValueMid - baseSignal.GetValueRange().MidValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal OffsetValueMaxTo(this Float64ScalarSignal baseSignal, double outValueMax)
        {
            return baseSignal.OffsetValueBy(outValueMax - baseSignal.GetValueRange().MaxValue);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapValueUsing(this Float64ScalarSignal baseSignal, Float64AffineMap1D affineMap)
        {
            if (affineMap.IsIdentity())
                return baseSignal;

            if (baseSignal is Float64ScalarAffineMappedSignal affineScalar)
                return Float64ScalarAffineMappedSignal.Create(
                    affineScalar.BaseSignal,
                    affineMap * affineScalar.AffineMap
                );

            return Float64ScalarAffineMappedSignal.Create(baseSignal, affineMap);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapValueUsing(this Float64ScalarSignal baseSignal, Float64ScalarRange inValueRange, Float64ScalarRange outValueRange)
        {
            var valueMap = Float64AffineMap1D.CreateFromRanges(
                inValueRange, 
                outValueRange
            );

            return baseSignal.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapValueUsing(this Float64ScalarSignal baseSignal, double inValue1, double inValue2, double outValue1, double outValue2)
        {
            var valueMap = Float64AffineMap1D.CreateFromRanges(
                inValue1, 
                inValue2, 
                outValue1, 
                outValue2
            );

            return baseSignal.MapValueUsing(valueMap);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapValueRangeTo(this Float64ScalarSignal baseSignal, Float64ScalarRange valueRange)
        {
            return baseSignal.MapValueUsing(
                baseSignal.GetValueRange(),
                valueRange
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal MapValueRangeTo(this Float64ScalarSignal baseSignal, double outValue1, double outValue2)
        {
            var (inValue1, inValue2) =
                baseSignal.GetValueRange();

            return baseSignal.MapValueUsing(
                inValue1,
                inValue2,
                outValue1,
                outValue2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FlipValueRange(this Float64ScalarSignal baseSignal)
        {
            var (valueMin, valueMax) = 
                baseSignal.GetValueRange();

            return baseSignal.MapValueUsing(
                valueMin,
                valueMax,
                valueMax,
                valueMin
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal FlipValueRange(this Float64ScalarSignal baseSignal, double value1, double value2)
        {
            return baseSignal.MapValueUsing(
                value1,
                value2,
                value2,
                value1
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64ScalarSignal ToFloat64ScalarSignal<T>(this Float64Trajectory<T> baseSignal, Func<T, double> valueMap)
        {
            return Float64ScalarMappedTrajectorySignal<T>.Create(baseSignal, valueMap);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64SampledTimeSignal GetSampledSignal(this Float64ScalarSignal baseSignal, int sampleCount)
        {
            return Float64SampledTimeSignal.CreateNonPeriodic(
                sampleCount, 
                baseSignal.MinTime, 
                baseSignal.MaxTime, 
                baseSignal.GetValue
            );
        }

    }
}
