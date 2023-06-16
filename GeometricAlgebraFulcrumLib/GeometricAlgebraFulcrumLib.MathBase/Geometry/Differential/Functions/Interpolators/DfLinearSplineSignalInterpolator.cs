using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Interpolators
{
    public class DfLinearSplineSignalInterpolator :
        DifferentialSignalInterpolatorFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfLinearSplineSignalInterpolator Create(Float64Signal signal, DfLinearSplineSignalInterpolatorOptions options)
        {
            signal = signal.GetSmoothedSignal(options);

            var x = signal.GetSampledTimeArray();
            var y = signal.GetArray();

            var spline = LinearSpline.InterpolateSorted(x, y);

            return new DfLinearSplineSignalInterpolator(
                signal.SamplingSpecs, 
                0,
                signal.Count - 1,
                options,
                spline
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfLinearSplineSignalInterpolator Create(Float64Signal signal, int sampleIndex1, int sampleIndex2, DfLinearSplineSignalInterpolatorOptions options)
        {
            // Smooth the given signal, if specified in options
            signal = signal.GetSmoothedSignal(options);

            var sampleCount = sampleIndex2 - sampleIndex1 + 1;
            var x = signal.GetSampledTimeArray(sampleIndex1, sampleCount);
            var y = signal.GetArray(sampleIndex1, sampleCount);

            var spline = LinearSpline.InterpolateSorted(x, y);

            return new DfLinearSplineSignalInterpolator(
                signal.SamplingSpecs, 
                sampleIndex1,
                sampleIndex2,
                options,
                spline
            );
        }


        public DfLinearSplineSignalInterpolatorOptions Options { get; }

        public LinearSpline Spline { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfLinearSplineSignalInterpolator(Float64SignalSamplingSpecs samplingSpecs, int sampleIndex1, int sampleIndex2, DfLinearSplineSignalInterpolatorOptions options, LinearSpline spline) 
            : base(samplingSpecs, sampleIndex1, sampleIndex2)
        {
            Options = options;
            Spline = spline;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            return Spline.Interpolate(t);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            return Create(
                SamplingSpecs.GetSampledFunctionSignal(Spline.Differentiate), 
                Options
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative2()
        {
            return Create(
                SamplingSpecs.GetSampledFunctionSignal(Spline.Differentiate2), 
                Options
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivativeN(int order)
        {
            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            if (order == 0) return this;

            if (order == 1) return GetDerivative1();

            if (order == 2) return GetDerivative2();

            order -= 2;
            var df = GetDerivative2();

            while (order > 0)
            {
                df = df.GetDerivative1();

                order--;
            }

            return df;
        }
    }
}
