using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class SignalProcessingFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateSignal(this IScalarD0Function scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
        {
            var tValues = 
                tMin.GetLinearRange(tMax, sampleCount, periodicRange).ToImmutableArray();

            var samplingRate = 
                (sampleCount - 1) / (tValues[^1] - tValues[0]);

            return tValues
                .Select(scalarFunction.GetValue)
                .CreateSignal(samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateSignal(this IEnumerable<double> signalSamples, double samplingRate)
        {
            return ScalarSignalFloat64.Create(samplingRate, signalSamples, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateSignal(this Scalar<IReadOnlyList<double>> signalSamples, double samplingRate)
        {
            return ScalarSignalFloat64.Create(samplingRate, signalSamples.ScalarValue, false);
        }
        
        public static GaVector<ScalarSignalFloat64> MapSignalVectors(this IGeometricAlgebraProcessor<double> geometricProcessor, GaVector<ScalarSignalFloat64> vectorSignal, Func<GaVector<double>, GaVector<double>> vectorMapping)
        {
            var vSpaceDimension = (int) geometricProcessor.VSpaceDimension;
            var scalarSignals = vectorSignal.GetScalars().ToArray();
            var samplingRate = scalarSignals[0].SamplingRate;
            var vectorCount = scalarSignals.Max(s => s.Count);

            // Extract separate vectors from signal
            var vectorScalarArray = new double[vectorCount][];

            for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
                vectorScalarArray[rowIndex] = new double[vSpaceDimension];

            foreach (var (colIndex, scalarArray) in vectorSignal.GetIndexScalarRecords())
                for (var rowIndex = 0; rowIndex < scalarArray.Count; rowIndex++)
                    vectorScalarArray[rowIndex][colIndex] = scalarArray[rowIndex];

            // Map signal vectors
            for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
            {
                var scalarArray = vectorScalarArray[rowIndex];

                var mappedVector = vectorMapping(
                    geometricProcessor.CreateVector(scalarArray)
                );

                for (var colIndex = 0; colIndex < vSpaceDimension; colIndex++)
                    scalarArray[colIndex] = mappedVector[colIndex];
            }

            // Construct mapped signal
            var signalScalarArray = new ScalarSignalFloat64[vSpaceDimension];

            for (var colIndex = 0; colIndex < vSpaceDimension; colIndex++)
            {
                var scalarArray = new double[vectorCount];

                for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
                {
                    scalarArray[rowIndex] = vectorScalarArray[rowIndex][colIndex];
                }

                signalScalarArray[colIndex] = scalarArray.CreateSignal(samplingRate);
            }

            return vectorSignal.GeometricProcessor.CreateVector(signalScalarArray);
        }

        public static GaVector<IReadOnlyList<T>> MapSignalVectors<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, GaVector<IReadOnlyList<T>> vectorSignal, Func<GaVector<T>, GaVector<T>> vectorMapping)
        {
            var vSpaceDimension = (int) geometricProcessor.VSpaceDimension;
            var vectorCount = vectorSignal.GetScalars().Max(s => s.Count);

            // Extract separate vectors from signal
            var vectorScalarArray = new T[vectorCount][];

            for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
            {
                vectorScalarArray[rowIndex] = 
                    Enumerable
                        .Repeat(geometricProcessor.ScalarZero, vSpaceDimension)
                        .ToArray();
            }

            foreach (var (colIndex, scalarArray) in vectorSignal.GetIndexScalarRecords())
                for (var rowIndex = 0; rowIndex < scalarArray.Count; rowIndex++)
                    vectorScalarArray[rowIndex][colIndex] = scalarArray[rowIndex];

            // Map signal vectors
            for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
            {
                var scalarArray = vectorScalarArray[rowIndex];

                var mappedVector = vectorMapping(
                    geometricProcessor.CreateVector(scalarArray)
                );

                for (var colIndex = 0; colIndex < vSpaceDimension; colIndex++)
                    scalarArray[colIndex] = mappedVector[colIndex];
            }

            // Construct mapped signal
            var signalScalarArray = new IReadOnlyList<T>[vSpaceDimension];

            for (var colIndex = 0; colIndex < vSpaceDimension; colIndex++)
            {
                var scalarArray = new T[vectorCount];

                for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
                {
                    scalarArray[rowIndex] = vectorScalarArray[rowIndex][colIndex];
                }

                signalScalarArray[colIndex] = scalarArray;
            }

            return vectorSignal.GeometricProcessor.CreateVector(signalScalarArray);
        }
    }
}
