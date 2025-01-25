using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Normalized;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars
{
    public abstract class TemporalFloat64Scalar
        : TemporalValue<double>
    {
        public static TsConstant Zero()
        {
            return TsConstant.Create(0, -1, 1);
        }
        
        public static TsConstant Zero(double minTime, double maxTime)
        {
            return TsConstant.Create(0, minTime, maxTime);
        }

        public static TsConstant Constant(double value)
        {
            return TsConstant.Create(value, -1, 1);
        }

        public static TsConstant Constant(double value, double minTime, double maxTime)
        {
            return TsConstant.Create(value, minTime, maxTime);
        }

        public static TsComputed Computed(Func<double, double> timeMapFunc, double minTime, double maxTime)
        {
            return TsComputed.Create(timeMapFunc, minTime, maxTime);
        }
        
        public static TsComputed Computed(Func<double, double> timeMapFunc, Float64ScalarRange timeRange)
        {
            return TsComputed.Create(timeMapFunc, timeRange);
        }

        public static TsnRamp Ramp()
        {
            return TsnRamp.Instance;
        }
        
        public static TsnSharpStep SharpStep()
        {
            return TsnSharpStep.Instance;
        }
        
        public static TsnSharpRectangle SharpRectangle()
        {
            return TsnSharpRectangle.Instance;
        }

        public static TsnSmoothStep SmoothStep()
        {
            return TsnSmoothStep.Instance;
        }
        
        public static TemporalFloat64Scalar SmoothRectangle()
        {
            return TsnSmoothRectangle.Instance;
        }

        public static TsnFullCos FullCos()
        {
            return TsnFullCos.Instance;
        }
        
        public static TemporalFloat64Scalar FullCos(double minValue, double maxValue)
        {
            return TsnFullCos.Instance.MapValueRangeTo(minValue, maxValue);
        }

        public static TsnFullSin FullSin()
        {
            return TsnFullSin.Instance;
        }
        
        public static TsnHalfCos HalfCos()
        {
            return TsnHalfCos.Instance;
        }

        public static TsnHalfSin HalfSin()
        {
            return TsnHalfSin.Instance;
        }
        
        public static TsnTriangle Triangle()
        {
            return TsnTriangle.Default;
        }

        public static TsnTriangle Triangle(double vertexRelativeTime)
        {
            return TsnTriangle.Create(vertexRelativeTime);
        }

        
        public static implicit operator TemporalFloat64Scalar(IntegerSign s1)
        {
            return TsConstant.Create(s1.ToFloat64(), -1, 1);
        }

        public static implicit operator TemporalFloat64Scalar(double s1)
        {
            return TsConstant.Create(s1, -1, 1);
        }
        
        public static implicit operator TemporalFloat64Scalar(Float64Scalar s1)
        {
            return TsConstant.Create(s1, -1, 1);
        }

        public static TemporalFloat64Scalar operator +(TemporalFloat64Scalar s1)
        {
            return s1;
        }

        public static TemporalFloat64Scalar operator -(TemporalFloat64Scalar s1)
        {
            return s1.NegativeValue();
        }
        

        public static TemporalFloat64Scalar operator +(TemporalFloat64Scalar s1, double s2)
        {
            return s1.OffsetValueBy(s2);
        }
        
        public static TemporalFloat64Scalar operator +(double s1, TemporalFloat64Scalar s2)
        {
            return s2.OffsetValueBy(s1);
        }

        public static TemporalFloat64Scalar operator +(TemporalFloat64Scalar s1, TemporalFloat64Scalar s2)
        {
            return s1.Plus(s2);
        }
        

        public static TemporalFloat64Scalar operator -(TemporalFloat64Scalar s1, double s2)
        {
            return s1.OffsetValueBy(-s2);
        }
        
        public static TemporalFloat64Scalar operator -(double s1, TemporalFloat64Scalar s2)
        {
            return s2.NegativeValue().OffsetValueBy(s1);
        }

        public static TemporalFloat64Scalar operator -(TemporalFloat64Scalar s1, TemporalFloat64Scalar s2)
        {
            return s1.Plus(s2.NegativeValue());
        }
        
        
        public static TemporalFloat64Scalar operator *(TemporalFloat64Scalar s1, double s2)
        {
            return s1.ScaleValueBy(s2);
        }
        
        public static TemporalFloat64Scalar operator *(double s1, TemporalFloat64Scalar s2)
        {
            return s2.ScaleValueBy(s1);
        }

        public static TemporalFloat64Scalar operator *(TemporalFloat64Scalar s1, TemporalFloat64Scalar s2)
        {
            return s1.Times(s2);
        }


        private Float64ScalarRange? _valueRange;
        public Float64ScalarRange ValueRange
        {
            get
            {
                _valueRange ??= FindValueRange();

                return _valueRange.Value;
            }
        }
        
        public double ValueRangeLength 
            => ValueRange.Length.ScalarValue;

        public double MinValue 
            => ValueRange.MinValue.ScalarValue;

        public double MidValue 
            => ValueRange.MidValue.ScalarValue;
        
        public double MaxValue 
            => ValueRange.MaxValue.ScalarValue;

        
        protected double GetSampledMinValueTime(int sampleCount)
        {
            var tValues = 
                MinTime.GetLinearRange(MaxTime, sampleCount);
        
            var minValueTime = MinTime;
            var minValue = GetValue(MinTime);

            foreach (var t in tValues)
            {
                var v = GetValue(t);

                if (minValue > v)
                {
                    minValue = v;
                    minValueTime = t;
                }
            }

            return minValueTime;
        }
    
        protected double GetSampledMaxValueTime(int sampleCount)
        {
            var tValues = 
                MinTime.GetLinearRange(MaxTime, sampleCount);
        
            var maxValueTime = MinTime;
            var maxValue = GetValue(MinTime);

            foreach (var t in tValues)
            {
                var v = GetValue(t);

                if (maxValue < v)
                {
                    maxValue = v;
                    maxValueTime = t;
                }
            }

            return maxValueTime;
        }
    
        protected Pair<double> GetSampledMinMaxValueTime(int sampleCount)
        {
            var tValues = 
                MinTime.GetLinearRange(MaxTime, sampleCount);
        
            var minValueTime = MinTime;
            var minValue = GetValue(MinTime);

            var maxValueTime = MinTime;
            var maxValue = minValue;

            foreach (var t in tValues)
            {
                var v = GetValue(t);

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
        

        protected double FindMinValueTime(int sampleCount = 1024)
        {
            var initialTime = GetSampledMinValueTime(sampleCount);

            return MathNet.Numerics.FindMinimum.OfScalarFunction(
                GetValue,
                initialTime,
                1e-12,
                10000
            );
        }
        
        protected double FindMaxValueTime(int sampleCount = 1024)
        {
            var initialTime = GetSampledMaxValueTime(sampleCount);

            return MathNet.Numerics.FindMinimum.OfScalarFunction(
                x => -GetValue(x),
                initialTime,
                1e-12,
                10000
            );
        }
        
        protected Pair<double> FindMinMaxValueTime(int sampleCount = 1024)
        {
            var (initialMinTime, initialMaxTime) = 
                GetSampledMinMaxValueTime(sampleCount);
            
            var tMin = MathNet.Numerics.FindMinimum.OfScalarFunction(
                GetValue,
                initialMinTime,
                1e-12,
                10000
            );

            var tMax = MathNet.Numerics.FindMinimum.OfScalarFunction(
                x => -GetValue(x),
                initialMaxTime,
                1e-12,
                10000
            );

            return new Pair<double>(tMin, tMax);
        }
        
        protected virtual Float64ScalarRange FindValueRange()
        {
            var (tMin, tMax) = FindMinMaxValueTime();

            return Float64ScalarRange.Create(
                GetValue(tMin),
                GetValue(tMax)
            );
        }


        public virtual double GetDerivativeValue(double time)
        {
            return MathNet.Numerics.Differentiate.Derivative(GetValue, time, 1);
        }

        public TemporalFloat64Scalar RadiansToDegrees()
        {
            return this.MapValueUsing(v => v.RadiansToDegrees());
        }
        
        public TemporalFloat64Scalar DegreesToRadians()
        {
            return this.MapValueUsing(v => v.DegreesToRadians());
        }

        public TemporalValue<LinFloat64PolarAngle> RadiansToPolarAngle()
        {
            return this.MapValueUsing(v => v.RadiansToPolarAngle());
        }
        
        public TemporalValue<LinFloat64PolarAngle> DegreesToPolarAngle()
        {
            return this.MapValueUsing(v => v.DegreesToPolarAngle());
        }
        
        public TemporalValue<LinFloat64DirectedAngle> RadiansToDirectedAngle()
        {
            return this.MapValueUsing(v => v.RadiansToDirectedAngle());
        }
        
        public TemporalValue<LinFloat64DirectedAngle> DegreesToDirectedAngle()
        {
            return this.MapValueUsing(v => v.DegreesToDirectedAngle());
        }
    }
}

