using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Algebra.Signals.Interpolators;
using GeometricAlgebraFulcrumLib.Algebra.Signals.Processors;
using MathNet.Numerics;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OxyPlot;
using OxyPlot.Series;
using Float64SignalComposerUtils = GeometricAlgebraFulcrumLib.Algebra.Signals.Float64SignalComposerUtils;
using GeometricAlgebraFulcrumLib.Algebra.Utilities;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.SystemIdentification;

public static class RlCircuitPaperSample
{
    private const string WorkingPath
        = @"D:\Projects\Books\The Geometric Algebra Cookbook\Geometric Frequency\Data";

    // This is a pre-defined scalar processor for numeric scalars
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
        = ScalarProcessorOfFloat64.Instance;

    public static int VSpaceDimensions
        => 4;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;

    // This is a pre-defined text generator for displaying multivectors
    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;

    public static double SamplingRate
        => 98000d; //1000d; //10000d; 

    public static int SignalSamplesCount
        => 5000; //1001; //10001; 

    public static double SignalTime
        => (SignalSamplesCount - 1) / SamplingRate;

    // This is a pre-defined scalar processor for tuples of numeric scalars
    public static ScalarProcessorOfFloat64Signal ScalarSignalProcessor { get; }
        = Float64SignalComposerUtils.CreateFloat64ScalarSignalProcessor(SamplingRate, SignalSamplesCount);

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected tuple scalar processor
    public static RGaProcessor<Float64Signal> GeometricSignalProcessor { get; }
        = ScalarSignalProcessor.CreateEuclideanRGaProcessor();

    private static string CombineWorkingPath(this string fileName)
    {
        return Path.Combine(WorkingPath, fileName);
    }

    private static double LinearInterpolation(this IReadOnlyList<double> scalarList, double t)
    {
        var sampleIndex = SamplingRate * t;

        var i1 = Math.Max(0, (int)Math.Floor(sampleIndex));
        var i2 = Math.Min(scalarList.Count - 1, (int)Math.Ceiling(sampleIndex));

        var v1 = scalarList[i1];
        var v2 = scalarList[i2];

        t = sampleIndex - Math.Truncate(sampleIndex);

        return (1 - t) * v1 + t * v2;
    }

    private static void PlotCurve(this Scalar<Float64Signal> curve, string title, string filePath)
    {
        curve.ScalarValue.PlotCurve(title, filePath);
    }

    private static void PlotCurve(this IReadOnlyList<double> curve, string title, string filePath)
    {
        const int sampleTrim = 0;
        var tMin = sampleTrim / SamplingRate;
        var tMax = (SignalSamplesCount - 1 - sampleTrim) / SamplingRate;

        curve.PlotCurve(tMin, tMax, title, filePath);
    }

    private static void PlotCurves(IReadOnlyList<double> curve1, IReadOnlyList<double> curve2, string title, string filePath)
    {
        const int sampleTrim = 0;
        var tMin = sampleTrim / SamplingRate;
        var tMax = (SignalSamplesCount - 1 - sampleTrim) / SamplingRate;

        PlotCurves(curve1, curve2, tMin, tMax, title, filePath);
    }

    private static void PlotCurve(this IReadOnlyList<double> curve, double tMin, double tMax, string title, string filePath)
    {
        filePath = Path.Combine(WorkingPath, filePath);

        var pm = new PlotModel
        {
            Title = title,
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            curve.LinearInterpolation,
            tMin,
            tMax,
            SignalSamplesCount * 2
        );

        pm.Series.Add(s1);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + ".png", SignalSamplesCount * 2, 750, 200);
    }

    private static void PlotCurves(IReadOnlyList<double> curve1, IReadOnlyList<double> curve2, double tMin, double tMax, string title, string filePath)
    {
        filePath = Path.Combine(WorkingPath, filePath);

        var pm = new PlotModel
        {
            Title = title,
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            curve1.LinearInterpolation,
            tMin,
            tMax,
            SignalSamplesCount * 2
        );

        var s2 = new FunctionSeries(
            curve2.LinearInterpolation,
            tMin,
            tMax,
            SignalSamplesCount * 2
        );

        pm.Series.Add(s1);
        pm.Series.Add(s2);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        OxyPlot.SkiaSharp.PngExporter.Export(pm, filePath + ".png", SignalSamplesCount * 2, 750, 200);
    }


    private static Quint<Scalar<Float64Signal>> GetCurveWithDt4(this RGaVector<Float64Signal> signalSamples, IEnumerable<double> tData, RGaVectorNevilleInterpolator interpolator)
    {
        var u = 
            tData.Select(t => 
                interpolator.GetVector(signalSamples, SignalSamplesCount, t)
            ).CreateVectorSignal(GeometricSignalProcessor, SamplingRate);

        var uDt1 =
            interpolator.GetVectorsDt1(u, SignalSamplesCount);

        var uDt2 =
            interpolator.GetVectorsDt1(uDt1, SignalSamplesCount);

        var uDt3 =
            interpolator.GetVectorsDt1(uDt2, SignalSamplesCount);

        var uDt4 =
            interpolator.GetVectorsDt1(uDt3, SignalSamplesCount);

        var s = u.Scalar(0);
        var sDt1 = uDt1.Scalar(0);
        var sDt2 = uDt2.Scalar(0);
        var sDt3 = uDt3.Scalar(0);
        var sDt4 = uDt4.Scalar(0);

        //var s = u.WienerFilter1D(SignalSamplesCount, 5)[0];
        //var sDt1 = uDt1.WienerFilter1D(SignalSamplesCount, 5)[0];
        //var sDt2 = uDt2.WienerFilter1D(SignalSamplesCount, 5)[0];
        //var sDt3 = uDt3.WienerFilter1D(SignalSamplesCount, 5)[0];
        //var sDt4 = uDt4.WienerFilter1D(SignalSamplesCount, 5)[0];

        return new Quint<Scalar<Float64Signal>>(s, sDt1, sDt2, sDt3, sDt4);
    }

    private static Triplet<Scalar<Float64Signal>> GetCurveWithDt2(this IReadOnlyList<double> tData, PolynomialFunction<double> interpolator)
    {
        var u =
            interpolator.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i1 = interpolator.GetDerivative();
        var uDt1 =
            i1.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i2 = i1.GetDerivative();
        var uDt2 =
            i2.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        return new Triplet<Scalar<Float64Signal>>(u, uDt1, uDt2);
    }

    private static Quad<Scalar<Float64Signal>> GetCurveWithDt3(this IReadOnlyList<double> tData, PolynomialFunction<double> interpolator)
    {
        var u =
            interpolator.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i1 = interpolator.GetDerivative();
        var uDt1 =
            i1.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i2 = i1.GetDerivative();
        var uDt2 =
            i2.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i3 = i2.GetDerivative();
        var uDt3 =
            i3.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        return new Quad<Scalar<Float64Signal>>(u, uDt1, uDt2, uDt3);
    }

    private static Quint<Scalar<Float64Signal>> GetCurveWithDt4(this IReadOnlyList<double> tData, PolynomialFunction<double> interpolator)
    {
        var u =
            interpolator.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i1 = interpolator.GetDerivative();
        var uDt1 =
            i1.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i2 = i1.GetDerivative();
        var uDt2 =
            i2.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i3 = i2.GetDerivative();
        var uDt3 =
            i3.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        var i4 = i3.GetDerivative();
        var uDt4 =
            i4.GetValues(tData).CreateSignal(SamplingRate).ScalarFromValue(ScalarSignalProcessor);

        return new Quint<Scalar<Float64Signal>>(u, uDt1, uDt2, uDt3, uDt4);
    }

    //public static void Example1()
    //{
    //    var inputFilePath = 
    //        Path.Combine(WorkingPath, @"RLCircuit.xlsx");

    //    var outputFilePath = 
    //        Path.Combine(WorkingPath, @"RLCircuit_Output.xlsx");

    //    // Read Signal
    //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    //    using var package = new ExcelPackage(new FileInfo(inputFilePath));

    //    var workSheet = package.Workbook.Worksheets[0];

    //    const int firstRowIndex = 2;

    //    var tData = ScalarProcessor.ReadScalars(
    //        workSheet, 
    //        firstRowIndex, 
    //        SignalSamplesCount, 
    //        1
    //    );

    //    var uData = GeometricSignalProcessor.ReadVectors(
    //        workSheet, 
    //        firstRowIndex, 
    //        SignalSamplesCount, 
    //        2,
    //        1
    //    );

    //    var iData = GeometricSignalProcessor.ReadVectors(
    //        workSheet, 
    //        firstRowIndex, 
    //        SignalSamplesCount, 
    //        3,
    //        1
    //    );

    //    var rData = GeometricSignalProcessor.ReadVectors(
    //        workSheet, 
    //        firstRowIndex, 
    //        SignalSamplesCount, 
    //        4,
    //        1
    //    );

    //    var lData = GeometricSignalProcessor.ReadVectors(
    //        workSheet, 
    //        firstRowIndex, 
    //        SignalSamplesCount, 
    //        5,
    //        1
    //    );

    //    const double energyThreshold = 0.999999d;

    //    //var freqIndexSet = Enumerable.Range(0, 33).Select(i => i * 10);
    //    var uFrequencyIndexSet = VectorFourierInterpolator.GetDominantFrequencyIndexSet(
    //        uData[0].ScalarValue,
    //        energyThreshold
    //    );

    //    // For validation
    //    var df = 1d / SignalTime;

    //    Console.WriteLine("u frequencies:");
    //    foreach (var freqIndex in uFrequencyIndexSet)
    //    {
    //        var freqHz = freqIndex * df;

    //        Console.WriteLine($"Frequency Samples ({freqIndex}, {SignalSamplesCount - freqIndex}) = ±{freqHz:G} Hz");
    //        Console.WriteLine();
    //    }

    //    var uFourierInterpolator = GeometricProcessor.CreateFourierInterpolator(
    //        uData,
    //        SamplingRate,
    //        uFrequencyIndexSet
    //    );

    //    var u = 
    //        uFourierInterpolator.GetVectors(tData).Vector(GeometricSignalProcessor);

    //    var uDt1 = 
    //        uFourierInterpolator.GetVectorsDt(tData, 1).Vector(GeometricSignalProcessor);

    //    var uDt2 = 
    //        uFourierInterpolator.GetVectorsDt(tData, 2).Vector(GeometricSignalProcessor);

    //    //var uDt3 = 
    //    //    uFourierInterpolator.GetVectorsDt(tData, 3).Vector(GeometricSignalProcessor);

    //    //v = v.WienerFilter1D(SignalSamplesCount, WienerFilterOrder);
    //    //vDt1 = vDt1.WienerFilter1D(SignalSamplesCount, WienerFilterOrder);
    //    //vDt2 = vDt2.WienerFilter1D(SignalSamplesCount, WienerFilterOrder);
    //    //vDt3 = vDt3.WienerFilter1D(SignalSamplesCount, WienerFilterOrder);

    //    var iFrequencyIndexSet = VectorFourierInterpolator.GetDominantFrequencyIndexSet(
    //        iData[0].ScalarValue,
    //        energyThreshold
    //    );

    //    // For validation
    //    Console.WriteLine("i frequencies:");
    //    foreach (var freqIndex in iFrequencyIndexSet)
    //    {
    //        var freqHz = freqIndex * df;

    //        Console.WriteLine($"Frequency Samples ({freqIndex}, {SignalSamplesCount - freqIndex}) = ±{freqHz:G} Hz");
    //        Console.WriteLine();
    //    }

    //    var iFourierInterpolator = GeometricProcessor.CreateFourierInterpolator(
    //        iData,
    //        SamplingRate,
    //        iFrequencyIndexSet
    //    );

    //    var i = 
    //        iFourierInterpolator.GetVectors(tData).Vector(GeometricSignalProcessor);

    //    var iDt1 = 
    //        iFourierInterpolator.GetVectorsDt(tData, 1).Vector(GeometricSignalProcessor);

    //    var iDt2 = 
    //        iFourierInterpolator.GetVectorsDt(tData, 2).Vector(GeometricSignalProcessor);

    //    var iDt3 = 
    //        iFourierInterpolator.GetVectorsDt(tData, 3).Vector(GeometricSignalProcessor);


    //    var r1 = 
    //        (uDt1.Sp(iDt3) - iDt2.Sp(uDt2)) / 
    //        (iDt1.Sp(iDt3) - iDt2.Sp(iDt2));

    //    var r2 = 
    //        (uData.Sp(iDt2) - iDt1.Sp(uDt1)) / 
    //        (iData.Sp(iDt2) - iDt1.Sp(iDt1));

    //    var l1 = 
    //        (iDt1.Sp(uDt2) - uDt1.Sp(iDt2)) / 
    //        (iDt1.Sp(iDt3) - iDt2.Sp(iDt2));

    //    var l2 = 
    //        (iData.Sp(uDt1) - uData.Sp(iDt1)) / 
    //        (iData.Sp(iDt2) - iDt1.Sp(iDt1));

    //    var columnIndex = 6;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, uData[0], "uData");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, u[0], "u");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, uDt1[0], "u'");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, uDt2[0], "u''");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, iData[0], "iData");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, i[0], "i");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, iDt1[0], "i'");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, iDt2[0], "i''");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, iDt3[0], "i'''");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, r1, "R1");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, l1, "L1");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, r2, "R2");
    //    columnIndex += 1;

    //    workSheet.WriteScalars(firstRowIndex - 1, columnIndex, l2, "L2");
    //    columnIndex += 1;

    //    package.SaveAs(outputFilePath);

    //    PlotCurve(uData[0].ScalarValue, "u", "uData");
    //    PlotCurve(iData[0].ScalarValue, "i", "iData");

    //    PlotCurve(uDt1[0].ScalarValue, "u'", "uDt1");
    //    PlotCurve(uDt2[0].ScalarValue, "u''", "uDt2");

    //    PlotCurve(iDt1[0].ScalarValue, "i'", "iDt1");
    //    PlotCurve(iDt2[0].ScalarValue, "i''", "iDt2");
    //    PlotCurve(iDt3[0].ScalarValue, "i'''", "iDt3");

    //    PlotCurve(rData[0].ScalarValue, "R", "R");
    //    PlotCurve(lData[0].ScalarValue, "L", "L");

    //    PlotCurve(r1.ScalarValue, "R", "R1");
    //    PlotCurve(l1.ScalarValue, "L", "L1");

    //    PlotCurve(r2.ScalarValue, "R", "R2");
    //    PlotCurve(l2.ScalarValue, "L", "L2");
    //}

    public static void Example1()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"RLCircuit.xlsx");

        var outputFilePath =
            Path.Combine(WorkingPath, @"RLCircuit_Output.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRowIndex = 2;

        var tData = ScalarProcessor.ReadScalarColumn(
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            1
        );

        var uData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            2,
            1
        );

        var iData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            3,
            1
        );

        var rData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            4,
            1
        );

        var lData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            5,
            1
        );


        var interpolationOptions = new DfFourierSignalInterpolatorOptions()
        {
            EnergyAcThreshold = 10d,
            EnergyAcPercentThreshold = 1d,
            SignalToNoiseRatioThreshold = 600000d,
            FrequencyThreshold = double.PositiveInfinity,//400 * 2 * Math.PI,
            FrequencyCountThreshold = 5000,
            PaddingTrendSampleCount = 50,
            PaddingPolynomialDegree = 6
        };

        var vectorSignalProcessor = new RGaGeometricFrequencyFourierProcessor(
            VSpaceDimensions,
            interpolationOptions
        );

        //var vectorSignalProcessor = new GeometricFrequencyPolynomialProcessor(
        //    GeometricProcessor,
        //    7,
        //    51
        //);

        vectorSignalProcessor.ProcessVectorSignal(uData);
        var u = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var uDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var uDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);

        vectorSignalProcessor.ProcessVectorSignal(iData);
        var i = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var iDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var iDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);
        var iDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2].Scalar(0);

        var tValues = u.ScalarValue.GetSampledTimeSignal();
        var tMin = tValues[0];
        var tMax = tValues[^1];


        var r1 =
            (uDt1 * iDt3 - iDt2 * uDt2) /
            (iDt1 * iDt3 - iDt2 * iDt2);

        var r2 =
            (u * iDt2 - iDt1 * uDt1) /
            (i * iDt2 - iDt1 * iDt1);

        var l1 =
            (iDt1 * uDt2 - uDt1 * iDt2) /
            (iDt1 * iDt3 - iDt2 * iDt2);

        var l2 =
            (i * uDt1 - u * iDt1) /
            (i * iDt2 - iDt1 * iDt1);

        var columnIndex = 6;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, uData.Scalar(0), "uData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, u, "u");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, uDt1, "u'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, uDt2, "u''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iData.Scalar(0), "iData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, i, "i");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iDt1, "i'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iDt2, "i''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iDt3, "i'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, r1, "R1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, l1, "L1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, r2, "R2");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, l2, "L2");
        columnIndex += 1;

        package.SaveAs(outputFilePath);

        uData.Scalar(0).PlotSignal(u, tMin, tMax, "u".CombineWorkingPath());
        iData.Scalar(0).PlotSignal(i, tMin, tMax, "i".CombineWorkingPath());

        //uData[0].PlotCurve("u", "uData");
        //iData[0].PlotCurve("i", "iData");

        uDt1.PlotCurve("u'", "uDt1");
        uDt2.PlotCurve("u''", "uDt2");

        iDt1.PlotCurve("i'", "iDt1");
        iDt2.PlotCurve("i''", "iDt2");
        iDt3.PlotCurve("i'''", "iDt3");

        rData.Scalar(0).PlotCurve("R", "R");
        lData.Scalar(0).PlotCurve("L", "L");

        r1.PlotCurve("R", "R1");
        l1.PlotCurve("L", "L1");

        r2.PlotCurve("R", "R2");
        l2.PlotCurve("L", "L2");
    }

    public static void Example2()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"DCmotor.xlsx");

        var outputFilePath =
            Path.Combine(WorkingPath, @"DCmotor_Output.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRowIndex = 2;

        var tData = ScalarProcessor.ReadScalarColumn(
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            1
        );

        var uData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            8,
            1
        );

        var iData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            9,
            1
        );

        var wData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            10,
            1
        );

        var uPolynomial = PolynomialFunction<double>.Create(
            ScalarProcessor,
            Fit.Polynomial(tData, uData.Scalar(0).ScalarValue.ToArray(), 19)
        );

        var iPolynomial = PolynomialFunction<double>.Create(
            ScalarProcessor,
            Fit.Polynomial(tData, iData.Scalar(0).ScalarValue.ToArray(), 19)
        );

        var wPolynomial = PolynomialFunction<double>.Create(
            ScalarProcessor,
            Fit.Polynomial(tData, wData.Scalar(0).ScalarValue.ToArray(), 19)
        );

        var (u, uDt1, uDt2, uDt3, uDt4) =
            tData.GetCurveWithDt4(uPolynomial);

        var (i, iDt1, iDt2, iDt3, iDt4) =
            tData.GetCurveWithDt4(iPolynomial);

        var (w, wDt1, wDt2, wDt3, wDt4) =
            tData.GetCurveWithDt4(wPolynomial);


        var k1 =
            iDt3 * (iDt2 * uDt2 - uDt1 * iDt3) -
            iDt4 * (iDt1 * uDt2 - uDt1 * iDt2) +
            uDt3 * (iDt1 * iDt3 - iDt2 * iDt2);

        var r1 =
            iDt4 * (wDt1 * uDt2 - uDt1 * wDt2) -
            wDt3 * (iDt2 * uDt2 - uDt1 * iDt3) +
            uDt3 * (iDt2 * wDt2 - wDt1 * iDt3);

        var l1 =
            iDt3 * (wDt1 * uDt2 - uDt1 * wDt2) -
            wDt3 * (iDt1 * uDt2 - uDt1 * iDt2) +
            uDt3 * (iDt1 * wDt2 - wDt1 * iDt2);

        var d =
            iDt3 * (iDt2 * wDt2 - wDt1 * iDt3) -
            iDt4 * (iDt1 * wDt2 - wDt1 * iDt2) +
            wDt3 * (iDt1 * iDt3 - iDt2 * iDt2);

        //k1 = k1.WienerFilter1D(5);
        //r1 = r1.WienerFilter1D(5);
        //l1 = l1.WienerFilter1D(5);
        //d = d.WienerFilter1D(5);

        var k = k1 / d;
        var r = r1 / d;
        var l = -l1 / d;

        var j =
            k * (wDt1 * iDt2 - iDt1 * wDt2) / (wDt1 * wDt3 - wDt2 * wDt2);

        var b =
            k * (wDt2 * iDt2 - iDt1 * wDt3) / (wDt1 * wDt3 - wDt2 * wDt2);

        var columnIndex = 11;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uData.Scalar(0), "uData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, u, "u");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt1, "u'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt2, "u''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt3, "u'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt4, "u''''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iData.Scalar(0), "iData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, i, "i");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt1, "i'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt2, "i''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt3, "i'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt4, "i''''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wData.Scalar(0), "wData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, w, "w");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt1, "w'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt2, "w''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt3, "w'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt4, "w''''");
        columnIndex += 1;

        //workSheet.WriteScalars(firstRowIndex + 1, columnIndex, d, "d");
        //columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, r, "R");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, l, "L");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, k, "K");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, j, "J");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, b, "b");
        columnIndex += 1;

        package.SaveAs(outputFilePath);

        uData.Scalar(0).ScalarValue.PlotCurve("u", "uData");
        iData.Scalar(0).ScalarValue.PlotCurve("i", "iData");
        wData.Scalar(0).ScalarValue.PlotCurve("w", "wData");

        u.ScalarValue.PlotCurve("u", "u");
        uDt1.ScalarValue.PlotCurve("u'", "uDt1");
        uDt2.ScalarValue.PlotCurve("u''", "uDt2");
        uDt3.ScalarValue.PlotCurve("u'''", "uDt3");
        uDt4.ScalarValue.PlotCurve("u''''", "uDt4");

        i.ScalarValue.PlotCurve("i", "i");
        iDt1.ScalarValue.PlotCurve("i'", "iDt1");
        iDt2.ScalarValue.PlotCurve("i''", "iDt2");
        iDt3.ScalarValue.PlotCurve("i'''", "iDt3");
        iDt4.ScalarValue.PlotCurve("i''''", "iDt4");

        w.ScalarValue.PlotCurve("w", "w");
        wDt1.ScalarValue.PlotCurve("w'", "wDt1");
        wDt2.ScalarValue.PlotCurve("w''", "wDt2");
        wDt3.ScalarValue.PlotCurve("w'''", "wDt3");
        wDt4.ScalarValue.PlotCurve("w''''", "wDt4");

        d.ScalarValue.PlotCurve("d", "d");
        k1.ScalarValue.PlotCurve("k1", "k1");
        r1.ScalarValue.PlotCurve("r1", "r1");
        l1.ScalarValue.PlotCurve("l1", "l1");

        //PlotCurve(k1.ScalarValue, 0.005, 0.158, "k1", "k1_part1");
        //PlotCurve(k1.ScalarValue, 0.0158, 0.3, "k1", "k1_part2");
        //PlotCurve(k1.ScalarValue, 0.3, 0.998, "k1", "k1_part3");

        r.ScalarValue.PlotCurve("R", "R");
        l.ScalarValue.PlotCurve("L", "L");

        k.ScalarValue.PlotCurve("K", "K");
        j.ScalarValue.PlotCurve("J", "J");
        b.ScalarValue.PlotCurve("b", "b");
    }

    public static void Example3()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"DCmotor.xlsx");

        var outputFilePath =
            Path.Combine(WorkingPath, @"DCmotor_Output.xlsx");

        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(new FileInfo(inputFilePath));

        var workSheet = package.Workbook.Worksheets[0];

        const int firstRowIndex = 2;

        var tData = ScalarProcessor.ReadScalarColumn(
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            1
        );

        var uData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            8,
            1
        );

        var iData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            9,
            1
        );

        var wData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            10,
            1
        );


        //var interpolationOptions = new SpectrumInterpolationOptions()
        //{
        //    EnergyAcThreshold = 10d,
        //    EnergyAcPercentThreshold = 1d,
        //    SignalToNoiseRatioThreshold = 600000d,
        //    FrequencyThreshold = double.PositiveInfinity,//400 * 2 * Math.PI,
        //    FrequencyCountThreshold = 5000,
        //    PaddingPolynomialSampleCount = 50,
        //    PaddingPolynomialDegree = 6
        //};

        //var vectorSignalProcessor = new GeometricFrequencyFourierProcessor(
        //    GeometricProcessor,
        //    interpolationOptions
        //);

        var vectorSignalProcessor = new RGaGeometricFrequencyPolynomialProcessor(
            VSpaceDimensions,
            7,
            51
        );

        vectorSignalProcessor.ProcessVectorSignal(uData);
        var u = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var uDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var uDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);
        var uDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2].Scalar(0);
        var uDt4 = vectorSignalProcessor.VectorSignalTimeDerivatives[3].Scalar(0);

        //Console.WriteLine("u Data");
        //Console.WriteLine(
        //    vectorSignalProcessor.VectorSignalSpectrum.GetTextDescription(uData)
        //);
        //Console.WriteLine();

        //vectorSignalProcessor = new GeometricFrequencyFourierProcessor(
        //    GeometricProcessor,
        //    interpolationOptions
        //);

        vectorSignalProcessor = new RGaGeometricFrequencyPolynomialProcessor(
            VSpaceDimensions,
            7,
            51
        );

        vectorSignalProcessor.ProcessVectorSignal(iData);
        var i = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var iDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var iDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);
        var iDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2].Scalar(0);
        var iDt4 = vectorSignalProcessor.VectorSignalTimeDerivatives[3].Scalar(0);

        //Console.WriteLine("i Data");
        //Console.WriteLine(
        //    vectorSignalProcessor.VectorSignalSpectrum.GetTextDescription(iData)
        //);
        //Console.WriteLine();

        //vectorSignalProcessor = new GeometricFrequencyFourierProcessor(
        //    GeometricProcessor,
        //    interpolationOptions
        //);

        vectorSignalProcessor = new RGaGeometricFrequencyPolynomialProcessor(
            VSpaceDimensions,
            7,
            51
        );

        vectorSignalProcessor.ProcessVectorSignal(wData);
        var w = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var wDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var wDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);
        var wDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2].Scalar(0);
        var wDt4 = vectorSignalProcessor.VectorSignalTimeDerivatives[3].Scalar(0);

        //Console.WriteLine("w Data");
        //Console.WriteLine(
        //    vectorSignalProcessor.VectorSignalSpectrum.GetTextDescription(wData)
        //);
        //Console.WriteLine();

        var k1 =
            iDt3 * (iDt2 * uDt2 - uDt1 * iDt3) -
            iDt4 * (iDt1 * uDt2 - uDt1 * iDt2) +
            uDt3 * (iDt1 * iDt3 - iDt2 * iDt2);

        var r1 =
            iDt4 * (wDt1 * uDt2 - uDt1 * wDt2) -
            wDt3 * (iDt2 * uDt2 - uDt1 * iDt3) +
            uDt3 * (iDt2 * wDt2 - wDt1 * iDt3);

        var l1 =
            iDt3 * (wDt1 * uDt2 - uDt1 * wDt2) -
            wDt3 * (iDt1 * uDt2 - uDt1 * iDt2) +
            uDt3 * (iDt1 * wDt2 - wDt1 * iDt2);

        var d =
            iDt3 * (iDt2 * wDt2 - wDt1 * iDt3) -
            iDt4 * (iDt1 * wDt2 - wDt1 * iDt2) +
            wDt3 * (iDt1 * iDt3 - iDt2 * iDt2);

        //k1 = k1.WienerFilter1D(5);
        //r1 = r1.WienerFilter1D(5);
        //l1 = l1.WienerFilter1D(5);
        //d = d.WienerFilter1D(5);

        var k = k1 / d;
        var r = r1 / d;
        var l = -l1 / d;

        var j =
            k * (wDt1 * iDt2 - iDt1 * wDt2) / (wDt1 * wDt3 - wDt2 * wDt2);

        var b =
            k * (wDt2 * iDt2 - iDt1 * wDt3) / (wDt1 * wDt3 - wDt2 * wDt2);

        var columnIndex = 11;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uData.Scalar(0), "uData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, u, "u");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt1, "u'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt2, "u''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt3, "u'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, uDt4, "u''''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iData.Scalar(0), "iData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, i, "i");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt1, "i'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt2, "i''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt3, "i'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, iDt4, "i''''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wData.Scalar(0), "wData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, w, "w");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt1, "w'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt2, "w''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt3, "w'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, wDt4, "w''''");
        columnIndex += 1;

        //workSheet.WriteScalars(firstRowIndex + 1, columnIndex, d, "d");
        //columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, r, "R");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, l, "L");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, k, "K");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, j, "J");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex + 1, columnIndex, b, "b");
        columnIndex += 1;

        package.SaveAs(outputFilePath);

        uData.Scalar(0).ScalarValue.PlotCurve("u", "uData");
        iData.Scalar(0).ScalarValue.PlotCurve("i", "iData");
        wData.Scalar(0).ScalarValue.PlotCurve("w", "wData");

        u.ScalarValue.PlotCurve("u", "u");
        uDt1.ScalarValue.PlotCurve("u'", "uDt1");
        uDt2.ScalarValue.PlotCurve("u''", "uDt2");
        uDt3.ScalarValue.PlotCurve("u'''", "uDt3");
        uDt4.ScalarValue.PlotCurve("u''''", "uDt4");

        i.ScalarValue.PlotCurve("i", "i");
        iDt1.ScalarValue.PlotCurve("i'", "iDt1");
        iDt2.ScalarValue.PlotCurve("i''", "iDt2");
        iDt3.ScalarValue.PlotCurve("i'''", "iDt3");
        iDt4.ScalarValue.PlotCurve("i''''", "iDt4");

        w.ScalarValue.PlotCurve("w", "w");
        wDt1.ScalarValue.PlotCurve("w'", "wDt1");
        wDt2.ScalarValue.PlotCurve("w''", "wDt2");
        wDt3.ScalarValue.PlotCurve("w'''", "wDt3");
        wDt4.ScalarValue.PlotCurve("w''''", "wDt4");

        d.ScalarValue.PlotCurve("d", "d");
        k1.ScalarValue.PlotCurve("k1", "k1");
        r1.ScalarValue.PlotCurve("r1", "r1");
        l1.ScalarValue.PlotCurve("l1", "l1");

        //PlotCurve(k1.ScalarValue, 0.005, 0.158, "k1", "k1_part1");
        //PlotCurve(k1.ScalarValue, 0.0158, 0.3, "k1", "k1_part2");
        //PlotCurve(k1.ScalarValue, 0.3, 0.998, "k1", "k1_part3");

        r.ScalarValue.PlotCurve("R", "R");
        l.ScalarValue.PlotCurve("L", "L");

        k.ScalarValue.PlotCurve("K", "K");
        j.ScalarValue.PlotCurve("J", "J");
        b.ScalarValue.PlotCurve("b", "b");
    }

    public static void Example4()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"Values.json");

        var outputFilePath =
            Path.Combine(WorkingPath, @"Values (98kHz)_Output.xlsx");

        // Read Signal
        var jsonString = File.ReadAllText(inputFilePath);

        var jsonArray = JArray.Parse(jsonString);

        var tData = new double[SignalSamplesCount];
        var uData = new double[SignalSamplesCount];
        var iData = new double[SignalSamplesCount];

        for (var k = 0; k < SignalSamplesCount; k++)
        {
            var list = jsonArray[k];

            tData[k] = k / SamplingRate;
            iData[k] = list.Value<double>(0);
            uData[k] = list.Value<double>(1);
        }

        var uSignal =
            GeometricSignalProcessor.Vector(
                new[] { uData.CreateSignal(SamplingRate) }
            );

        var iSignal =
            GeometricSignalProcessor.Vector(
                new[] { iData.CreateSignal(SamplingRate) }
            );


        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();

        var workSheet = package.Workbook.Worksheets.Add("Sheet1");

        const int firstRowIndex = 3;

        var interpolationOptions = new DfFourierSignalInterpolatorOptions()
        {
            EnergyAcThreshold = 10d,
            EnergyAcPercentThreshold = 0.99995d,
            SignalToNoiseRatioThreshold = 100000d,
            FrequencyThreshold = double.PositiveInfinity, //200 * 2 * Math.PI,
            FrequencyCountThreshold = 500,
            PaddingTrendSampleCount = 50,
            PaddingPolynomialDegree = 6
        };

        var vectorSignalProcessor = new RGaGeometricFrequencyFourierProcessor(
            VSpaceDimensions,
            interpolationOptions
        );

        //var vectorSignalProcessor = new GeometricFrequencyPolynomialProcessor(
        //    GeometricProcessor,
        //    7,
        //    51
        //);

        vectorSignalProcessor.ProcessVectorSignal(uSignal);
        var u = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var uDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var uDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);

        vectorSignalProcessor.ProcessVectorSignal(iSignal);
        var i = vectorSignalProcessor.VectorSignalInterpolated.Scalar(0);
        var iDt1 = vectorSignalProcessor.VectorSignalTimeDerivatives[0].Scalar(0);
        var iDt2 = vectorSignalProcessor.VectorSignalTimeDerivatives[1].Scalar(0);
        var iDt3 = vectorSignalProcessor.VectorSignalTimeDerivatives[2].Scalar(0);

        var tValues = u.ScalarValue.GetSampledTimeSignal();
        var tMin = tValues[0];
        var tMax = tValues[^1];


        var r1 =
            (uDt1 * iDt3 - iDt2 * uDt2) /
            (iDt1 * iDt3 - iDt2 * iDt2);

        var r2 =
            (u * iDt2 - iDt1 * uDt1) /
            (i * iDt2 - iDt1 * iDt1);

        var l1 =
            (iDt1 * uDt2 - uDt1 * iDt2) /
            (iDt1 * iDt3 - iDt2 * iDt2);

        var l2 =
            (i * uDt1 - u * iDt1) /
            (i * iDt2 - iDt1 * iDt1);

        var columnIndex = 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, uSignal.Scalar(0), "uData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, u, "u");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, uDt1, "u'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, uDt2, "u''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iSignal.Scalar(0), "iData");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, i, "i");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iDt1, "i'");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iDt2, "i''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, iDt3, "i'''");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, r1, "R1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, l1, "L1");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, r2, "R2");
        columnIndex += 1;

        workSheet.WriteScalarSignal(firstRowIndex, columnIndex, l2, "L2");
        columnIndex += 1;

        package.SaveAs(outputFilePath);

        uSignal.Scalar(0).PlotSignal(u, tMin, tMax, "u".CombineWorkingPath());
        iSignal.Scalar(0).PlotSignal(i, tMin, tMax, "i".CombineWorkingPath());

        //uData[0].PlotCurve("u", "uData");
        //iData[0].PlotCurve("i", "iData");

        uDt1.PlotCurve("u'", "uDt1");
        uDt2.PlotCurve("u''", "uDt2");

        iDt1.PlotCurve("i'", "iDt1");
        iDt2.PlotCurve("i''", "iDt2");
        iDt3.PlotCurve("i'''", "iDt3");

        //PlotCurve(rData[0], "R", "R");
        //PlotCurve(lData[0], "L", "L");

        r1.PlotCurve("R", "R1");
        l1.PlotCurve("L", "L1");

        r2.PlotCurve("R", "R2");
        l2.PlotCurve("L", "L2");

        package.SaveAs(outputFilePath);
    }
}