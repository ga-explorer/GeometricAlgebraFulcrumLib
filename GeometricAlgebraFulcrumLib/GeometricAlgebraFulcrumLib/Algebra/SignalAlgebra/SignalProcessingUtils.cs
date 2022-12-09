using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra.Functions;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra
{
    public static class SignalProcessingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSamplingRate(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.GetScalars().First().SamplingRate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSampleCount(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.GetScalars().Max(s => s.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignalSamplingSpecs GetSamplingSpecs(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount = vectorSignal.GetSampleCount();
            var samplingRate = vectorSignal.GetSamplingRate();

            return new SignalSamplingSpecs(sampleCount, samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<ScalarSignalFloat64> GetSubSignal(this GaVector<ScalarSignalFloat64> vectorSignal, int index, int count)
        {
            return vectorSignal
                .MapScalars(
                    scalarSignal => scalarSignal.GetSubSignal(index, count)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<ScalarSignalFloat64> GetSubSignal(this GaBivector<ScalarSignalFloat64> bivectorSignal, int index, int count)
        {
            return bivectorSignal
                .MapScalars(
                    scalarSignal => scalarSignal.GetSubSignal(index, count)
                );
        }

        public static double[,] ToArray(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            var colCount = (int)vectorSignal.GeometricProcessor.VSpaceDimension;
            var rowCount = vectorSignal.GetScalars().Max(s => s.Count);

            var array = new double[rowCount, colCount];

            foreach (var (j, scalarList) in vectorSignal.GetIndexScalarRecords())
                for (var i = 0; i < scalarList.Count; i++)
                    array[i, j] = scalarList[i];

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] ToArray(this IScalarAlgebraProcessor<double> scalarProcessor, GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.ToArray();
        }

        public static T[,] ToArray<T>(this GaVector<IReadOnlyList<T>> vectorSignal, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            var colCount = (int)vectorSignal.GeometricProcessor.VSpaceDimension;
            var rowCount = vectorSignal.GetScalars().Max(s => s.Count);

            var array =
                scalarProcessor.CreateArrayZero2D(rowCount, colCount);

            foreach (var (j, scalarList) in vectorSignal.GetIndexScalarRecords())
                for (var i = 0; i < scalarList.Count; i++)
                    array[i, j] = scalarList[i] ?? scalarProcessor.ScalarZero;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IScalarAlgebraProcessor<T> scalarProcessor, GaVector<IReadOnlyList<T>> vectorSignal)
        {
            return vectorSignal.ToArray(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix ToMatrix(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return (Matrix)Matrix.Build.DenseOfArray(vectorSignal.ToArray());
        }

        public static GaVector<ScalarSignalFloat64> ToVectorSignal(this IGeometricAlgebraProcessor<ScalarSignalFloat64> geometricProcessor, Matrix vectorMatrix, double samplingRate)
        {
            var sampleCount = vectorMatrix.RowCount;
            var scalarArray = new ScalarSignalFloat64[geometricProcessor.VSpaceDimension];

            for (var j = 0; j < geometricProcessor.VSpaceDimension; j++)
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

        public static IEnumerable<GaVector<T>> ToVectorList<T>(this GaVector<IReadOnlyList<T>> vectorSignal, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var array = vectorSignal.ToArray(geometricProcessor);

            var rowCount = array.GetLength(0);
            var colCount = array.GetLength(1);

            for (var i = 0; i < rowCount; i++)
            {
                var scalarArray = geometricProcessor.CreateArrayZero1D(colCount);

                for (var j = 0; j < colCount; j++)
                    scalarArray[j] = array[i, j];

                yield return geometricProcessor.CreateVector(scalarArray);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> ToVectorList<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, GaVector<IReadOnlyList<T>> vectorSignal)
        {
            return vectorSignal.ToVectorList(geometricProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<double> Mean(this IScalarAlgebraProcessor<double> scalarProcessor, Scalar<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal.ScalarValue.Mean().CreateScalar(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<double> Mean(this GaVector<ScalarSignalFloat64> vectorSignal, IGeometricAlgebraProcessor<double> geometricProcessor)
        {
            return geometricProcessor.Mean(vectorSignal);
        }

        public static GaVector<double> Mean(this IGeometricAlgebraProcessor<double> geometricProcessor, GaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount =
                vectorSignal.GetScalars().Max(s => s.Count);

            var indexScalarDictionary = new Dictionary<ulong, double>();

            foreach (var (index, scalar) in vectorSignal.GetIndexScalarRecords())
                indexScalarDictionary.Add(index, scalar.Sum() / sampleCount);

            return geometricProcessor.CreateVector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<double> Mean(this GaBivector<ScalarSignalFloat64> bivectorSignal, IGeometricAlgebraProcessor<double> geometricProcessor)
        {
            return geometricProcessor.Mean(bivectorSignal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<double> Sum(this GaBivector<ScalarSignalFloat64> bivectorSignal, IGeometricAlgebraProcessor<double> geometricProcessor)
        {
            return geometricProcessor.Sum(bivectorSignal);
        }

        public static GaBivector<double> Sum(this IGeometricAlgebraProcessor<double> geometricProcessor, GaBivector<ScalarSignalFloat64> bivectorSignal)
        {
            if (bivectorSignal.IsZero())
                return geometricProcessor.CreateBivectorZero();

            var indexScalarDictionary = new Dictionary<ulong, double>();

            foreach (var (index, scalar) in bivectorSignal.GetIndexScalarRecords())
                indexScalarDictionary.Add(index, scalar.Sum());

            return geometricProcessor.CreateBivector(indexScalarDictionary);
        }

        public static GaBivector<double> Mean(this IGeometricAlgebraProcessor<double> geometricProcessor, GaBivector<ScalarSignalFloat64> bivectorSignal)
        {
            if (bivectorSignal.IsZero())
                return geometricProcessor.CreateBivectorZero();

            var sampleCount =
                bivectorSignal.GetScalars().Max(s => s.Count);

            var indexScalarDictionary = new Dictionary<ulong, double>();

            foreach (var (index, scalar) in bivectorSignal.GetIndexScalarRecords())
                indexScalarDictionary.Add(index, scalar.Sum() / sampleCount);

            return geometricProcessor.CreateBivector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<ScalarSignalFloat64> RunningAverage(this Scalar<ScalarSignalFloat64> scalarSignal, int averageSampleCount)
        {
            return scalarSignal
                .ScalarValue
                .RunningAverage(averageSampleCount)
                .CreateScalar(scalarSignal.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<ScalarSignalFloat64> RunningAverage(this GaVector<ScalarSignalFloat64> vectorSignal, int averageSampleCount)
        {
            return vectorSignal.MapScalars(s =>
                s.RunningAverage(averageSampleCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<ScalarSignalFloat64> RunningAverage(this GaBivector<ScalarSignalFloat64> bivectorSignal, int averageSampleCount)
        {
            return bivectorSignal.MapScalars(s =>
                s.RunningAverage(averageSampleCount)
            );
        }

        public static IReadOnlyList<GaVector<T>> MapComponentSignals<T>(this IReadOnlyList<GaVector<T>> vectorSamples, Func<IReadOnlyList<T>, IReadOnlyList<T>> componentMapping)
        {
            var sampleCount = vectorSamples.Count;
            var geometricProcessor = vectorSamples[0].GeometricProcessor;
            var vSpaceDimension = (int)geometricProcessor.VSpaceDimension;

            var componentSignals = new List<T[]>(vSpaceDimension);

            for (var i = 0; i < vSpaceDimension; i++)
                componentSignals.Add(new T[sampleCount]);

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                for (var i = 0; i < vSpaceDimension; i++)
                    componentSignals[i][sampleIndex] = vectorSamples[sampleIndex][i];
            }

            for (var i = 0; i < vSpaceDimension; i++)
                componentSignals[i] = componentMapping(componentSignals[i]).ToArray();

            var mappedVectorSignal = new GaVector<T>[sampleCount];

            for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
            {
                var scalarArray = new T[vSpaceDimension];

                for (var i = 0; i < vSpaceDimension; i++)
                    scalarArray[i] = componentSignals[i][sampleIndex];

                mappedVectorSignal[sampleIndex] = geometricProcessor.CreateVector(scalarArray);
            }

            return mappedVectorSignal;
        }

        private static IEnumerable<double> CrossCorrelationWiener(IReadOnlyList<double> scalarSignal, int newOrder, int oldOrder)
        {
            var nTaps = scalarSignal.Count;
            var nSize = nTaps > newOrder ? nTaps : newOrder;

            var outData = new List<double>(2 * nTaps + 1);
            for (var i = -nTaps; i < nTaps; ++i)
            {
                var tsSum = 0.0;
                for (var j = 0; j < nSize; ++j)
                {
                    if (j + i < nTaps && j < newOrder && j + i > -1)
                        tsSum += scalarSignal[j + i];
                }

                outData.Add(tsSum);
            }

            var outVector = new List<double>(nTaps + 1);

            for (var i = nTaps; i >= 0; i--)
            {
                var index = outData.Count - oldOrder - i;

                outVector.Add(outData[index]);
            }

            return outVector;
        }

        public static ScalarSignalFloat64 WienerFilter(this IReadOnlyList<double> scalarSignal, double samplingRate, int order)
        {
            var newOrder = 2 * order + 1;

            // Estimate the local mean
            var localMean =
                CrossCorrelationWiener(scalarSignal, newOrder, order)
                    .Select(value => value / newOrder).ToArray();

            // Estimate the local variance
            var t2Series =
                scalarSignal.Select(value => Math.Pow(value, 2)).ToArray();

            var localVariance =
                CrossCorrelationWiener(t2Series, newOrder, order)
                    .Select((value, index) =>
                        value / newOrder - Math.Pow(localMean[index], 2)
                    ).ToArray();

            // Estimate the noise power
            var noisePowerEstimate = localVariance.Sum() / localVariance.Length;

            return scalarSignal
                .Select((t, i) =>
                    localVariance[i] < noisePowerEstimate
                        ? localMean[i]
                        : (t - localMean[i]) * (1 - noisePowerEstimate / localVariance[i]) + localMean[i]
                )
                .Select(v => double.IsNaN(v) ? 0d : v)
                .CreateSignal(samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 WienerFilter(this ScalarSignalFloat64 scalarSignal, int order)
        {
            return scalarSignal.WienerFilter(scalarSignal.SamplingRate, order);
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
        public static IReadOnlyList<GaVector<double>> WienerFilter1D(this IReadOnlyList<GaVector<double>> vectorSamples, double samplingRate, int order)
        {
            return vectorSamples.MapComponentSignals(
                c => c.WienerFilter(samplingRate, order)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<ScalarSignalFloat64> NormWienerFilter(this GaVector<ScalarSignalFloat64> vectorSamples, int order)
        {
            var norm1 = vectorSamples.Norm();
            var norm2 = norm1.WienerFilter(order);

            return vectorSamples * (norm2 / norm1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatio(this IEnumerable<double> vectorSignal, IEnumerable<double> noiseSignal)
        {
            return vectorSignal.Select(s => s.Square()).Average() /
                   noiseSignal.Select(s => s.Square()).Average();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatioDb(this IEnumerable<double> vectorSignal, IEnumerable<double> noiseSignal)
        {
            return (vectorSignal.Select(s => s.Square()).Average() /
                    noiseSignal.Select(s => s.Square()).Average()).Log10() * 10d;
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
        //public static double SignalToNoiseRatio(this GaVector<ScalarSignalFloat64> vectorSignal, GaVector<ScalarSignalFloat64> noiseSignal)
        //{
        //    return vectorSignal.NormSquared().ScalarValue.Average() / 
        //           noiseSignal.NormSquared().ScalarValue.Average();
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatioDb(this GaVector<ScalarSignalFloat64> vectorSignal, GaVector<ScalarSignalFloat64> noiseSignal)
        {
            return (vectorSignal.NormSquared().ScalarValue.Average() /
                    noiseSignal.NormSquared().ScalarValue.Average()).Log10() * 10d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 FourierInterpolate(this ScalarSignalFloat64 scalarSignal, double samplingRate, double energyThreshold = 0.998)
        {
            var normSignal = scalarSignal.CreateSignal(samplingRate);

            var normInterpolator = normSignal.CreateFourierInterpolator(
                normSignal.GetDominantFrequencyIndexSet(energyThreshold)
            );

            var t = 0d.GetLinearRange(
                (scalarSignal.Count - 1) / samplingRate,
                scalarSignal.Count
            );

            return normInterpolator.GetScalars(t).CreateSignal(samplingRate);
        }


        public static Pair<GaVector<double>> Pca2(this IGeometricAlgebraProcessor<double> geometricProcessor, GaVector<ScalarSignalFloat64> vectorSignal)
        {
            var vSpaceDimension = (int)geometricProcessor.VSpaceDimension;
            var colCount = vSpaceDimension;
            var rowCount = vectorSignal.GetScalars().Max(s => s.Count);

            var matrix = Matrix<double>.Build.Dense(rowCount, colCount);

            foreach (var (colIndex, columnArray) in vectorSignal.GetIndexScalarRecords())
            {
                for (var rowIndex = 0; rowIndex < columnArray.Count; rowIndex++)
                    matrix[rowIndex, (int)colIndex] = columnArray[rowIndex];
            }

            var svdData = matrix.Svd();

            Console.WriteLine(
                "Singular values:" + svdData.S.Select(v => v.ToString(CultureInfo.InvariantCulture)).Concatenate(", ")
            );

            var vMatrix = svdData.VT;

            var v1ScalarArray = new double[vSpaceDimension];
            var v2ScalarArray = new double[vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
            {
                v1ScalarArray[i] = vMatrix[0, i];
                v2ScalarArray[i] = vMatrix[1, i];
            }

            return new Pair<GaVector<double>>(
                geometricProcessor.CreateVector(v1ScalarArray),
                geometricProcessor.CreateVector(v2ScalarArray)
            );
        }



        public static GaVector<ScalarSignalFloat64> GetSmoothedNormSignal(this GaVector<ScalarSignalFloat64> vectorSignal, int interpolationSampleCount = 128, int polynomialOrder = 7)
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

        public static GaVector<ScalarSignalFloat64> GetSmoothedSignal(this IGeometricAlgebraProcessor<double> processor, GaVector<ScalarSignalFloat64> vectorSignal, int interpolationSampleCount = 128, int polynomialOrder = 7)
        {
            var vectorInterpolator =
                processor.CreatePolynomialInterpolator(vectorSignal, vectorSignal.GetSamplingRate());

            vectorInterpolator.InterpolationSamples = interpolationSampleCount;
            vectorInterpolator.PolynomialOrder = polynomialOrder;

            return vectorInterpolator.GetVectors();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 ReSampleForBezierSmoothing(this ScalarSignalFloat64 signal, int bezierDegree)
        {
            var sampleCount =
                (int)(Math.Ceiling((signal.Count - 1) / (double)bezierDegree) * bezierDegree) + 1;

            return signal.Count == sampleCount
                ? signal
                : signal.ReSample(sampleCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AkimaSplineFunction CreateSmoothedAkimaSplineFunction(this ScalarSignalFloat64 signal, int bezierDegree)
        {
            var reSampledSignal =
                signal.ReSampleForBezierSmoothing(bezierDegree);

            var tSignal =
                reSampledSignal.GetTimeValuesSignal();

            var (xArray, yArray) =
                reSampledSignal.GetBezierSmoothingPairs(
                    tSignal,
                    bezierDegree,
                    true
                );

            var f = yArray.CreateAkimaSplineFunction(xArray, true);

            return f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SmoothedCatmullRomSplineD0Function CreateSmoothedCatmullRomSplineD0Function(this ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        {
            return SmoothedCatmullRomSplineD0Function.CreateSmoothedCatmullRomSplineD0Function(
                signal,
                bezierDegree,
                downSampleCount,
                curveType
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IScalarD1Function CreateSmoothedCatmullRomSplineD1Function(this ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        {
            return SmoothedCatmullRomSplineD1Function.CreateSmoothedCatmullRomSplineD1Function(
                signal,
                bezierDegree,
                downSampleCount,
                curveType
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IScalarD2Function CreateSmoothedCatmullRomSplineD2Function(this ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        {
            return SmoothedCatmullRomSplineD2Function.CreateSmoothedCatmullRomSplineD2Function(
                signal,
                bezierDegree,
                downSampleCount,
                curveType
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IScalarD3Function CreateSmoothedCatmullRomSplineD3Function(this ScalarSignalFloat64 signal, int bezierDegree, int downSampleCount = 0, CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal)
        {
            return SmoothedCatmullRomSplineD3Function.CreateSmoothedCatmullRomSplineD3Function(
                signal,
                bezierDegree,
                downSampleCount,
                curveType
            );
        }


        public static void PlotSignal(this ScalarSignalFloat64 scalarSignal1, ScalarSignalFloat64 scalarSignal2, double tMin, double tMax, string plotFileName)
        {
            var samplingSpecs = scalarSignal1.SamplingSpecs;

            var pm = new PlotModel
            {
                //Title = "title",
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            var s1 = new FunctionSeries(
                t => scalarSignal1.LinearInterpolation(t - tMin),
                tMin,
                tMax,
                samplingSpecs.SampleCount,
                @"Signal 1"
            )
            {
                LineStyle = LineStyle.Dot,
                StrokeThickness = 1,
                //MarkerType = MarkerType.Diamond,
                //MarkerStrokeThickness = 1,
                //MarkerSize = 4
            };


            var s2 = new FunctionSeries(
                t => scalarSignal2.LinearInterpolation(t - tMin),
                tMin,
                tMax,
                samplingSpecs.SampleCount * 2,
                @$"Signal 2"
            )
            {
                StrokeThickness = 1.5
            };

            pm.Series.Add(s1);
            pm.Series.Add(s2);

            //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            PngExporter.Export(pm, $"{plotFileName}.png", samplingSpecs.SampleCount * 2, 750, 200);
        }

        public static void PlotSignal(this Scalar<ScalarSignalFloat64> scalarSignal1, Scalar<ScalarSignalFloat64> scalarSignal2, double tMin, double tMax, string plotFileName)
        {
            scalarSignal1.ScalarValue.PlotSignal(scalarSignal2.ScalarValue, tMin, tMax, plotFileName);
        }

        public static void PlotSignal(this GaVector<ScalarSignalFloat64> vectorSignal1, GaVector<ScalarSignalFloat64> vectorSignal2, double tMin, double tMax, string plotFileName)
        {
            var vSpaceDimension = vectorSignal1.GeometricProcessor.VSpaceDimension;
            var samplingSpecs = vectorSignal1.GetSamplingSpecs();

            for (var i = 0; i < vSpaceDimension; i++)
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

        public static void PlotVectorSignalComponents(this GaVector<ScalarSignalFloat64> vectorSignal, string title, string filePath)
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

            foreach (var (index, scalarSignal) in vectorSignal.GetIdScalarRecords())
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

        public static void PlotScalarSignal(this ScalarSignalFloat64 scalarSignal, string title, string filePath)
        {
            filePath = Path.Combine(filePath);

            const int sampleTrim = 0;
            var tMin = sampleTrim / scalarSignal.SamplingRate;
            var tMax = (scalarSignal.Count - 1 - sampleTrim) / scalarSignal.SamplingRate;

            var pm = new PlotModel
            {
                Title = title,
                Background = OxyColor.FromRgb(255, 255, 255)
            };

            var s1 = new FunctionSeries(
                scalarSignal.LinearInterpolation,
                tMin,
                tMax,
                scalarSignal.Count * 2
            );

            pm.Series.Add(s1);

            //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
            PngExporter.Export(pm, filePath + ".png", scalarSignal.Count * 2, 750, 200);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Energy(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .GetScalars()
                .Select(s => s.Energy())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EnergyDc(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .GetScalars()
                .Select(s => s.EnergyDc())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EnergyAc(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .GetScalars()
                .Select(s => s.EnergyAc())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SumOfSquares(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            return vectorSignal
                .GetScalars()
                .Select(s => s.SumOfSquares())
                .Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignalToNoiseRatio(this GaVector<ScalarSignalFloat64> vectorSignal, GaVector<ScalarSignalFloat64> noiseSignal)
        {
            return vectorSignal.SumOfSquares() / noiseSignal.SumOfSquares();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Complex> GetValue(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, IReadOnlyList<ScalarSignalSpectrumComplex.SignalSpectrumSample> vectorSpectrumSample, double t)
        {
            var valueArray = new Complex[vectorSpectrumSample.Count];

            for (var i = 0; i < vectorSpectrumSample.Count; i++)
            {
                valueArray[i] = vectorSpectrum[i].GetValue(vectorSpectrumSample[i], t);
            }

            return valueArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<ScalarSignalFloat64> GetRealSignal(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, ScalarSignalFloat64 tValues)
        {
            return vectorSpectrum
                .Select(spectrum => spectrum.GetRealSignal(tValues))
                .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<ScalarSignalFloat64> GetRealSignalDt(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, int degree, ScalarSignalFloat64 tValues)
        {
            return vectorSpectrum
                .Select(spectrum => spectrum.GetRealSignalDt(degree, tValues))
                .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<ScalarSignalFloat64> GetRealSignal(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, IReadOnlyList<ScalarSignalSpectrumComplex.SignalSpectrumSample> vectorSpectrumSample, IEnumerable<double> tValues)
        {
            return vectorSpectrum
                .Select((spectrum, i) => spectrum.GetRealSignal(vectorSpectrumSample[i], tValues))
                .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<ScalarSignalFloat64> GetPolynomialPaddedSignal(this GaVector<ScalarSignalFloat64> vectorSignal, int trendSampleCount, int polynomialDegree)
        {
            return vectorSignal
                .MapScalars(s =>
                    s.GetPolynomialPaddedSignal(trendSampleCount, polynomialDegree)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<ScalarSignalSpectrumComplex.SignalSpectrumSample> GetSample(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, int index)
        {
            return vectorSpectrum
                .Select(s => s.GetSample(index))
                .ToImmutableArray();
        }

        public static IReadOnlyList<ScalarSignalSpectrumComplex> Add(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, IEnumerable<ScalarSignalSpectrumComplex.SignalSpectrumSample> vectorSpectrumSample)
        {
            var i = 0;
            foreach (var sample in vectorSpectrumSample)
            {
                vectorSpectrum[i].Add(sample);

                i++;
            }

            return vectorSpectrum;
        }

        public static IReadOnlyList<ScalarSignalSpectrumComplex> GetFourierSpectrum(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            var sampleCount = vectorSignal.GetSampleCount();
            var samplingRate = vectorSignal.GetSamplingRate();
            var vSpaceDimension = (int)vectorSignal.GeometricProcessor.VSpaceDimension;
            var spectrumArray = new ScalarSignalSpectrumComplex[vSpaceDimension];
            var samplingSpecs = new SignalSamplingSpecs(sampleCount, samplingRate);

            for (var i = 0; i < vSpaceDimension; i++)
                spectrumArray[i] = new ScalarSignalSpectrumComplex(samplingSpecs);

            foreach (var (index, scalarSignal) in vectorSignal.GetIndexScalarRecords())
                spectrumArray[index] = scalarSignal.GetFourierSpectrum();

            return spectrumArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<ScalarSignalSpectrumFloat64> RemoveHighFrequencySamples(this IReadOnlyList<ScalarSignalSpectrumFloat64> vectorSpectrum, double cutoffFrequency)
        {
            return vectorSpectrum
                .Select(s => (ScalarSignalSpectrumFloat64)s.RemoveHighFrequencySamples(cutoffFrequency))
                .ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<ScalarSignalSpectrumComplex> RemoveHighFrequencySamples(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, double cutoffFrequency)
        {
            return vectorSpectrum
                .Select(s => (ScalarSignalSpectrumComplex)s.RemoveHighFrequencySamples(cutoffFrequency))
                .ToArray();
        }

        public static ScalarSignalSpectrumFloat64 GetEnergySpectrum(this GaVector<ScalarSignalFloat64> vectorSignal)
        {
            var energySpectrum = vectorSignal
                .Norm()
                .ScalarValue
                .GetEnergySpectrum();

            foreach (var scalarSignal in vectorSignal.GetScalars())
            {
                var spectrum = scalarSignal.GetEnergySpectrum();

                energySpectrum.Add(spectrum);
            }

            return energySpectrum;
        }

        public static IReadOnlyList<ScalarSignalSpectrumComplex> GetFourierSpectrum(this GaVector<ScalarSignalFloat64> vectorSignal, SpectrumInterpolationOptions spectrumThresholdSpecs)
        {
            if (spectrumThresholdSpecs.EnergyAcPercentThreshold is <= 0 or > 1d)
                throw new ArgumentOutOfRangeException();

            if (spectrumThresholdSpecs.SignalToNoiseRatioThreshold <= 1d)
                throw new ArgumentOutOfRangeException();

            var geometricProcessor = vectorSignal.GeometricProcessor;

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
                samplingSpecs.GetTimeValuesSignal();

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
                    .CreateVector(vectorSignal.GeometricProcessor);

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
                            .CreateVector(geometricProcessor);
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
                            .CreateVector(geometricProcessor);

                    errorSignal -=
                        vectorSpectrumFull
                            .GetRealSignal(sample2, tValues)
                            .CreateVector(geometricProcessor);
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

        public static string GetTextDescription(this IReadOnlyList<ScalarSignalSpectrumComplex> vectorSpectrum, GaVector<ScalarSignalFloat64> vectorSignal1)
        {
            var composer = new LinearTextComposer();

            var geometricProcessor = vectorSignal1.GeometricProcessor;
            var vSpaceDimension = (int)geometricProcessor.VSpaceDimension;
            var samplingSpecs = vectorSignal1.GetSamplingSpecs();
            var tValues = samplingSpecs.GetTimeValuesSignal();

            //for (var i = 0; i < vSpaceDimension; i++)
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
                vectorSpectrum.GetRealSignal(tValues).CreateVector(geometricProcessor);

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


        public static Image Plot(this Func<double, double> scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax)
        {
            var model = new PlotModel
            {
                Background = OxyColors.White
            };

            var dx = (xMax - xMin) / 1024;

            model.Axes.Add(
                new LinearAxis()
                {
                    Minimum = xMin - (xMax - xMin) / 20,
                    Maximum = xMax + (xMax - xMin) / 20,
                    Position = AxisPosition.Bottom
                }
            );

            model.Series.Add(
                new FunctionSeries(scalarFunction, xMin, xMax, dx)
                {
                    Color = OxyColors.Blue,
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2
                }
            );

            model.Series.Add(
                new FunctionSeries(scalarSignal.LinearInterpolation, xMin, xMax, dx)
                {
                    Color = OxyColors.Black,
                    LineStyle = LineStyle.Dot,
                    StrokeThickness = 1
                }
            );

            var tSignal = scalarSignal.GetTimeValuesSignal();
            var scatterPoints =
                Enumerable
                    .Range(0, scalarSignal.Count)
                    .Select(i => new ScatterPoint(tSignal[i], scalarSignal[i]))
                    .ToList();

            var scatterSeries = new ScatterSeries
            {
                MarkerSize = 4,
                MarkerStroke = OxyColors.Green,
                MarkerFill = OxyColors.Transparent,
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle
            };

            scatterSeries.Points.AddRange(scatterPoints);

            model.Series.Add(scatterSeries);

            var renderer = new PngExporter
            {
                //Dpi = 120,
                Width = 1200,
                Height = 600
            };

            using var stream = new MemoryStream();
            renderer.Export(model, stream);

            stream.Position = 0;

            return Image.Load(stream);
        }

        public static Image Plot(this ScalarSignalFloat64 scalarSignal, double xMin, double xMax)
        {
            return ((Func<double, double>)scalarSignal.LinearInterpolation).Plot(xMin, xMax);
        }

        public static Image PlotValue(this IScalarD0Function scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax)
        {
            return ((Func<double, double>)scalarFunction.GetValue).Plot(scalarSignal, xMin, xMax);
        }

        public static Image PlotFirstDerivative(this IScalarD1Function scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax)
        {
            return ((Func<double, double>)scalarFunction.GetFirstDerivativeValue).Plot(scalarSignal, xMin, xMax);
        }

        public static Image PlotSecondDerivative(this IScalarD2Function scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax)
        {
            return ((Func<double, double>)scalarFunction.GetSecondDerivativeValue).Plot(scalarSignal, xMin, xMax);
        }

        public static Image Plot(this Func<double, double> scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax, double yMin, double yMax)
        {
            var model = new PlotModel
            {
                Background = OxyColors.White
            };

            var dx = (xMax - xMin) / 1024;

            model.Axes.Add(
                new LinearAxis()
                {
                    Minimum = xMin - (xMax - xMin) / 20,
                    Maximum = xMax + (xMax - xMin) / 20,
                    Position = AxisPosition.Bottom
                }
            );

            model.Axes.Add(
                new LinearAxis()
                {
                    Minimum = yMin - (yMax - yMin) / 20,
                    Maximum = yMax + (yMax - yMin) / 20,
                    Position = AxisPosition.Left
                }
            );

            model.Series.Add(
                new FunctionSeries(scalarFunction, xMin, xMax, dx)
                {
                    Color = OxyColors.Blue,
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 2
                }
            );

            model.Series.Add(
                new FunctionSeries(scalarSignal.LinearInterpolation, xMin, xMax, dx)
                {
                    Color = OxyColors.Black,
                    LineStyle = LineStyle.LongDash,
                    StrokeThickness = 1
                }
            );

            var tSignal = scalarSignal.GetTimeValuesSignal();
            var scatterPoints =
                Enumerable
                    .Range(0, scalarSignal.Count)
                    .Select(i => new ScatterPoint(tSignal[i], scalarSignal[i]))
                    .ToList();

            var scatterSeries = new ScatterSeries
            {
                MarkerSize = 4,
                MarkerStroke = OxyColors.Green,
                MarkerFill = OxyColors.Transparent,
                MarkerStrokeThickness = 1,
                MarkerType = MarkerType.Circle
            };

            scatterSeries.Points.AddRange(scatterPoints);

            model.Series.Add(scatterSeries);

            var renderer = new PngExporter
            {
                //Dpi = 120,
                Width = 1200,
                Height = 600
            };

            using var stream = new MemoryStream();
            renderer.Export(model, stream);

            stream.Position = 0;

            return Image.Load(stream);
        }

        public static Image Plot(this ScalarSignalFloat64 scalarSignal, double xMin, double xMax, double yMin, double yMax)
        {
            return ((Func<double, double>)scalarSignal.LinearInterpolation).Plot(xMin, xMax, yMin, yMax);
        }

        public static Image PlotValue(this IScalarD0Function scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax, double yMin, double yMax)
        {
            return ((Func<double, double>)scalarFunction.GetValue).Plot(scalarSignal, xMin, xMax, yMin, yMax);
        }

        public static Image PlotFirstDerivative(this IScalarD1Function scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax, double yMin, double yMax)
        {
            return ((Func<double, double>)scalarFunction.GetFirstDerivativeValue).Plot(scalarSignal, xMin, xMax, yMin, yMax);
        }

        public static Image PlotSecondDerivative(this IScalarD2Function scalarFunction, ScalarSignalFloat64 scalarSignal, double xMin, double xMax, double yMin, double yMax)
        {
            return ((Func<double, double>)scalarFunction.GetSecondDerivativeValue).Plot(scalarSignal, xMin, xMax, yMin, yMax);
        }
    }
}
