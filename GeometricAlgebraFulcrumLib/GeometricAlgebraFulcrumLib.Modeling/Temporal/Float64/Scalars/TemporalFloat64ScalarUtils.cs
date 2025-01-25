using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars
{
    public static class TemporalFloat64ScalarUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValueAtTime(this TemporalFloat64Scalar baseScalar, int t)
        {
            return baseScalar.GetValue(t);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValueAtTime(this TemporalFloat64Scalar baseScalar, Float64Scalar t)
        {
            return baseScalar.GetValue(t.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValueAtTime(this TemporalFloat64Scalar baseScalar, IFloat64Scalar t)
        {
            return baseScalar.GetValue(t.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscParametricScalar ToTemporalScalar(this IFloat64ParametricScalar scalar)
        {
            return TscParametricScalar.Create(scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscParametricPolarAngle ToTemporalScalar(this IParametricPolarAngle angle)
        {
            return TscParametricPolarAngle.Create(angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscDerivative Derivative(this TemporalFloat64Scalar baseScalar)
        {
            return TscDerivative.Create(baseScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscSegment Segment(this TemporalFloat64Scalar baseScalar, Float64Scalar timeMin, Float64Scalar timeMax)
        {
            return TscSegment.Create(baseScalar, timeMin, timeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscSegment SegmentFromRelativeTime(this TemporalFloat64Scalar baseScalar, Float64Scalar timeMin, Float64Scalar timeMax)
        {
            return TscSegment.CreateFromRelativeTime(baseScalar, timeMin, timeMax);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar Repeat(this TemporalFloat64Scalar baseScalar, int count)
        {
            return count == 1
                ? baseScalar
                : TscRepeated.Create(baseScalar, count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar Repeat(this TemporalFloat64Scalar baseScalar, int count, double time1, double time2)
        {
            return baseScalar
                .Repeat(count)
                .MapTimeRangeTo(time1, time2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar Repeat(this TemporalFloat64Scalar baseScalar, int count, double time1, double time2, double value1, double value2)
        {
            return baseScalar
                .Repeat(count)
                .MapTimeRangeTo(time1, time2)
                .MapValueRangeTo(value1, value2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscList Concat(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2)
        {
            return TscList.Create(baseScalar1, baseScalar2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscList Concat(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, params TemporalFloat64Scalar[] scalarList)
        {
            return TscList.Create(baseScalar1, baseScalar2, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscList Concat(this IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
        {
            return TscList.Create(scalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscBlend Blend(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, double blendTimeMin, double blendTimeMax)
        {
            return TscBlend.Create(baseScalar1, baseScalar2, blendTimeMin, blendTimeMax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscBlend ConcatBlend(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, double blendTimeLength)
        {
            var scalar2 = baseScalar2.OffsetTimeMinTo(baseScalar1.MaxTime - blendTimeLength);

            return TscBlend.Create(
                baseScalar1, 
                scalar2, 
                baseScalar1.MaxTime - blendTimeLength, 
                scalar2.MinTime + blendTimeLength
            );
        }
        
        public static TscList ConcatBlend(this IReadOnlyList<TemporalFloat64Scalar> baseScalarList, double blendTimeLength)
        {
            if (blendTimeLength <= 0)
                return baseScalarList.Concat();

            if (baseScalarList.Count < 2)
                throw new InvalidOperationException();

            if (baseScalarList.Any(s => s.TimeRangeLength <= blendTimeLength))
                throw new InvalidOperationException();

            var scalarList = new List<TemporalFloat64Scalar>(
                baseScalarList.Count * 2 - 1
            )
            {
                baseScalarList[0].Segment(
                    baseScalarList[0].MinTime,
                    baseScalarList[0].MaxTime - blendTimeLength
                )
            };

            var baseScalar1 = baseScalarList[0];
            for (var i = 1; i < baseScalarList.Count; i++)
            {
                var baseScalar2 = baseScalarList[i].OffsetTimeMinTo(baseScalar1.MaxTime);

                scalarList.Add(
                    baseScalar1.Blend(
                        baseScalar2, 
                        baseScalar1.MaxTime - blendTimeLength,
                        baseScalar2.MinTime + blendTimeLength
                    )
                );

                var s = i == baseScalarList.Count - 1 ? 0 : blendTimeLength;

                scalarList.Add(
                    baseScalar2.Segment(
                        baseScalar2.MinTime + blendTimeLength,
                        baseScalar2.MaxTime - s
                    )
                );

                baseScalar1 = baseScalar2;
            }

            return TscList.Create(scalarList);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscPlus Plus(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2)
        {
            return TscPlus.Create(baseScalar1, baseScalar2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscPlus Plus(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, params TemporalFloat64Scalar[] scalarList)
        {
            return TscPlus.Create(baseScalar1, baseScalar2, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscPlus Plus(this IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
        {
            return TscPlus.Create(scalarList);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscTimes Times(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2)
        {
            return TscTimes.Create(baseScalar1, baseScalar2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscTimes Times(this TemporalFloat64Scalar baseScalar1, TemporalFloat64Scalar baseScalar2, params TemporalFloat64Scalar[] scalarList)
        {
            return TscTimes.Create(baseScalar1, baseScalar2, scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscTimes Times(this IReadOnlyCollection<TemporalFloat64Scalar> scalarList)
        {
            return TscTimes.Create(scalarList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar NegativeTime(this TemporalFloat64Scalar baseScalar)
        {
            var timeMap = Float64AffineMap1D.Create(-1);

            return baseScalar.MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar ScaleTimeBy(this TemporalFloat64Scalar baseScalar, double timeScaling)
        {
            var timeMap = Float64AffineMap1D.Create(timeScaling);

            return baseScalar.MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar ScaleTimeUsing(this TemporalFloat64Scalar baseScalar, double inTime, double outTime)
        {
            return baseScalar.ScaleTimeBy(outTime / inTime);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetTimeBy(this TemporalFloat64Scalar baseScalar, double timeOffset)
        {
            if (timeOffset.IsZero()) 
                return baseScalar;
            
            return baseScalar.MapTimeUsing(
                Float64AffineMap1D.Create(
                    Float64Scalar.One, 
                    timeOffset
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetTimeUsing(this TemporalFloat64Scalar baseScalar, double inTime, double outTime)
        {
            return baseScalar.OffsetTimeBy(outTime - inTime);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetTimeMinTo(this TemporalFloat64Scalar baseScalar, double outTimeMin)
        {
            return baseScalar.OffsetTimeBy(outTimeMin - baseScalar.MinTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetTimeMaxTo(this TemporalFloat64Scalar baseScalar, double outTimeMax)
        {
            return baseScalar.OffsetTimeBy(outTimeMax - baseScalar.MaxTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetTimeMidTo(this TemporalFloat64Scalar baseScalar, double outTimeMid)
        {
            return baseScalar.OffsetTimeBy(outTimeMid - baseScalar.MidTime);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapTimeUsing(this TemporalFloat64Scalar baseScalar, Float64AffineMap1D timeMap)
        {
            if (baseScalar is TscAffineMappedTime affineScalar)
                return new TscAffineMappedValue(
                    affineScalar.BaseScalar,
                    timeMap * affineScalar.TimeMap
                );

            return new TscAffineMappedTime(
                baseScalar,
                timeMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapTimeUsing(this TemporalFloat64Scalar baseScalar, double inTime1, double inTime2, double outTime1, double outTime2)
        {
            var timeMap = Float64AffineMap1D.Create(inTime1, inTime2, outTime1, outTime2);

            return baseScalar.MapTimeUsing(timeMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapTimeRangeTo(this TemporalFloat64Scalar baseScalar, double outTime1, double outTime2)
        {
            return baseScalar.MapTimeUsing(
                baseScalar.MinTime,
                baseScalar.MaxTime,
                outTime1,
                outTime2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar FlipTimeRange(this TemporalFloat64Scalar baseScalar)
        {
            return baseScalar.MapTimeUsing(
                baseScalar.MinTime,
                baseScalar.MaxTime,
                baseScalar.MaxTime,
                baseScalar.MinTime
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar FlipTimeRange(this TemporalFloat64Scalar baseScalar, double time1, double time2)
        {
            return baseScalar.MapTimeUsing(
                time1,
                time2,
                time2,
                time1
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar Periodic(this TemporalFloat64Scalar baseScalar)
        {
            return baseScalar as TscPeriodic ?? new TscPeriodic(baseScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar NegativeValue(this TemporalFloat64Scalar baseScalar)
        {
            var valueMap = Float64AffineMap1D.Create(-1);

            return baseScalar.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar ScaleValueBy(this TemporalFloat64Scalar baseScalar, double valueScaling)
        {
            var valueMap = Float64AffineMap1D.Create(valueScaling);

            return baseScalar.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar ScaleValueUsing(this TemporalFloat64Scalar baseScalar, double inValue, double outValue)
        {
            return baseScalar.ScaleValueBy(outValue / inValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetValueBy(this TemporalFloat64Scalar baseScalar, double valueOffset)
        {
            var valueMap = Float64AffineMap1D.Create(1, valueOffset);

            return baseScalar.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetValueUsing(this TemporalFloat64Scalar baseScalar, double inValue, double outValue)
        {
            return baseScalar.OffsetValueBy(outValue - inValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetValueMinTo(this TemporalFloat64Scalar baseScalar, double outValueMin)
        {
            return baseScalar.OffsetValueBy(outValueMin - baseScalar.MinValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetValueMaxTo(this TemporalFloat64Scalar baseScalar, double outValueMax)
        {
            return baseScalar.OffsetValueBy(outValueMax - baseScalar.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar OffsetValueMidTo(this TemporalFloat64Scalar baseScalar, double outValueMid)
        {
            return baseScalar.OffsetValueBy(outValueMid - baseScalar.MidValue);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapValueUsing(this TemporalFloat64Scalar baseScalar, Func<double, double> valueMap)
        {
            if (baseScalar is TscMappedValue mappedScalar)
                return new TscMappedValue(
                    mappedScalar.BaseScalar,
                    t => valueMap(mappedScalar.ValueMap(t))
                );
            
            return new TscMappedValue(baseScalar, valueMap);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TscMappedValue<T> MapValueUsing<T>(this TemporalFloat64Scalar baseScalar, Func<double, T> valueMap)
        {
            return new TscMappedValue<T>(baseScalar, valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapValueUsing(this TemporalFloat64Scalar baseScalar, Float64AffineMap1D valueMap)
        {
            if (baseScalar is TscAffineMappedValue affineScalar)
                return new TscAffineMappedValue(
                    affineScalar.BaseScalar,
                    valueMap * affineScalar.ValueMap
                );

            return new TscAffineMappedValue(
                baseScalar,
                valueMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapValueUsing(this TemporalFloat64Scalar baseScalar, double inValue1, double inValue2, double outValue1, double outValue2)
        {
            var valueMap = Float64AffineMap1D.Create(inValue1, inValue2, outValue1, outValue2);

            return baseScalar.MapValueUsing(valueMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar MapValueRangeTo(this TemporalFloat64Scalar baseScalar, double outValue1, double outValue2)
        {
            var (valueMin, valueMax) =
                baseScalar.ValueRange;

            return baseScalar.MapValueUsing(
                valueMin,
                valueMax,
                outValue1,
                outValue2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar FlipValueRange(this TemporalFloat64Scalar baseScalar)
        {
            var (valueMin, valueMax) = 
                baseScalar.ValueRange;

            return baseScalar.MapValueUsing(
                valueMin,
                valueMax,
                valueMax,
                valueMin
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TemporalFloat64Scalar FlipValueRange(this TemporalFloat64Scalar baseScalar, double value1, double value2)
        {
            return baseScalar.MapValueUsing(
                value1,
                value2,
                value2,
                value1
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Signal GetSampledSignal(this TemporalFloat64Scalar baseScalar, int sampleCount)
        {
            return Float64Signal.CreateNonPeriodic(
                sampleCount, 
                baseScalar.MinTime, 
                baseScalar.MaxTime, 
                baseScalar.GetValue, 
                true
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetMatlabPlotCode(this TemporalFloat64Scalar baseScalar, int sampleCount = 1024)
        {
            var (tMin, tMax) = baseScalar.TimeRange.ExpandByFactor(0.1);
            var (vMin, vMax) = baseScalar.ValueRange.ExpandByFactor(0.1);

            return baseScalar.GetMatlabPlotCode(
                tMin, 
                tMax, 
                vMin,
                vMax,
                sampleCount
            );
        }

        public static string GetMatlabPlotCode(this TemporalFloat64Scalar baseScalar, double tMin, double tMax, double vMin, double vMax, int sampleCount = 1024)
        {
            var tValues =
                tMin.GetLinearRange(tMax, sampleCount).ToArray();

            var fValues =
                tValues.Select(baseScalar.GetValue).ToArray();

            var composer = new StringBuilder();

            var tArrayText =
                tValues.Select(t => t.ToString("G"))
                    .Concatenate(", ", "[", "]");

            var fArrayText =
                fValues.Select(f => f.ToString("G"))
                    .Concatenate(", ", "[", "]");

            var tMinText = tMin.ToString("G");
            var tMaxText = tMax.ToString("G");

            var vMinText = vMin.ToString("G");
            var vMaxText = vMax.ToString("G");

            composer.AppendLine($"t = {tArrayText};");
            composer.AppendLine($"f = {fArrayText};");
            composer.AppendLine("plot(t, f);");
            composer.AppendLine($"axis([{tMinText}, {tMaxText}, {vMinText}, {vMaxText}]);");

            return composer.ToString();
        }


    }
}
