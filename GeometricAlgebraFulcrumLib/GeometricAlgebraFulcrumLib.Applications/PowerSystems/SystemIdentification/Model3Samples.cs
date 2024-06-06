using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Algebra.Utilities;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;
using OfficeOpenXml;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.SystemIdentification;

public static class Model3Samples
{
    private const string WorkingPath
        = @"D:\Projects\Books\The Geometric Algebra Cookbook\GAPoT\System Identification\Model 3";

    private static string FileName = "";
    
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
        = ScalarProcessorOfFloat64.Instance;

    public static int VSpaceDimensions
        => 5;

    public static RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;

    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;

    public static Float64SignalSamplingSpecs SamplingSpecs { get; }
        = new Float64SignalSamplingSpecs(100002, 1000000);

    public static int SampleStep
        => 1;

    public static double SamplingRate
        => SamplingSpecs.SamplingRate;

    public static int SignalSamplesCount
        => SamplingSpecs.SampleCount;


    public static ScalarProcessorOfFloat64Signal ScalarSignalProcessor { get; }
        = Float64SignalComposerUtils.CreateFloat64ScalarSignalProcessor(
            SamplingRate,
            SignalSamplesCount
        );

    public static RGaEuclideanProcessor<Float64Signal> GeometricSignalProcessor { get; }
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
    
    
    private static void ParallelRL0(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc, Float64Signal vIt1)
    {
        const string modelName = "Parallel - RL 0";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1) = 
            tData.SampleFunctionDerivatives1(vFunc);
        
        var (iDt0, iDt1) = 
            tData.SampleFunctionDerivatives1(iFunc);
        
        var g = 
            (vDt0 * iDt0 - vIt1 * iDt1) / (vDt0 * vDt0 - vIt1 * vDt1);

        var gamma = 
            (vDt0 * iDt1 - vDt1 * iDt0) / (vDt0 * vDt0 - vIt1 * vDt1);

        var avgFactors = new[] { 3, 5, 7, 9 };
        var gAvg = g.GetRunningAverageSignal(avgFactors);
        var gammaAvg = gamma.GetRunningAverageSignal(avgFactors);

        vIt1.PlotScalarSignal("vIt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vIt1.png"));
        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        
        gAvg.PlotScalarSignal("G", Path.Combine(WorkingPath, modelName, $"{FileName} - G.png"));
        gammaAvg.PlotScalarSignal("Gamma", Path.Combine(WorkingPath, modelName, $"{FileName} - Gamma.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vIt1, "v~");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        
        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");

        workSheet.WriteScalarSignal(3, columnIndex++, gAvg, "G");
        workSheet.WriteScalarSignal(3, columnIndex++, gammaAvg, "Gamma");
        
        package.SaveAs(outputFilePath);
    }
    
    private static void ParallelRL1(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Parallel - RL 1";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2) = 
            tData.SampleFunctionDerivatives2(vFunc);
        
        var (iDt0, iDt1, iDt2) = 
            tData.SampleFunctionDerivatives2(iFunc);
    
        var g = 
            (vDt1 * iDt1 - vDt0 * iDt2) / (vDt1 * vDt1 - vDt0 * vDt2);

        var gamma = 
            (vDt1 * iDt2 - vDt2 * iDt1) / (vDt1 * vDt1 - vDt0 * vDt2);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var gAvg = g.GetRunningAverageSignal(avgFactors);
        var gammaAvg = gamma.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        //vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        //iDt4.PlotScalarSignal("iDt4", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt4.png"));
        
        gAvg.PlotScalarSignal("G", Path.Combine(WorkingPath, modelName, $"{FileName} - G.png"));
        gammaAvg.PlotScalarSignal("Gamma", Path.Combine(WorkingPath, modelName, $"{FileName} - Gamma.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        //workSheet.WriteScalarSignal(3, columnIndex++, iDt4, "i1''''");

        workSheet.WriteScalarSignal(3, columnIndex++, gAvg, "G");
        workSheet.WriteScalarSignal(3, columnIndex++, gammaAvg, "Gamma");
        
        package.SaveAs(outputFilePath);
    }
    

    private static void ParallelRC0(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Parallel - RC 0";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2) = 
            tData.SampleFunctionDerivatives2(vFunc);
        
        var (iDt0, iDt1) = 
            tData.SampleFunctionDerivatives1(iFunc);
    
        var c = 
            (vDt0 * iDt1 - iDt0 * vDt1) / (vDt0 * vDt2 - vDt1 * vDt1);

        var g = 
            (iDt0 * vDt2 - vDt1 * iDt1) / (vDt0 * vDt2 - vDt1 * vDt1);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var cAvg = c.GetRunningAverageSignal(avgFactors);
        var gAvg = g.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        //vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        //iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        //iDt4.PlotScalarSignal("iDt4", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt4.png"));
        
        cAvg.PlotScalarSignal("C", Path.Combine(WorkingPath, modelName, $"{FileName} - C.png"));
        gAvg.PlotScalarSignal("G", Path.Combine(WorkingPath, modelName, $"{FileName} - G.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        //workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        //workSheet.WriteScalarSignal(3, columnIndex++, iDt4, "i''''");

        workSheet.WriteScalarSignal(3, columnIndex++, cAvg, "C");
        workSheet.WriteScalarSignal(3, columnIndex++, gAvg, "G");
        
        package.SaveAs(outputFilePath);
    }
    
    private static void ParallelRC1(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Parallel - RC 1";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2, vDt3) = 
            tData.SampleFunctionDerivatives3(vFunc);
        
        var (iDt0, iDt1, iDt2) = 
            tData.SampleFunctionDerivatives2(iFunc);
    
        var c = 
            (vDt1 * iDt2 - iDt1 * vDt2) / (vDt1 * vDt3 - vDt2 * vDt2);

        var g = 
            (iDt1 * vDt3 - vDt2 * iDt2) / (vDt1 * vDt3 - vDt2 * vDt2);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var cAvg = c.GetRunningAverageSignal(avgFactors);
        var gAvg = g.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        //iDt4.PlotScalarSignal("iDt4", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt4.png"));
        
        cAvg.PlotScalarSignal("C", Path.Combine(WorkingPath, modelName, $"{FileName} - C.png"));
        gAvg.PlotScalarSignal("G", Path.Combine(WorkingPath, modelName, $"{FileName} - G.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        //workSheet.WriteScalarSignal(3, columnIndex++, iDt4, "i1''''");

        workSheet.WriteScalarSignal(3, columnIndex++, cAvg, "C");
        workSheet.WriteScalarSignal(3, columnIndex++, gAvg, "G");
        
        package.SaveAs(outputFilePath);
    }
    
    
    private static void ParallelRLC0(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc, Float64Signal vIt1)
    {
        const string modelName = "Parallel - RLC 0";
        
        Console.WriteLine("   Computing Model: " + modelName);

        var (vDt0, vDt1, vDt2, vDt3) = 
            tData.SampleFunctionDerivatives3(vFunc);
        
        var (iDt0, iDt1, iDt2) = 
            tData.SampleFunctionDerivatives2(iFunc);

        var d = 
            vDt3 * (vDt0 * vDt0 - vDt1 * vIt1) -
            vDt1 * (vDt0 * vDt2 - vDt1 * vDt1) +
            vDt2 * (vIt1 * vDt2 - vDt0 * vDt1);

        var g = (
            iDt2 * (vIt1 * vDt2 - vDt0 * vDt1) -
            vDt3 * (vIt1 * iDt1 - vDt0 * iDt0) +
            vDt1 * (vDt1 * iDt1 - vDt2 * iDt0)
        ) / d;

        var gamma = -(
            iDt2 * (vDt0 * vDt2 - vDt1 * vDt1) -
            vDt3 * (vDt0 * iDt1 - vDt1 * iDt0) +
            vDt2 * (vDt1 * iDt1 - vDt2 * iDt0)
        ) / d;

        var c = (
            iDt2 * (vDt0 * vDt0 - vDt1 * vIt1) -
            vDt1 * (vDt0 * iDt1 - vDt1 * iDt0) +
            vDt2 * (vIt1 * iDt1 - vDt0 * iDt0)
        ) / d;
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var gAvg = g.GetRunningAverageSignal(avgFactors);
        var gammaAvg = gamma.GetRunningAverageSignal(avgFactors);
        var cAvg = c.GetRunningAverageSignal(avgFactors);

        vIt1.PlotScalarSignal("vIt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vIt1.png"));
        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        
        gAvg.PlotScalarSignal("G", Path.Combine(WorkingPath, modelName, $"{FileName} - G.png"));
        gammaAvg.PlotScalarSignal("Gamma", Path.Combine(WorkingPath, modelName, $"{FileName} - Gamma.png"));
        cAvg.PlotScalarSignal("C", Path.Combine(WorkingPath, modelName, $"{FileName} - C.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vIt1, "v~");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");
        
        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        
        workSheet.WriteScalarSignal(3, columnIndex++, gAvg, "G");
        workSheet.WriteScalarSignal(3, columnIndex++, gammaAvg, "Gamma");
        workSheet.WriteScalarSignal(3, columnIndex++, cAvg, "C");

        package.SaveAs(outputFilePath);
    }
    
    private static void ParallelRLC1(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Parallel - RLC 1";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2, vDt3, vDt4) = 
            tData.SampleFunctionDerivatives4(vFunc);
        
        var (iDt0, iDt1, iDt2, iDt3) = 
            tData.SampleFunctionDerivatives3(iFunc);

        var d = 
            vDt4 * (vDt1 * vDt1 - vDt2 * vDt0) -
            vDt2 * (vDt1 * vDt3 - vDt2 * vDt2) +
            vDt3 * (vDt0 * vDt3 - vDt1 * vDt2);

        var g = (
            iDt3 * (vDt0 * vDt3 - vDt1 * vDt2) -
            vDt4 * (vDt0 * iDt2 - vDt1 * iDt1) +
            vDt2 * (vDt2 * iDt2 - vDt3 * iDt1)
        ) / d;

        var gamma = -(
            iDt3 * (vDt1 * vDt3 - vDt2 * vDt2) -
            vDt4 * (vDt1 * iDt2 - vDt2 * iDt1) +
            vDt3 * (vDt2 * iDt2 - vDt3 * iDt1)
        ) / d;

        var c = (
            iDt3 * (vDt1 * vDt1 - vDt2 * vDt0) -
            vDt2 * (vDt1 * iDt2 - vDt2 * iDt1) +
            vDt3 * (vDt0 * iDt2 - vDt1 * iDt1)
        ) / d;
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var gAvg = g.GetRunningAverageSignal(avgFactors);
        var gammaAvg = gamma.GetRunningAverageSignal(avgFactors);
        var cAvg = c.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
        vDt4.PlotScalarSignal("vDt4", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt4.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt3.png"));
        
        gAvg.PlotScalarSignal("G", Path.Combine(WorkingPath, modelName, $"{FileName} - G.png"));
        gammaAvg.PlotScalarSignal("Gamma", Path.Combine(WorkingPath, modelName, $"{FileName} - Gamma.png"));
        cAvg.PlotScalarSignal("C", Path.Combine(WorkingPath, modelName, $"{FileName} - C.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt4, "v''''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i'''");

        workSheet.WriteScalarSignal(3, columnIndex++, gAvg, "G");
        workSheet.WriteScalarSignal(3, columnIndex++, gammaAvg, "Gamma");
        workSheet.WriteScalarSignal(3, columnIndex++, cAvg, "C");

        package.SaveAs(outputFilePath);
    }
    

    private static void SeriesRL0(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Series - RL 0";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1) = 
            tData.SampleFunctionDerivatives1(vFunc);
        
        var (iDt0, iDt1, iDt2) = 
            tData.SampleFunctionDerivatives2(iFunc);
    
        var r = 
            (vDt0 * iDt2 - iDt1 * vDt1) / (iDt0 * iDt2 - iDt1 * iDt1);

        var l = 
            (iDt0 * vDt1 - vDt0 * iDt1) / (iDt0 * iDt2 - iDt1 * iDt1);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var rAvg = r.GetRunningAverageSignal(avgFactors);
        var lAvg = l.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        //vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        //vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        //iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt3.png"));
        
        rAvg.PlotScalarSignal("R", Path.Combine(WorkingPath, modelName, $"{FileName} - R.png"));
        lAvg.PlotScalarSignal("L", Path.Combine(WorkingPath, modelName, $"{FileName} - L.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        //workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        //workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i'''");

        workSheet.WriteScalarSignal(3, columnIndex++, rAvg, "R");
        workSheet.WriteScalarSignal(3, columnIndex++, lAvg, "L");
        
        package.SaveAs(outputFilePath);
    }
    
    private static void SeriesRL1(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Series - RL 1";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2) = 
            tData.SampleFunctionDerivatives2(vFunc);
        
        var (iDt0, iDt1, iDt2, iDt3) = 
            tData.SampleFunctionDerivatives3(iFunc);
    
        var r = 
            (vDt1 * iDt3 - iDt2 * vDt2) / (iDt1 * iDt3 - iDt2 * iDt2);

        var l = 
            (iDt1 * vDt2 - vDt1 * iDt2) / (iDt1 * iDt3 - iDt2 * iDt2);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var rAvg = r.GetRunningAverageSignal(avgFactors);
        var lAvg = l.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        //vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt3.png"));
        
        rAvg.PlotScalarSignal("R", Path.Combine(WorkingPath, modelName, $"{FileName} - R.png"));
        lAvg.PlotScalarSignal("L", Path.Combine(WorkingPath, modelName, $"{FileName} - L.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i'''");

        workSheet.WriteScalarSignal(3, columnIndex++, rAvg, "R");
        workSheet.WriteScalarSignal(3, columnIndex++, lAvg, "L");
        
        package.SaveAs(outputFilePath);
    }
    

    private static void SeriesRC0(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc, Float64Signal iIt1)
    {
        const string modelName = "Series - RC 0";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1) = 
            tData.SampleFunctionDerivatives1(vFunc);
        
        var (iDt0, iDt1) = 
            tData.SampleFunctionDerivatives1(iFunc);
    
        var s = 
            (iDt0 * vDt1 - vDt0 * iDt1) / (iDt0 * iDt0 - iIt1 * iDt1);

        var r = 
            (vDt0 * iDt0 - iIt1 * vDt1) / (iDt0 * iDt0 - iIt1 * iDt1);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var sAvg = s.GetRunningAverageSignal(avgFactors);
        var rAvg = r.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        
        
        iIt1.PlotScalarSignal("iIt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iIt1.png"));
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        
        sAvg.PlotScalarSignal("R", Path.Combine(WorkingPath, modelName, $"{FileName} - S.png"));
        rAvg.PlotScalarSignal("L", Path.Combine(WorkingPath, modelName, $"{FileName} - R.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");

        workSheet.WriteScalarSignal(3, columnIndex++, iIt1, "i~");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        
        workSheet.WriteScalarSignal(3, columnIndex++, sAvg, "S");
        workSheet.WriteScalarSignal(3, columnIndex++, rAvg, "R");
        
        package.SaveAs(outputFilePath);
    }
    
    private static void SeriesRC1(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Series - RC 1";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2) = 
            tData.SampleFunctionDerivatives2(vFunc);
        
        var (iDt0, iDt1, iDt2) = 
            tData.SampleFunctionDerivatives2(iFunc);
    
        var s = 
            (iDt1 * vDt2 - vDt1 * iDt2) / (iDt1 * iDt1 - iDt0 * iDt2);

        var r = 
            (vDt1 * iDt1 - iDt0 * vDt2) / (iDt1 * iDt1 - iDt0 * iDt2);
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var sAvg = s.GetRunningAverageSignal(avgFactors);
        var rAvg = r.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        //vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        //iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt3.png"));
        
        sAvg.PlotScalarSignal("R", Path.Combine(WorkingPath, modelName, $"{FileName} - S.png"));
        rAvg.PlotScalarSignal("L", Path.Combine(WorkingPath, modelName, $"{FileName} - R.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        //workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i'''");

        workSheet.WriteScalarSignal(3, columnIndex++, sAvg, "S");
        workSheet.WriteScalarSignal(3, columnIndex++, rAvg, "R");
        
        package.SaveAs(outputFilePath);
    }
    
    
    private static void SeriesRLC0(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc, Float64Signal iIt1)
    {
        const string modelName = "Series - RLC 0";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2, vDt3) = 
            tData.SampleFunctionDerivatives3(vFunc);
        
        var (iDt0, iDt1, iDt2, iDt3) = 
            tData.SampleFunctionDerivatives3(iFunc);

        var d = 
            iDt3 * (iDt0 * iDt0 - iDt1 * iIt1) -
            iDt1 * (iDt0 * iDt2 - iDt1 * iDt1) +
            iDt2 * (iIt1 * iDt2 - iDt0 * iDt1);

        var r = (
            vDt2 * (iIt1 * iDt2 - iDt0 * iDt1) -
            iDt3 * (iIt1 * vDt1 - iDt0 * vDt0) +
            iDt1 * (iDt1 * vDt1 - iDt2 * vDt0)
        ) / d;

        var s = -(
            vDt3 * (iDt0 * iDt2 - iDt1 * iDt1) -
            iDt2 * (iDt0 * vDt1 - iDt1 * vDt0) +
            iDt2 * (iDt1 * vDt1 - iDt2 * vDt0)
        ) / d;

        var l = (
            vDt3 * (iDt0 * iDt0 - iDt1 * iIt1) -
            iDt1 * (iDt0 * vDt1 - iDt1 * vDt0) +
            iDt2 * (iIt1 * vDt1 - iDt0 * vDt0)
        ) / d;
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var rAvg = r.GetRunningAverageSignal(avgFactors);
        var sAvg = s.GetRunningAverageSignal(avgFactors);
        var lAvg = l.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
        
        iIt1.PlotScalarSignal("iIt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iIt1.png"));
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt3.png"));
        
        rAvg.PlotScalarSignal("R", Path.Combine(WorkingPath, modelName, $"{FileName} - R.png"));
        sAvg.PlotScalarSignal("S", Path.Combine(WorkingPath, modelName, $"{FileName} - S.png"));
        lAvg.PlotScalarSignal("L", Path.Combine(WorkingPath, modelName, $"{FileName} - L.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");
        
        workSheet.WriteScalarSignal(3, columnIndex++, iIt1, "i~");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i'''");

        workSheet.WriteScalarSignal(3, columnIndex++, rAvg, "R");
        workSheet.WriteScalarSignal(3, columnIndex++, sAvg, "S");
        workSheet.WriteScalarSignal(3, columnIndex++, lAvg, "L");

        package.SaveAs(outputFilePath);
    }
    
    private static void SeriesRLC1(Float64Signal tData, DifferentialFunction iFunc, DifferentialFunction vFunc)
    {
        const string modelName = "Series - RLC 1";
        
        Console.WriteLine("   Computing Model: " + modelName);
        
        var (vDt0, vDt1, vDt2, vDt3, vDt4) = 
            tData.SampleFunctionDerivatives4(vFunc);
        
        var (iDt0, iDt1, iDt2, iDt3, iDt4) = 
            tData.SampleFunctionDerivatives4(iFunc);

        var d = 
            iDt4 * (iDt1 * iDt1 - iDt2 * iDt0) -
            iDt2 * (iDt1 * iDt3 - iDt2 * iDt2) +
            iDt3 * (iDt0 * iDt3 - iDt1 * iDt2);

        var r = (
            vDt3 * (iDt0 * iDt3 - iDt1 * iDt2) -
            iDt4 * (iDt0 * vDt2 - iDt1 * vDt1) +
            iDt2 * (iDt2 * vDt2 - iDt3 * vDt1)
        ) / d;

        var s = -(
            vDt4 * (iDt1 * iDt3 - iDt2 * iDt2) -
            iDt3 * (iDt1 * vDt2 - iDt2 * vDt1) +
            iDt3 * (iDt2 * vDt2 - iDt3 * vDt1)
        ) / d;

        var l = (
            vDt4 * (iDt1 * iDt1 - iDt2 * iDt0) -
            iDt2 * (iDt1 * vDt2 - iDt2 * vDt1) +
            iDt3 * (iDt0 * vDt2 - iDt1 * vDt1)
        ) / d;
        
        var avgFactors = new[] { 3, 5, 7, 9 };
        var rAvg = r.GetRunningAverageSignal(avgFactors);
        var sAvg = s.GetRunningAverageSignal(avgFactors);
        var lAvg = l.GetRunningAverageSignal(avgFactors);

        vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, modelName, $"{FileName} - v.png"));
        vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt1.png"));
        vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt2.png"));
        vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt3.png"));
        vDt4.PlotScalarSignal("vDt4", Path.Combine(WorkingPath, modelName, $"{FileName} - vDt4.png"));
    
        iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, modelName, $"{FileName} - i.png"));
        iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt1.png"));
        iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt2.png"));
        iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt3.png"));
        iDt4.PlotScalarSignal("iDt4", Path.Combine(WorkingPath, modelName, $"{FileName} - iDt4.png"));
        
        rAvg.PlotScalarSignal("R", Path.Combine(WorkingPath, modelName, $"{FileName} - R.png"));
        sAvg.PlotScalarSignal("S", Path.Combine(WorkingPath, modelName, $"{FileName} - S.png"));
        lAvg.PlotScalarSignal("L", Path.Combine(WorkingPath, modelName, $"{FileName} - L.png"));
        
        
        var columnIndex = 1;
    
        // Read Signal
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var outputFilePath = Path.Combine(WorkingPath, modelName, $"{FileName} - Output.xlsx");
        
        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        using var package = new ExcelPackage(new FileInfo(outputFilePath));

        var workSheet = package.Workbook.Worksheets.Add(modelName);

        workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

        workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");
        workSheet.WriteScalarSignal(3, columnIndex++, vDt4, "v''''");

        workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i'");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i''");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i'''");
        workSheet.WriteScalarSignal(3, columnIndex++, iDt4, "i''''");

        workSheet.WriteScalarSignal(3, columnIndex++, rAvg, "R");
        workSheet.WriteScalarSignal(3, columnIndex++, sAvg, "S");
        workSheet.WriteScalarSignal(3, columnIndex++, lAvg, "L");

        package.SaveAs(outputFilePath);
    }
    

    public static void Example1()
    {
        var fileNames = new[]
        {
            @"values_C__4_7nanos_standard_exp_4",
            @"values_C__5_micro_standard_exp_5",
            @"values_R_5K_standard_exp_1",
            @"values_RC_5k_1_micro_standard_exp_6",
            @"values_RC_paralelo_5K_4_7nanos_standard_exp_2",
            @"values_RC_serie_5K_4_7nanos_standard_exp_3"
        };

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        var tData =
            SamplingSpecs.GetSampledTimeSignal();

        var interpolatorOptions =
            new DfLinearSplineSignalInterpolatorOptions
            {
                SmoothingFactors = new[] { 3, 5, 7, 9 }, //new[] { 3, 5 },
            };

        //var interpolatorOptions =
        //    new DfCatmullRomSplineSignalInterpolatorOptions
        //    {
        //        BezierDegree = 3,
        //        SmoothingFactors = new[] { 3, 5, 7, 9 }, //new[] { 3, 5 },
        //        SplineType = CatmullRomSplineType.Centripetal
        //    };

        foreach (var fileName in fileNames)
        {
            FileName = fileName;

            var inputFilePath =
                Path.Combine(WorkingPath, $"{fileName}.xlsx");
            
            Console.WriteLine($"Reading File {fileName}.xlsx");
            
            using var package = new ExcelPackage(new FileInfo(inputFilePath));

            var workSheet = package.Workbook.Worksheets[0];

            var iFunc = 
                workSheet.GetDifferentialInterpolatorOfColumn(
                    2,
                    1, 
                    SamplingSpecs,
                    1,
                    interpolatorOptions
                );
            
            var vFunc = 
                workSheet.GetDifferentialInterpolatorOfColumn(
                    2,
                    2, 
                    SamplingSpecs,
                    1,
                    interpolatorOptions
                );

            
            var iIt1 = tData.MapSamples(
                tData.MapSamples(iFunc.GetValue)
                    .IntegrateTrapezoidal()
                    .GetDifferentialInterpolator(interpolatorOptions)
                    .GetValue
            );
            
            var vIt1 = tData.MapSamples(
                tData.MapSamples(vFunc.GetValue)
                    .IntegrateTrapezoidal()
                    .GetDifferentialInterpolator(interpolatorOptions)
                    .GetValue
            );


            ParallelRL0(tData, iFunc, vFunc, vIt1);
            ParallelRL1(tData, iFunc, vFunc);
            
            ParallelRC0(tData, iFunc, vFunc);
            ParallelRC1(tData, iFunc, vFunc);
            
            ParallelRLC0(tData, iFunc, vFunc, vIt1);
            ParallelRLC1(tData, iFunc, vFunc);

            SeriesRL0(tData, iFunc, vFunc);
            SeriesRL1(tData, iFunc, vFunc);
            
            SeriesRC0(tData, iFunc, vFunc, iIt1);
            SeriesRC1(tData, iFunc, vFunc);
            
            SeriesRLC0(tData, iFunc, vFunc, iIt1);
            SeriesRLC1(tData, iFunc, vFunc);

            Console.WriteLine();
        }

        
        
    }
    
    public static void Example2()
    {
        //var fileNames = new[]
        //{
        //    @"RCPar10KHz",
        //    @"RCSerie10KHz",
        //    @"RLCPar10KHz",
        //    @"RLCSerie10KHz",
        //    @"RLPar10KHz",
        //    @"RLSerie10KHz"
        //};

        //var fileNames = new[]
        //{
        //    @"RCPar100KHz",
        //    @"RCSerie100KHz",
        //    @"RLCPar100KHz",
        //    @"RLCSerie100KHz",
        //    @"RLPar100KHz",
        //    @"RLSerie100KHz"
        //};

        var fileNames = new[]
        {
            @"RCPar1GHz",
            @"RCSerie1GHz",
            @"RLCPar1GHz",
            @"RLCSerie1GHz",
            @"RLPar1GHz",
            @"RLSerie1GHz"
        };

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        var tData =
            SamplingSpecs.GetSampledTimeSignal();

        var interpolatorOptions =
            new DfLinearSplineSignalInterpolatorOptions
            {
                SmoothingFactors = new[] { 3, 5 }, //new[] { 3, 5, 7, 9 },
            };

        //var interpolatorOptions =
        //    new DfCatmullRomSplineSignalInterpolatorOptions
        //    {
        //        BezierDegree = 3,
        //        SmoothingFactors = new[] { 3, 5, 7, 9 }, //new[] { 3, 5 },
        //        SplineType = CatmullRomSplineType.Centripetal
        //    };

        foreach (var fileName in fileNames)
        {
            FileName = fileName;

            var inputFilePath =
                Path.Combine(WorkingPath, $"{fileName}.xlsx");
            
            Console.WriteLine($"Reading File {fileName}.xlsx");
            
            using var package = new ExcelPackage(new FileInfo(inputFilePath));

            var workSheet = package.Workbook.Worksheets[0];

            var vFunc = 
                workSheet.GetDifferentialInterpolatorOfColumn(
                    2,
                    2, 
                    SamplingSpecs,
                    1,
                    interpolatorOptions
                );
            
            var iFunc = 
                workSheet.GetDifferentialInterpolatorOfColumn(
                    2,
                    3, 
                    SamplingSpecs,
                    1,
                    interpolatorOptions
                );

            
            var vIt1 = tData.MapSamples(
                tData.MapSamples(vFunc.GetValue)
                    .IntegrateTrapezoidal()
                    .GetDifferentialInterpolator(interpolatorOptions)
                    .GetValue
            );
            
            var iIt1 = tData.MapSamples(
                tData.MapSamples(iFunc.GetValue)
                    .IntegrateTrapezoidal()
                    .GetDifferentialInterpolator(interpolatorOptions)
                    .GetValue
            );


            ParallelRL0(tData, iFunc, vFunc, vIt1);
            ParallelRL1(tData, iFunc, vFunc);
            
            ParallelRC0(tData, iFunc, vFunc);
            ParallelRC1(tData, iFunc, vFunc);
            
            ParallelRLC0(tData, iFunc, vFunc, vIt1);
            ParallelRLC1(tData, iFunc, vFunc);

            SeriesRL0(tData, iFunc, vFunc);
            SeriesRL1(tData, iFunc, vFunc);
            
            SeriesRC0(tData, iFunc, vFunc, iIt1);
            SeriesRC1(tData, iFunc, vFunc);
            
            SeriesRLC0(tData, iFunc, vFunc, iIt1);
            SeriesRLC1(tData, iFunc, vFunc);

            Console.WriteLine();
        }

        
        
    }

}