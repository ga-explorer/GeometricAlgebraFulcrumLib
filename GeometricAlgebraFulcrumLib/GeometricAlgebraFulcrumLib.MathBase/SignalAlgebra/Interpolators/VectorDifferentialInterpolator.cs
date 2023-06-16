using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Interpolators;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators
{
    public class VectorDifferentialInterpolator
    {
        public static VectorDifferentialInterpolator CreateLinearSpline(XGaVector<Float64Signal> vectorSignal, Float64SignalSamplingSpecs samplingSpecs, DfLinearSplineSignalInterpolatorOptions options)
        {
            var n = vectorSignal.VSpaceDimensions;

            var interpolatorArray = new DfLinearSplineSignalInterpolator[n];

            for (var i = 0; i < n; i++)
            {
                var signal = vectorSignal[i].ScalarValue;
            
                interpolatorArray[i] = DfLinearSplineSignalInterpolator.Create(signal, options);
            }

            var interpolatorList = new List<DfLinearSplineSignalInterpolator[]>(n + 1)
            {
                interpolatorArray
            };

            for (var i = 1; i <= n; i++)
                interpolatorList.Add(
                    interpolatorList[i - 1]
                        .Select(f => (DfLinearSplineSignalInterpolator)f.GetDerivative1())
                        .ToArray()
                );

            return new VectorDifferentialInterpolator(
                vectorSignal,
                samplingSpecs,
                interpolatorList
            );
        }

        public static VectorDifferentialInterpolator CreateCatmullRomSpline(XGaVector<Float64Signal> vectorSignal, Float64SignalSamplingSpecs samplingSpecs, DfCatmullRomSplineSignalInterpolatorOptions options)
        {
            var n = vectorSignal.VSpaceDimensions;

            var interpolatorArray = new DfCatmullRomSplineSignalInterpolator[n];

            for (var i = 0; i < n; i++)
            {
                var signal = vectorSignal[i].ScalarValue;
            
                interpolatorArray[i] = 
                    DfCatmullRomSplineSignalInterpolator.Create(signal, options);
            }

            var interpolatorList = new List<DfCatmullRomSplineSignalInterpolator[]>(n + 1)
            {
                interpolatorArray
            };

            for (var i = 1; i <= n; i++)
                interpolatorList.Add(
                    interpolatorList[i - 1]
                        .Select(f => (DfCatmullRomSplineSignalInterpolator)f.GetDerivative1())
                        .ToArray()
                );

            return new VectorDifferentialInterpolator(
                vectorSignal,
                samplingSpecs,
                interpolatorList
            );
        }

        public static VectorDifferentialInterpolator CreateChebyshev(XGaVector<Float64Signal> vectorSignal, Float64SignalSamplingSpecs samplingSpecs, DfChebyshevSignalInterpolatorOptions options)
        {
            var n = vectorSignal.VSpaceDimensions;

            var interpolatorArray = new DfChebyshevSignalInterpolator[n];

            for (var i = 0; i < n; i++)
            {
                var signal = vectorSignal[i].ScalarValue;
            
                interpolatorArray[i] = DfChebyshevSignalInterpolator.Create(signal, options);
            }

            var interpolatorList = new List<DfChebyshevSignalInterpolator[]>(n + 1)
            {
                interpolatorArray
            };

            for (var i = 1; i <= n; i++)
                interpolatorList.Add(
                    interpolatorList[i - 1]
                        .Select(f => (DfChebyshevSignalInterpolator)f.GetDerivative1())
                        .ToArray()
                );

            return new VectorDifferentialInterpolator(
                vectorSignal,
                samplingSpecs,
                interpolatorList
            );
        }
    
        public static VectorDifferentialInterpolator CreateFourier(XGaVector<Float64Signal> vectorSignal, Float64SignalSamplingSpecs samplingSpecs, DfFourierSignalInterpolatorOptions options)
        {
            var n = vectorSignal.VSpaceDimensions;

            var interpolatorArray = new DfFourierSignalInterpolator[n];

            for (var i = 0; i < n; i++)
            {
                var signal = vectorSignal[i].ScalarValue;
            
                interpolatorArray[i] = DfFourierSignalInterpolator.Create(signal, options);
            }

            var interpolatorList = new List<DfFourierSignalInterpolator[]>(n + 1)
            {
                interpolatorArray
            };

            for (var i = 1; i <= n; i++)
                interpolatorList.Add(
                    interpolatorList[i - 1]
                        .Select(f => (DfFourierSignalInterpolator)f.GetDerivative1())
                        .ToArray()
                );

            return new VectorDifferentialInterpolator(
                vectorSignal,
                samplingSpecs,
                interpolatorList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorDifferentialInterpolator Create(XGaVector<Float64Signal> vectorSignal, Float64SignalSamplingSpecs samplingSpecs, DfSignalInterpolatorOptions options)
        {
            return options switch
            {
                DfLinearSplineSignalInterpolatorOptions options1 =>
                    CreateLinearSpline(
                        vectorSignal, 
                        samplingSpecs, 
                        options1
                    ),

                DfCatmullRomSplineSignalInterpolatorOptions options1 =>
                    CreateCatmullRomSpline(
                        vectorSignal, 
                        samplingSpecs, 
                        options1
                    ),

                DfChebyshevSignalInterpolatorOptions options1 =>
                    CreateChebyshev(
                        vectorSignal, 
                        samplingSpecs, 
                        options1
                    ),

                DfFourierSignalInterpolatorOptions options1 =>
                    CreateFourier(
                        vectorSignal, 
                        samplingSpecs, 
                        options1
                    ),

                _ => throw new ArgumentException(nameof(options))
            };
        }

        
        public XGaProcessor<Float64Signal> SignalProcessor 
            => VectorSamples.Processor;

        public XGaFloat64Processor SampleProcessor { get; }
            = XGaFloat64Processor.Euclidean;

        public int VSpaceDimensions { get; }

        public Float64SignalSamplingSpecs SamplingSpecs { get; }

        public double SamplingRate 
            => SamplingSpecs.SamplingRate;
    
        public int SampleCount 
            => SamplingSpecs.SampleCount;

        public XGaVector<Float64Signal> VectorSamples { get; }

        public IReadOnlyList<DifferentialFunction[]> InterpolatorList { get; }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private VectorDifferentialInterpolator(XGaVector<Float64Signal> scalarSamples, Float64SignalSamplingSpecs samplingSpecs, IReadOnlyList<DifferentialFunction[]> interpolatorList)
        {
            VSpaceDimensions = scalarSamples.VSpaceDimensions;
            SamplingSpecs = samplingSpecs;
            VectorSamples = scalarSamples;
            InterpolatorList = interpolatorList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVector(double t)
        {
            //var signalTime = (SampleCount - 1) / SamplingRate;
            //if (t < 0 || t > signalTime)
            //    return ScalarProcessor.CreateVectorZero();

            var scalarArray = InterpolatorList[0]
                .Select(p => p.GetValue(t))
                .ToArray();
        
            return SampleProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVectorDt1(double t)
        {
            //var signalTime = (SampleCount - 1) / SamplingRate;
            //if (t < 0 || t > signalTime)
            //    return ScalarProcessor.CreateVectorZero();
        
            var scalarArray = InterpolatorList[1]
                .Select(p => p.GetValue(t))
                .ToArray();
        
            return SampleProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVectorDt2(double t)
        {
            //var signalTime = (SampleCount - 1) / SamplingRate;
            //if (t < 0 || t > signalTime)
            //    return ScalarProcessor.CreateVectorZero();
        
            var scalarArray = InterpolatorList[2]
                .Select(p => p.GetValue(t))
                .ToArray();
        
            return SampleProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVectorDt(double t, int order = 1)
        {
            //var signalTime = (SampleCount - 1) / SamplingRate;
            //if (t < 0 || t > signalTime)
            //    return ScalarProcessor.CreateVectorZero();

            var scalarArray = InterpolatorList[order]
                .Select(p => p.GetValue(t))
                .ToArray();
        
            return SampleProcessor.CreateVector(scalarArray);
        }

        
        public XGaVector<Float64Signal> GetVectors()
        {
            var processor = VectorSamples.Processor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVector(i / SamplingRate))
                .ToArray();

            var columnVectorArray = new Float64Signal[VSpaceDimensions];

            for (var j = 0; j < VSpaceDimensions; j++)
            {
                var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
        
        public XGaVector<Float64Signal> GetVectorsDt1()
        {
            var processor = VectorSamples.Processor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVectorDt1(i / SamplingRate))
                .ToArray();

            var columnVectorArray = new Float64Signal[VSpaceDimensions];

            for (var j = 0; j < VSpaceDimensions; j++)
            {
                var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
        
        public XGaVector<Float64Signal> GetVectorsDt2()
        {
            var processor = VectorSamples.Processor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVectorDt2(i / SamplingRate))
                .ToArray();

            var columnVectorArray = new Float64Signal[VSpaceDimensions];

            for (var j = 0; j < VSpaceDimensions; j++)
            {
                var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
        
        public XGaVector<Float64Signal> GetVectorsDt(int degree = 1)
        {
            var processor = VectorSamples.Processor;

            var vectorList = Enumerable
                .Range(0, SampleCount)
                .Select(i => GetVectorDt(i / SamplingRate, degree))
                .ToArray();

            var columnVectorArray = new Float64Signal[VSpaceDimensions];

            for (var j = 0; j < VSpaceDimensions; j++)
            {
                var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

                for (var i = 0; i < SampleCount; i++)
                    columnVector[i] = vectorList[i][j];

                columnVectorArray[j] = columnVector;
            }

            return processor.CreateVector(columnVectorArray);
        }
    }
}