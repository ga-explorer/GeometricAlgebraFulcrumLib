﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;

public static class Float64SignalComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this Func<double, double> scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
    {
        //var tValues =
        //    tMin.GetLinearRange(tMax, sampleCount, periodicRange).ToImmutableArray();

        //var samplingRate =
        //    (sampleCount - 1) / (tValues[^1] - tValues[0]);

        //return tValues
        //    .Select(scalarFunction)
        //    .CreateSignal(samplingRate);

        var sampleList = SampledTimeMapList<double>.Create(
            tMin, 
            tMax,
            periodicRange,
            scalarFunction,
            sampleCount
        );

        return Float64SampledTimeSignal.Create(
            1d / sampleList.TimeResolution, 
            sampleList, 
            periodicRange
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this DifferentialFunction scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
    {
        //var tValues =
        //    tMin.GetLinearRange(tMax, sampleCount, periodicRange).ToImmutableArray();

        //var samplingRate =
        //    (sampleCount - 1) / (tValues[^1] - tValues[0]);

        //return tValues
        //    .Select(scalarFunction.GetValue)
        //    .CreateSignal(samplingRate);

        return CreateSignal(
            scalarFunction.GetValue,
            tMin,
            tMax,
            sampleCount, 
            periodicRange
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateSignalComposer(this IEnumerable<double> signalSamples, double samplingRate)
    {
        return Float64SampledTimeSignalComposer.Create(samplingRate, signalSamples);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this IEnumerable<double> signalSamples, double samplingRate)
    {
        return Float64SampledTimeSignal.Finite(samplingRate, signalSamples);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this IEnumerable<Float64Scalar> signalSamples, double samplingRate)
    {
        return Float64SampledTimeSignal.Finite(
            samplingRate, 
            signalSamples.Select(s => s.ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this IEnumerable<double> signalSamples, double samplingRate, Func<double, double> scalarMapping)
    {
        return Float64SampledTimeSignal.Finite(
            samplingRate, 
            signalSamples.Select(scalarMapping)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this IEnumerable<Float64Scalar> signalSamples, double samplingRate, Func<Float64Scalar, double> scalarMapping)
    {
        return Float64SampledTimeSignal.Finite(
            samplingRate, 
            signalSamples.Select(scalarMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal<T>(this IEnumerable<T> signalSamples, double samplingRate, Func<T, double> scalarMapping)
    {
        return Float64SampledTimeSignal.Finite(
            samplingRate, 
            signalSamples.Select(scalarMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarProcessorOfFloat64Signal CreateFloat64ScalarSignalProcessor(double samplingRate, int sampleCount)
    {
        return ScalarProcessorOfFloat64Signal.Create(samplingRate, sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateSignal(this Scalar<IReadOnlyList<double>> signalSamples, double samplingRate)
    {
        return Float64SampledTimeSignal.Create(samplingRate, signalSamples.ScalarValue, false);
    }


    public static XGaVector<Float64SampledTimeSignal> MapSignalVectors(this XGaVector<Float64SampledTimeSignal> vectorSignal, Func<XGaFloat64Vector, XGaFloat64Vector> vectorMapping, int vSpaceDimensions)
    {
        var processor = vectorSignal.Processor;
        var scalarSignals = vectorSignal.Scalars.ToArray();
        var samplingRate = scalarSignals[0].SamplingRate;
        var vectorCount = scalarSignals.Max(s => s.Count);

        // Extract separate vectors from signal
        var vectorScalarArray = new double[vectorCount][];

        for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
            vectorScalarArray[rowIndex] = new double[vSpaceDimensions];

        foreach (var (colIndex, scalarArray) in vectorSignal.IndexScalarPairs)
            for (var rowIndex = 0; rowIndex < scalarArray.Count; rowIndex++)
                vectorScalarArray[rowIndex][colIndex] = scalarArray[rowIndex];

        // Map signal vectors
        for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
        {
            var scalarArray = vectorScalarArray[rowIndex];

            var mappedVector = vectorMapping(
                XGaFloat64Processor.Euclidean.Vector(scalarArray)
            );

            for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
                scalarArray[colIndex] = mappedVector[colIndex];
        }

        // Construct mapped signal
        var signalScalarArray = new Float64SampledTimeSignal[vSpaceDimensions];

        for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
        {
            var scalarArray = new double[vectorCount];

            for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
            {
                scalarArray[rowIndex] = vectorScalarArray[rowIndex][colIndex];
            }

            signalScalarArray[colIndex] = scalarArray.CreateSignal(samplingRate);
        }

        return processor.Vector(signalScalarArray);
    }

    public static XGaVector<IReadOnlyList<T>> MapSignalVectors<T>(this XGaVector<IReadOnlyList<T>> vectorSignal, XGaProcessor<T> processor, Func<XGaVector<T>, XGaVector<T>> vectorMapping, int vSpaceDimensions)
    {
        var vectorCount = vectorSignal.Scalars.Max(s => s.Count);

        // Extract separate vectors from signal
        var vectorScalarArray = new T[vectorCount][];

        for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
        {
            vectorScalarArray[rowIndex] =
                Enumerable
                    .Repeat(processor.ScalarProcessor.ZeroValue, vSpaceDimensions)
                    .ToArray();
        }

        foreach (var (colIndex, scalarArray) in vectorSignal.IndexScalarPairs)
            for (var rowIndex = 0; rowIndex < scalarArray.Count; rowIndex++)
                vectorScalarArray[rowIndex][colIndex] = scalarArray[rowIndex];

        // Map signal vectors
        for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
        {
            var scalarArray = vectorScalarArray[rowIndex];

            var mappedVector = vectorMapping(
                processor.Vector(scalarArray)
            );

            for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
                scalarArray[colIndex] = mappedVector[colIndex].ScalarValue;
        }

        // Construct mapped signal
        var signalScalarArray = new IReadOnlyList<T>[vSpaceDimensions];

        for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
        {
            var scalarArray = new T[vectorCount];

            for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
            {
                scalarArray[rowIndex] = vectorScalarArray[rowIndex][colIndex];
            }

            signalScalarArray[colIndex] = scalarArray;
        }

        return vectorSignal.Processor.Vector(signalScalarArray);
    }
}