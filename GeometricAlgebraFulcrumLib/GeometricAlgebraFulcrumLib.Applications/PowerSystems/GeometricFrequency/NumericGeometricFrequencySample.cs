using System.Collections.Immutable;
using System.Diagnostics;
using System.Drawing;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Algebra.Signals.Interpolators;
using GeometricAlgebraFulcrumLib.Algebra.Signals.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Utilities;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Interpolators;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Mutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Svg.DrawingBoard;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using OfficeOpenXml;
using Float64SignalComposerUtils = GeometricAlgebraFulcrumLib.Algebra.Signals.Float64SignalComposerUtils;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.GeometricFrequency;

public static class NumericGeometricFrequencySample
{
    private const string WorkingPath
        = @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";

    //public static string OutputFilePath { get; private set; }

    // This is a pre-defined scalar processor for numeric scalars
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
        = ScalarProcessorOfFloat64.Instance;

    public static int VSpaceDimensions { get; private set; }
        = 3;

    public static double GridFrequencyHz { get; private set; }
    //=> 50d; //60d;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static XGaFloat64Processor GeometricProcessor { get; private set; }

    // This is a pre-defined text generator for displaying multivectors
    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;

    public static Float64SignalValidator SignalValidator { get; }
        = new Float64SignalValidator()
        {
            ZeroEpsilon = 2e-3
        };

    public static int DownSampleFactor
        => 8;

    public static double SamplingRate
        => ScalarSignalProcessor.SamplingRate;

    public static int SampleCount
        => ScalarSignalProcessor.SampleCount;

    public static int SectionSampleCount { get; private set; } = 9625;

    public static int OverlapSampleCount
        => 250;

    public static int SectionIndex { get; private set; }

    public static double SignalTime
        => (SampleCount - 1) / SamplingRate;

    // This is a pre-defined scalar processor for tuples of numeric scalars
    public static ScalarProcessorOfFloat64Signal ScalarSignalProcessor { get; private set; }

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected tuple scalar processor
    public static XGaProcessor<Float64Signal> GeometricSignalProcessor { get; private set; }


    private static XGaVector<Float64Signal> ReadExcelVectorSignal(string fileName, int firstRowIndex, int rowCount, int firstColIndex, int colCount)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package =
            new ExcelPackage(new FileInfo(fileName));

        var workSheet =
            package.Workbook.Worksheets[0];

        return GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            rowCount,
            firstColIndex,
            colCount
        );
    }

    private static IReadOnlyList<double[]> ReadData(string fileName, int maxRecordCount = int.MaxValue)
    {
        var recordList = new List<double[]>(2002000);

        var i = 0;
        using var sr = File.OpenText(fileName);
        string s;
        while ((s = sr.ReadLine()) != null && recordList.Count <= maxRecordCount)
        {
            var numberArrayText =
                s.Split(",")
                    .Select(t => t.Trim())
                    .Where(t => !string.IsNullOrEmpty(t));

            var numberList =
                numberArrayText.Select(double.Parse).ToList();

            if (numberList.Count != 6)
                continue;

            if (i % DownSampleFactor == 0)
            {
                var numberArray = new[]
                {
                    numberList[0],
                    numberList[1],
                    numberList[2],
                    numberList[3],
                    numberList[4],
                    numberList[5],
                };

                recordList.Add(numberArray);
            }

            i++;
        }

        return recordList;
    }

    private static void WriteData(XGaVector<Float64Signal> vectorSignal, string fileName)
    {
        var dataArray =
            vectorSignal.ToArray2D();

        var rowCount = dataArray.GetLength(0);
        var colCount = dataArray.GetLength(1);

        // Enforce KCL on 3rd and 6th column vectors
        for (var i = 0; i < rowCount; i++)
        {
            dataArray[i, 2] = -(dataArray[i, 0] + dataArray[i, 1]);
            dataArray[i, 5] = -(dataArray[i, 3] + dataArray[i, 4]);
        }

        var contents =
            Enumerable
                .Range(0, dataArray.GetLength(0))
                .Select(i =>
                    dataArray
                        .RowToArray1D(i)
                        .Select(s => s.ToString("G"))
                        .Concatenate(", ")
                );

        File.Delete(fileName);
        File.AppendAllLines(fileName, contents);
    }


    private static XGaVector<Float64Signal> GetSignal(this IReadOnlyList<double[]> recordList, int index1, int index2)
    {
        var scalarArray = new Float64Signal[VSpaceDimensions];

        var sampleCount = index2 - index1 + 1;
        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var scalar = new double[sampleCount];

            for (var i = 0; i < sampleCount; i++)
                scalar[i] = recordList[i + index1][j];

            scalarArray[j] = scalar.CreateSignal(SamplingRate);
        }

        return GeometricSignalProcessor.Vector(scalarArray);
    }


    private static XGaVector<Float64Signal>[] GetSignalDerivatives1(this IReadOnlyList<double[]> recordList, int index1, int index2)
    {
        var tValues = 0d.GetLinearRange(
            (SampleCount - 1) / SamplingRate,
            SampleCount
        ).ToArray();

        var vData =
            recordList
                .GetSignal(index1, index2)
                .MapScalars(s =>
                    s.CreateSignal(SamplingRate).GetPeriodicPaddedSignal(20)
                );

        //Instead of interpolating each vector component, you should interpolate the
        //vector norm only to keep the direction of the signal vectors intact
        var normSignal1 = vData.Norm();

        //var freqIndexSet = Enumerable.Range(0, 33).Select(i => i * 10).ToArray();
        var freqIndexSet = normSignal1.ScalarValue.GetDominantFrequencyIndexSet(0.998d).ToArray();

        var normSignal2 = normSignal1.ScalarValue.FourierInterpolate(freqIndexSet);
        var v = normSignal2 / normSignal1 * vData;

        var fourierInterpolator =
            v.CreateFourierInterpolator(freqIndexSet);

        v = fourierInterpolator
            .GetVectors(tValues)
            .CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var freqIndexValueEnergySet =
            normSignal2
                .GetDominantFrequencyDataRecords()
                .Where(f => f.Index > 0)
                .ToArray();

        var freqSampleIndex = 0;
        foreach (var (freqIndex, freqHz, energyRatio) in freqIndexValueEnergySet)
        {
            Console.WriteLine(
                $"{freqSampleIndex:D4}: Frequency Samples ({freqIndex}, {normSignal2.Count - freqIndex}) = ±{freqHz:G} Hz hold {100 * energyRatio:G3} % of energy"
            );

            freqSampleIndex++;
        }
        Console.WriteLine();

        // Signal to noise ratio
        var vSnr = v.SignalToNoiseRatio(v - vData);

        Console.WriteLine($"Signal interpolation Signal-to-noise ratio: {vSnr:G}");
        Console.WriteLine();

        var vDt1 =
            fourierInterpolator.GetVectorsDt(tValues, 1).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt2 =
            fourierInterpolator.GetVectorsDt(tValues, 2).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt3 =
            fourierInterpolator.GetVectorsDt(tValues, 3).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt4 =
            fourierInterpolator.GetVectorsDt(tValues, 4).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt5 =
            fourierInterpolator.GetVectorsDt(tValues, 5).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var vDt6 =
            fourierInterpolator.GetVectorsDt(tValues, 6).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        return new[]
        {
            v, vDt1, vDt2, vDt3, vDt4, vDt5, vDt6
        };
    }

    private static XGaVector<Float64Signal>[] GetSignalDerivatives2(this IReadOnlyList<double[]> recordList, int index1, int index2)
    {
        const int wienerFilterOrder = 7;

        var nevilleInterpolator = SamplingRate.CreateXGaNevillePolynomialInterpolator();
        nevilleInterpolator.InterpolationSamples = 13;

        var v = recordList.GetSignal(index1, index2).NormWienerFilter(wienerFilterOrder);
        var vDt1 = nevilleInterpolator.GetVectorsDt1(v, SampleCount).NormWienerFilter(wienerFilterOrder);
        var vDt2 = nevilleInterpolator.GetVectorsDt1(vDt1, SampleCount).NormWienerFilter(wienerFilterOrder);
        var vDt3 = nevilleInterpolator.GetVectorsDt1(vDt2, SampleCount).NormWienerFilter(wienerFilterOrder);
        var vDt4 = nevilleInterpolator.GetVectorsDt1(vDt3, SampleCount).NormWienerFilter(wienerFilterOrder);
        var vDt5 = nevilleInterpolator.GetVectorsDt1(vDt4, SampleCount).NormWienerFilter(wienerFilterOrder);
        var vDt6 = nevilleInterpolator.GetVectorsDt1(vDt5, SampleCount).NormWienerFilter(wienerFilterOrder);

        return new[]
        {
            v, vDt1, vDt2, vDt3, vDt4, vDt5, vDt6
        };
    }

    private static XGaVector<Float64Signal>[] GetSignalDerivatives3(this IReadOnlyList<double[]> recordList, int index1, int index2)
    {
        var vData = recordList.GetSignal(index1, index2);
        var vDataNorm1 = vData.Norm();

        var normInterpolator =
            vDataNorm1.ScalarValue
                .CreateScalarPolynomialInterpolator();

        normInterpolator.InterpolationSamples = 128;
        normInterpolator.PolynomialOrder = 7;

        var vDataNorm2 =
            normInterpolator.GetValues().CreateSignal(SamplingRate);

        vData *= vDataNorm2 / vDataNorm1;

        var vectorInterpolator = vData.CreatePolynomialInterpolator(SamplingRate);

        vectorInterpolator.InterpolationSamples = 128;
        vectorInterpolator.PolynomialOrder = 7;

        var v = vectorInterpolator.GetVectors();
        var vDt1 = vectorInterpolator.GetVectorsDt(1);
        var vDt2 = vectorInterpolator.GetVectorsDt(2);
        var vDt3 = vectorInterpolator.GetVectorsDt(3);
        var vDt4 = vectorInterpolator.GetVectorsDt(4);
        var vDt5 = vectorInterpolator.GetVectorsDt(5);
        var vDt6 = vectorInterpolator.GetVectorsDt(6);

        return new[]
        {
            v, vDt1, vDt2, vDt3, vDt4, vDt5, vDt6
        };
    }

    private static XGaVector<Float64Signal>[] GetSignalDerivatives4(this XGaVector<Float64Signal> vectorSignal, int index1, int index2)
    {
        const double energyThreshold = 0.975d;

        var sampleCount = index2 - index1 + 1;
        var tMin = index1 / SamplingRate;
        var tMax = index2 / SamplingRate;

        var tValues =
            tMin.GetLinearRange(tMax, sampleCount).CreateSignal(SamplingRate);

        var vectorSignal1 =
            vectorSignal.GetSubSignal(index1, sampleCount);

        var normSignal1 =
            vectorSignal1.Norm();

        var vectorInterpolator =
            vectorSignal1.CreatePolynomialInterpolator(SamplingRate);

        vectorInterpolator.InterpolationSamples = 256;
        vectorInterpolator.PolynomialOrder = 6;

        var vectorSignal2 =
            vectorInterpolator.GetVectors();

        var normSignal2 =
            vectorSignal2.Norm();

        normSignal1.PlotSignal(
            normSignal2,
            tMin,
            tMax,
            "Signal Norm".CombineFolderPath()
        );

        vectorSignal1.PlotSignal(
            vectorSignal2,
            tMin,
            tMax,
            "Signal".CombineFolderPath()
        );

        // Signal to noise ratio
        var vSnr = vectorSignal1.SignalToNoiseRatio(vectorSignal1 - vectorSignal2);

        Console.WriteLine($"Polynomial interpolation Signal-to-noise ratio: {vSnr:G}");
        Console.WriteLine();

        var signalArray = new XGaVector<Float64Signal>[7];
        signalArray[0] = vectorSignal2;

        for (var degree = 1; degree <= 6; degree++)
        {
            signalArray[degree] = vectorInterpolator.GetVectorsDt(degree);
        }

        return signalArray;
    }


    public static void GenerateDownSampledSignal(int factor)
    {
        Console.Write("Reading data .. ");

        var inputFileName =
            @"Corrientes_malaga_smoothed.csv".CombineFolderPath();

        var dataList = ReadData(inputFileName);

        Console.WriteLine($"done reading {dataList.Count} records.");
        Console.WriteLine();

        var sampleCount = dataList.Count / factor;
        var samplingRate = SamplingRate / factor;


        Console.Write($"Processing data .. ");

        var dataList2 = new List<double[]>();
        for (var i = 0; i < dataList.Count; i += factor)
            dataList2.Add(dataList[i]);

        Console.WriteLine($"done processing {dataList2.Count} records.");
        Console.WriteLine();


        Console.Write($"Writing data .. ");

        var outputFileName =
            @$"Corrientes_malaga_{samplingRate}Hz.xlsx".CombineFolderPath();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();

        var vectorColumnNames = new[] { "ia", "ib", "ic", "id", "ie", "if" };

        var workSheet = package.Workbook.Worksheets.Add("Sheet1");

        var rowIndex = 2;
        var columnIndex = 1;

        var tValues =
            0d.GetLinearRange((sampleCount - 1) / samplingRate, sampleCount).ToArray();

        workSheet.WriteIndices(rowIndex, columnIndex, 0, sampleCount, "n");
        columnIndex += 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tValues, "t");
        columnIndex += 1;

        workSheet.WriteRowVectors(rowIndex, columnIndex, dataList2, vectorColumnNames);
        columnIndex += VSpaceDimensions;

        package.SaveAs(outputFileName);

        Console.WriteLine($"done writing {dataList2.Count} records.");
        Console.WriteLine();
    }

    public static void GenerateSmoothedData()
    {
        var inputFileName = @"Corrientes_malaga_raw.csv".CombineFolderPath();
        var outputFileName = @"Corrientes_malaga_smoothed.csv".CombineFolderPath();

        Console.Write("Reading data .. ");

        var dataList = ReadData(inputFileName);

        ScalarSignalProcessor =
            Float64SignalComposerUtils.CreateFloat64ScalarSignalProcessor(
                50000d / DownSampleFactor,
                dataList.Count
            );

        GeometricSignalProcessor =
            XGaProcessor<Float64Signal>.CreateEuclidean(ScalarSignalProcessor);

        var vectorSignal =
            dataList.GetSignal(0, dataList.Count - 1);

        Console.WriteLine($"done reading {dataList.Count} records.");
        Console.WriteLine();

        Console.Write($"Processing data .. ");

        var smoothedVectorSignal =
            vectorSignal.GetSmoothedSignal(256, 6);

        Console.WriteLine($"done processing {dataList.Count} records.");
        Console.WriteLine();

        if (dataList.Count <= 15001)
        {
            var smoothedNormVectorSignal =
                vectorSignal.GetSmoothedNormSignal(256, 6);

            var tMax = (dataList.Count - 1) / SamplingRate;

            vectorSignal.Norm().PlotSignal(
                smoothedVectorSignal.Norm(),
                0,
                tMax,
                @"Signal Norm1".CombineFolderPath()
            );


            vectorSignal.Norm().PlotSignal(
                smoothedVectorSignal.Norm(),
                0,
                tMax,
                @"Signal Norm2".CombineFolderPath()
            );


            vectorSignal.PlotSignal(
                smoothedVectorSignal,
                0,
                tMax,
                @"Signal".CombineFolderPath()
            );
        }

        Console.Write($"Writing data .. ");

        WriteData(smoothedVectorSignal, outputFileName);

        Console.WriteLine($"done writing {dataList.Count} records.");
        Console.WriteLine();
    }


    public static void PlotData()
    {
        var fileName1 = @"Corrientes_malaga_6250Hz.xlsx".CombineFolderPath();
        //var fileName1 = @"Corrientes_malaga_25000Hz.xlsx".CombineFolderPath();
        //var fileName1 = @"Corrientes_malaga_12500Hz.xlsx".CombineFolderPath();

        var index1 = 0 * SampleCount;
        var index2 = index1 + SampleCount - 1;

        ScalarSignalProcessor =
            Float64SignalComposerUtils.CreateFloat64ScalarSignalProcessor(
                50000d / DownSampleFactor,
                4096 * 4
            );

        GeometricSignalProcessor =
            XGaProcessor<Float64Signal>.CreateEuclidean(ScalarSignalProcessor);

        var vectorSignal1 = ReadExcelVectorSignal(
            fileName1,
            3 + index1,
            SampleCount,
            3,
            VSpaceDimensions
        );

        var tMin = index1 / SamplingRate;
        var tMax = (index2 - 1) / SamplingRate;

        var plotFileName = "Signal".CombineFolderPath();

        vectorSignal1.PlotSignal(vectorSignal1, tMin, tMax, plotFileName);
    }


    private static string CombineFolderPath(this string fileName)
    {
        return Path.Combine(WorkingPath, fileName);
    }

    private static string CombineFolderPath(this string fileName, int sectionIndex)
    {
        return Path.Combine(WorkingPath, $"Section{SectionIndex:000}", fileName);
    }

    private static XGaVector<Float64Signal>[] GetSignalDerivatives5(this XGaVector<Float64Signal> vectorSignal, int index1, int index2)
    {
        const double energyThreshold = 0.9998d;
        const double snrThreshold = 200d;

        var sampleCount = index2 - index1 + 1;
        var tMin = index1 / SamplingRate;
        var tMax = index2 / SamplingRate;

        var tValues =
            tMin.GetLinearRange(tMax, sampleCount).CreateSignal(SamplingRate);

        var vectorSignal1 =
            vectorSignal.GetSubSignal(index1, sampleCount); ;

        var normSignal1 =
            vectorSignal1.Norm();

        var normSignal1PaddedLinear =
            normSignal1.ScalarValue
                .GetLinearPaddedSignal();

        var normSignal1Padded =
            normSignal1.ScalarValue
                .GetPeriodicPaddedSignal(sampleCount / 200);

        normSignal1PaddedLinear.PlotSignal(
            normSignal1Padded,
            tMin,
            (normSignal1Padded.Count - 1) / SamplingRate,
            "Padded Signal Norm".CombineFolderPath()
        );

        //var freqIndexSet = Enumerable.Range(0, 33).Select(i => i * 10).ToArray();
        var freqIndexSet =
            normSignal1Padded
                .GetDominantFrequencyIndexSet(energyThreshold)
                .ToHashSet();

        for (var i = 0; i < VSpaceDimensions; i++)
        {
            var freqIndexList =
                vectorSignal1
                    .Scalar(i)
                    .ScalarValue
                    .GetPeriodicPaddedSignal(sampleCount / 200)
                    .GetDominantFrequencyIndexSet(energyThreshold);

            foreach (var freqIndex in freqIndexList)
                freqIndexSet.Add(freqIndex);
        }

        //var normSignal2 = 
        //    normSignal1Padded
        //        .CreateFourierInterpolator(freqIndexSet)
        //        .GetScalars(tValues)
        //        .CreateScalar(GeometricSignalProcessor);

        //var vectorSignal2 = (normSignal2 / normSignal1) * vectorSignal1;

        var scalarSignalArray = new Float64Signal[VSpaceDimensions];
        var scalarInterpolatorArray = new ScalarFourierSeries[VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions; i++)
        {
            var scalarSignal1 =
                vectorSignal1.Scalar(i).ScalarValue;

            if (i is 2 or 5)
            {
                //scalarInterpolatorArray[i] = 
                //    -(scalarInterpolatorArray[i - 2] + scalarInterpolatorArray[i - 1]);

                var vSum123 =
                    vectorSignal1.Scalar(i - 2) + vectorSignal1.Scalar(i - 1) + vectorSignal1.Scalar(i);

                Debug.Assert(vSum123.IsNearZero());


                scalarSignalArray[i] =
                    -(scalarSignalArray[i - 2] + scalarSignalArray[i - 1]);
            }
            else
            {
                var scalarSignal1Padded =
                    scalarSignal1.GetPeriodicPaddedSignal(
                        sampleCount / 200
                    );

                var sampleCountPadded = scalarSignal1Padded.Count;
                var tMaxPadded = (index1 + sampleCountPadded - 1) / SamplingRate;

                var tValuesPadded =
                    tMin.GetLinearRange(tMaxPadded, sampleCountPadded).CreateSignal(SamplingRate);

                scalarInterpolatorArray[i] =
                    //scalarSignal1Padded.CreateFourierInterpolator(snrThreshold, energyThreshold);
                    scalarSignal1Padded.CreateFourierInterpolator(snrThreshold, freqIndexSet);

                Console.WriteLine(
                    scalarInterpolatorArray[i].GetTextDescription($"v[{i}]", scalarSignal1Padded.EnergyAc(), tValuesPadded)
                );


                var scalarInterpolator =
                    scalarInterpolatorArray[i];

                scalarSignalArray[i] =
                    scalarInterpolator.GetValues(tValues).CreateSignal(SamplingRate);
            }

            var scalarSignal2 = scalarSignalArray[i];

            var signalToNoiseRatio =
                scalarSignal1.PeakSignalToNoiseRatioDb(scalarSignal2);

            Console.WriteLine($"Scalar signal component {i} SNR: {signalToNoiseRatio:G}");

            scalarSignal1.PlotSignal(
                scalarSignal2,
                tMin,
                tMax,
                $"Scalar signal component {i}".CombineFolderPath()
            );
        }

        var vectorSignal2 =
            scalarSignalArray.CreateXGaVector(GeometricSignalProcessor);

        var normSignal2 =
            vectorSignal2.Norm();

        var vSnr = vectorSignal1.SignalToNoiseRatio(vectorSignal1 - vectorSignal2);

        Console.WriteLine($"Vector signal SNR: {vSnr:G}");
        Console.WriteLine();


        normSignal1.PlotSignal(
            normSignal2,
            tMin,
            tMax,
            "Signal Norm".CombineFolderPath()
        );

        vectorSignal1.PlotSignal(
            vectorSignal2,
            tMin,
            tMax,
            "Signal".CombineFolderPath()
        );

        var signalArray = new XGaVector<Float64Signal>[7];
        signalArray[0] = vectorSignal2;

        for (var degree = 1; degree <= 6; degree++)
        {
            scalarSignalArray = new Float64Signal[VSpaceDimensions];

            for (var i = 0; i < VSpaceDimensions; i++)
            {
                if (i is 2 or 5)
                {
                    scalarSignalArray[i] =
                        -(scalarSignalArray[i - 2] + scalarSignalArray[i - 1]);
                }
                else
                {
                    scalarSignalArray[i] =
                        scalarInterpolatorArray[i]
                            .GetScalarsDt(tValues, degree)
                            .CreateSignal(SamplingRate);
                }
            }

            signalArray[degree] = scalarSignalArray.CreateXGaVector(GeometricSignalProcessor);
        }

        return signalArray;
    }

    private static XGaVector<Float64Signal>[] GetSignalDerivatives6(this XGaVector<Float64Signal> vectorSignal, int index1, int index2)
    {
        const double energyThreshold = 0.998d;
        const double snrThreshold = 100d;
        const int freqCountThreshold = 15000;

        var sampleCount = index2 - index1 + 1;
        var tMin = index1 / SamplingRate;
        var tMax = index2 / SamplingRate;

        var tValues =
            tMin.GetLinearRange(tMax, sampleCount).CreateSignal(SamplingRate);

        var vectorSignal1 =
            vectorSignal.GetSubSignal(index1, sampleCount);

        Console.Write($"   Computing frequency spectrum .. ");

        var normSignal1 =
            vectorSignal1.Norm();

        var normSignal1PaddedLinear =
            normSignal1.ScalarValue
                .GetLinearPaddedSignal();

        var normSignal1Padded =
            normSignal1.ScalarValue
                .GetPeriodicPaddedSignal(50);

        normSignal1PaddedLinear.PlotSignal(
            normSignal1Padded,
            tMin,
            (normSignal1Padded.Count - 1) / SamplingRate,
            "Padded Signal Norm".CombineFolderPath()
        );

        var freqIndexSet =
            normSignal1Padded
                .GetDominantFrequencyIndexSet(energyThreshold, freqCountThreshold)
                .ToHashSet();

        //for (var i = 0; i < VSpaceDimensions; i++)
        //{
        //    var freqIndexList =
        //        vectorSignal1[i]
        //            .ScalarValue
        //            .GetPolynomialPaddedSignal(50, 6)
        //            .GetDominantFrequencyIndexSet(energyThreshold, freqCountThreshold);

        //    foreach (var freqIndex in freqIndexList)
        //        freqIndexSet.Add(freqIndex);
        //}

        Console.WriteLine($" Found {freqIndexSet.Count} frequencies.");

        //var normSignal2 =
        //    normSignal1Padded
        //        .CreateFourierInterpolator(freqIndexSet)
        //        .GetScalars(tValues)
        //        .CreateSignal(SamplingRate)
        //        .CreateScalar(GeometricSignalProcessor);

        //vectorSignal1 = (normSignal2 / normSignal1) * vectorSignal1;

        var scalarSignalArray = new Float64Signal[VSpaceDimensions];
        var scalarInterpolatorArray = new ScalarFourierSeries[VSpaceDimensions];

        for (var i = 0; i < VSpaceDimensions; i++)
        {
            Console.Write($"   Interpolating signal component {i} .. ");

            var scalarSignal1 =
                vectorSignal1.Scalar(i).ScalarValue;


            var scalarSignal1Padded =
                scalarSignal1.GetPeriodicPaddedSignal(
                    50
                );

            var sampleCountPadded = scalarSignal1Padded.Count;
            var tMaxPadded = (index1 + sampleCountPadded - 1) / SamplingRate;

            var tValuesPadded =
                tMin.GetLinearRange(tMaxPadded, sampleCountPadded).CreateSignal(SamplingRate);

            scalarInterpolatorArray[i] =
                //scalarSignal1Padded.CreateFourierInterpolator(snrThreshold, energyThreshold);
                scalarSignal1Padded.CreateFourierInterpolator(snrThreshold, freqIndexSet);
            //scalarSignal1Padded.CreateFourierInterpolator(freqIndexSet);

            //Console.WriteLine(
            //    scalarInterpolatorArray[i].GetTextDescription($"v[{i}]", scalarSignal1Padded.EnergyAc(), tValuesPadded)
            //);


            var scalarInterpolator =
                scalarInterpolatorArray[i];

            scalarSignalArray[i] =
                scalarInterpolator.GetValues(tValues).CreateSignal(SamplingRate);

            var scalarSignal2 = scalarSignalArray[i];

            var signalToNoiseRatio =
                scalarSignal1.PeakSignalToNoiseRatioDb(scalarSignal2);

            Console.WriteLine($"SNR = {signalToNoiseRatio:G}");

            scalarSignal1.PlotSignal(
                scalarSignal2,
                tMin,
                tMax,
                $"Scalar signal component {i}".CombineFolderPath()
            );
        }

        var vectorSignal2 =
            scalarSignalArray.CreateXGaVector(GeometricSignalProcessor);

        var normSignal2 =
            vectorSignal2.Norm();

        normSignal1.PlotSignal(
            normSignal2,
            tMin,
            tMax,
            "Signal Norm".CombineFolderPath()
        );

        vectorSignal1.PlotSignal(
            vectorSignal2,
            tMin,
            tMax,
            "Signal".CombineFolderPath()
        );

        var signalArray = new XGaVector<Float64Signal>[7];
        signalArray[0] = vectorSignal2;

        for (var degree = 1; degree <= 6; degree++)
        {
            Console.WriteLine($"   Computing signal derivative {degree}");

            scalarSignalArray = new Float64Signal[VSpaceDimensions];

            for (var i = 0; i < VSpaceDimensions; i++)
            {
                scalarSignalArray[i] =
                    scalarInterpolatorArray[i]
                        .GetScalarsDt(tValues, degree)
                        .CreateSignal(SamplingRate);
            }

            signalArray[degree] = scalarSignalArray.CreateXGaVector(GeometricSignalProcessor);
        }

        Console.WriteLine();

        return signalArray;
    }


    private static XGaVector<Float64Signal>[] GetSignalDerivatives(this XGaVector<Float64Signal> vectorSignal, int index1, int index2)
    {
        var interpolatorOptions = new DfFourierSignalInterpolatorOptions()
        {
            EnergyAcThreshold = 1000d,
            EnergyAcPercentThreshold = 0.9998d,
            SignalToNoiseRatioThreshold = 3000d,
            FrequencyThreshold = 200 * 2 * Math.PI,
            FrequencyCountThreshold = 3500,
            PaddingTrendSampleCount = 50,
            PaddingPolynomialDegree = 6
        };

        var sampleCount = index2 - index1 + 1;
        var tMin = index1 / SamplingRate;
        var tMax = index2 / SamplingRate;

        var tValues =
            tMin.GetLinearRange(tMax, sampleCount).CreateSignal(SamplingRate);

        var vectorSignal1 =
            vectorSignal.GetSubSignal(index1, sampleCount);

        Console.Write($"   Computing frequency spectrum .. ");

        var vectorSignal1Padded =
            vectorSignal1.GetPeriodicPaddedSignal(50);

        var vectorSpectrum =
            vectorSignal1Padded.GetFourierSpectrum(interpolatorOptions);

        Console.WriteLine();
        Console.WriteLine(
            vectorSpectrum.GetTextDescription(vectorSignal1)
        );
        Console.WriteLine();

        var samplingSpecsPadded = vectorSignal1Padded.GetSamplingSpecs();

        var frequencyIndexList =
            vectorSpectrum
                .SelectMany(s => s.Samples)
                .Select(s => s.Index)
                .Distinct()
                .OrderBy(s => s)
                .ToImmutableArray();

        var frequencyList =
            frequencyIndexList
                .Select(i => samplingSpecsPadded.GetFrequencyHz(i))
                .ToImmutableArray();

        Console.WriteLine($" Found {frequencyList.Length} frequencies.");

        var vectorSignal2 =
            vectorSpectrum.GetRealSignal(tValues).CreateXGaVector(GeometricSignalProcessor);

        var vectorSignalError = vectorSignal1 - vectorSignal2;
        var vectorSignalSnr = vectorSignal1.SignalToNoiseRatio(vectorSignalError);

        Console.WriteLine($" Signal to noise ratio {vectorSignalSnr:G}.");

        var normSignal1 =
            vectorSignal1.Norm();

        var normSignal2 =
            vectorSignal2.Norm();

        normSignal1.PlotSignal(
            normSignal2,
            tMin,
            tMax,
            "Signal Norm".CombineFolderPath()
        );

        vectorSignal1.PlotSignal(
            vectorSignal2,
            tMin,
            tMax,
            "Signal".CombineFolderPath()
        );

        var signalArray = new XGaVector<Float64Signal>[7];
        signalArray[0] = vectorSignal2;

        for (var degree = 1; degree <= 6; degree++)
        {
            Console.WriteLine($"   Computing signal derivative {degree}");

            signalArray[degree] =
                vectorSpectrum.GetRealSignalDt(degree, tValues).CreateXGaVector(GeometricSignalProcessor);
        }

        Console.WriteLine();

        return signalArray;
    }

    private static XGaVector<Float64Signal>[] GetSignalDerivatives(this XGaVector<Float64Signal> vectorSignal)
    {
        return vectorSignal.GetSignalDerivatives(0, SampleCount - 1);
    }


    private static XGaVector<Float64Signal> InitializeSignal(int vSpaceDimensions, int sampleCount, double samplingRate, string filePath, int firstRow, int firstCol)
    {
        VSpaceDimensions = vSpaceDimensions;

        GeometricProcessor = XGaFloat64Processor.Euclidean;
        ScalarSignalProcessor = new ScalarProcessorOfFloat64Signal(samplingRate, sampleCount);
        GeometricSignalProcessor = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package =
            new ExcelPackage(new FileInfo(filePath));

        var workSheet =
            package.Workbook.Worksheets[0];

        return GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRow,
            sampleCount,
            firstCol,
            vSpaceDimensions
        );
    }

    private static XGaVector<Float64Signal> InitializeAulario3PhaseVoltageSignal()
    {
        //OutputFilePath = "Aulario3_PhaseVoltage.xlsx".CombineFolderPath();

        GridFrequencyHz = 50d;

        return InitializeSignal(
            3,
            4801,
            24000,
            "Aulario3_derivatives.xlsx".CombineFolderPath(),
            3,
            2
        );
    }

    private static XGaVector<Float64Signal> InitializeAulario3LineVoltageSignal()
    {
        //OutputFilePath = "Aulario3_LineVoltage.xlsx".CombineFolderPath();
            
        GridFrequencyHz = 50d;

        return InitializeSignal(
            3,
            4801,
            24000,
            "Aulario3_derivatives.xlsx".CombineFolderPath(),
            3,
            5
        );
    }

    private static XGaVector<Float64Signal> InitializeAulario3PhaseNeutralVoltageSignal()
    {
        //OutputFilePath = "Aulario3_PhaseNeutralVoltage.xlsx".CombineFolderPath();
            
        GridFrequencyHz = 50d;

        return InitializeSignal(
            4,
            4801,
            24000,
            "Aulario3_derivatives.xlsx".CombineFolderPath(),
            3,
            8
        );
    }

    private static XGaVector<Float64Signal> InitializeAulario3LineCurrentSignal()
    {
        //OutputFilePath = "Aulario3_LineCurrent.xlsx".CombineFolderPath();
            
        GridFrequencyHz = 50d;

        return InitializeSignal(
            4,
            4801,
            24000,
            "Aulario3_derivatives.xlsx".CombineFolderPath(),
            3,
            12
        );
    }

    private static XGaVector<Float64Signal> InitializeEMTPPhaseVoltageSignal()
    {
        //OutputFilePath = "EMTP_PhaseVoltage.xlsx".CombineFolderPath();
            
        GridFrequencyHz = 60d;

        return InitializeSignal(
            3,
            15001,
            50000,
            "EMTP_transient_ahmad.xlsx".CombineFolderPath(),
            5,
            8
        );
    }

    private static XGaVector<Float64Signal> InitializeEMTPLineVoltageSignal()
    {
        //OutputFilePath = "EMTP_LineVoltage.xlsx".CombineFolderPath();
            
        GridFrequencyHz = 60d;

        return InitializeSignal(
            3,
            15001,
            50000,
            "EMTP_transient_ahmad.xlsx".CombineFolderPath(),
            5,
            11
        );
    }

    private static XGaVector<Float64Signal> InitializeEMTPPhaseNeutralVoltageSignal()
    {
        //OutputFilePath = "EMTP_PhaseNeutralVoltage.xlsx".CombineFolderPath();
            
        GridFrequencyHz = 60d;

        return InitializeSignal(
            4,
            15001,
            50000,
            "EMTP_transient_ahmad.xlsx".CombineFolderPath(),
            5,
            14
        );
    }

    private static XGaVector<Float64Signal> InitializeCorrientesMalagaLineCurrentSignal(int firstIndex, int sampleCount)
    {
        //OutputFilePath = "CorrientesMalaga_LineCurrent.xlsx".CombineFolderPath();

        return InitializeSignal(
            6,
            sampleCount,
            6250,
            "Corrientes_malaga_6250Hz.xlsx".CombineFolderPath(),
            3 + firstIndex,
            3
        );
    }

    private static XGaVector<Float64Signal> InitializeSyntheticSignal1()
    {
        //OutputFilePath = "Synthetic1.xlsx".CombineFolderPath();

        var sqrt2 = Math.Sqrt(2d);

        var signalComposer = Float64HarmonicSignalComposer.Create(
            50d,
            1000,
            10
        );

        var samplingSpecs = signalComposer.GetSamplingSpecs();

        var sampleCount = samplingSpecs.SampleCount;
        var samplingRate = samplingSpecs.SamplingRate;

        VSpaceDimensions = 3;
        GeometricProcessor = XGaFloat64Processor.Euclidean;
        ScalarSignalProcessor = new ScalarProcessorOfFloat64Signal(samplingRate, sampleCount);
        GeometricSignalProcessor = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

        var v1 =
            signalComposer
                .GenerateOddSignalComponents(200 * sqrt2, 1, 3)
                .CreateXGaVector(GeometricSignalProcessor);

        var v2 =
            signalComposer
                .GenerateOddSignalComponents(20 * sqrt2, 2, 3)
                .CreateXGaVector(GeometricSignalProcessor);

        var v3 =
            signalComposer
                .GenerateOddSignalComponents(-30 * sqrt2, 7, 3)
                .CreateXGaVector(GeometricSignalProcessor);

        return v1 + v2 + v3;
    }

    private static XGaVector<Float64Signal> InitializeSyntheticSignal2()
    {
        //OutputFilePath = "Synthetic2.xlsx".CombineFolderPath();

        var sqrt2 = Math.Sqrt(2d);

        var signalComposer = Float64HarmonicSignalComposer.Create(
            50d,
            1000,
            10
        );

        var samplingSpecs = signalComposer.GetSamplingSpecs();

        var sampleCount = samplingSpecs.SampleCount;
        var samplingRate = samplingSpecs.SamplingRate;

        VSpaceDimensions = 6;
        GeometricProcessor = XGaFloat64Processor.Euclidean;
        ScalarSignalProcessor = new ScalarProcessorOfFloat64Signal(samplingRate, sampleCount);
        GeometricSignalProcessor = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

        var vMag = 1;
        var phaseCount = 6;

        var v1Scalars =
            signalComposer.GenerateOddSignalComponents(vMag, 1, phaseCount);

        var v2Scalars =
            signalComposer.GenerateOddSignalComponents(0.1 * vMag, 7, phaseCount);

        v1Scalars[VSpaceDimensions - 1] /= 2d;
        v2Scalars[VSpaceDimensions - 1] /= 2d;

        var v1 =
            v1Scalars.CreateXGaVector(GeometricSignalProcessor);

        var v2 =
            v2Scalars.CreateXGaVector(GeometricSignalProcessor);

        return v1 + v2;
    }
        
    private static XGaVector<Float64Signal> InitializeSyntheticSignal4()
    {
        //OutputFilePath = "Synthetic3.xlsx".CombineFolderPath();

        var freqHz = 50d;
        var freq = 2d * Math.PI * freqHz;
        var period = 1d / freqHz;
        var phi = 2d * Math.PI / 3d;
        var samplingRate = 50000;
        var sampleCountPerCycle = 1000;
        var cycleCount = 10;
        var sqrt2 = Math.Sqrt(2d);
        var sampleCount = sampleCountPerCycle * cycleCount;

        var v1 = Float64Signal.CreatePeriodic(
            samplingRate,
            sampleCountPerCycle,
            period,
            t =>
                210 * sqrt2 * (freq * t).Sin() +
                22 * sqrt2 * (2 * (freq * t)).Sin() +
                -29 * sqrt2 * (7 * (freq * t)).Sin(),
            false
        ).Repeat(cycleCount);

        var v2 = Float64Signal.CreatePeriodic(
            samplingRate,
            sampleCountPerCycle,
            period,
            t =>
                200 * sqrt2 * (freq * t - phi).Sin() +
                20 * sqrt2 * (2 * (freq * t - phi)).Sin() +
                -30 * sqrt2 * (7 * (freq * t - phi)).Sin(),
            false
        ).Repeat(cycleCount);

        var v3 = Float64Signal.CreatePeriodic(
            samplingRate,
            sampleCountPerCycle,
            period,
            t =>
                192 * sqrt2 * (freq * t + phi).Sin() +
                15 * sqrt2 * (2 * (freq * t + phi)).Sin() +
                -35 * sqrt2 * (7 * (freq * t + phi)).Sin(),
            false
        ).Repeat(cycleCount);

        VSpaceDimensions = 3;
        GeometricProcessor = XGaFloat64Processor.Euclidean;
        ScalarSignalProcessor = new ScalarProcessorOfFloat64Signal(samplingRate, sampleCount);
        GeometricSignalProcessor = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

        return GeometricSignalProcessor.Vector(v1, v2, v3);
    }
        
    private static void ProcessSignalSection3D1(XGaVector<Float64Signal> vectorSignal, ExcelWorksheet workSheet, int sectionFirstIndex)
    {
        var sectionLastIndex = SampleCount - 1;

        var tMin = sectionFirstIndex / SamplingRate;
        var tMax = sectionLastIndex / SamplingRate;

        var tValues =
            tMin
                .GetLinearRange(tMax, SampleCount)
                .CreateSignal(SamplingRate);

        Console.WriteLine($"   Processing data");

        //var smoothingFactors = new[] { 3, 5, 7, 9 };

        var options = new DfChebyshevSignalInterpolatorOptions()
        {
            SectionCount = 10,
            PolynomialDegree = 39,
            SmoothingFactors = Array.Empty<int>()
        };

        var xFunction = vectorSignal.Scalar(0).ScalarValue.CreateChebyshevInterpolator(options);
        var yFunction = vectorSignal.Scalar(1).ScalarValue.CreateChebyshevInterpolator(options);
        var zFunction = vectorSignal.Scalar(2).ScalarValue.CreateChebyshevInterpolator(options);

        var curve = DifferentialCurve3D.Create(
            xFunction,
            yFunction,
            zFunction
        );

        var tSignal = vectorSignal.Scalar(0).ScalarValue.GetSampledTimeSignal();

        var xSignal = tSignal.SampleFunction(xFunction);
        var ySignal = tSignal.SampleFunction(yFunction);
        var zSignal = tSignal.SampleFunction(zFunction);

        var xPsnr = vectorSignal.Scalar(0).ScalarValue.PeakSignalToNoiseRatioDb(xSignal);
        var yPsnr = vectorSignal.Scalar(1).ScalarValue.PeakSignalToNoiseRatioDb(ySignal);
        var zPsnr = vectorSignal.Scalar(2).ScalarValue.PeakSignalToNoiseRatioDb(zSignal);

        Console.WriteLine($"x-Signal PSNR: {xPsnr} Db");
        Console.WriteLine($"y-Signal PSNR: {yPsnr} Db");
        Console.WriteLine($"z-Signal PSNR: {zPsnr} Db");
        Console.WriteLine();

        var v = GeometricSignalProcessor.Vector(
            xSignal,
            ySignal,
            zSignal
        );

        var vDt1 = GeometricSignalProcessor.Vector(
            curve
                .ComponentsDerivative1
                .MapItems(f => tSignal.SampleFunction(f))
                .GetItems()
        );

        var vDt2 = GeometricSignalProcessor.Vector(
            curve
                .ComponentsDerivative2
                .MapItems(f => tSignal.SampleFunction(f))
                .GetItems()
        );

        var vDt3 = GeometricSignalProcessor.Vector(
            curve
                .ComponentsDerivative3
                .MapItems(f => tSignal.SampleFunction(f))
                .GetItems()
        );

        var signalToNoiseRatio =
            vectorSignal.SignalToNoiseRatio(vectorSignal - v);

        Console.WriteLine($"   Vector signal SNR: {signalToNoiseRatio:G}");
        Console.WriteLine();

        //var vSum123 = 
        //    (v[0] + v[1] + v[2]).ScalarValue.Rms().Square() / 
        //    (v[0].ScalarValue.Rms().Square() + v[1].ScalarValue.Rms().Square() + v[2].ScalarValue.Rms().Square());

        //var vSum456 = 
        //    (v[3] + v[4] + v[5]).ScalarValue.Rms().Square() /
        //    (v[3].ScalarValue.Rms().Square() + v[4].ScalarValue.Rms().Square() + v[5].ScalarValue.Rms().Square());

        //Debug.Assert(vSum123.IsNearZero(0.01d));
        //Debug.Assert(vSum456.IsNearZero(0.01d));
            
        var sDt1 = tSignal.SampleFunction(curve.GetArcLengthVariableDerivative1());
        var sDt2 = tSignal.SampleFunction(curve.GetArcLengthVariableDerivative2());
        var sDt3 = tSignal.SampleFunction(curve.GetArcLengthVariableDerivative3());
            
        var vDs1 = GeometricSignalProcessor.Vector(
            curve
                .GetComponentsArcLengthDerivative1()
                .MapItems(f => tSignal.SampleFunction(f))
                .GetItems()
        );

        var vDs2 = GeometricSignalProcessor.Vector(
            curve
                .GetComponentsArcLengthDerivative2()
                .MapItems(f => tSignal.SampleFunction(f))
                .GetItems()
        );

        var vDs3 = GeometricSignalProcessor.Vector(
            curve
                .GetComponentsArcLengthDerivative3()
                .MapItems(f => tSignal.SampleFunction(f))
                .GetItems()
        );

        var uFrameList =
            tSignal.Select(curve.GetArcLengthFrame).ToImmutableArray();

        var u1s = GeometricSignalProcessor.Vector(
            uFrameList.Select(f => f.Direction1.X).CreateSignal(tSignal.SamplingRate),
            uFrameList.Select(f => f.Direction1.Y).CreateSignal(tSignal.SamplingRate),
            uFrameList.Select(f => f.Direction1.Z).CreateSignal(tSignal.SamplingRate)
        );

        var u2s = GeometricSignalProcessor.Vector(
            uFrameList.Select(f => f.Direction2.X).CreateSignal(tSignal.SamplingRate),
            uFrameList.Select(f => f.Direction2.Y).CreateSignal(tSignal.SamplingRate),
            uFrameList.Select(f => f.Direction2.Z).CreateSignal(tSignal.SamplingRate)
        );

        var u3s = GeometricSignalProcessor.Vector(
            uFrameList.Select(f => f.Direction3.X).CreateSignal(tSignal.SamplingRate),
            uFrameList.Select(f => f.Direction3.Y).CreateSignal(tSignal.SamplingRate),
            uFrameList.Select(f => f.Direction3.Z).CreateSignal(tSignal.SamplingRate)
        );

        var u1sNorm = u1s.Norm();
        var u2sNorm = u2s.Norm();
        var u3sNorm = u3s.Norm();

        // Curvatures
        var kappaPairList = 
            tSignal.Select(curve.GetCurvatures).ToImmutableArray();

        var kappa1 = kappaPairList.Select(p => p.Item1).CreateSignal(tSignal.SamplingRate);
        var kappa2 = kappaPairList.Select(p => p.Item2).CreateSignal(tSignal.SamplingRate);

        var kappa1Mean = kappa1.Mean();
        var kappa2Mean = kappa2.Mean();

        Console.WriteLine($"   Mean of kappa1: {kappa1Mean:G}");
        Console.WriteLine($"   Mean of kappa2: {kappa2Mean:G}");
        Console.WriteLine();

        var kappa1Rms = kappa1.RootMeanSquare();
        var kappa2Rms = kappa2.RootMeanSquare();

        Console.WriteLine($"   RMS of kappa1: {kappa1Rms:G}");
        Console.WriteLine($"   RMS of kappa2: {kappa2Rms:G}");
        Console.WriteLine();

        // Darboux blades and Darboux bivector
        var darbouxBladesList = 
            tSignal.Select(curve.GetDarbouxBlades).ToImmutableArray();

        var darbouxBivectorList = 
            tSignal.Select(curve.GetDarbouxBivector).ToImmutableArray();

        var omega1 = GeometricSignalProcessor.Bivector3D(
            darbouxBladesList.Select(db => db.Item1.Scalar12).CreateSignal(tSignal.SamplingRate),
            darbouxBladesList.Select(db => db.Item1.Scalar13).CreateSignal(tSignal.SamplingRate),
            darbouxBladesList.Select(db => db.Item1.Scalar23).CreateSignal(tSignal.SamplingRate)
        );
            
        var omega2 = GeometricSignalProcessor.Bivector3D(
            darbouxBladesList.Select(db => db.Item2.Scalar12).CreateSignal(tSignal.SamplingRate),
            darbouxBladesList.Select(db => db.Item2.Scalar13).CreateSignal(tSignal.SamplingRate),
            darbouxBladesList.Select(db => db.Item2.Scalar23).CreateSignal(tSignal.SamplingRate)
        );

        var omega3 = GeometricSignalProcessor.Bivector3D(
            darbouxBladesList.Select(db => db.Item3.Scalar12).CreateSignal(tSignal.SamplingRate),
            darbouxBladesList.Select(db => db.Item3.Scalar13).CreateSignal(tSignal.SamplingRate),
            darbouxBladesList.Select(db => db.Item3.Scalar23).CreateSignal(tSignal.SamplingRate)
        );

        var omegaBivector = GeometricSignalProcessor.Bivector3D(
            darbouxBivectorList.Select(db => db.Scalar12).CreateSignal(tSignal.SamplingRate),
            darbouxBivectorList.Select(db => db.Scalar13).CreateSignal(tSignal.SamplingRate),
            darbouxBivectorList.Select(db => db.Scalar23).CreateSignal(tSignal.SamplingRate)
        );

        var omegaList = new[]
        {
            omega1,
            omega2,
            omega3
        };

        var omegaAverageList = 
            omegaList.Select(b=>
                b.GetRunningAverageSignal(480)
            ).ToImmutableArray();

        var omegaIndex = 1;
        foreach (var omega in omegaList)
        {
            var omegaMean = omega.Mean();
            var omegaMeanNorm = omegaMean.Norm().ScalarValue;
            var omegaMeanNormHz = omegaMeanNorm / (2 * Math.PI);

            Console.WriteLine($"Omega {omegaIndex}");
            Console.WriteLine($"   Mean Bivector: {LaTeXComposer.GetMultivectorText(omegaMean)}");
            Console.WriteLine($"   Mean Bivector Norm: {omegaMeanNorm:G} rad/sec = {omegaMeanNormHz:G} Hz");
            Console.WriteLine();

            omegaIndex++;
        }

        //var omegaBarNorm = omegaBar.Norm();
        //var omegaBarNormScaled = sDt1 * omegaBarNorm;

        //// Darboux bivector
        //var omega = vectorSignalProcessor.DarbouxBivectors;
        //var omegaNorm = omega.Norm();

        //// Bivector B
        //var bBivector = omega - omegaBar;
        //var bBivectorNorm = bBivector.Norm();

        //var omegaScaledMeanNorm = 
        //    (omega * sDt1).Mean(GeometricProcessor).Norm().ScalarValue;

        //var omegaBarScaledMeanNorm = 
        //    (omegaBar * sDt1).Mean(GeometricProcessor).Norm().ScalarValue;

        //Console.WriteLine($"   Norm of Scaled OmegaBar Signal Mean: {omegaBarScaledMeanNorm:G}");
        //Console.WriteLine($"   Norm of Scaled Omega Signal Mean: {omegaScaledMeanNorm:G}");
        //Console.WriteLine();


        //interpolatorOptions.FrequencyCountThreshold = 10;

        //var sig = (omega.Norm() * sDt1).ScalarValue;
        //var sigEnergy = sig.EnergyAc();
        //var kappa1Spectrum = 
        //    sig
        //    .GetPolynomialPaddedSignal(50, 6)
        //    .GetFourierSpectrum(interpolatorOptions);

        //var kappa1SpectrumSamples =
        //    kappa1Spectrum
        //        .SamplePairsAc
        //        .OrderByDescending(s => s.Item1.Value.MagnitudeSquared() + s.Item2.Value.MagnitudeSquared())
        //        .Take(5)
        //        .ToArray();

        //Console.WriteLine("Kappa 1 frequency samples:");
        //foreach (var ((freqIndex1, value1), (freqIndex2, value2)) in kappa1SpectrumSamples)
        //{
        //    var freqHz = kappa1Spectrum.GetFrequencyHz(freqIndex1);
        //    var energy = (value1.MagnitudeSquared() + value2.MagnitudeSquared()) / sigEnergy;

        //    Console.WriteLine($"Frequency: ±{freqHz:G4} Hz, Energy: {energy:P4}");
        //}
        //Console.WriteLine();


        //Console.WriteLine($"   Plotting data");


        vectorSignal.PlotSignal(
            v,
            0,
            (SampleCount - 1) / SamplingRate,
            "Signal".CombineFolderPath()
        );

        v.PlotVectorSignalComponents("Signal", "v".CombineFolderPath());

        vDt1.PlotVectorSignalComponents("1st t-derivative", "vDt1".CombineFolderPath());
        vDt2.PlotVectorSignalComponents("2nd t-derivative", "vDt2".CombineFolderPath());
        vDt3.PlotVectorSignalComponents("3rd t-derivative", "vDt3".CombineFolderPath());

        vDs1.PlotVectorSignalComponents("1st s-derivative", "vDs1".CombineFolderPath());
        vDs2.PlotVectorSignalComponents("2nd s-derivative", "vDs2".CombineFolderPath());
        vDs3.PlotVectorSignalComponents("3rd s-derivative", "vDs3".CombineFolderPath());

        sDt1.PlotScalarSignal("1st t-derivative of arc-length", "sDt1".CombineFolderPath());
        sDt1.Log10().PlotScalarSignal("1st t-derivative of arc-length Log10", "sDt1Log10".CombineFolderPath());

        sDt2.PlotScalarSignal("2nd t-derivative of arc-length", "sDt2".CombineFolderPath());
        sDt2.Log10().PlotScalarSignal("2nd t-derivative of arc-length Log10", "sDt2Log10".CombineFolderPath());

        sDt3.PlotScalarSignal("3rd t-derivative of arc-length", "sDt3".CombineFolderPath());
        sDt3.Log10().PlotScalarSignal("3rd t-derivative of arc-length Log10", "sDt3Log10".CombineFolderPath());

        for (var i = 0; i < omegaAverageList.Length; i++)
        {
            var omegaNorm = omegaAverageList[i].Norm() / (2 * Math.PI);

            omegaNorm.PlotScalarSignal($"Darboux Blade Average Norm {i + 1}", $"DBANorm{i + 1}".CombineFolderPath());
            omegaNorm.Log10().PlotScalarSignal($"Darboux Blade Average Norm {i + 1} Log10", $"DBANorm{i + 1}Log10".CombineFolderPath());
        }

        //var vNorm = v.Norm();
        //kappa1 *= vNorm;
        //kappa2 *= vNorm;
        //kappa3 *= vNorm;
        //kappa4 *= vNorm;
        //kappa5 *= vNorm;

        kappa1.PlotScalarSignal("1st curvature coefficient", "kappa1".CombineFolderPath());
        kappa1.Log10().PlotScalarSignal("1st curvature coefficient Log10", "kappa1Log10".CombineFolderPath());

        kappa2.PlotScalarSignal("2nd curvature coefficient", "kappa2".CombineFolderPath());
        kappa2.Log10().PlotScalarSignal("2nd curvature coefficient Log10", "kappa2Log10".CombineFolderPath());


        Console.WriteLine($"   Writing data");

        var vectorColumnNames = new[] { "1", "2", "3" };
        var bivectorColumnNames = new[] { "12", "13", "23" };

        var rowIndex = 3 + sectionFirstIndex;
        var columnIndex = 1;

        workSheet.WriteIndices(rowIndex, columnIndex, sectionFirstIndex, SampleCount, "n");
        columnIndex += 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tValues, "t");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, v, "Signal", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt1, "1st t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt2, "2nd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt3, "3rd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt1, "s'(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt2, "s''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt3, "s'''(t)");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs1, "1st s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs2, "2nd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs3, "3rd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u1s, "u1(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u2s, "u2(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u3s, "u3(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u1sNorm, "|| u1(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u2sNorm, "|| u2(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u3sNorm, "|| u3(s) ||");
        columnIndex += 1;

        for (var i = 0; i < omegaList.Length; i++)
        {
            var omega = omegaList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1}", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa1, "kappa1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa2, "kappa2");
        columnIndex += 1;

        for (var i = 0; i < omegaAverageList.Length; i++)
        {
            var omega = omegaAverageList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1} Average", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        for (var i = 0; i < omegaAverageList.Length; i++)
        {
            var omegaNorm = omegaAverageList[i].Norm();

            workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, $"Darboux Blade {i + 1} Average Norm");
            columnIndex += 1;
        }

        //workSheet.WriteBivectorSignal(rowIndex, columnIndex, omegaBar, "Angular Velocity Blade", bivectorColumnNames);
        //columnIndex += (int)(VSpaceDimensions * (VSpaceDimensions - 1)) / 2;

        //workSheet.WriteBivectorSignal(rowIndex, columnIndex, bBivector, "B Bivector", bivectorColumnNames);
        //columnIndex += (int)(VSpaceDimensions * (VSpaceDimensions - 1)) / 2;

        //workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaBarNormScaled, "Scaled Angular Velocity Blade Norm");
        //columnIndex += 1;

        //workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaBarNorm, "Angular Velocity Blade Norm");
        //columnIndex += 1;

        //workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, "Darboux Bivector Norm");
        //columnIndex += 1;

        //workSheet.WriteScalarSignal(rowIndex, columnIndex, bBivectorNorm, "B Bivector Norm");
        //columnIndex += 1;

        Console.WriteLine($"Done");
        Console.WriteLine();
    }

    private static void ProcessSignalSection3D(XGaVector<Float64Signal> vectorSignal, DfSignalInterpolatorOptions interpolatorOptions, ExcelWorksheet workSheet, int sectionFirstIndex)
    {
        ScalarProcessor.ZeroEpsilon = 1e-7d;

        var sectionLastIndex = SampleCount - 1;

        var tMin = sectionFirstIndex / SamplingRate;
        var tMax = sectionLastIndex / SamplingRate;

        var tValues =
            tMin
                .GetLinearRange(tMax, SampleCount)
                .CreateSignal(SamplingRate);

        Console.WriteLine($"   Processing data");

        //var options = new DfFourierSignalInterpolatorOptions()
        //{
        //    EnergyAcThreshold = 100000d,
        //    EnergyAcPercentThreshold = 0.99999998d,
        //    SignalToNoiseRatioThreshold = 300000d,
        //    FrequencyThreshold = 1000 * 2 * Math.PI,
        //    FrequencyCountThreshold = 750,
        //    PaddingTrendSampleCount = 1000,
        //    PaddingPolynomialDegree = 5,
        //    PaddingSampleCount = -1,
        //    AssumePeriodic = false
        //};

        ////var options = new SpectrumInterpolationOptions()
        ////{
        ////    EnergyAcThreshold = 1000d,
        ////    EnergyAcPercentThreshold = 0.9998d,
        ////    SignalToNoiseRatioThreshold = 3000d,
        ////    FrequencyThreshold = 750 * 2 * Math.PI,
        ////    FrequencyCountThreshold = 750,
        ////    PaddingPolynomialSampleCount = 50,
        ////    PaddingPolynomialDegree = 6
        ////};

        //var options = new DfLinearSplineSignalInterpolatorOptions
        //{
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        //var options = new DfCatmullRomSplineSignalInterpolatorOptions
        //{
        //    BezierDegree = 3,
        //    SplineType = CatmullRomSplineType.Centripetal,
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        //var options = new DfChebyshevSignalInterpolatorOptions()
        //{
        //    SectionCount = 1,
        //    PolynomialDegree = 300,
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        var vectorSignalProcessor = new XGaGeometricFrequencyDifferentialProcessor(
            3,
            (vSignal =>
                VectorDifferentialInterpolator.Create(
                    vSignal,
                    tValues.SamplingSpecs,
                    interpolatorOptions
                )
            )
        );

        vectorSignalProcessor.RunningAverageSampleCount = 
            (int)(SamplingRate / GridFrequencyHz);

        vectorSignalProcessor.ProcessVectorSignal(vectorSignal);

        //var vectorSignalProcessor = new VectorSignalPolynomialProcessor(GeometricProcessor)
        //{
        //    PolynomialOrder = 13
        //};

        //vectorSignalProcessor.ProcessVectorSignal(vectorSignal);

        var v = vectorSignalProcessor.VectorSignalInterpolated;

        var vDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0];
        var vDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1];
        var vDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2];

        var signalToNoiseRatio =
            vectorSignal.SignalToNoiseRatio(vectorSignal - v);

        Console.WriteLine($"   Vector signal SNR: {signalToNoiseRatio:G}");
        Console.WriteLine();

        //var vSum123 = 
        //    (v[0] + v[1] + v[2]).ScalarValue.Rms().Square() / 
        //    (v[0].ScalarValue.Rms().Square() + v[1].ScalarValue.Rms().Square() + v[2].ScalarValue.Rms().Square());

        //var vSum456 = 
        //    (v[3] + v[4] + v[5]).ScalarValue.Rms().Square() /
        //    (v[3].ScalarValue.Rms().Square() + v[4].ScalarValue.Rms().Square() + v[5].ScalarValue.Rms().Square());

        //Debug.Assert(vSum123.IsNearZero(0.01d));
        //Debug.Assert(vSum456.IsNearZero(0.01d));

        var sDt1 = vectorSignalProcessor.ArcLengthTimeDerivatives[0];
        var sDt2 = vectorSignalProcessor.ArcLengthTimeDerivatives[1];
        var sDt3 = vectorSignalProcessor.ArcLengthTimeDerivatives[2];

        var vDs1 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[0];
        var vDs2 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[1];
        var vDs3 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[2];

        var u1s = vectorSignalProcessor.ArcLengthFramesOrthogonal[0];
        var u2s = vectorSignalProcessor.ArcLengthFramesOrthogonal[1];
        var u3s = vectorSignalProcessor.ArcLengthFramesOrthogonal[2];

        var u1sNorm = u1s.Norm();
        var u2sNorm = u2s.Norm();
        var u3sNorm = u3s.Norm();

        // Curvatures
        var kappa1 = vectorSignalProcessor.Curvatures[0];
        var kappa2 = vectorSignalProcessor.Curvatures[1];

        var kappa1Mean = kappa1.Mean();
        var kappa2Mean = kappa2.Mean();

        Console.WriteLine($"   Mean of kappa1: {kappa1Mean:G}");
        Console.WriteLine($"   Mean of kappa2: {kappa2Mean:G}");
        Console.WriteLine();

        var kappa1Rms = kappa1.RootMeanSquare();
        var kappa2Rms = kappa2.RootMeanSquare();

        Console.WriteLine($"   RMS of kappa1: {kappa1Rms:G}");
        Console.WriteLine($"   RMS of kappa2: {kappa2Rms:G}");
        Console.WriteLine();

        // Angular velocity blades
        var omegaList = vectorSignalProcessor.AngularVelocityBlades;
        var omegaMeanList = vectorSignalProcessor.AngularVelocityBladesMeans;
            
        var db = vectorSignalProcessor.DarbouxBivector;
        var dbMean = vectorSignalProcessor.DarbouxBivectorMean;

        var dbNorm = db.ENormSquared();
        var kappaNorm = kappa1.Square() + kappa2.Square();

        Debug.Assert(
            (dbNorm - kappaNorm).IsNearZero()
        );

        var omegaIndex = 1;
        foreach (var omega in omegaList)
        {
            var omegaMean = omega.Mean();
            var omegaMeanNorm = omegaMean.Norm().ScalarValue;
            var omegaMeanNormHz = omegaMeanNorm / (2 * Math.PI);

            Console.WriteLine($"Omega {omegaIndex}");
            Console.WriteLine($"   Mean Bivector: {LaTeXComposer.GetMultivectorText(omegaMean)}");
            Console.WriteLine($"   Mean Bivector Norm: {omegaMeanNorm:G} rad/sec = {omegaMeanNormHz:G} Hz");
            Console.WriteLine();

            omegaIndex++;
        }

        var dbSignalsMean = db.Mean();
        var dbSignalsMeanNorm = dbSignalsMean.Norm().ScalarValue;
        var dbSignalsMeanNormHz = dbSignalsMeanNorm / (2 * Math.PI);

        Console.WriteLine($"Darboux Bivector");
        Console.WriteLine($"   Mean Bivector: {LaTeXComposer.GetMultivectorText(dbSignalsMean)}");
        Console.WriteLine($"   Mean Bivector Norm: {dbSignalsMeanNorm:G} rad/sec = {dbSignalsMeanNormHz:G} Hz");
        Console.WriteLine();
            
        //vectorSignal.PlotSignal(
        //    v,
        //    0,
        //    (SampleCount - 1) / SamplingRate,
        //    "Signal".CombineFolderPath()
        //);

        //v.PlotVectorSignalComponents("Signal", "v".CombineFolderPath());

        //vDt1.PlotVectorSignalComponents("1st t-derivative", "vDt1".CombineFolderPath());
        //vDt2.PlotVectorSignalComponents("2nd t-derivative", "vDt2".CombineFolderPath());
        //vDt3.PlotVectorSignalComponents("3rd t-derivative", "vDt3".CombineFolderPath());

        //vDs1.PlotVectorSignalComponents("1st s-derivative", "vDs1".CombineFolderPath());
        //vDs2.PlotVectorSignalComponents("2nd s-derivative", "vDs2".CombineFolderPath());
        //vDs3.PlotVectorSignalComponents("3rd s-derivative", "vDs3".CombineFolderPath());

        //sDt1.PlotScalarSignal("1st t-derivative of arc-length", "sDt1".CombineFolderPath());
        //sDt1.Log10().PlotScalarSignal("1st t-derivative of arc-length Log10", "sDt1Log10".CombineFolderPath());

        //sDt2.PlotScalarSignal("2nd t-derivative of arc-length", "sDt2".CombineFolderPath());
        //sDt2.Log10().PlotScalarSignal("2nd t-derivative of arc-length Log10", "sDt2Log10".CombineFolderPath());

        //sDt3.PlotScalarSignal("3rd t-derivative of arc-length", "sDt3".CombineFolderPath());
        //sDt3.Log10().PlotScalarSignal("3rd t-derivative of arc-length Log10", "sDt3Log10".CombineFolderPath());

        //for (var i = 0; i < omegaMeanList.Count; i++)
        //{
        //    var omegaNorm = omegaMeanList[i].Norm() / (2 * Math.PI);

        //    omegaNorm.Scalar.PlotScalarSignal($"Darboux Blade Average Norm {i + 1}", $"DBANorm{i + 1}".CombineFolderPath());
        //    omegaNorm.Scalar.Log10().PlotScalarSignal($"Darboux Blade Average Norm {i + 1} Log10", $"DBANorm{i + 1}Log10".CombineFolderPath());
        //}

        //var dbNorm = dbMean.Norm() / (2 * Math.PI);

        //dbNorm.Scalar.PlotScalarSignal($"Darboux Bivector Average Norm", $"DBNorm".CombineFolderPath());
        //dbNorm.Scalar.Log10().PlotScalarSignal($"Darboux Bivector Average Norm Log10", $"DBNormLog10".CombineFolderPath());

        ////var vNorm = v.Norm();
        ////kappa1 *= vNorm;
        ////kappa2 *= vNorm;
        ////kappa3 *= vNorm;
        ////kappa4 *= vNorm;
        ////kappa5 *= vNorm;

        //kappa1.PlotScalarSignal("1st curvature coefficient", "kappa1".CombineFolderPath());
        //kappa1.Log10().PlotScalarSignal("1st curvature coefficient Log10", "kappa1Log10".CombineFolderPath());

        //kappa2.PlotScalarSignal("2nd curvature coefficient", "kappa2".CombineFolderPath());
        //kappa2.Log10().PlotScalarSignal("2nd curvature coefficient Log10", "kappa2Log10".CombineFolderPath());


        Console.WriteLine($"   Writing data");

        var vectorColumnNames = new[] { "1", "2", "3" };
        var bivectorColumnNames = new[] { "12", "13", "23" };

        var rowIndex = 3 + sectionFirstIndex;
        var columnIndex = 1;

        workSheet.WriteIndices(rowIndex, columnIndex, sectionFirstIndex, SampleCount, "n");
        columnIndex += 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tValues, "t");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, v, "Signal", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt1, "1st t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt2, "2nd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt3, "3rd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt1, "s'(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt2, "s''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt3, "s'''(t)");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs1, "1st s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs2, "2nd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs3, "3rd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u1s, "u1(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u2s, "u2(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u3s, "u3(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u1sNorm, "|| u1(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u2sNorm, "|| u2(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u3sNorm, "|| u3(s) ||");
        columnIndex += 1;

        for (var i = 0; i < omegaList.Count; i++)
        {
            var omega = omegaList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1}", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, db, $"Darboux Bivector", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa1, "kappa1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa2, "kappa2");
        columnIndex += 1;

        for (var i = 0; i < omegaMeanList.Count; i++)
        {
            var omega = omegaMeanList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1} Average", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        for (var i = 0; i < omegaMeanList.Count; i++)
        {
            var omegaNorm = omegaMeanList[i].Norm();

            workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, $"Darboux Blade {i + 1} Average Norm");
            columnIndex += 1;
        }

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, dbMean, $"Darboux Bivector Average", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, dbMean.Norm(), $"Darboux Bivector Average Norm");
        columnIndex += 1;
            
        Console.WriteLine($"Done");
        Console.WriteLine();
    }
        
    private static void ProcessSignalSection4D(XGaVector<Float64Signal> vectorSignal, DfSignalInterpolatorOptions interpolatorOptions, ExcelWorksheet workSheet, int sectionFirstIndex)
    {
        ScalarProcessor.ZeroEpsilon = 1e-7d;

        var sectionLastIndex = SampleCount - 1;

        var tMin = sectionFirstIndex / SamplingRate;
        var tMax = sectionLastIndex / SamplingRate;

        var tValues =
            tMin
                .GetLinearRange(tMax, SampleCount)
                .CreateSignal(SamplingRate);

        Console.WriteLine($"   Processing data");

        //var options = new DfFourierSignalInterpolatorOptions()
        //{
        //    EnergyAcThreshold = 100000d,
        //    EnergyAcPercentThreshold = 0.99999998d,
        //    SignalToNoiseRatioThreshold = 300000d,
        //    FrequencyThreshold = 1000 * 2 * Math.PI,
        //    FrequencyCountThreshold = 750,
        //    PaddingTrendSampleCount = 1000,
        //    PaddingPolynomialDegree = 5,
        //    PaddingSampleCount = -1,
        //    AssumePeriodic = false
        //};

        ////var options = new SpectrumInterpolationOptions()
        ////{
        ////    EnergyAcThreshold = 1000d,
        ////    EnergyAcPercentThreshold = 0.9998d,
        ////    SignalToNoiseRatioThreshold = 3000d,
        ////    FrequencyThreshold = 750 * 2 * Math.PI,
        ////    FrequencyCountThreshold = 750,
        ////    PaddingPolynomialSampleCount = 50,
        ////    PaddingPolynomialDegree = 6
        ////};

        //var options = new DfLinearSplineSignalInterpolatorOptions()
        //{
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        //var options = new DfCatmullRomSplineSignalInterpolatorOptions()
        //{
        //    BezierDegree = 3,
        //    SplineType = CatmullRomSplineType.Centripetal,
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        //var options = new DfChebyshevInterpolatorOptions()
        //{
        //    SectionCount = 1,
        //    PolynomialDegree = 300,
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        var vectorSignalProcessor = new XGaGeometricFrequencyDifferentialProcessor(
            4,
            (vSignal =>
                VectorDifferentialInterpolator.Create(
                    vSignal,
                    tValues.SamplingSpecs,
                    interpolatorOptions
                )
            )
        );

        vectorSignalProcessor.RunningAverageSampleCount = 
            (int)(SamplingRate / GridFrequencyHz);

        vectorSignalProcessor.ProcessVectorSignal(vectorSignal);


        //var affineFrameMatrixList1 = 
        //    vectorSignalProcessor.VectorSignalArcLengthDerivatives.ComputeAffineFrameMatrices(SampleCount, VSpaceDimensions);
            
        //var affineFrameMatrixList2 = 
        //    vectorSignalProcessor.ArcLengthFramesOrthonormal.ComputeAffineFrameMatrices(SampleCount, VSpaceDimensions);

        //Console.WriteLine("Affine Frame Matrices:");

        //for (var i = 0; i < 10; i++)
        //{
        //    var matrix1 = affineFrameMatrixList1[i + 1] * 
        //                 affineFrameMatrixList1[i].Inverse();
                
        //    var matrix2 = affineFrameMatrixList2[i + 1] * 
        //                 affineFrameMatrixList2[i].Inverse();
                
        //    Console.WriteLine(matrix1);
        //    Console.WriteLine();

        //    Console.WriteLine(matrix2);
        //    Console.WriteLine();

        //    var subspaceList1 = matrix1.GetSimpleEigenSubspaces();

        //    foreach (var subspace in subspaceList1)
        //        Console.WriteLine(subspace.ToString());

                
        //    var subspaceList2 = matrix2.GetSimpleEigenSubspaces();

        //    foreach (var subspace in subspaceList2)
        //        Console.WriteLine(subspace.ToString());
        //}
            
        //Console.WriteLine();

        var v = vectorSignalProcessor.VectorSignalInterpolated;

        var vDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0];
        var vDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1];
        var vDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2];
        var vDt4 = vectorSignalProcessor.VectorSignalTimeDerivatives[3];

        var signalToNoiseRatio =
            vectorSignal.SignalToNoiseRatio(vectorSignal - v);

        Console.WriteLine($"   Vector signal SNR: {signalToNoiseRatio:G}");
        Console.WriteLine();

        //var vSum123 = 
        //    (v[0] + v[1] + v[2]).ScalarValue.Rms().Square() / 
        //    (v[0].ScalarValue.Rms().Square() + v[1].ScalarValue.Rms().Square() + v[2].ScalarValue.Rms().Square());

        //var vSum456 = 
        //    (v[3] + v[4] + v[5]).ScalarValue.Rms().Square() /
        //    (v[3].ScalarValue.Rms().Square() + v[4].ScalarValue.Rms().Square() + v[5].ScalarValue.Rms().Square());

        //Debug.Assert(vSum123.IsNearZero(0.01d));
        //Debug.Assert(vSum456.IsNearZero(0.01d));

        var sDt1 = vectorSignalProcessor.ArcLengthTimeDerivatives[0];
        var sDt2 = vectorSignalProcessor.ArcLengthTimeDerivatives[1];
        var sDt3 = vectorSignalProcessor.ArcLengthTimeDerivatives[2];
        var sDt4 = vectorSignalProcessor.ArcLengthTimeDerivatives[3];

        var vDs1 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[0];
        var vDs2 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[1];
        var vDs3 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[2];
        var vDs4 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[3];

        var u1s = vectorSignalProcessor.ArcLengthFramesOrthogonal[0];
        var u2s = vectorSignalProcessor.ArcLengthFramesOrthogonal[1];
        var u3s = vectorSignalProcessor.ArcLengthFramesOrthogonal[2];
        var u4s = vectorSignalProcessor.ArcLengthFramesOrthogonal[3];

        var u1sNorm = u1s.Norm();
        var u2sNorm = u2s.Norm();
        var u3sNorm = u3s.Norm();
        var u4sNorm = u4s.Norm();

        // Angular velocity blades
        var omegaList = vectorSignalProcessor.AngularVelocityBladesMeans;
        var omegaAverageList = vectorSignalProcessor.AngularVelocityBladesMeans;
            
        var db = vectorSignalProcessor.DarbouxBivector;
        var dbMean = vectorSignalProcessor.DarbouxBivectorMean;

        var omegaIndex = 1;
        foreach (var omega in omegaList)
        {
            var omegaMean = omega.Mean();
            var omegaMeanNorm = omegaMean.Norm().ScalarValue;
            var omegaMeanNormHz = omegaMeanNorm / (2 * Math.PI);

            Console.WriteLine($"Omega {omegaIndex}");
            Console.WriteLine($"   Mean Bivector: {LaTeXComposer.GetMultivectorText(omegaMean)}");
            Console.WriteLine($"   Mean Bivector Norm: {omegaMeanNorm:G} rad/sec = {omegaMeanNormHz:G} Hz");
            Console.WriteLine();

            omegaIndex++;
        }
            
        var dbSignalsMean = db.Mean();
        var dbSignalsMeanNorm = dbSignalsMean.Norm().ScalarValue;
        var dbSignalsMeanNormHz = dbSignalsMeanNorm / (2 * Math.PI);

        Console.WriteLine($"Darboux Bivector");
        Console.WriteLine($"   Mean Bivector: {LaTeXComposer.GetMultivectorText(dbSignalsMean)}");
        Console.WriteLine($"   Mean Bivector Norm: {dbSignalsMeanNorm:G} rad/sec = {dbSignalsMeanNormHz:G} Hz");
        Console.WriteLine();

        // Curvatures
        var kappa1 = vectorSignalProcessor.Curvatures[0];
        var kappa2 = vectorSignalProcessor.Curvatures[1];
        var kappa3 = vectorSignalProcessor.Curvatures[2];

        var kappa1Mean = kappa1.Mean();
        var kappa2Mean = kappa2.Mean();
        var kappa3Mean = kappa3.Mean();

        Console.WriteLine($"   Mean of kappa1: {kappa1Mean:G}");
        Console.WriteLine($"   Mean of kappa2: {kappa2Mean:G}");
        Console.WriteLine($"   Mean of kappa3: {kappa3Mean:G}");
        Console.WriteLine();

        var kappa1Rms = kappa1.RootMeanSquare();
        var kappa2Rms = kappa2.RootMeanSquare();
        var kappa3Rms = kappa3.RootMeanSquare();

        Console.WriteLine($"   RMS of kappa1: {kappa1Rms:G}");
        Console.WriteLine($"   RMS of kappa2: {kappa2Rms:G}");
        Console.WriteLine($"   RMS of kappa3: {kappa3Rms:G}");
        Console.WriteLine();
            
            
        var dbNorm = db.ENormSquared();
        var kappaNorm = kappa1.Square() + kappa2.Square() + kappa3.Square();
            
        Debug.Assert(
            (dbNorm - kappaNorm).IsNearZero()
        );

        //Console.WriteLine($"   Plotting data");
            
        //vectorSignal.PlotSignal(
        //    v,
        //    0,
        //    (SampleCount - 1) / SamplingRate,
        //    "Signal".CombineFolderPath()
        //);

        //v.PlotVectorSignalComponents("Signal", "v".CombineFolderPath());

        //vDt1.PlotVectorSignalComponents("1st t-derivative", "vDt1".CombineFolderPath());
        //vDt2.PlotVectorSignalComponents("2nd t-derivative", "vDt2".CombineFolderPath());
        //vDt3.PlotVectorSignalComponents("3rd t-derivative", "vDt3".CombineFolderPath());
        //vDt4.PlotVectorSignalComponents("4th t-derivative", "vDt4".CombineFolderPath());

        //vDs1.PlotVectorSignalComponents("1st s-derivative", "vDs1".CombineFolderPath());
        //vDs2.PlotVectorSignalComponents("2nd s-derivative", "vDs2".CombineFolderPath());
        //vDs3.PlotVectorSignalComponents("3rd s-derivative", "vDs3".CombineFolderPath());
        //vDs4.PlotVectorSignalComponents("4th s-derivative", "vDs4".CombineFolderPath());

        //sDt1.PlotScalarSignal("1st t-derivative of arc-length", "sDt1".CombineFolderPath());
        //sDt1.Log10().PlotScalarSignal("1st t-derivative of arc-length Log10", "sDt1Log10".CombineFolderPath());

        //sDt2.PlotScalarSignal("2nd t-derivative of arc-length", "sDt2".CombineFolderPath());
        //sDt2.Log10().PlotScalarSignal("2nd t-derivative of arc-length Log10", "sDt2Log10".CombineFolderPath());

        //sDt3.PlotScalarSignal("3rd t-derivative of arc-length", "sDt3".CombineFolderPath());
        //sDt3.Log10().PlotScalarSignal("3rd t-derivative of arc-length Log10", "sDt3Log10".CombineFolderPath());

        //sDt4.PlotScalarSignal("4th t-derivative of arc-length", "sDt4".CombineFolderPath());
        //sDt4.Log10().PlotScalarSignal("4th t-derivative of arc-length Log10", "sDt4Log10".CombineFolderPath());

        //for (var i = 0; i < omegaAverageList.Count; i++)
        //{
        //    var omegaNorm = omegaAverageList[i].Norm() / (2 * Math.PI);

        //    omegaNorm.Scalar.PlotScalarSignal($"Darboux Blade Average Norm {i + 1}", $"DBANorm{i + 1}".CombineFolderPath());
        //    omegaNorm.Scalar.Log10().PlotScalarSignal($"Darboux Blade Average Norm {i + 1} Log10", $"DBANorm{i + 1}Log10".CombineFolderPath());
        //}
            
        //var dbNorm = dbMean.Norm() / (2 * Math.PI);

        //dbNorm.Scalar.PlotScalarSignal($"Darboux Bivector Average Norm", $"DBNorm".CombineFolderPath());
        //dbNorm.Scalar.Log10().PlotScalarSignal($"Darboux Bivector Average Norm Log10", $"DBNormLog10".CombineFolderPath());

        ////var vNorm = v.Norm();
        ////kappa1 *= vNorm;
        ////kappa2 *= vNorm;
        ////kappa3 *= vNorm;
        ////kappa4 *= vNorm;
        ////kappa5 *= vNorm;

        //kappa1.PlotScalarSignal("1st curvature coefficient", "kappa1".CombineFolderPath());
        //kappa1.Log10().PlotScalarSignal("1st curvature coefficient Log10", "kappa1Log10".CombineFolderPath());

        //kappa2.PlotScalarSignal("2nd curvature coefficient", "kappa2".CombineFolderPath());
        //kappa2.Log10().PlotScalarSignal("2nd curvature coefficient Log10", "kappa2Log10".CombineFolderPath());

        //kappa3.PlotScalarSignal("3rd curvature coefficient", "kappa3".CombineFolderPath());
        //kappa3.Log10().PlotScalarSignal("3rd curvature coefficient Log10", "kappa3Log10".CombineFolderPath());


        Console.WriteLine($"   Writing data");

        var vectorColumnNames = new[] { "1", "2", "3", "4" };
        var bivectorColumnNames = new[] { "12", "13", "23", "14", "24", "34" };

        var rowIndex = 3 + sectionFirstIndex;
        var columnIndex = 1;

        workSheet.WriteIndices(rowIndex, columnIndex, sectionFirstIndex, SampleCount, "n");
        columnIndex += 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tValues, "t");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, v, "Signal", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt1, "1st t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt2, "2nd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt3, "3rd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt4, "4th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt1, "s'(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt2, "s''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt3, "s'''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt4, "s''''(t)");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs1, "1st s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs2, "2nd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs3, "3rd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs4, "4th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u1s, "u1(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u2s, "u2(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u3s, "u3(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u4s, "u4(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u1sNorm, "|| u1(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u2sNorm, "|| u2(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u3sNorm, "|| u3(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u4sNorm, "|| u4(s) ||");
        columnIndex += 1;

        for (var i = 0; i < omegaList.Count; i++)
        {
            var omega = omegaList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1}", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        for (var i = 0; i < omegaAverageList.Count; i++)
        {
            var omega = omegaAverageList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1} Average", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }
            
        workSheet.WriteBivectorSignal(rowIndex, columnIndex, db, $"Darboux Bivector", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, dbMean, $"Darboux Bivector Average", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa1, "kappa1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa2, "kappa2");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa3, "kappa3");
        columnIndex += 1;

        for (var i = 0; i < omegaAverageList.Count; i++)
        {
            var omegaNorm = omegaAverageList[i].Norm();

            workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, $"Darboux Blade {i + 1} Average Norm");
            columnIndex += 1;
        }
            
        workSheet.WriteScalarSignal(rowIndex, columnIndex, dbMean.Norm(), $"Darboux Bivector Average Norm");
        columnIndex += 1;
            
        Console.WriteLine($"Done");
        Console.WriteLine();
    }

    private static void ProcessSignalSection6D(XGaVector<Float64Signal> vectorSignal, DfSignalInterpolatorOptions interpolatorOptions, ExcelWorksheet workSheet, int sectionFirstIndex)
    {
        ScalarProcessor.ZeroEpsilon = 1e-7d;

        var sectionLastIndex = SampleCount - 1;

        var tMin = sectionFirstIndex / SamplingRate;
        var tMax = sectionLastIndex / SamplingRate;

        var tValues =
            tMin
                .GetLinearRange(tMax, SampleCount)
                .CreateSignal(SamplingRate);

        Console.WriteLine($"   Processing data");
            
        //var interpolatorOptions = new DfFourierInterpolatorOptions()
        //{
        //    EnergyAcThreshold = 100000d,
        //    EnergyAcPercentThreshold = 0.99999998d,
        //    SignalToNoiseRatioThreshold = 300000d,
        //    FrequencyThreshold = 1000 * 2 * Math.PI,
        //    FrequencyCountThreshold = 750,
        //    PaddingTrendSampleCount = 1000,
        //    PaddingPolynomialDegree = 5,
        //    PaddingSampleCount = -1,
        //    AssumePeriodic = false
        //};

        //var interpolatorOptions = new SpectrumInterpolationOptions()
        //{
        //    EnergyAcThreshold = 1000d,
        //    EnergyAcPercentThreshold = 0.9998d,
        //    SignalToNoiseRatioThreshold = 3000d,
        //    FrequencyThreshold = 750 * 2 * Math.PI,
        //    FrequencyCountThreshold = 750,
        //    PaddingPolynomialSampleCount = 50,
        //    PaddingPolynomialDegree = 6
        //};

        //var vectorSignalProcessor = new GeometricFrequencyFourierProcessor(
        //    GeometricProcessor,
        //    interpolatorOptions
        //);

        //var vectorSignalProcessor = new GeometricFrequencyPolynomialProcessor(
        //    GeometricProcessor,
        //    7,
        //    51
        //);

        //var interpolatorOptions = new DfChebyshevInterpolatorOptions()
        //{
        //    SectionCount = 1,
        //    PolynomialDegree = 300,
        //    SmoothingFactors = new [] { 3, 5 } //Array.Empty<int>()
        //};
            
        //var interpolatorOptions = new DfChebyshevInterpolatorOptions()
        //{
        //    SectionCount = 1,
        //    PolynomialDegree = 300,
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        //var interpolatorOptions = new DfCatmullRomSplineInterpolatorOptions()
        //{
        //    BezierDegree = 3,
        //    SplineType = CatmullRomSplineType.Centripetal,
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};
            
        //var interpolatorOptions = new DfLinearSplineSignalInterpolatorOptions()
        //{
        //    SmoothingFactors = new[] { 3, 5 } //Array.Empty<int>()
        //};

        var vectorSignalProcessor = 
            new XGaGeometricFrequencyDifferentialProcessor(
                6,
                (vSignal =>
                    VectorDifferentialInterpolator.Create(
                        vSignal,
                        tValues.SamplingSpecs,
                        interpolatorOptions
                    )
                )
            );

        //new XGaGeometricFrequencyDifferentialProcessor(
        //    6,
        //    (v => 
        //        VectorDifferentialInterpolator.CreateLinearSpline(
        //            v,
        //            tValues.SamplingSpecs,
        //            interpolatorOptions
        //        )

        //        //VectorDifferentialInterpolator.CreateChebyshev(
        //        //    GeometricProcessor,
        //        //    v,
        //        //    tValues.SamplingSpecs,
        //        //    interpolatorOptions
        //        //)

        //        //VectorDifferentialInterpolator.CreateCatmullRomSpline(
        //        //    GeometricProcessor,
        //        //    v,
        //        //    tValues.SamplingSpecs,
        //        //    interpolatorOptions
        //        //)
        //    )
        //);

        //var interpolatorOptions = new FourierScalarInterpolatorOptions()
        //{
        //    EnergyAcThreshold = 1000d,
        //    EnergyAcPercentThreshold = 0.9998d,
        //    SignalToNoiseRatioThreshold = 3000d,
        //    FrequencyThreshold = 900 * 2 * Math.PI,
        //    FrequencyCountThreshold = 3500,
        //    PaddingPolynomialSampleCount = 50,
        //    PaddingPolynomialDegree = 6
        //};
            
        vectorSignalProcessor.RunningAverageSampleCount = (int)(SamplingRate / GridFrequencyHz);

        vectorSignalProcessor.ProcessVectorSignal(vectorSignal);

        var v = vectorSignalProcessor.VectorSignalInterpolated;

        var vDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0];
        var vDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1];
        var vDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2];
        var vDt4 = vectorSignalProcessor.VectorSignalTimeDerivatives[3];
        var vDt5 = vectorSignalProcessor.VectorSignalTimeDerivatives[4];
        var vDt6 = vectorSignalProcessor.VectorSignalTimeDerivatives[5];

        var signalToNoiseRatio =
            vectorSignal.SignalToNoiseRatio(vectorSignal - v);

        Console.WriteLine($"   Vector signal SNR: {signalToNoiseRatio:G}");
        Console.WriteLine();

        //var vSum123 = 
        //    (v[0] + v[1] + v[2]).ScalarValue.Rms().Square() / 
        //    (v[0].ScalarValue.Rms().Square() + v[1].ScalarValue.Rms().Square() + v[2].ScalarValue.Rms().Square());

        //var vSum456 = 
        //    (v[3] + v[4] + v[5]).ScalarValue.Rms().Square() /
        //    (v[3].ScalarValue.Rms().Square() + v[4].ScalarValue.Rms().Square() + v[5].ScalarValue.Rms().Square());

        //Debug.Assert(vSum123.IsNearZero(0.01d));
        //Debug.Assert(vSum456.IsNearZero(0.01d));

        var sDt1 = vectorSignalProcessor.ArcLengthTimeDerivatives[0];
        var sDt2 = vectorSignalProcessor.ArcLengthTimeDerivatives[1];
        var sDt3 = vectorSignalProcessor.ArcLengthTimeDerivatives[2];
        var sDt4 = vectorSignalProcessor.ArcLengthTimeDerivatives[3];
        var sDt5 = vectorSignalProcessor.ArcLengthTimeDerivatives[4];
        var sDt6 = vectorSignalProcessor.ArcLengthTimeDerivatives[5];

        var vDs1 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[0];
        var vDs2 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[1];
        var vDs3 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[2];
        var vDs4 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[3];
        var vDs5 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[4];
        var vDs6 = vectorSignalProcessor.VectorSignalArcLengthDerivatives[5];

        var u1s = vectorSignalProcessor.ArcLengthFramesOrthogonal[0];
        var u2s = vectorSignalProcessor.ArcLengthFramesOrthogonal[1];
        var u3s = vectorSignalProcessor.ArcLengthFramesOrthogonal[2];
        var u4s = vectorSignalProcessor.ArcLengthFramesOrthogonal[3];
        var u5s = vectorSignalProcessor.ArcLengthFramesOrthogonal[4];
        var u6s = vectorSignalProcessor.ArcLengthFramesOrthogonal[5];

        var u1sNorm = u1s.Norm();
        var u2sNorm = u2s.Norm();
        var u3sNorm = u3s.Norm();
        var u4sNorm = u4s.Norm();
        var u5sNorm = u5s.Norm();
        var u6sNorm = u6s.Norm();

        // Angular velocity blades
        var omegaList = vectorSignalProcessor.AngularVelocityBladesMeans;
        var omegaAverageList = vectorSignalProcessor.AngularVelocityBladesMeans;

        var omegaIndex = 1;
        foreach (var omega in omegaList)
        {
            var omegaMean = omega.Mean();
            var omegaMeanNorm = omegaMean.Norm().ScalarValue;
            var omegaMeanNormHz = omegaMeanNorm / (2 * Math.PI);

            Console.WriteLine($"Omega {omegaIndex}");
            Console.WriteLine($"   Mean Bivector: {LaTeXComposer.GetMultivectorText(omegaMean)}");
            Console.WriteLine($"   Mean Bivector Norm: {omegaMeanNorm:G} rad/sec = {omegaMeanNormHz:G} Hz");
            Console.WriteLine();

            omegaIndex++;
        }

        // Curvatures
        var kappa1 = vectorSignalProcessor.Curvatures[0];
        var kappa2 = vectorSignalProcessor.Curvatures[1];
        var kappa3 = vectorSignalProcessor.Curvatures[2];
        var kappa4 = vectorSignalProcessor.Curvatures[3];
        var kappa5 = vectorSignalProcessor.Curvatures[4];

        var kappa1Mean = kappa1.Mean();
        var kappa2Mean = kappa2.Mean();
        var kappa3Mean = kappa3.Mean();
        var kappa4Mean = kappa4.Mean();
        var kappa5Mean = kappa5.Mean();

        Console.WriteLine($"   Mean of kappa1: {kappa1Mean:G}");
        Console.WriteLine($"   Mean of kappa2: {kappa2Mean:G}");
        Console.WriteLine($"   Mean of kappa3: {kappa3Mean:G}");
        Console.WriteLine($"   Mean of kappa4: {kappa4Mean:G}");
        Console.WriteLine($"   Mean of kappa5: {kappa5Mean:G}");
        Console.WriteLine();

        var kappa1Rms = kappa1.RootMeanSquare();
        var kappa2Rms = kappa2.RootMeanSquare();
        var kappa3Rms = kappa3.RootMeanSquare();
        var kappa4Rms = kappa4.RootMeanSquare();
        var kappa5Rms = kappa5.RootMeanSquare();

        Console.WriteLine($"   RMS of kappa1: {kappa1Rms:G}");
        Console.WriteLine($"   RMS of kappa2: {kappa2Rms:G}");
        Console.WriteLine($"   RMS of kappa3: {kappa3Rms:G}");
        Console.WriteLine($"   RMS of kappa4: {kappa4Rms:G}");
        Console.WriteLine($"   RMS of kappa5: {kappa5Rms:G}");
        Console.WriteLine();
            

        //Console.WriteLine($"   Plotting data");
            
        //vectorSignal.PlotSignal(
        //    v,
        //    0,
        //    (SampleCount - 1) / SamplingRate,
        //    "Signal".CombineFolderPath()
        //);

        //v.PlotVectorSignalComponents("Signal", "v".CombineFolderPath());

        //vDt1.PlotVectorSignalComponents("1st t-derivative", "vDt1".CombineFolderPath());
        //vDt2.PlotVectorSignalComponents("2nd t-derivative", "vDt2".CombineFolderPath());
        //vDt3.PlotVectorSignalComponents("3rd t-derivative", "vDt3".CombineFolderPath());
        //vDt4.PlotVectorSignalComponents("4th t-derivative", "vDt4".CombineFolderPath());
        //vDt5.PlotVectorSignalComponents("5th t-derivative", "vDt5".CombineFolderPath());
        //vDt6.PlotVectorSignalComponents("6th t-derivative", "vDt6".CombineFolderPath());

        //vDs1.PlotVectorSignalComponents("1st s-derivative", "vDs1".CombineFolderPath());
        //vDs2.PlotVectorSignalComponents("2nd s-derivative", "vDs2".CombineFolderPath());
        //vDs3.PlotVectorSignalComponents("3rd s-derivative", "vDs3".CombineFolderPath());
        //vDs4.PlotVectorSignalComponents("4th s-derivative", "vDs4".CombineFolderPath());
        //vDs5.PlotVectorSignalComponents("5th s-derivative", "vDs5".CombineFolderPath());
        //vDs6.PlotVectorSignalComponents("6th s-derivative", "vDs6".CombineFolderPath());

        //sDt1.PlotScalarSignal("1st t-derivative of arc-length", "sDt1".CombineFolderPath());
        //sDt1.Log10().PlotScalarSignal("1st t-derivative of arc-length Log10", "sDt1Log10".CombineFolderPath());

        //sDt2.PlotScalarSignal("2nd t-derivative of arc-length", "sDt2".CombineFolderPath());
        //sDt2.Log10().PlotScalarSignal("2nd t-derivative of arc-length Log10", "sDt2Log10".CombineFolderPath());

        //sDt3.PlotScalarSignal("3rd t-derivative of arc-length", "sDt3".CombineFolderPath());
        //sDt3.Log10().PlotScalarSignal("3rd t-derivative of arc-length Log10", "sDt3Log10".CombineFolderPath());

        //sDt4.PlotScalarSignal("4th t-derivative of arc-length", "sDt4".CombineFolderPath());
        //sDt4.Log10().PlotScalarSignal("4th t-derivative of arc-length Log10", "sDt4Log10".CombineFolderPath());

        //sDt5.PlotScalarSignal("5th t-derivative of arc-length", "sDt5".CombineFolderPath());
        //sDt5.Log10().PlotScalarSignal("5th t-derivative of arc-length Log10", "sDt5Log10".CombineFolderPath());

        //sDt6.PlotScalarSignal("6th t-derivative of arc-length", "sDt6".CombineFolderPath());
        //sDt6.Log10().PlotScalarSignal("6th t-derivative of arc-length Log10", "sDt6Log10".CombineFolderPath());

        //for (var i = 0; i < omegaAverageList.Count; i++)
        //{
        //    var omegaNorm = omegaAverageList[i].Norm() / (2 * Math.PI);

        //    omegaNorm.Scalar.PlotScalarSignal($"Darboux Blade Average Norm {i + 1}", $"DBANorm{i + 1}".CombineFolderPath());
        //    omegaNorm.Scalar.Log10().PlotScalarSignal($"Darboux Blade Average Norm {i + 1} Log10", $"DBANorm{i + 1}Log10".CombineFolderPath());
        //}

        ////var vNorm = v.Norm();
        ////kappa1 *= vNorm;
        ////kappa2 *= vNorm;
        ////kappa3 *= vNorm;
        ////kappa4 *= vNorm;
        ////kappa5 *= vNorm;

        //kappa1.PlotScalarSignal("1st curvature coefficient", "kappa1".CombineFolderPath());
        //kappa1.Log10().PlotScalarSignal("1st curvature coefficient Log10", "kappa1Log10".CombineFolderPath());

        //kappa2.PlotScalarSignal("2nd curvature coefficient", "kappa2".CombineFolderPath());
        //kappa2.Log10().PlotScalarSignal("2nd curvature coefficient Log10", "kappa2Log10".CombineFolderPath());

        //kappa3.PlotScalarSignal("3rd curvature coefficient", "kappa3".CombineFolderPath());
        //kappa3.Log10().PlotScalarSignal("3rd curvature coefficient Log10", "kappa3Log10".CombineFolderPath());

        //kappa4.PlotScalarSignal("4th curvature coefficient", "kappa4".CombineFolderPath());
        //kappa4.Log10().PlotScalarSignal("4th curvature coefficient Log10", "kappa4Log10".CombineFolderPath());

        //kappa5.PlotScalarSignal("5th curvature coefficient", "kappa5".CombineFolderPath());
        //kappa5.Log10().PlotScalarSignal("5th curvature coefficient Log10", "kappa5Log10".CombineFolderPath());


        Console.WriteLine($"   Writing data");

        var vectorColumnNames = new[] { "ia", "ib", "ic", "id", "ie", "if" };
        var bivectorColumnNames = new[] { "12", "13", "23", "14", "24", "34", "15", "25", "35", "45", "16", "26", "36", "46", "56" };

        var rowIndex = 3 + sectionFirstIndex;
        var columnIndex = 1;

        workSheet.WriteIndices(rowIndex, columnIndex, sectionFirstIndex, SampleCount, "n");
        columnIndex += 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tValues, "t");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, v, "Signal", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt1, "1st t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt2, "2nd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt3, "3rd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt4, "4th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt5, "5th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt6, "6th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt1, "s'(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt2, "s''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt3, "s'''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt4, "s''''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt5, "s'''''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt6, "s''''''(t)");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs1, "1st s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs2, "2nd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs3, "3rd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs4, "4th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs5, "5th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs6, "6th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u1s, "u1(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u2s, "u2(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u3s, "u3(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u4s, "u4(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u5s, "u5(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u6s, "u6(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u1sNorm, "|| u1(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u2sNorm, "|| u2(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u3sNorm, "|| u3(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u4sNorm, "|| u4(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u5sNorm, "|| u5(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u6sNorm, "|| u6(s) ||");
        columnIndex += 1;

        for (var i = 0; i < omegaList.Count; i++)
        {
            var omega = omegaList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1}", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        for (var i = 0; i < omegaAverageList.Count; i++)
        {
            var omega = omegaAverageList[i];

            workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, $"Darboux Blade {i + 1} Average", bivectorColumnNames);
            columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;
        }

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa1, "kappa1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa2, "kappa2");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa3, "kappa3");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa4, "kappa4");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa5, "kappa5");
        columnIndex += 1;

        for (var i = 0; i < omegaAverageList.Count; i++)
        {
            var omegaNorm = omegaAverageList[i].Norm();

            workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, $"Darboux Blade {i + 1} Average Norm");
            columnIndex += 1;
        }
            
        Console.WriteLine($"Done");
        Console.WriteLine();
    }


    /// <summary>
    /// Read the data set and split it into multiple files
    /// </summary>
    public static void Example1()
    {
        var fileName = @"Corrientes_malaga_6250Hz.xlsx".CombineFolderPath();

        ScalarSignalProcessor =
            Float64SignalComposerUtils.CreateFloat64ScalarSignalProcessor(
                50000d / DownSampleFactor,
                250250 / 26
            );

        GeometricSignalProcessor =
            ScalarSignalProcessor.CreateEuclideanXGaProcessor();

        var index1 = 2 * SampleCount;
        var index2 = index1 + SampleCount - 1;

        var tMin = index1 / SamplingRate;
        var tMax = index2 / SamplingRate;

        var tValues =
            tMin
                .GetLinearRange(tMax, SampleCount)
                .CreateSignal(SamplingRate);

        var vectorSignal =
            ReadExcelVectorSignal(
                fileName,
                3 + index1,
                SampleCount,
                3,
                VSpaceDimensions
            );



        var signalArray =
            vectorSignal.GetSignalDerivatives(0, SampleCount - 1);

        var v = signalArray[0];
        var vDt1 = signalArray[1];
        var vDt2 = signalArray[2];
        var vDt3 = signalArray[3];
        var vDt4 = signalArray[4];
        var vDt5 = signalArray[5];
        var vDt6 = signalArray[6];

        var vSum123 =
            (v.Scalar(0) + v.Scalar(1) + v.Scalar(2)).ScalarValue.RootMeanSquare().Square() /
            (v.Scalar(0).ScalarValue.RootMeanSquare().Square() + v.Scalar(1).ScalarValue.RootMeanSquare().Square() + v.Scalar(2).ScalarValue.RootMeanSquare().Square());

        var vSum456 =
            (v.Scalar(3) + v.Scalar(4) + v.Scalar(5)).ScalarValue.RootMeanSquare().Square() /
            (v.Scalar(3).ScalarValue.RootMeanSquare().Square() + v.Scalar(4).ScalarValue.RootMeanSquare().Square() + v.Scalar(5).ScalarValue.RootMeanSquare().Square());

        Debug.Assert(vSum123.IsNearZero(0.01d));
        Debug.Assert(vSum456.IsNearZero(0.01d));

        var vDt1NormSquared = vDt1.NormSquared();
        var vDt1Norm = vDt1NormSquared.Sqrt();

        var vDt2NormSquared = vDt2.NormSquared();
        var vDt2Norm = vDt2NormSquared.Sqrt();
        var vDt1Dt2Dot = vDt1.Sp(vDt2);

        var sDt1 = vDt1.Sp(vDt1).Sqrt();
        var sDt2 = vDt1.Sp(vDt2) / sDt1;
        var sDt3 = (vDt2.Sp(vDt2) + vDt1.Sp(vDt3) - sDt2.Square()) / sDt1;
        var sDt4 = (3 * vDt2.Sp(vDt3) + vDt1.Sp(vDt4) - 3 * sDt2 * sDt3) / sDt1;
        var sDt5 = (3 * vDt3.Sp(vDt3) + 4 * vDt2.Sp(vDt4) + vDt1.Sp(vDt5) - 3 * sDt3.Square() - 4 * sDt2 * sDt4) / sDt1;
        var sDt6 = (10 * vDt3.Sp(vDt4) + 5 * vDt2.Sp(vDt5) + vDt1.Sp(vDt6) - 5 * sDt2 * sDt5 - 10 * sDt3 * sDt4) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Power(2) - sDt1 * sDt3) * vDt1) / sDt1.Power(5);
        var vDs4 = (sDt1.Cube() * vDt4 - 6 * sDt1.Square() * sDt2 * vDt3 - (4 * sDt1.Square() * sDt3 - 15 * sDt1 * sDt2.Square()) * vDt2 + (10 * sDt1 * sDt2 * sDt3 - 15 * sDt2.Cube() - sDt1.Square() * sDt4) * vDt1) / sDt1.Power(7);
        var vDs5 = ((45 * sDt1.Square() * sDt2.Square() - 10 * sDt1.Cube() * sDt3) * vDt3 + vDt2 * (-105 * sDt1 * sDt2.Cube() + 60 * sDt1.Square() * sDt2 * sDt3 - 5 * sDt1.Cube() * sDt4) - 10 * sDt1.Cube() * sDt2 * vDt4 + vDt1 * (5 * (21 * sDt2.Power(4) - 21 * sDt1 * sDt2.Square() * sDt3 + 2 * sDt1.Square() * sDt3.Square() + 3 * sDt1.Square() * sDt2 * sDt4) - sDt1.Cube() * sDt5) + sDt1.Power(4) * vDt5) / sDt1.Power(9);
        var vDs6 = (sDt1.Power(5) * vDt6 - 15 * sDt1.Power(4) * sDt2 * vDt5 + (105 * sDt1.Cube() * sDt2.Square() - 20 * sDt1.Power(4) * sDt3) * vDt4 - (420 * sDt1.Square() * sDt2.Cube() - 210 * sDt1.Cube() * sDt2 * sDt3 + 15 * sDt1.Power(4) * sDt4) * vDt3 + (945 * sDt1 * sDt2.Power(4) + 70 * sDt1.Cube() * sDt3.Square() - 840 * sDt1.Square() * sDt2.Square() * sDt3 + 105 * sDt1.Cube() * sDt2 * sDt4 - 6 * sDt1.Power(4) * sDt5) * vDt2 + (21 * sDt1.Cube() * sDt2 * sDt5 - sDt1.Power(4) * sDt6 - 35 * (27 * sDt2.Power(5) - 36 * sDt1 * sDt2.Cube() * sDt3 + 8 * sDt1.Square() * sDt2 * sDt3.Square() + sDt1.Square() * (6 * sDt2.Square() - sDt1 * sDt3) * sDt4)) * vDt1) / sDt1.Power(11);

        var vDs1NormSquared = vDs1.NormSquared();
        var vDs1Norm = vDs1NormSquared.Sqrt();

        var vDs2NormSquared = vDs2.NormSquared();
        var vDs2Norm = vDs2NormSquared.Sqrt();

        var vDs3NormSquared = vDs3.NormSquared();
        var vDs3Norm = vDs3NormSquared.Sqrt();

        var vDs4NormSquared = vDs4.NormSquared();
        var vDs4Norm = vDs4NormSquared.Sqrt();

        // Make sure vDs1 is a unit vector, and orthogonal to vDs2
        Debug.Assert(
            SignalValidator.ValidateUnitNormSquared(vDs1)
        );

        Debug.Assert(SignalValidator.ValidateOrthogonal(vDs1, vDs2));

        // Validate the general expression for norm of vDs2
        var vDs2Norm_1 =
            (vDt2NormSquared - 2 * (sDt2 / sDt1) * vDt1Dt2Dot + sDt2.Square()).Sqrt() / sDt1.Square();

        Debug.Assert(
            SignalValidator.ValidateEqual(vDs2Norm, vDs2Norm_1)
        );

        // Gram-Schmidt frame and its derivative of arc-length parametrization of curve
        var usArray =
            new[] { vDs1, vDs2, vDs3, vDs4, vDs5, vDs6 }.ApplyGramSchmidtByProjections(VSpaceDimensions, false);

        var u1s = usArray.Count < 1 ? GeometricSignalProcessor.VectorZero : usArray[0];
        var u2s = usArray.Count < 2 ? GeometricSignalProcessor.VectorZero : usArray[1];
        var u3s = usArray.Count < 3 ? GeometricSignalProcessor.VectorZero : usArray[2];
        var u4s = usArray.Count < 4 ? GeometricSignalProcessor.VectorZero : usArray[3];
        var u5s = usArray.Count < 5 ? GeometricSignalProcessor.VectorZero : usArray[4];
        var u6s = usArray.Count < 6 ? GeometricSignalProcessor.VectorZero : usArray[5];

        var u1sNorm = u1s.Norm();
        var u2sNorm = u2s.Norm();
        var u3sNorm = u3s.Norm();
        var u4sNorm = u4s.Norm();
        var u5sNorm = u5s.Norm();
        var u6sNorm = u6s.Norm();

        var e1s = u1s / u1sNorm;
        var e2s = u2s / u2sNorm;
        var e3s = u3s / u3sNorm;
        var e4s = u4s / u4sNorm;
        var e5s = u5s / u5sNorm;
        var e6s = u6s / u6sNorm;

        // Curvatures
        var kappa1 = (u2sNorm / u1sNorm).ScalarValue.MapSamples(s => s.NaNToZero()).ScalarFromValue(ScalarSignalProcessor);
        var kappa2 = (u3sNorm / u2sNorm).ScalarValue.MapSamples(s => s.NaNToZero()).ScalarFromValue(ScalarSignalProcessor);
        var kappa3 = (u4sNorm / u3sNorm).ScalarValue.MapSamples(s => s.NaNToZero()).ScalarFromValue(ScalarSignalProcessor);
        var kappa4 = (u5sNorm / u4sNorm).ScalarValue.MapSamples(s => s.NaNToZero()).ScalarFromValue(ScalarSignalProcessor);
        var kappa5 = (u6sNorm / u5sNorm).ScalarValue.MapSamples(s => s.NaNToZero()).ScalarFromValue(ScalarSignalProcessor);

        var kappa1Mean = kappa1.ScalarValue.Mean();
        var kappa2Mean = kappa2.ScalarValue.Mean();
        var kappa3Mean = kappa3.ScalarValue.Mean();
        var kappa4Mean = kappa4.ScalarValue.Mean();
        var kappa5Mean = kappa5.ScalarValue.Mean();

        Console.WriteLine($"Mean of kappa1: {kappa1Mean:G}");
        Console.WriteLine($"Mean of kappa2: {kappa2Mean:G}");
        Console.WriteLine($"Mean of kappa3: {kappa3Mean:G}");
        Console.WriteLine($"Mean of kappa4: {kappa4Mean:G}");
        Console.WriteLine($"Mean of kappa5: {kappa5Mean:G}");
        Console.WriteLine();

        var kappa1Rms = kappa1.ScalarValue.RootMeanSquare();
        var kappa2Rms = kappa2.ScalarValue.RootMeanSquare();
        var kappa3Rms = kappa3.ScalarValue.RootMeanSquare();
        var kappa4Rms = kappa4.ScalarValue.RootMeanSquare();
        var kappa5Rms = kappa5.ScalarValue.RootMeanSquare();

        Console.WriteLine($"RMS of kappa1: {kappa1Rms:G}");
        Console.WriteLine($"RMS of kappa2: {kappa2Rms:G}");
        Console.WriteLine($"RMS of kappa3: {kappa3Rms:G}");
        Console.WriteLine($"RMS of kappa4: {kappa4Rms:G}");
        Console.WriteLine($"RMS of kappa5: {kappa5Rms:G}");
        Console.WriteLine();

        // Angular velocity blade
        var omegaBar =
            kappa1 * e2s.Op(e1s);

        var omegaBarNorm = omegaBar.Norm();
        var omegaBarNormScaled = sDt1 * omegaBarNorm;

        // Darboux bivector
        var omega =
            kappa1 * e2s.Op(e1s) + kappa2 * e3s.Op(e2s) + kappa3 * e4s.Op(e3s) + kappa4 * e5s.Op(e4s) + kappa5 * e6s.Op(e5s);

        var omegaNorm = omega.Norm();

        // Bivector B = omega - omegaBar
        var bBivector = omega - omegaBar;

        var bBivectorNorm = bBivector.Norm();

        // Validate vDs1 is orthogonal to Bivector B
        var vDs1DotB =
            vDs1.Lcp(bBivector);

        Debug.Assert(
            SignalValidator.ValidateZeroNorm(vDs1DotB)
        );

        //// Display fundamental frequency and average value of omegaBarNormScaled
        //var omegaBarNormScaledSignal =
        //    omegaBarNormScaled.ScalarValue;

        //var omegaBarNormScaledFrequencies = 
        //    omegaBarNormScaledSignal.GetDominantFrequencyDataRecords(0.998d).ToArray();

        var omegaScaledMeanNorm =
            (omega * sDt1).Mean().Norm().ScalarValue;

        var omegaBarScaledMeanNorm =
            (omegaBar * sDt1).Mean().Norm().ScalarValue;

        Console.WriteLine($"Norm of Scaled OmegaBar Signal Mean: {omegaBarScaledMeanNorm:G}");
        Console.WriteLine($"Norm of Scaled Omega Signal Mean: {omegaScaledMeanNorm:G}");
        Console.WriteLine();

        //foreach (var (freqIndex, freqHz, energyRatio) in omegaBarNormScaledFrequencies)
        //{
        //    if (freqIndex == 0)
        //        continue;

        //    Console.WriteLine($"Scaled OmegaBar Norm Signal Frequency Samples ({freqIndex}, {omegaBarNormScaledSignal.Count - freqIndex}) = ±{freqHz:G} Hz holds {100 * energyRatio:G3}% of energy");
        //}

        //Console.WriteLine();

        var e1sDs = -e1s.Lcp(omega);
        var e2sDs = -e2s.Lcp(omega);
        var e3sDs = -e3s.Lcp(omega);
        var e4sDs = -e4s.Lcp(omega);
        var e5sDs = -e5s.Lcp(omega);
        var e6sDs = -e6s.Lcp(omega);

        var omega2 =
            (e1sDs.Op(e1s) + e2sDs.Op(e2s) + e3sDs.Op(e3s) + e4sDs.Op(e4s) + e5sDs.Op(e5s) + e6sDs.Op(e6s)) / 2;

        Debug.Assert(
            SignalValidator.ValidateEqual(omega, omega2)
        );

        // Validate curvature values
        Debug.Assert(
            SignalValidator.ValidateEqual(kappa1, e1sDs.Sp(e2s))
        );

        Debug.Assert(
            SignalValidator.ValidateEqual(kappa2, e2sDs.Sp(e3s))
        );

        Debug.Assert(
            SignalValidator.ValidateEqual(kappa3, e3sDs.Sp(e4s))
        );

        Debug.Assert(
            SignalValidator.ValidateEqual(kappa4, e4sDs.Sp(e5s))
        );

        Debug.Assert(
            SignalValidator.ValidateEqual(kappa5, e5sDs.Sp(e6s))
        );

        v.PlotVectorSignalComponents("Signal", "v");

        vDt1.PlotVectorSignalComponents("1st t-derivative", "vDt1".CombineFolderPath());
        vDt2.PlotVectorSignalComponents("2nd t-derivative", "vDt2".CombineFolderPath());
        vDt3.PlotVectorSignalComponents("3rd t-derivative", "vDt3".CombineFolderPath());
        vDt4.PlotVectorSignalComponents("4th t-derivative", "vDt4".CombineFolderPath());
        vDt5.PlotVectorSignalComponents("5th t-derivative", "vDt5".CombineFolderPath());
        vDt6.PlotVectorSignalComponents("6th t-derivative", "vDt6".CombineFolderPath());

        vDs1.PlotVectorSignalComponents("1st s-derivative", "vDs1".CombineFolderPath());
        vDs2.PlotVectorSignalComponents("2nd s-derivative", "vDs2".CombineFolderPath());
        vDs3.PlotVectorSignalComponents("3rd s-derivative", "vDs3".CombineFolderPath());
        vDs4.PlotVectorSignalComponents("4th s-derivative", "vDs4".CombineFolderPath());
        vDs5.PlotVectorSignalComponents("5th s-derivative", "vDs5".CombineFolderPath());
        vDs6.PlotVectorSignalComponents("6th s-derivative", "vDs6".CombineFolderPath());

        sDt1.PlotScalarSignal("1st t-derivative of arc-length", "sDt1".CombineFolderPath());
        sDt1.Log10().PlotScalarSignal("1st t-derivative of arc-length Log10", "sDt1Log10".CombineFolderPath());

        sDt2.ToScalar().PlotScalarSignal("2nd t-derivative of arc-length", "sDt2".CombineFolderPath());
        sDt2.Log10().PlotScalarSignal("2nd t-derivative of arc-length Log10", "sDt2Log10".CombineFolderPath());

        sDt3.PlotScalarSignal("3rd t-derivative of arc-length", "sDt3".CombineFolderPath());
        sDt3.Log10().PlotScalarSignal("3rd t-derivative of arc-length Log10", "sDt3Log10".CombineFolderPath());

        sDt4.PlotScalarSignal("4th t-derivative of arc-length", "sDt4".CombineFolderPath());
        sDt4.Log10().PlotScalarSignal("4th t-derivative of arc-length Log10", "sDt4Log10".CombineFolderPath());

        sDt5.PlotScalarSignal("5th t-derivative of arc-length", "sDt5".CombineFolderPath());
        sDt5.Log10().PlotScalarSignal("5th t-derivative of arc-length Log10", "sDt5Log10".CombineFolderPath());

        sDt6.PlotScalarSignal("6th t-derivative of arc-length", "sDt6".CombineFolderPath());
        sDt6.Log10().PlotScalarSignal("6th t-derivative of arc-length Log10", "sDt6Log10".CombineFolderPath());

        kappa1.PlotScalarSignal("1st curvature coefficient", "kappa1".CombineFolderPath());
        kappa1.Log10().PlotScalarSignal("1st curvature coefficient Log10", "kappa1Log10".CombineFolderPath());

        kappa2.PlotScalarSignal("2nd curvature coefficient", "kappa2".CombineFolderPath());
        kappa2.Log10().PlotScalarSignal("2nd curvature coefficient Log10", "kappa2Log10".CombineFolderPath());

        kappa3.PlotScalarSignal("3rd curvature coefficient", "kappa3".CombineFolderPath());
        kappa3.Log10().PlotScalarSignal("3rd curvature coefficient Log10", "kappa3Log10".CombineFolderPath());

        kappa4.PlotScalarSignal("4th curvature coefficient", "kappa4".CombineFolderPath());
        kappa4.Log10().PlotScalarSignal("4th curvature coefficient Log10", "kappa4Log10".CombineFolderPath());

        kappa5.PlotScalarSignal("5th curvature coefficient", "kappa5".CombineFolderPath());
        kappa5.Log10().PlotScalarSignal("5th curvature coefficient Log10", "kappa5Log10".CombineFolderPath());

        omegaBarNormScaled.PlotScalarSignal("Norm of scaled angular velocity blade", "omegaBarNormScaled".CombineFolderPath());
        omegaBarNormScaled.Log10().PlotScalarSignal("Log10 norm of scaled angular velocity blade", "omegaBarNormScaledLog10".CombineFolderPath());

        omegaBarNorm.PlotScalarSignal("Norm of angular velocity blade", "omegaBarNorm".CombineFolderPath());
        omegaBarNorm.Log10().PlotScalarSignal("Log10 norm of angular velocity blade", "omegaBarNormLog10".CombineFolderPath());

        omegaNorm.PlotScalarSignal("Norm of Darboux bivector", "omegaNorm".CombineFolderPath());
        omegaNorm.Log10().PlotScalarSignal("Log10 Norm of Darboux bivector", "omegaNormLog10".CombineFolderPath());

        bBivectorNorm.PlotScalarSignal("Norm of B bivector", "bBivectorNorm".CombineFolderPath());
        bBivectorNorm.Log10().PlotScalarSignal("Log10 Norm of B bivector", "bBivectorNormLog10".CombineFolderPath());


        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();

        var outputFilePath = @"Line currents.xlsx".CombineFolderPath();

        var vectorColumnNames = new[] { "ia", "ib", "ic", "id", "ie", "if" };
        var bivectorColumnNames = new[] { "12", "13", "23", "14", "24", "34", "15", "25", "35", "45", "16", "26", "36", "46", "56" };

        var workSheet = package.Workbook.Worksheets.Add("Sheet1");

        var rowIndex = 2 + index1;
        var columnIndex = 1;
        //var t1 = index1 / SamplingRate;
        //tValues = tValues.Select(t => t + t1).CreateSignal(SamplingRate);

        workSheet.WriteIndices(rowIndex, columnIndex, index1, SampleCount, "n");
        columnIndex += 1;

        workSheet.WriteScalars(rowIndex, columnIndex, tValues, "t");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, v, "Signal", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt1, "1st t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt2, "2nd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt3, "3rd t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt4, "4th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt5, "5th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDt6, "6th t-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt1, "s'(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt2, "s''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt3, "s'''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt4, "s''''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt5, "s'''''(t)");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, sDt6, "s''''''(t)");
        columnIndex += 1;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs1, "1st s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs2, "2nd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs3, "3rd s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs4, "4th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs5, "5th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, vDs6, "6th s-derivative", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u1s, "u1(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u2s, "u2(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u3s, "u3(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u4s, "u4(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u5s, "u5(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteVectorSignal(rowIndex, columnIndex, u6s, "u6(s)", vectorColumnNames);
        columnIndex += VSpaceDimensions;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u1sNorm, "|| u1(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u2sNorm, "|| u2(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u3sNorm, "|| u3(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u4sNorm, "|| u4(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u5sNorm, "|| u5(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, u6sNorm, "|| u6(s) ||");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa1, "kappa1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa2, "kappa2");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa3, "kappa3");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa4, "kappa4");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, kappa5, "kappa5");
        columnIndex += 1;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, omegaBar, "Angular Velocity Blade", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, omega, "Darboux Bivector", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteBivectorSignal(rowIndex, columnIndex, bBivector, "B Bivector", bivectorColumnNames);
        columnIndex += VSpaceDimensions * (VSpaceDimensions - 1) / 2;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaBarNormScaled, "Scaled Angular Velocity Blade Norm");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaBarNorm, "Angular Velocity Blade Norm");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, omegaNorm, "Darboux Bivector Norm");
        columnIndex += 1;

        workSheet.WriteScalarSignal(rowIndex, columnIndex, bBivectorNorm, "B Bivector Norm");
        columnIndex += 1;

        package.SaveAs(outputFilePath);
    }

    public static void Example2()
    {
        var smoothingFactors = new[] { 3, 5 };  //Array.Empty<int>()

        var options1 = new DfFourierSignalInterpolatorOptions()
        {
            SmoothingFactors = smoothingFactors,
            EnergyAcThreshold = 100000d,
            EnergyAcPercentThreshold = 0.99999998d,
            SignalToNoiseRatioThreshold = 300000d,
            FrequencyThreshold = 1000 * 2 * Math.PI,
            FrequencyCountThreshold = 750,
            PaddingTrendSampleCount = 1000,
            PaddingPolynomialDegree = 5,
            PaddingSampleCount = -1,
            AssumePeriodic = false
        };

        ////var options = new SpectrumInterpolationOptions()
        ////{
        ////    EnergyAcThreshold = 1000d,
        ////    EnergyAcPercentThreshold = 0.9998d,
        ////    SignalToNoiseRatioThreshold = 3000d,
        ////    FrequencyThreshold = 750 * 2 * Math.PI,
        ////    FrequencyCountThreshold = 750,
        ////    PaddingPolynomialSampleCount = 50,
        ////    PaddingPolynomialDegree = 6
        ////};

        var options2 = new DfLinearSplineSignalInterpolatorOptions
        {
            SmoothingFactors = smoothingFactors
        };

        var options3 = new DfCatmullRomSplineSignalInterpolatorOptions
        {
            BezierDegree = 3,
            SplineType = CatmullRomSplineType.Centripetal,
            SmoothingFactors = smoothingFactors
        };

        var options4 = new DfChebyshevSignalInterpolatorOptions()
        {
            SectionCount = 1,
            PolynomialDegree = 300,
            SmoothingFactors = smoothingFactors
        };

        var signalsArray = new[]
        {
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("Aulario3", "LineCurrent", InitializeAulario3LineCurrentSignal),
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("Aulario3", "PhaseNeutralVoltage", InitializeAulario3PhaseNeutralVoltageSignal),
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("Aulario3", "LineVoltage", InitializeAulario3LineVoltageSignal),
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("Aulario3", "PhaseVoltage", InitializeAulario3PhaseVoltageSignal),
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("EMTP", "LineVoltage", InitializeEMTPLineVoltageSignal),
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("EMTP", "PhaseVoltage", InitializeEMTPPhaseVoltageSignal),
            new Tuple<string, string, Func<XGaVector<Float64Signal>>>("EMTP", "PhaseNeutralVoltage", InitializeEMTPPhaseNeutralVoltageSignal)
        };

        var optionsArray = new[]
        {
            new Tuple<string, DfSignalInterpolatorOptions>("Fourier", options1),
            new Tuple<string, DfSignalInterpolatorOptions>("Linear", options2),
            new Tuple<string, DfSignalInterpolatorOptions>("CatmullRom", options3),
            new Tuple<string, DfSignalInterpolatorOptions>("Chebyshev", options4)
        };

        foreach (var (dataSetName, signalName, signalFunc) in signalsArray)
        {
            var vectorSignal = signalFunc();

            foreach (var (optionsName, options) in optionsArray)
            {
                //var filePath = $@"{dataSetName}_{signalName}\{optionsName} Interpolation";
                var outputFilePath = $"{dataSetName}_{optionsName}_{signalName}.xlsx";

                Console.WriteLine($"Processing data set {dataSetName} signal {signalName} using {optionsName} interpolation");
                Console.WriteLine();

                ProcessSignal(
                    vectorSignal,
                    options,
                    outputFilePath
                );

                Console.WriteLine("Processing Done.");
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Read the data set and split it into multiple files
    /// </summary>
    public static void ProcessSignal(XGaVector<Float64Signal> vectorSignal, DfSignalInterpolatorOptions interpolatorOptions, string outputFilePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();

        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            
        SectionIndex = 0;

        var sectionFolderPath =
            Path.Combine(WorkingPath, $"Section{SectionIndex:000}");

        if (Directory.Exists(sectionFolderPath))
            Directory.Delete(sectionFolderPath, true);

        Directory.CreateDirectory(sectionFolderPath);


        if (VSpaceDimensions == 3)
            ProcessSignalSection3D(vectorSignal, interpolatorOptions, workSheet, 0);

        else if (VSpaceDimensions == 4)
            ProcessSignalSection4D(vectorSignal, interpolatorOptions, workSheet, 0);

        else
            ProcessSignalSection6D(vectorSignal, interpolatorOptions, workSheet, 0);

        //var sectionCount = 26;
        //var sectionSampleCount = 250250 / sectionCount;

        //for (var sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++)
        //{
        //    SectionIndex = sectionIndex;

        //    var sectionFolderPath = 
        //        Path.Combine(WorkingPath, $"Section{SectionIndex:000}");

        //    if (Directory.Exists(sectionFolderPath))
        //        Directory.Delete(sectionFolderPath, true);

        //    Directory.CreateDirectory(sectionFolderPath);

        //    var sectionFirstIndex = sectionIndex * sectionSampleCount;
        //    var sectionLastIndex = sectionFirstIndex + sectionSampleCount - 1;

        //    var vectorSignal = InitializeCorrientesMalagaLineCurrentSignal(sectionFirstIndex, sectionSampleCount);

        //    var tMin = sectionFirstIndex / SamplingRate;
        //    var tMax = sectionLastIndex / SamplingRate;

        //    Console.WriteLine($"Processing section {sectionIndex + 1} of {sectionCount}: {SampleCount} samples from {sectionFirstIndex} to {sectionLastIndex} at time interval [{tMin:G}, {tMax:G}] sec");
        //    Console.WriteLine();

        //    ProcessSignalSection(vectorSignal, workSheet, sectionFirstIndex);
        //}

        package.SaveAs(
            Path.Combine(WorkingPath, outputFilePath)
        );
    }

    public static void Example3()
    {
        const string FolderPath =
            @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";

        var sqrt2 = Math.Sqrt(2);
        var phaseCountArray = new[] { 3, 4, 5, 6 };
        var harmonicArray = new[] { 1, 2, 7 };
        //var harmonicArray = new[] { 1, 2, 3, 7, 11, 12 };
        var magnitudeArray = new[] { 200d * sqrt2, 20d * sqrt2, -30d * sqrt2 };
        //var magnitudeArray = new[] { 199d, 23d, 101d, -31, 41d, -13d };

        var signalComposer =
            Float64HarmonicSignalComposer.Create(
                50,
                1000,
                10
            );

        var samplingSpecs = signalComposer.GetSamplingSpecs();

        foreach (var n in phaseCountArray)
        {
            VSpaceDimensions = n;
            GeometricProcessor = XGaFloat64Processor.Euclidean;
            ScalarSignalProcessor = new ScalarProcessorOfFloat64Signal(samplingSpecs.SamplingRate, samplingSpecs.SampleCount);
            GeometricSignalProcessor = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

            for (var harmonicCount = harmonicArray.Length; harmonicCount <= harmonicArray.Length; harmonicCount++)
            {
                var harmonicsListText =
                    harmonicArray.Take(harmonicCount).Concatenate(", ");

                Console.WriteLine($"{n}-Dimensions, Harmonics: {harmonicsListText}");


                var vectorSignal = GeometricSignalProcessor.VectorZero;

                for (var harmonicFactorIndex = 0; harmonicFactorIndex < harmonicCount; harmonicFactorIndex++)
                {
                    var harmonicFactor = harmonicArray[harmonicFactorIndex];
                    var magnitude = magnitudeArray[harmonicFactorIndex];

                    //var magnitudeList = 
                    //    Enumerable.Range(0, n).Select(
                    //        _ => Normal.Sample(
                    //            magnitude, 
                    //            magnitude.Abs() / 5
                    //        )
                    //    ).ToArray();

                    vectorSignal += signalComposer.GenerateOddSignalComponents(
                        magnitude,
                        harmonicFactor,
                        n
                    ).CreateXGaVector(GeometricSignalProcessor);
                }

                var interpolatorOptions = new DfFourierSignalInterpolatorOptions()
                {
                    EnergyAcThreshold = 1000d,
                    EnergyAcPercentThreshold = 0.9998d,
                    SignalToNoiseRatioThreshold = 3000d,
                    FrequencyThreshold = 1000 * 2 * Math.PI,
                    FrequencyCountThreshold = 750,
                    PaddingTrendSampleCount = 1,
                    PaddingPolynomialDegree = 6,
                    AssumePeriodic = true
                };

                var vectorSignalProcessor = new AngularVelocityFourierSignalProcessor(
                    interpolatorOptions
                );

                //var vectorSignalProcessor = new AngularVelocityPolynomialSignalProcessor(
                //    GeometricProcessor,
                //    7,
                //    51
                //);

                vectorSignalProcessor.RunningAverageSampleCount =
                    (int)(SamplingRate / GridFrequencyHz);

                vectorSignalProcessor.ProcessVectorSignal(vectorSignal);

                // Angular velocity blade
                var omega = vectorSignalProcessor.AngularVelocityBlades;
                var omegaMean = omega.Mean();
                var omegaMeanNorm = omegaMean.Norm().ScalarValue;
                var omegaMeanNormHz = (omegaMeanNorm / (2 * Math.PI)).Round(4);
                var omegaMeanNormRatioHz = (omegaMeanNormHz / 50).Round(4);

                Console.WriteLine($"Omega1 Mean Bivector Norm: {omegaMeanNorm:G} rad/sec = {omegaMeanNormHz:G} Hz = {omegaMeanNormRatioHz:G} * 50 Hz");
                Console.WriteLine();

                var (e1, e2) =
                    vectorSignalProcessor.ArcLengthFramesOrthonormal;

                var e12 =
                    e1.Op(e2).Mean();

                var (v1, v2) =
                    e12.GetVectorBasis();

                //Console.WriteLine($"$e_{{12}} = {LaTeXComposer.GetMultivectorText(e12 / e12.Norm())}$");
                //Console.WriteLine();

                //Console.WriteLine($"$v_{{1}} = {LaTeXComposer.GetMultivectorText(v1)}$");
                //Console.WriteLine($"$v_{{2}} = {LaTeXComposer.GetMultivectorText(v2)}$");
                //Console.WriteLine();

                var s1 = GeometricProcessor.VectorTerm(0);
                var s2 = GeometricProcessor.VectorTerm(1);

                var r =
                    v1.CreatePureRotorSequence(v2, s1, s2, false);

                //Console.WriteLine($"$R v_{{1}} R = {LaTeXComposer.GetMultivectorText(r.OmMap(v1))}$");
                //Console.WriteLine($"$R v_{{2}} R = {LaTeXComposer.GetMultivectorText(r.OmMap(v2))}$");
                //Console.WriteLine();

                var vectorSignal1 =
                    vectorSignal.MapSignalVectors(
                        v => r.OmMap(v),
                        VSpaceDimensions
                    );

                var indexList =
                    vectorSignal1
                        .IndexScalarPairs
                        .Where(r => !r.Value.IsNearZero())
                        .OrderBy(r => r.Key)
                        .Select(r => r.Key + 1)
                        .Concatenate(", ");

                //Debug.Assert(indexList == "1, 2");

                //Console.WriteLine($"Index: {indexList}");
                //Console.WriteLine();

                var xValues = vectorSignal1.Scalar(0).ScalarValue;
                var yValues = vectorSignal1.Scalar(1).ScalarValue;

                harmonicsListText =
                    harmonicArray.Take(harmonicCount).Concatenate("-");

                vectorSignal1.PlotVectorSignalComponents(
                    "Rotated signal",
                    Path.Combine(FolderPath, $"Signal_{n}D-{harmonicsListText}")
                );

                var (xMin, xMax) = xValues.GetMinMaxValues();
                var (yMin, yMax) = yValues.GetMinMaxValues();

                var drawingBoard =
                    MutableBoundingBox2D.CreateFromPoints(
                        xMin,
                        yMin,
                        xMax,
                        yMax
                    ).CreateDrawingBoard(1, 1.1);

                var pointList =
                    Enumerable
                        .Range(0, SampleCount)
                        .Select(j => LinFloat64Vector2D.Create((Float64Scalar)xValues[j], (Float64Scalar)yValues[j]))
                        .ToArray();

                var normList =
                    pointList.Select(p => p.VectorENorm()).ToArray();

                var minNorm = normList.Min();
                var maxNorm = normList.Max();

                Console.WriteLine($"Signal norm range: {minNorm}, {maxNorm}");
                Console.WriteLine();

                drawingBoard
                    .ActiveLayer
                    .SetPen(2, Color.Black)
                    .DrawPolyline(pointList);

                drawingBoard.SaveToPngFile(
                    Path.Combine(FolderPath, $"Curve_{n}D-H{harmonicsListText}.png")
                );
            }
        }
    }

    public static void Example4()
    {
        const string FolderPath =
            @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";

        var sqrt2 = Math.Sqrt(2);
        var phaseCountArray = new[] { 3, 4, 5, 6 };
        var harmonicArray = new[] { 1, 2, 7 };
        //var harmonicArray = new[] { 1, 2, 3, 7, 11, 12 };
        var magnitudeArray = new[] { 200d * sqrt2, 20d * sqrt2, -30d * sqrt2 };
        //var magnitudeArray = new[] { 199d, 23d, 101d, -31, 41d, -13d };

        var w = 31d;

        var signalComposer =
            Float64HarmonicSignalComposer.Create(
                w,
                1000,
                10
            );

        var samplingSpecs = signalComposer.GetSamplingSpecs();

        foreach (var n in phaseCountArray)
        {
            VSpaceDimensions = n;
            GeometricProcessor = XGaFloat64Processor.Euclidean;
            ScalarSignalProcessor = new ScalarProcessorOfFloat64Signal(samplingSpecs.SamplingRate, samplingSpecs.SampleCount);
            GeometricSignalProcessor = ScalarSignalProcessor.CreateEuclideanXGaProcessor();

            for (var harmonicCount = harmonicArray.Length; harmonicCount <= harmonicArray.Length; harmonicCount++)
            {
                var harmonicsListText =
                    harmonicArray.Take(harmonicCount).Concatenate(", ");

                Console.WriteLine($"{n}-Dimensions, Harmonics: {harmonicsListText}");


                var vectorSignal = GeometricSignalProcessor.VectorZero;

                for (var harmonicFactorIndex = 0; harmonicFactorIndex < harmonicCount; harmonicFactorIndex++)
                {
                    var harmonicFactor = harmonicArray[harmonicFactorIndex];
                    var magnitude = magnitudeArray[harmonicFactorIndex];

                    //var magnitudeList = 
                    //    Enumerable.Range(0, n).Select(
                    //        _ => Normal.Sample(
                    //            magnitude, 
                    //            magnitude.Abs() / 5
                    //        )
                    //    ).ToArray();

                    vectorSignal += signalComposer.GenerateOddSignalComponents(
                        magnitude,
                        harmonicFactor,
                        n
                    ).CreateXGaVector(GeometricSignalProcessor);
                }

                var interpolatorOptions = new DfFourierSignalInterpolatorOptions()
                {
                    EnergyAcThreshold = 1000d,
                    EnergyAcPercentThreshold = 0.9998d,
                    SignalToNoiseRatioThreshold = 3000d,
                    FrequencyThreshold = 1000 * 2 * Math.PI,
                    FrequencyCountThreshold = 750,
                    PaddingTrendSampleCount = 1,
                    PaddingPolynomialDegree = 6,
                    AssumePeriodic = true
                };

                var vectorSignalProcessor = new XGaGeometricFrequencyFourierProcessor(
                    VSpaceDimensions,
                    interpolatorOptions
                );

                //var vectorSignalProcessor = new AngularVelocityPolynomialSignalProcessor(
                //    GeometricProcessor,
                //    7,
                //    51
                //);

                vectorSignalProcessor.RunningAverageSampleCount =
                    (int)(SamplingRate / w);

                vectorSignalProcessor.ProcessVectorSignal(vectorSignal);

                // Angular velocity blades
                var omegaCount = vectorSignalProcessor.AngularVelocityBlades.Count;

                for (var omegaIndex = 0; omegaIndex < omegaCount; omegaIndex++)
                {
                    var omega = vectorSignalProcessor.AngularVelocityBlades[omegaIndex];
                    var omegaMean = omega.Mean();
                    var omegaMeanNorm = omegaMean.Norm().ScalarValue;
                    var omegaMeanNormHz = (omegaMeanNorm / (2 * Math.PI)).Round(4);
                    var omegaMeanNormRatioHz = (omegaMeanNormHz / w).Round(4);

                    Console.WriteLine($"Mean Omega {omegaIndex + 1}:");
                    Console.WriteLine($"   Direction: {LaTeXComposer.GetMultivectorText(omegaMean / omegaMeanNorm)}");
                    Console.WriteLine($"   Norm     : {omegaMeanNorm:G} rad/sec = {omegaMeanNormHz:G} Hz = {omegaMeanNormRatioHz:G} * {w} Hz");
                    Console.WriteLine();
                }
            }
        }
    }
        
}