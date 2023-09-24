using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public static class Float64SignalComposerUtils
    {
        public static XGaVector<Float64Signal> MapSignalVectors(this XGaVector<Float64Signal> vectorSignal, Func<XGaFloat64Vector, XGaFloat64Vector> vectorMapping, int vSpaceDimensions)
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
                    XGaFloat64Processor.Euclidean.CreateVector(scalarArray)
                );

                for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
                    scalarArray[colIndex] = mappedVector[colIndex];
            }

            // Construct mapped signal
            var signalScalarArray = new Float64Signal[vSpaceDimensions];

            for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
            {
                var scalarArray = new double[vectorCount];

                for (var rowIndex = 0; rowIndex < vectorCount; rowIndex++)
                {
                    scalarArray[rowIndex] = vectorScalarArray[rowIndex][colIndex];
                }

                signalScalarArray[colIndex] = scalarArray.CreateSignal(samplingRate);
            }

            return processor.CreateVector(signalScalarArray);
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
                        .Repeat(processor.ScalarProcessor.ScalarZero, vSpaceDimensions)
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
                    processor.CreateVector(scalarArray)
                );

                for (var colIndex = 0; colIndex < vSpaceDimensions; colIndex++)
                    scalarArray[colIndex] = mappedVector[colIndex];
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

            return vectorSignal.Processor.CreateVector(signalScalarArray);
        }
    }
}
