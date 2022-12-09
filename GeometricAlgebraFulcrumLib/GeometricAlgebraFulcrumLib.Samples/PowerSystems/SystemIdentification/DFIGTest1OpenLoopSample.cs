using System;
using System.Collections.Generic;
using System.IO;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using OfficeOpenXml;
using OxyPlot;
using OxyPlot.Series;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.SystemIdentification;

public static class DFIGTest1OpenLoopSample
{
    private const string WorkingPath
        = @"D:\Projects\Books\The Geometric Algebra Cookbook\GAPoT\System Identification";

    // This is a pre-defined scalar processor for numeric scalars
    public static ScalarAlgebraFloat64Processor ScalarProcessor { get; }
        = ScalarAlgebraFloat64Processor.DefaultProcessor;

    public static uint VSpaceDimension
        => 3;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static GeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
        = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(VSpaceDimension);

    // This is a pre-defined text generator for displaying multivectors
    public static TextFloat64Composer TextComposer { get; }
        = TextFloat64Composer.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    public static LaTeXFloat64Composer LaTeXComposer { get; }
        = LaTeXFloat64Composer.DefaultComposer;

    public static double SamplingRate
        => 100000d;

    public static int SignalSamplesCount
        => 400001;

    public static double SignalTime
        => (SignalSamplesCount - 1) / SamplingRate;

    // This is a pre-defined scalar processor for tuples of numeric scalars
    public static ScalarSignalFloat64Processor ScalarSignalProcessor { get; }
        = ProcessorFactory.CreateFloat64ScalarSignalProcessor(SamplingRate, SignalSamplesCount);

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected tuple scalar processor
    public static GeometricAlgebraEuclideanProcessor<ScalarSignalFloat64> GeometricSignalProcessor { get; }
        = ScalarSignalProcessor.CreateGeometricAlgebraEuclideanProcessor(VSpaceDimension);

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

    private static void PlotCurve(this Scalar<ScalarSignalFloat64> curve, string title, string filePath)
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


    public static void Example1()
    {
        var inputFilePath =
            Path.Combine(WorkingPath, @"DFIG test 1 open loop.xlsx");

        var outputFilePath =
            Path.Combine(WorkingPath, @"DFIG test 1 open loop Output.xlsx");

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

        var usData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            2,
            3
        );
            
        var urData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            5,
            3
        );

        var isData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            8,
            3
        );

        var irData = GeometricSignalProcessor.ReadVectorSignal(
            SamplingRate,
            workSheet,
            firstRowIndex,
            SignalSamplesCount,
            11,
            3
        );


    }
}