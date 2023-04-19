using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.Polynomials;
using GeometricAlgebraFulcrumLib.MathBase.Signals;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra
{
    public static class SignalAlgebraUtils
    {
        public static RGaVector<ScalarSignalFloat64> CreateVectorSignal(this RGaProcessor<ScalarSignalFloat64> geometricProcessor, IEnumerable<RGaFloat64Vector> vectorList, double samplingRate)
        {
            var vectorArray = vectorList.ToArray();
            var vSpaceDimensions = vectorArray.GetVSpaceDimensions();
            var scalarArray = new ScalarSignalFloat64[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
            {
                var index = i;

                scalarArray[i] = vectorArray
                    .Select(v => v[index])
                    .CreateSignal(samplingRate);
            }

            return geometricProcessor.CreateVector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<ScalarSignalFloat64> CreateVectorSignal(this IEnumerable<RGaFloat64Vector> vectorList, RGaProcessor<ScalarSignalFloat64> geometricProcessor, double samplingRate)
        {
            return geometricProcessor.CreateVectorSignal(vectorList, samplingRate);
        }

        public static RGaVector<IReadOnlyList<T>> CreateVector<T>(this RGaProcessor<IReadOnlyList<T>> geometricProcessor, IReadOnlyList<RGaVector<T>> vectorList)
        {
            var vSpaceDimensions = vectorList.GetVSpaceDimensions();
            var scalarArray = new IReadOnlyList<T>[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
                scalarArray[i] = vectorList.Select(v => v[i].ScalarValue).ToArray();

            return geometricProcessor.CreateVector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<IReadOnlyList<T>> CreateVector<T>(this IReadOnlyList<RGaVector<T>> vectorList, RGaProcessor<IReadOnlyList<T>> geometricProcessor)
        {
            return geometricProcessor.CreateVector(vectorList);
        }


        public static IReadOnlyList<RGaVector<ScalarSignalFloat64>> ApplyGramSchmidtByProjections(this IReadOnlyList<RGaVector<ScalarSignalFloat64>> vectorsList, int vSpaceDimensions, bool makeUnitVectors)
        {
            var vectorMatrixList =
                vectorsList
                    .Select(v => v.ToMatrix())
                    .ToArray();

            var geometricProcessor = vectorsList[0].Processor;
            var samplingSpecs = vectorsList[0].GetSamplingSpecs();
            var vectorCount = vectorMatrixList.Length;
            var samplingRate = samplingSpecs.SamplingRate;
            var sampleCount = samplingSpecs.SampleCount;
            //var vSpaceDimensions = vectorMatrixList[0].ColumnCount;

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var index = sampleIndex;

                var matrix = (Matrix)Matrix.Build.Dense(
                    vSpaceDimensions,
                    vectorCount,
                    (i, j) => vectorMatrixList[j][index, i]
                );

                var rank = matrix.Rank();

                var c = matrix.ColumnAbsoluteSums();
                var colList = new List<int>(c.Count);
                for (var i = 0; i < c.Count; i++)
                    if (c[i] != 0) colList.Add(i);

                var gramSchmidt = Matrix.Build.Dense(
                    vSpaceDimensions,
                    rank,
                    (i, j) => j < colList.Count ? matrix[i, colList[j]] : 0d
                ).GramSchmidt();

                //if (rank >= 3)
                //{
                //    Console.WriteLine($"Q: {gramSchmidt.Q}");
                //    Console.WriteLine($"R: {gramSchmidt.R}");
                //}

                var orthogonalMatrix = gramSchmidt.Q;
                var vectorNorms = gramSchmidt.R.Diagonal();

                for (var j = 0; j < vectorCount; j++)
                {
                    var vectorMatrix = vectorMatrixList[j];

                    if (j < orthogonalMatrix.ColumnCount)
                    {
                        var vectorNorm =
                            makeUnitVectors ? 1d : vectorNorms[j];

                        for (var i = 0; i < orthogonalMatrix.RowCount; i++)
                            vectorMatrix[index, i] = vectorNorm * orthogonalMatrix[i, j];
                    }
                    else
                    {
                        for (var i = 0; i < orthogonalMatrix.RowCount; i++)
                            vectorMatrix[index, i] = 0d;
                    }
                }
            }

            var orthogonalVectors =
                vectorMatrixList.Select(matrix =>
                    geometricProcessor.ToVectorSignal(vSpaceDimensions, matrix, samplingRate)
                );

            return makeUnitVectors
                ? orthogonalVectors.Select(v => v.DivideByNorm()).ToArray()
                : orthogonalVectors.ToArray();
        }
        
        public static IReadOnlyList<XGaVector<ScalarSignalFloat64>> ApplyGramSchmidtByProjections(this IReadOnlyList<XGaVector<ScalarSignalFloat64>> vectorsList, int vSpaceDimensions, bool makeUnitVectors)
        {
            var vectorMatrixList =
                vectorsList
                    .Select(v => v.ToMatrix())
                    .ToArray();

            var geometricProcessor = vectorsList[0].Processor;
            var samplingSpecs = vectorsList[0].GetSamplingSpecs();
            var vectorCount = vectorMatrixList.Length;
            var samplingRate = samplingSpecs.SamplingRate;
            var sampleCount = samplingSpecs.SampleCount;
            //var vSpaceDimensions = vectorMatrixList[0].ColumnCount;

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var index = sampleIndex;

                var matrix = (Matrix)Matrix.Build.Dense(
                    vSpaceDimensions,
                    vectorCount,
                    (i, j) => vectorMatrixList[j][index, i]
                );

                var rank = matrix.Rank();

                var c = matrix.ColumnAbsoluteSums();
                var colList = new List<int>(c.Count);
                for (var i = 0; i < c.Count; i++)
                    if (c[i] != 0) colList.Add(i);

                var gramSchmidt = Matrix.Build.Dense(
                    vSpaceDimensions,
                    rank,
                    (i, j) => j < colList.Count ? matrix[i, colList[j]] : 0d
                ).GramSchmidt();

                //if (rank >= 3)
                //{
                //    Console.WriteLine($"Q: {gramSchmidt.Q}");
                //    Console.WriteLine($"R: {gramSchmidt.R}");
                //}

                var orthogonalMatrix = gramSchmidt.Q;
                var vectorNorms = gramSchmidt.R.Diagonal();

                for (var j = 0; j < vectorCount; j++)
                {
                    var vectorMatrix = vectorMatrixList[j];

                    if (j < orthogonalMatrix.ColumnCount)
                    {
                        var vectorNorm =
                            makeUnitVectors ? 1d : vectorNorms[j];

                        for (var i = 0; i < orthogonalMatrix.RowCount; i++)
                            vectorMatrix[index, i] = vectorNorm * orthogonalMatrix[i, j];
                    }
                    else
                    {
                        for (var i = 0; i < orthogonalMatrix.RowCount; i++)
                            vectorMatrix[index, i] = 0d;
                    }
                }
            }

            var orthogonalVectors =
                vectorMatrixList.Select(matrix =>
                    geometricProcessor.ToVectorSignal(vSpaceDimensions, matrix, samplingRate)
                );

            return makeUnitVectors
                ? orthogonalVectors.Select(v => v.DivideByNorm()).ToArray()
                : orthogonalVectors.ToArray();
        }
        
        public static RGaVector<ScalarSignalFloat64> ToVectorSignal(this RGaProcessor<ScalarSignalFloat64> geometricProcessor, int vSpaceDimensions, Matrix vectorMatrix, double samplingRate)
        {
            var sampleCount = vectorMatrix.RowCount;
            var scalarArray = new ScalarSignalFloat64[vSpaceDimensions];

            for (var j = 0; j < vSpaceDimensions; j++)
            {
                var scalarSignal = ScalarSignalFloat64.CreateConstant(samplingRate, sampleCount, 0d, false);

                for (var i = 0; i < sampleCount; i++)
                {
                    scalarSignal[i] = vectorMatrix[i, j];
                }

                scalarArray[j] = scalarSignal;
            }

            return geometricProcessor.CreateVector(scalarArray);
        }

        public static XGaVector<ScalarSignalFloat64> ToVectorSignal(this XGaProcessor<ScalarSignalFloat64> geometricProcessor, int vSpaceDimensions, Matrix vectorMatrix, double samplingRate)
        {
            var sampleCount = vectorMatrix.RowCount;
            var scalarArray = new ScalarSignalFloat64[vSpaceDimensions];

            for (var j = 0; j < vSpaceDimensions; j++)
            {
                var scalarSignal = ScalarSignalFloat64.CreateConstant(samplingRate, sampleCount, 0d, false);

                for (var i = 0; i < sampleCount; i++)
                {
                    scalarSignal[i] = vectorMatrix[i, j];
                }

                scalarArray[j] = scalarSignal;
            }

            return geometricProcessor.CreateVector(scalarArray);
        }
        

        public static XGaVector<ScalarSignalFloat64> CreateVectorSignal(this XGaProcessor<ScalarSignalFloat64> geometricProcessor, IEnumerable<XGaFloat64Vector> vectorList, double samplingRate)
        {
            var vectorArray = vectorList.ToArray();
            var vSpaceDimensions = vectorArray.GetVSpaceDimensions();
            var scalarArray = new ScalarSignalFloat64[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
            {
                var index = i;

                scalarArray[i] = vectorArray
                    .Select(v => v[index])
                    .CreateSignal(samplingRate);
            }

            return geometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<ScalarSignalFloat64> CreateVectorSignal(this IEnumerable<XGaFloat64Vector> vectorList, XGaProcessor<ScalarSignalFloat64> geometricProcessor, double samplingRate)
        {
            return geometricProcessor.CreateVectorSignal(vectorList, samplingRate);
        }

        public static XGaVector<IReadOnlyList<T>> CreateVector<T>(this XGaProcessor<IReadOnlyList<T>> geometricProcessor, IReadOnlyList<XGaVector<T>> vectorList)
        {
            var vSpaceDimensions = vectorList.GetVSpaceDimensions();
            var scalarArray = new IReadOnlyList<T>[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
                scalarArray[i] = vectorList.Select(v => v[i].ScalarValue).ToArray();

            return geometricProcessor.CreateVector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<IReadOnlyList<T>> CreateVector<T>(this IReadOnlyList<XGaVector<T>> vectorList, XGaProcessor<IReadOnlyList<T>> geometricProcessor)
        {
            return geometricProcessor.CreateVector(vectorList);
        }


        public static IReadOnlyList<double> GetPaddedSignal1(IReadOnlyList<double> signalSamples, double samplingRate, int trendSampleCount)
        {
            var scalarProcessor = ScalarProcessorFloat64.DefaultProcessor;

            var sampleCount = signalSamples.Count;

            var tValues = new List<double>(trendSampleCount * 2);
            var uValues = new List<double>(trendSampleCount * 2);
            
            for (var i = 0; i < trendSampleCount; i++)
            {
                var sampleIndex = i + sampleCount - trendSampleCount;

                tValues.Add(sampleIndex / samplingRate);
                uValues.Add(signalSamples[sampleIndex]);
            }

            for (var i = 0; i < trendSampleCount; i++)
            {
                var sampleIndex = i + 2 * sampleCount;

                tValues.Add(sampleIndex / samplingRate);
                uValues.Add(signalSamples[i]);
            }

            var polynomial = PolynomialFunction<double>.Create(
                scalarProcessor,
                Fit.Polynomial(tValues.ToArray(), uValues.ToArray(), 5)
            );

            var paddedSignalSamples = new List<double>(signalSamples);

            for (var i = 0; i < sampleCount; i++)
            {
                var tValue = (sampleCount + i) / samplingRate;
                var uValue = polynomial.GetValue(tValue);

                paddedSignalSamples.Add(uValue);
            }

            return paddedSignalSamples;
        }
        
        public static ScalarSignalFloat64 GetPeriodicPaddedSignal1(this ScalarSignalFloat64 signal, int trendSampleCount, int polynomialDegree)
        {
            var scalarProcessor = ScalarProcessorFloat64.DefaultProcessor;

            var sampleCount = signal.Count;

            var tValues = new List<double>(trendSampleCount * 2 + sampleCount);
            var uValues = new List<double>(trendSampleCount * 2 + sampleCount);
            
            for (var i = 0; i < trendSampleCount; i++)
            {
                var sampleIndex = i + sampleCount - trendSampleCount;

                tValues.Add(sampleIndex / signal.SamplingRate);
                uValues.Add(signal[sampleIndex]);
            }

            var u1 = signal[^1];
            var u2 = signal[0];
            for (var i = 0; i < sampleCount - 1; i++)
            {
                var t = (i + 1) / (double) sampleCount;
                var u = (1 - t) * u1 + t * u2;

                var sampleIndex = i + sampleCount;

                tValues.Add(sampleIndex / signal.SamplingRate);
                uValues.Add(u);
            }
            
            for (var i = 0; i < trendSampleCount; i++)
            {
                var sampleIndex = i + 2 * sampleCount;

                tValues.Add(sampleIndex / signal.SamplingRate);
                uValues.Add(signal[i]);
            }

            var polynomial = PolynomialFunction<double>.Create(
                scalarProcessor,
                Fit.Polynomial(
                    tValues.ToArray(), 
                    uValues.ToArray(), 
                    polynomialDegree
                )
            );

            // The padded signal always has an odd number of samples
            var paddedSignalSamples = new List<double>(signal);

            for (var i = 0; i < sampleCount - 1; i++)
            {
                var tValue = (i + sampleCount) / signal.SamplingRate;
                var uValue = polynomial.GetValue(tValue);

                paddedSignalSamples.Add(uValue);
            }

            return paddedSignalSamples.CreateSignal(signal.SamplingRate);
        }
        
        public static ScalarSignalFloat64 GetPeriodicPaddedSignal2(this ScalarSignalFloat64 signal, int trendSampleCount, int polynomialDegree, int paddingSampleCount = -1)
        {
            if (paddingSampleCount == 0)
                return signal.Concat(signal.Reverse()).CreateSignal(signal.SamplingRate);

            if (paddingSampleCount < 0)
                paddingSampleCount = signal.Count;
            
            if (trendSampleCount < 1 || trendSampleCount > signal.Count)
                throw new ArgumentOutOfRangeException(nameof(trendSampleCount));

            if (polynomialDegree < 0 || polynomialDegree > trendSampleCount / 2)
                throw new ArgumentOutOfRangeException(nameof(polynomialDegree));

            var samplingSpecs = signal.SamplingSpecs;

            var n = signal.Count;
            var m = paddingSampleCount;
            var k = trendSampleCount;

            var tValues = new List<double>(trendSampleCount * 2);
            var uValues = new List<double>(trendSampleCount * 2);

            var t1Signal = samplingSpecs.GetSampledTimeSignal(n - k, k);
            var t2Signal = samplingSpecs.GetSampledTimeSignal(m + n, k);

            for (var i = 0; i < k; i++)
            {
                tValues.Add(t1Signal[i]);
                uValues.Add(signal[n - k + i]);
            }

            var tMeanCount = 10;
            var signalMean = signal.Mean();

            var tMeanList = 
                t1Signal[^1]
                    .GetLinearRange(t2Signal[0], tMeanCount + 2, false)
                    .Skip(1)
                    .Take(tMeanCount);

            foreach (var t in tMeanList)
            {
                tValues.Add(t);
                uValues.Add(signalMean);
            }

            for (var i = 0; i < k; i++)
            {
                tValues.Add(t2Signal[i]);
                uValues.Add(signal[n - 1 - i]);
            }
            
            //var p1 = PolynomialFunction<double>.Create(
            //    scalarProcessor,
            //    Fit.Polynomial(
            //        tValues.ToArray(), 
            //        uValues.ToArray(), 
            //        polynomialDegree
            //    )
            //);

            var p1 = DfComputedFunction.Create(
                MathNet.Numerics.Interpolation.NevillePolynomialInterpolation.InterpolateSorted(
                    tValues.ToArray(), 
                    uValues.ToArray()
                ).Interpolate
            );

            var p1Signal = samplingSpecs.GetSampledFunctionSignal(
                p1.GetValue, 
                n, 
                m
            );

            tValues.Clear();
            uValues.Clear();

            t1Signal = samplingSpecs.GetSampledTimeSignal(m + 2 * n - k, k);
            t2Signal = samplingSpecs.GetSampledTimeSignal(2 * m + 2 * n, k);

            for (var i = 0; i < k; i++)
            {
                tValues.Add(t1Signal[i]);
                uValues.Add(signal[k - 1 - i]);
            }
            
            tMeanList = t1Signal[^1]
                .GetLinearRange(t2Signal[0], tMeanCount + 2, false)
                .Skip(1)
                .Take(tMeanCount);

            foreach (var t in tMeanList)
            {
                tValues.Add(t);
                uValues.Add(signalMean);
            }

            for (var i = 0; i < k; i++)
            {
                tValues.Add(t2Signal[i]);
                uValues.Add(signal[i]);
            }
            
            //var p2 = PolynomialFunction<double>.Create(
            //    scalarProcessor,
            //    Fit.Polynomial(
            //        tValues.ToArray(), 
            //        uValues.ToArray(), 
            //        polynomialDegree
            //    )
            //);
            
            var p2 = DfComputedFunction.Create(
                MathNet.Numerics.Interpolation.NevillePolynomialInterpolation.InterpolateSorted(
                    tValues.ToArray(), 
                    uValues.ToArray()
                ).Interpolate
            );

            var p2Signal = samplingSpecs.GetSampledFunctionSignal(
                p2.GetValue, 
                m + 2 * n, 
                m
            );

            var paddedSignalSamples = new List<double>(2 * n + 2 * m);

            paddedSignalSamples.AddRange(signal);
            paddedSignalSamples.AddRange(p1Signal);
            paddedSignalSamples.AddRange(signal.Reverse());
            paddedSignalSamples.AddRange(p2Signal);

            var paddedSignal = paddedSignalSamples.CreateSignal(signal.SamplingRate);

            paddedSignal.PlotSignal(
                paddedSignal.SamplingSpecs.MinTime,
                paddedSignal.SamplingSpecs.MaxTime,
                @"D:\paddedSignal"
            );

            return paddedSignal;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSamplingRate(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.Scalars.First().SamplingRate;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSamplingRate(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.Scalars.First().SamplingRate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSampleCount(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.Scalars.Max(s => s.Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSampleCount(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.Scalars.Max(s => s.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignalSamplingSpecs GetSamplingSpecs(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount = vectorSignal.GetSampleCount();
            var samplingRate = vectorSignal.GetSamplingRate();

            return new SignalSamplingSpecs(sampleCount, samplingRate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignalSamplingSpecs GetSamplingSpecs(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount = vectorSignal.GetSampleCount();
            var samplingRate = vectorSignal.GetSamplingRate();

            return new SignalSamplingSpecs(sampleCount, samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<ScalarSignalFloat64> GetSubSignal(this XGaVector<ScalarSignalFloat64> vectorSignal, int index, int count)
        {
            return vectorSignal
                .MapScalars(
                    scalarSignal => scalarSignal.GetSubSignal(index, count)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<ScalarSignalFloat64> GetSubSignal(this XGaBivector<ScalarSignalFloat64> bivectorSignal, int index, int count)
        {
            return bivectorSignal
                .MapScalars(
                    scalarSignal => scalarSignal.GetSubSignal(index, count)
                );
        }

        public static double[,] ToArray2D(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var colCount = vectorSignal.VSpaceDimensions;
            var rowCount = vectorSignal.Scalars.Max(s => s.Count);

            var array = new double[rowCount, colCount];

            foreach (var (j, scalarList) in vectorSignal.IndexScalarPairs)
                for (var i = 0; i < scalarList.Count; i++)
                    array[i, j] = scalarList[i];

            return array;
        }
        
        public static double[,] ToArray2D(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var colCount = vectorSignal.VSpaceDimensions;
            var rowCount = vectorSignal.Scalars.Max(s => s.Count);

            var array = new double[rowCount, colCount];

            foreach (var (j, scalarList) in vectorSignal.IndexScalarPairs)
                for (var i = 0; i < scalarList.Count; i++)
                    array[i, j] = scalarList[i];

            return array;
        }

        public static T[,] ToArray2D<T>(this XGaVector<IReadOnlyList<T>> vectorSignal, IScalarProcessor<T> scalarProcessor)
        {
            var colCount = vectorSignal.VSpaceDimensions;
            var rowCount = vectorSignal.Scalars.Max(s => s.Count);

            var array =
                scalarProcessor.CreateArrayZero2D(rowCount, colCount);

            foreach (var (j, scalarList) in vectorSignal.IndexScalarPairs)
                for (var i = 0; i < scalarList.Count; i++)
                    array[i, j] = scalarList[i] ?? scalarProcessor.ScalarZero;

            return array;
        }
        
        public static T[,] ToArray2D<T>(this RGaVector<IReadOnlyList<T>> vectorSignal, IScalarProcessor<T> scalarProcessor)
        {
            var colCount = vectorSignal.VSpaceDimensions;
            var rowCount = vectorSignal.Scalars.Max(s => s.Count);

            var array =
                scalarProcessor.CreateArrayZero2D(rowCount, colCount);

            foreach (var (j, scalarList) in vectorSignal.IndexScalarPairs)
                for (var i = 0; i < scalarList.Count; i++)
                    array[i, j] = scalarList[i] ?? scalarProcessor.ScalarZero;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray2D<T>(this IScalarProcessor<T> scalarProcessor, XGaVector<IReadOnlyList<T>> vectorSignal)
        {
            return vectorSignal.ToArray2D(scalarProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray2D<T>(this IScalarProcessor<T> scalarProcessor, RGaVector<IReadOnlyList<T>> vectorSignal)
        {
            return vectorSignal.ToArray2D(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix ToMatrix(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return (Matrix)Matrix.Build.DenseOfArray(vectorSignal.ToArray2D());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix ToMatrix(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return (Matrix)Matrix.Build.DenseOfArray(vectorSignal.ToArray2D());
        }

        public static XGaVector<ScalarSignalFloat64> ToXGaVectorSignal(this XGaProcessor<ScalarSignalFloat64> processor, Matrix vectorMatrix, int vSpaceDimensions, double samplingRate)
        {
            var sampleCount = vectorMatrix.RowCount;
            var scalarArray = new ScalarSignalFloat64[vSpaceDimensions];

            for (var j = 0; j < vSpaceDimensions; j++)
            {
                var scalarSignal = ScalarSignalFloat64.CreateConstant(samplingRate, sampleCount, 0d, false);

                for (var i = 0; i < sampleCount; i++)
                {
                    scalarSignal[i] = vectorMatrix[i, j];
                }

                scalarArray[j] = scalarSignal;
            }

            return processor.CreateVector(scalarArray);
        }
        
        public static RGaVector<ScalarSignalFloat64> ToRGaVectorSignal(this RGaProcessor<ScalarSignalFloat64> processor, Matrix vectorMatrix, int vSpaceDimensions, double samplingRate)
        {
            var sampleCount = vectorMatrix.RowCount;
            var scalarArray = new ScalarSignalFloat64[vSpaceDimensions];

            for (var j = 0; j < vSpaceDimensions; j++)
            {
                var scalarSignal = ScalarSignalFloat64.CreateConstant(samplingRate, sampleCount, 0d, false);

                for (var i = 0; i < sampleCount; i++)
                {
                    scalarSignal[i] = vectorMatrix[i, j];
                }

                scalarArray[j] = scalarSignal;
            }

            return processor.CreateVector(scalarArray);
        }

        public static IEnumerable<XGaVector<T>> ToVectorList<T>(this XGaVector<IReadOnlyList<T>> vectorSignal, XGaProcessor<T> processor)
        {
            var array = vectorSignal.VectorToArray1D();
            var sampleCount = array.Max(a => a.Count);
            var vSpaceDimensions = array.Length;
            
            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var scalarArray = processor.ScalarProcessor.CreateArrayZero1D(vSpaceDimensions);

                for (var j = 0; j < vSpaceDimensions; j++)
                    scalarArray[j] = array[j][sampleIndex];

                yield return processor.CreateVector(scalarArray);
            }
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<double> Mean(this IScalarProcessor<double> scalarProcessor, Scalar<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.ScalarValue.Mean().CreateScalar(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector Mean(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaFloat64Processor processor)
        {
            var composer = processor.CreateComposer();

            foreach (var (id, signal) in vectorSignal)
                composer.SetTerm(id, signal.Mean());

            return composer.GetVector();
        }

        //public static XGaFloat64Vector Mean(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaMetricScalarProcessorOfDouble processor)
        //{
        //    var sampleCount =
        //        vectorSignal.Scalars.Max(s => s.Count);

        //    var indexScalarDictionary = IndexSetUtils.CreateDictionary<double>();

        //    foreach (var (index, scalar) in vectorSignal.IdScalarPairs)
        //        indexScalarDictionary.Add(
        //            index, 
        //            scalar.Sum() / sampleCount
        //        );

        //    return processor.CreateVector(indexScalarDictionary);
        //}
        
        public static XGaFloat64Bivector Sum(this XGaBivector<ScalarSignalFloat64> bivectorSignal)
        {
            if (bivectorSignal.IsZero)
                return XGaFloat64Processor.Euclidean.CreateZeroBivector();

            var composer = 
                XGaFloat64Processor
                    .Euclidean
                    .CreateComposer();

            foreach (var (index, scalar) in bivectorSignal.IdScalarPairs)
                composer.SetTerm(index, scalar.Sum());

            return composer.GetBivector();
        }

        public static XGaFloat64Bivector Mean(this XGaBivector<ScalarSignalFloat64> bivectorSignal)
        {
            if (bivectorSignal.IsZero)
                return XGaFloat64Processor.Euclidean.CreateZeroBivector();

            var sampleCount =
                bivectorSignal.Scalars.Max(s => s.Count);
            
            var composer = 
                XGaFloat64Processor
                    .Euclidean
                    .CreateComposer();

            foreach (var (index, scalar) in bivectorSignal.IdScalarPairs)
                composer.SetTerm(index, scalar.Sum() / sampleCount);

            return composer.GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<ScalarSignalFloat64> GetRunningAverageSignal(this Scalar<ScalarSignalFloat64> scalarSignal, int averageSampleCount)
        {
            return scalarSignal
                .ScalarValue
                .GetRunningAverageSignal(averageSampleCount)
                .CreateScalar(scalarSignal.ScalarProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<ScalarSignalFloat64> GetRunningAverageSignal(this RGaVector<ScalarSignalFloat64> vectorSignal, int averageSampleCount)
        {
            return vectorSignal.MapScalars(s =>
                s.GetRunningAverageSignal(averageSampleCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<ScalarSignalFloat64> GetRunningAverageSignal(this XGaVector<ScalarSignalFloat64> vectorSignal, int averageSampleCount)
        {
            return vectorSignal.MapScalars(s =>
                s.GetRunningAverageSignal(averageSampleCount)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBivector<ScalarSignalFloat64> GetRunningAverageSignal(this RGaBivector<ScalarSignalFloat64> bivectorSignal, int averageSampleCount)
        {
            return bivectorSignal.MapScalars(s =>
                s.GetRunningAverageSignal(averageSampleCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBivector<ScalarSignalFloat64> GetRunningAverageSignal(this XGaBivector<ScalarSignalFloat64> bivectorSignal, int averageSampleCount)
        {
            return bivectorSignal.MapScalars(s =>
                s.GetRunningAverageSignal(averageSampleCount)
            );
        }
        
        public static IReadOnlyList<XGaFloat64Vector> MapComponentSignals(this IReadOnlyList<XGaFloat64Vector> vectorSamples, Func<IReadOnlyList<double>, IReadOnlyList<double>> componentMapping)
        {
            var sampleCount = vectorSamples.Count;
            var metric = XGaFloat64Processor.Euclidean;
            var vSpaceDimensions = vectorSamples.GetVSpaceDimensions();

            var componentSignals = new List<double[]>(vSpaceDimensions);

            for (var i = 0; i < vSpaceDimensions; i++)
                componentSignals.Add(new double[sampleCount]);

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                for (var i = 0; i < vSpaceDimensions; i++)
                    componentSignals[i][sampleIndex] = vectorSamples[sampleIndex][i];
            }

            for (var i = 0; i < vSpaceDimensions; i++)
                componentSignals[i] = componentMapping(componentSignals[i]).ToArray();

            var mappedVectorSignal = new XGaFloat64Vector[sampleCount];

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var scalarArray = new double[vSpaceDimensions];

                for (var i = 0; i < vSpaceDimensions; i++)
                    scalarArray[i] = componentSignals[i][sampleIndex];

                mappedVectorSignal[sampleIndex] = metric.CreateVector(scalarArray);
            }

            return mappedVectorSignal;
        }

        public static IReadOnlyList<XGaVector<T>> MapComponentSignals<T>(this IReadOnlyList<XGaVector<T>> vectorSamples, Func<IReadOnlyList<T>, IReadOnlyList<T>> componentMapping)
        {
            var sampleCount = vectorSamples.Count;
            var metric = vectorSamples[0].Processor;
            var vSpaceDimensions = vectorSamples.GetVSpaceDimensions();

            var componentSignals = new List<T[]>(vSpaceDimensions);

            for (var i = 0; i < vSpaceDimensions; i++)
                componentSignals.Add(new T[sampleCount]);

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                for (var i = 0; i < vSpaceDimensions; i++)
                    componentSignals[i][sampleIndex] = vectorSamples[sampleIndex][i];
            }

            for (var i = 0; i < vSpaceDimensions; i++)
                componentSignals[i] = componentMapping(componentSignals[i]).ToArray();

            var mappedVectorSignal = new XGaVector<T>[sampleCount];

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var scalarArray = new T[vSpaceDimensions];

                for (var i = 0; i < vSpaceDimensions; i++)
                    scalarArray[i] = componentSignals[i][sampleIndex];

                mappedVectorSignal[sampleIndex] = metric.CreateVector(scalarArray);
            }

            return mappedVectorSignal;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<ScalarSignalFloat64> WienerFilter(this Scalar<ScalarSignalFloat64> scalarSamples, int order)
        {
            return scalarSamples
                .ScalarValue
                .WienerFilter(order)
                .CreateScalar(scalarSamples.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<XGaFloat64Vector> WienerFilter1D(this IReadOnlyList<XGaFloat64Vector> vectorSamples, double samplingRate, int order)
        {
            return vectorSamples.MapComponentSignals(
                c => c.WienerFilter(samplingRate, order)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<ScalarSignalFloat64> NormWienerFilter(this XGaVector<ScalarSignalFloat64> vectorSamples, int order)
        {
            var norm1 = vectorSamples.Norm();
            var norm2 = norm1.ScalarValue.WienerFilter(order);

            return vectorSamples * (norm2 / norm1);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatio(this Scalar<ScalarSignalFloat64> scalarSignal, Scalar<ScalarSignalFloat64> noiseSignal)
        {
            return scalarSignal.Square().ScalarValue.Average() /
                   noiseSignal.Square().ScalarValue.Average();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatioDb(this Scalar<ScalarSignalFloat64> scalarSignal, Scalar<ScalarSignalFloat64> noiseSignal)
        {
            return (scalarSignal.Square().ScalarValue.Average() /
                    noiseSignal.Square().ScalarValue.Average()).Log10() * 10d;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static double SignalToNoiseRatio(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaVector<ScalarSignalFloat64> noiseSignal)
        //{
        //    return vectorSignal.NormSquared().ScalarValue.Average() / 
        //           noiseSignal.NormSquared().ScalarValue.Average();
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatioDb(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaVector<ScalarSignalFloat64> noiseSignal)
        {
            return (vectorSignal.NormSquared().ScalarValue.Average() /
                    noiseSignal.NormSquared().ScalarValue.Average()).Log10() * 10d;
        }

        
        public static Pair<XGaFloat64Vector> Pca2(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var vSpaceDimensions = vectorSignal.VSpaceDimensions;
            var colCount = vSpaceDimensions;
            var rowCount = vectorSignal.Scalars.Max(s => s.Count);

            var matrix = Matrix<double>.Build.Dense(rowCount, colCount);

            foreach (var (colIndex, columnArray) in vectorSignal.IndexScalarPairs)
            {
                for (var rowIndex = 0; rowIndex < columnArray.Count; rowIndex++)
                    matrix[rowIndex, colIndex] = columnArray[rowIndex];
            }

            var svdData = matrix.Svd();

            Console.WriteLine(
                "Singular values:" + svdData.S.Select(v => v.ToString(CultureInfo.InvariantCulture)).Concatenate(", ")
            );

            var vMatrix = svdData.VT;

            var v1ScalarArray = new double[vSpaceDimensions];
            var v2ScalarArray = new double[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
            {
                v1ScalarArray[i] = vMatrix[0, i];
                v2ScalarArray[i] = vMatrix[1, i];
            }

            var metric = XGaFloat64Processor.Euclidean;

            return new Pair<XGaFloat64Vector>(
                metric.CreateVector(v1ScalarArray),
                metric.CreateVector(v2ScalarArray)
            );
        }



        public static XGaVector<ScalarSignalFloat64> GetSmoothedNormSignal(this XGaVector<ScalarSignalFloat64> vectorSignal, int interpolationSampleCount = 128, int polynomialOrder = 7)
        {
            var vDataNorm1 =
                vectorSignal.Norm();

            var normInterpolator =
                vDataNorm1
                    .ScalarValue
                    .CreateScalarPolynomialInterpolator();

            normInterpolator.InterpolationSamples = interpolationSampleCount;
            normInterpolator.PolynomialOrder = polynomialOrder;

            var vDataNorm2 =
                normInterpolator.GetValues().CreateSignal(vectorSignal.GetSamplingRate());

            return vectorSignal * (vDataNorm2 / vDataNorm1);
        }

        public static XGaVector<ScalarSignalFloat64> GetSmoothedSignal(this XGaVector<ScalarSignalFloat64> vectorSignal, int interpolationSampleCount = 128, int polynomialOrder = 7)
        {
            var vectorInterpolator =
                vectorSignal.CreatePolynomialInterpolator(vectorSignal.GetSamplingRate());

            vectorInterpolator.InterpolationSamples = interpolationSampleCount;
            vectorInterpolator.PolynomialOrder = polynomialOrder;

            return vectorInterpolator.GetVectors();
        }

        
        public static void PlotSignal(this Scalar<ScalarSignalFloat64> scalarSignal1, Scalar<ScalarSignalFloat64> scalarSignal2, double tMin, double tMax, string plotFileName)
        {
            scalarSignal1.ScalarValue.PlotSignal(scalarSignal2.ScalarValue, tMin, tMax, plotFileName);
        }

        public static void PlotSignal(this XGaVector<ScalarSignalFloat64> vectorSignal1, XGaVector<ScalarSignalFloat64> vectorSignal2, double tMin, double tMax, string plotFileName)
        {
            var vSpaceDimensions = vectorSignal1.VSpaceDimensions;
            var samplingSpecs = vectorSignal1.GetSamplingSpecs();

            for (var i = 0; i < vSpaceDimensions; i++)
            {
                var pm = new PlotModel
                {
                    //Title = "title",
                    Background = OxyColor.FromRgb(255, 255, 255)
                };

                var scalarList1 = vectorSignal1[i].ScalarValue;

                var s1 = new FunctionSeries(
                    t => scalarList1.LinearInterpolation(t - tMin),
                    tMin,
                    tMax,
                    samplingSpecs.SampleCount,
                    @$"Component {i + 1}"
                )
                {
                    LineStyle = LineStyle.Dot,
                    StrokeThickness = 1,
                    //MarkerType = MarkerType.Diamond,
                    //MarkerStrokeThickness = 1,
                    //MarkerSize = 4
                };


                var scalarList2 = vectorSignal2[i].ScalarValue;

                var s2 = new FunctionSeries(
                    t => scalarList2.LinearInterpolation(t - tMin),
                    tMin,
                    tMax,
                    samplingSpecs.SampleCount * 2,
                    @$"Component {i + 1}"
                )
                {
                    StrokeThickness = 1.5
                };

                pm.Series.Add(s1);
                pm.Series.Add(s2);

                //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
                PngExporter.Export(pm, $"{plotFileName}{i}.png", samplingSpecs.SampleCount * 2, 750, 200);
            }
        }

        public static void PlotVectorSignalComponents(this XGaVector<ScalarSignalFloat64> vectorSignal, string title, string filePath)
        {
            var samplingRate = vectorSignal.GetSamplingRate();
            var sampleCount = vectorSignal.GetSampleCount();

            const int sampleTrim = 0;
            var tMin = sampleTrim / samplingRate;
            var tMax = (sampleCount - 1 - sampleTrim) / samplingRate;

            var pm = new PlotModel
            {
                Title = title,
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            foreach (var (index, scalarSignal) in vectorSignal.IndexScalarPairs)
            {
                var s1 = new FunctionSeries(
                    scalarSignal.LinearInterpolation,
                    tMin,
                    tMax,
                    sampleCount * 2,
                    @$"Component {index + 1}"
                );

                pm.Series.Add(s1);
            }

            //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            PngExporter.Export(pm, filePath + ".png", sampleCount * 2, 750, 200);
        }

        public static void PlotScalarSignal(this Scalar<ScalarSignalFloat64> scalarSignal, string title, string filePath)
        {
            scalarSignal.ScalarValue.PlotScalarSignal(title, filePath);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Energy(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.Energy())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Energy(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.Energy())
                .Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EnergyDc(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.EnergyDc())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EnergyDc(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.EnergyDc())
                .Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EnergyAc(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.EnergyAc())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EnergyAc(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.EnergyAc())
                .Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SumOfSquares(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.SumOfSquares())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SumOfSquares(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .Scalars
                .Select(s => s.SumOfSquares())
                .Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatio(this RGaVector<ScalarSignalFloat64> vectorSignal, RGaVector<ScalarSignalFloat64> noiseSignal)
        {
            return vectorSignal.SumOfSquares() / noiseSignal.SumOfSquares();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatio(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaVector<ScalarSignalFloat64> noiseSignal)
        {
            return vectorSignal.SumOfSquares() / noiseSignal.SumOfSquares();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVector<ScalarSignalFloat64> GetPeriodicPaddedSignal(this RGaVector<ScalarSignalFloat64> vectorSignal, int trendSampleCount, int paddingSampleCount = -1, bool useCatmullRomInterpolator = true)
        {
            return vectorSignal
                .MapScalars(s =>
                    s.GetPeriodicPaddedSignal(
                        trendSampleCount, 
                        paddingSampleCount,
                        useCatmullRomInterpolator
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaVector<ScalarSignalFloat64> GetPeriodicPaddedSignal(this XGaVector<ScalarSignalFloat64> vectorSignal, int trendSampleCount, int paddingSampleCount = -1, bool useCatmullRomInterpolator = true)
        {
            return vectorSignal
                .MapScalars(s =>
                    s.GetPeriodicPaddedSignal(
                        trendSampleCount, 
                        paddingSampleCount,
                        useCatmullRomInterpolator
                    )
                );
        }

        
        public static IReadOnlyList<ScalarSignalSpectrumComplex> GetFourierSpectrum(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount = vectorSignal.GetSampleCount();
            var samplingRate = vectorSignal.GetSamplingRate();
            var vSpaceDimensions = vectorSignal.VSpaceDimensions;
            var spectrumArray = new ScalarSignalSpectrumComplex[vSpaceDimensions];
            var samplingSpecs = new SignalSamplingSpecs(sampleCount, samplingRate);

            for (var i = 0; i < vSpaceDimensions; i++)
                spectrumArray[i] = new ScalarSignalSpectrumComplex(samplingSpecs);

            foreach (var (index, scalarSignal) in vectorSignal.IndexScalarPairs)
                spectrumArray[index] = scalarSignal.GetFourierSpectrum();

            return spectrumArray;
        }

        public static IReadOnlyList<ScalarSignalSpectrumComplex> GetFourierSpectrum(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount = vectorSignal.GetSampleCount();
            var samplingRate = vectorSignal.GetSamplingRate();
            var vSpaceDimensions = vectorSignal.VSpaceDimensions;
            var spectrumArray = new ScalarSignalSpectrumComplex[vSpaceDimensions];
            var samplingSpecs = new SignalSamplingSpecs(sampleCount, samplingRate);

            for (var i = 0; i < vSpaceDimensions; i++)
                spectrumArray[i] = new ScalarSignalSpectrumComplex(samplingSpecs);

            foreach (var (index, scalarSignal) in vectorSignal.IndexScalarPairs)
                spectrumArray[index] = scalarSignal.GetFourierSpectrum();

            return spectrumArray;
        }

        
        public static ScalarSignalSpectrumFloat64 GetEnergySpectrum(this RGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var energySpectrum = vectorSignal
                .Norm()
                .ScalarValue
                .GetEnergySpectrum();

            foreach (var scalarSignal in vectorSignal.Scalars)
            {
                var spectrum = scalarSignal.GetEnergySpectrum();

                energySpectrum.Add(spectrum);
            }

            return energySpectrum;
        }

        public static ScalarSignalSpectrumFloat64 GetEnergySpectrum(this XGaVector<ScalarSignalFloat64> vectorSignal)
        {
            var energySpectrum = vectorSignal
                .Norm()
                .ScalarValue
                .GetEnergySpectrum();

            foreach (var scalarSignal in vectorSignal.Scalars)
            {
                var spectrum = scalarSignal.GetEnergySpectrum();

                energySpectrum.Add(spectrum);
            }

            return energySpectrum;
        }
        
        public static IReadOnlyList<ScalarSignalSpectrumComplex> GetFourierSpectrum(this RGaVector<ScalarSignalFloat64> vectorSignal, DfFourierSignalInterpolatorOptions spectrumThresholdSpecs)
        {
            if (spectrumThresholdSpecs.EnergyAcPercentThreshold is <= 0 or > 1d)
                throw new ArgumentOutOfRangeException();

            if (spectrumThresholdSpecs.SignalToNoiseRatioThreshold <= 1d)
                throw new ArgumentOutOfRangeException();

            var geometricProcessor = vectorSignal.Processor;

            // Compute complete Fourier spectrum of vector component signals
            var vectorSpectrumFull =
                vectorSignal
                    .GetFourierSpectrum()
                    .RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

            // Compute a single joint energy spectrum of vector signal
            var energySpectrumFull =
                (ScalarSignalSpectrumFloat64)vectorSignal
                    .GetEnergySpectrum()
                    .RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

            var samplingSpecs = energySpectrumFull.SamplingSpecs;

            // Define time axis values
            var tValues =
                samplingSpecs.GetSampledTimeSignal();

            // Add DC components to final vector spectrum
            var vectorSpectrum =
                vectorSpectrumFull.Select(s =>
                    new ScalarSignalSpectrumComplex(samplingSpecs) { s.SamplesDc }
                ).ToImmutableArray();

            // Test total AC energy threshold
            var vectorSignalEnergyAc = vectorSignal.EnergyAc();
            if (vectorSignalEnergyAc < spectrumThresholdSpecs.EnergyAcThreshold)
            {
                // Add a single frequency to the spectrum
                var (energySample1, energySample2) =
                    energySpectrumFull
                        .SamplePairsAc
                        .OrderByDescending(p => energySpectrumFull.GetValueSumAc(p))
                        .First();

                vectorSpectrum.Add(vectorSpectrumFull.GetSample(energySample1.Index));
                vectorSpectrum.Add(vectorSpectrumFull.GetSample(energySample2.Index));

                return vectorSpectrum;
            }

            // Select all energy spectrum AC sample pairs
            var energySamplePairs =
                energySpectrumFull
                    .SamplePairsAc
                    .OrderByDescending(p =>
                        energySpectrumFull.GetValueSumAc(p)
                    ).ToArray();

            // Compute energy threshold for selecting suitable spectrum samples
            var energy = energySpectrumFull.Sum(s => s.Value);
            var energyThreshold = spectrumThresholdSpecs.EnergyAcPercentThreshold * energy;

            // Define initial error signal for gradually computing SNR
            var sumOfSquares = vectorSignal.SumOfSquares();
            var errorSignal =
                vectorSignal -
                vectorSpectrum
                    .Select(s => s.GetRealSignal(tValues))
                    .ToArray()
                    .CreateRGaVector(vectorSignal.Processor);

            var frequencyCountThreshold = spectrumThresholdSpecs.FrequencyCountThreshold;

            foreach (var (energySample1, energySample2) in energySamplePairs)
            {
                var index1 = energySample1.Index;
                var index2 = energySample2.Index;

                frequencyCountThreshold--;

                if (index1 == index2)
                {
                    // Add the selected samples to vector spectrum
                    var sample1 = vectorSpectrumFull.GetSample(index1);

                    vectorSpectrum.Add(sample1);

                    if (frequencyCountThreshold <= 0)
                        return vectorSpectrum;

                    // Update energy threshold
                    energyThreshold -= energySpectrumFull.GetValueAc(index1);

                    //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                    // Test energy threshold stop condition
                    if (energyThreshold < 0)
                        return vectorSpectrum;

                    // Update error signal
                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample1, tValues)
                            .CreateRGaVector(geometricProcessor);
                }
                else
                {
                    // Add the selected samples to vector spectrum
                    var sample1 = vectorSpectrumFull.GetSample(index1);
                    var sample2 = vectorSpectrumFull.GetSample(index2);

                    vectorSpectrum.Add(sample1);
                    vectorSpectrum.Add(sample2);

                    if (frequencyCountThreshold <= 0)
                        return vectorSpectrum;

                    // Update energy threshold
                    energyThreshold -= energySpectrumFull.GetValueAc(index1);
                    energyThreshold -= energySpectrumFull.GetValueAc(index2);

                    //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                    // Test energy threshold stop condition
                    if (energyThreshold < 0)
                        return vectorSpectrum;

                    // Update error signal
                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample1, tValues)
                            .CreateRGaVector(geometricProcessor);

                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample2, tValues)
                            .CreateRGaVector(geometricProcessor);
                }

                // Test SNR threshold stop condition
                var signalToNoiseRatio =
                    sumOfSquares / errorSignal.SumOfSquares();

                //Console.WriteLine($"SNR = {signalToNoiseRatio:G}");

                if (signalToNoiseRatio >= spectrumThresholdSpecs.SignalToNoiseRatioThreshold)
                    break;
            }

            Console.WriteLine();

            return vectorSpectrum;
        }

        public static IReadOnlyList<ScalarSignalSpectrumComplex> GetFourierSpectrum(this XGaVector<ScalarSignalFloat64> vectorSignal, DfFourierSignalInterpolatorOptions spectrumThresholdSpecs)
        {
            if (spectrumThresholdSpecs.EnergyAcPercentThreshold is <= 0 or > 1d)
                throw new ArgumentOutOfRangeException();

            if (spectrumThresholdSpecs.SignalToNoiseRatioThreshold <= 1d)
                throw new ArgumentOutOfRangeException();

            var geometricProcessor = vectorSignal.Processor;

            // Compute complete Fourier spectrum of vector component signals
            var vectorSpectrumFull =
                vectorSignal
                    .GetFourierSpectrum()
                    .RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

            // Compute a single joint energy spectrum of vector signal
            var energySpectrumFull =
                (ScalarSignalSpectrumFloat64)vectorSignal
                    .GetEnergySpectrum()
                    .RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

            var samplingSpecs = energySpectrumFull.SamplingSpecs;

            // Define time axis values
            var tValues =
                samplingSpecs.GetSampledTimeSignal();

            // Add DC components to final vector spectrum
            var vectorSpectrum =
                vectorSpectrumFull.Select(s =>
                    new ScalarSignalSpectrumComplex(samplingSpecs) { s.SamplesDc }
                ).ToImmutableArray();

            // Test total AC energy threshold
            var vectorSignalEnergyAc = vectorSignal.EnergyAc();
            if (vectorSignalEnergyAc < spectrumThresholdSpecs.EnergyAcThreshold)
            {
                // Add a single frequency to the spectrum
                var (energySample1, energySample2) =
                    energySpectrumFull
                        .SamplePairsAc
                        .OrderByDescending(p => energySpectrumFull.GetValueSumAc(p))
                        .First();

                vectorSpectrum.Add(vectorSpectrumFull.GetSample(energySample1.Index));
                vectorSpectrum.Add(vectorSpectrumFull.GetSample(energySample2.Index));

                return vectorSpectrum;
            }

            // Select all energy spectrum AC sample pairs
            var energySamplePairs =
                energySpectrumFull
                    .SamplePairsAc
                    .OrderByDescending(p =>
                        energySpectrumFull.GetValueSumAc(p)
                    ).ToArray();

            // Compute energy threshold for selecting suitable spectrum samples
            var energy = energySpectrumFull.Sum(s => s.Value);
            var energyThreshold = spectrumThresholdSpecs.EnergyAcPercentThreshold * energy;

            // Define initial error signal for gradually computing SNR
            var sumOfSquares = vectorSignal.SumOfSquares();
            var errorSignal =
                vectorSignal -
                vectorSpectrum
                    .Select(s => s.GetRealSignal(tValues))
                    .ToArray()
                    .CreateXGaVector(vectorSignal.Processor);

            var frequencyCountThreshold = spectrumThresholdSpecs.FrequencyCountThreshold;

            foreach (var (energySample1, energySample2) in energySamplePairs)
            {
                var index1 = energySample1.Index;
                var index2 = energySample2.Index;

                frequencyCountThreshold--;

                if (index1 == index2)
                {
                    // Add the selected samples to vector spectrum
                    var sample1 = vectorSpectrumFull.GetSample(index1);

                    vectorSpectrum.Add(sample1);

                    if (frequencyCountThreshold <= 0)
                        return vectorSpectrum;

                    // Update energy threshold
                    energyThreshold -= energySpectrumFull.GetValueAc(index1);

                    //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                    // Test energy threshold stop condition
                    if (energyThreshold < 0)
                        return vectorSpectrum;

                    // Update error signal
                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample1, tValues)
                            .CreateXGaVector(geometricProcessor);
                }
                else
                {
                    // Add the selected samples to vector spectrum
                    var sample1 = vectorSpectrumFull.GetSample(index1);
                    var sample2 = vectorSpectrumFull.GetSample(index2);

                    vectorSpectrum.Add(sample1);
                    vectorSpectrum.Add(sample2);

                    if (frequencyCountThreshold <= 0)
                        return vectorSpectrum;

                    // Update energy threshold
                    energyThreshold -= energySpectrumFull.GetValueAc(index1);
                    energyThreshold -= energySpectrumFull.GetValueAc(index2);

                    //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                    // Test energy threshold stop condition
                    if (energyThreshold < 0)
                        return vectorSpectrum;

                    // Update error signal
                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample1, tValues)
                            .CreateXGaVector(geometricProcessor);

                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample2, tValues)
                            .CreateXGaVector(geometricProcessor);
                }

                // Test SNR threshold stop condition
                var signalToNoiseRatio =
                    sumOfSquares / errorSignal.SumOfSquares();

                //Console.WriteLine($"SNR = {signalToNoiseRatio:G}");

                if (signalToNoiseRatio >= spectrumThresholdSpecs.SignalToNoiseRatioThreshold)
                    break;
            }

            Console.WriteLine();

            return vectorSpectrum;
        }

        
        public static string GetTextDescription(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, RGaVector<ScalarSignalFloat64> vectorSignal1)
        {
            var composer = new LinearTextComposer();

            var geometricProcessor = vectorSignal1.Processor;
            
            var samplingSpecs = vectorSignal1.GetSamplingSpecs();
            var tValues = samplingSpecs.GetSampledTimeSignal();

            //for (var i = 0; i < vSpaceDimensions; i++)
            //{
            //    var scalarSpectrum = vectorSpectrum[i];
            //    var scalarSignal = vectorSignal1[i];

            //    composer
            //        .AppendLine($"Signal component {i + 1}")
            //        .IncreaseIndentation()
            //        .AppendLine(scalarSpectrum.GetTextDescription(scalarSignal))
            //        .AppendLine()
            //        .DecreaseIndentation();
            //}

            composer
                .AppendLine("Vector Signal")
                .IncreaseIndentation();

            composer
                .AppendLine("Original Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy: {vectorSignal1.Energy():G}")
                .AppendLine($"Energy DC: {vectorSignal1.EnergyDc():G}")
                .AppendLine($"Energy AC: {vectorSignal1.EnergyAc():G}")
                .AppendLine()
                .DecreaseIndentation();

            var vectorSignal2 =
                vectorSpectrum.GetRealSignal(tValues).CreateRGaVector(geometricProcessor);

            composer
                .AppendLine("Interpolated Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy: {vectorSignal2.Energy():G}")
                .AppendLine($"Energy DC: {vectorSignal2.EnergyDc():G}")
                .AppendLine($"Energy AC: {vectorSignal2.EnergyAc():G}")
                .AppendLine()
                .DecreaseIndentation();

            var errorSignal =
                vectorSignal1 - vectorSignal2;

            composer
                .AppendLine("Error Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy ratio: {vectorSignal2.Energy() / vectorSignal1.Energy():P3}")
                .AppendLine($"Energy ratio DC: {vectorSignal2.EnergyDc() / vectorSignal1.EnergyDc():P3}")
                .AppendLine($"Energy ratio AC: {vectorSignal2.EnergyAc() / vectorSignal1.EnergyAc():P3}")
                .AppendLine($"Signal to noise ratio: {vectorSignal1.SignalToNoiseRatio(errorSignal)}")
                .AppendLine()
                .DecreaseIndentation();

            var minFreqHz =
                vectorSpectrum
                    .Select(s => s.FrequencyMinHz)
                    .Min();

            var maxFreqHz =
                vectorSpectrum
                    .Select(s => s.FrequencyMaxHz)
                    .Max();

            var freqSampleCount =
                vectorSpectrum
                    .SelectMany(s => s.FrequencyIndices)
                    .Distinct()
                    .Count();

            composer
                .AppendLine("Spectrum:")
                .IncreaseIndentation()
                .AppendLine($"Frequency range: {minFreqHz:G}, {maxFreqHz:G}")
                .AppendLine($"Frequency sample count: {freqSampleCount}")
                .AppendLine()
                .DecreaseIndentation();

            composer
                .AppendLine()
                .DecreaseIndentation()
                .DecreaseIndentation();

            return composer.ToString();
        }

        public static string GetTextDescription(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, XGaVector<ScalarSignalFloat64> vectorSignal1)
        {
            var composer = new LinearTextComposer();

            var geometricProcessor = vectorSignal1.Processor;
            
            var samplingSpecs = vectorSignal1.GetSamplingSpecs();
            var tValues = samplingSpecs.GetSampledTimeSignal();

            //for (var i = 0; i < vSpaceDimensions; i++)
            //{
            //    var scalarSpectrum = vectorSpectrum[i];
            //    var scalarSignal = vectorSignal1[i];

            //    composer
            //        .AppendLine($"Signal component {i + 1}")
            //        .IncreaseIndentation()
            //        .AppendLine(scalarSpectrum.GetTextDescription(scalarSignal))
            //        .AppendLine()
            //        .DecreaseIndentation();
            //}

            composer
                .AppendLine("Vector Signal")
                .IncreaseIndentation();

            composer
                .AppendLine("Original Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy: {vectorSignal1.Energy():G}")
                .AppendLine($"Energy DC: {vectorSignal1.EnergyDc():G}")
                .AppendLine($"Energy AC: {vectorSignal1.EnergyAc():G}")
                .AppendLine()
                .DecreaseIndentation();

            var vectorSignal2 =
                vectorSpectrum.GetRealSignal(tValues).CreateXGaVector(geometricProcessor);

            composer
                .AppendLine("Interpolated Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy: {vectorSignal2.Energy():G}")
                .AppendLine($"Energy DC: {vectorSignal2.EnergyDc():G}")
                .AppendLine($"Energy AC: {vectorSignal2.EnergyAc():G}")
                .AppendLine()
                .DecreaseIndentation();

            var errorSignal =
                vectorSignal1 - vectorSignal2;

            composer
                .AppendLine("Error Signal:")
                .IncreaseIndentation()
                .AppendLine($"Energy ratio: {vectorSignal2.Energy() / vectorSignal1.Energy():P3}")
                .AppendLine($"Energy ratio DC: {vectorSignal2.EnergyDc() / vectorSignal1.EnergyDc():P3}")
                .AppendLine($"Energy ratio AC: {vectorSignal2.EnergyAc() / vectorSignal1.EnergyAc():P3}")
                .AppendLine($"Signal to noise ratio: {vectorSignal1.SignalToNoiseRatio(errorSignal)}")
                .AppendLine()
                .DecreaseIndentation();

            var minFreqHz =
                vectorSpectrum
                    .Select(s => s.FrequencyMinHz)
                    .Min();

            var maxFreqHz =
                vectorSpectrum
                    .Select(s => s.FrequencyMaxHz)
                    .Max();

            var freqSampleCount =
                vectorSpectrum
                    .SelectMany(s => s.FrequencyIndices)
                    .Distinct()
                    .Count();

            composer
                .AppendLine("Spectrum:")
                .IncreaseIndentation()
                .AppendLine($"Frequency range: {minFreqHz:G}, {maxFreqHz:G}")
                .AppendLine($"Frequency sample count: {freqSampleCount}")
                .AppendLine()
                .DecreaseIndentation();

            composer
                .AppendLine()
                .DecreaseIndentation()
                .DecreaseIndentation();

            return composer.ToString();
        }
    }
}
