using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Phasors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;
using GeometricAlgebraFulcrumLib.MathBase;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.Text;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.SignalAlgebra;
using OfficeOpenXml;
using OxyPlot;
using OxyPlot.Series;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.SystemIdentification
{
    public static class DFIGTest1OpenLoopSample
    {
        private const string WorkingPath
            = @"D:\Projects\Books\The Geometric Algebra Cookbook\GAPoT\System Identification";

        // This is a pre-defined scalar processor for numeric scalars
        public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
            = ScalarProcessorOfFloat64.DefaultProcessor;

        public static uint VSpaceDimensions
            => 5;

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

        public static int SampleStep 
            => 1;

        public static double SamplingRate
            => 100000d / (SampleStep * SamplesPerCycle);

        public static int SignalSamplesCount
            => 400001 / SampleStep;

        public static int SamplesPerCycle 
            => 2000;

        public static double SignalTime 
            => 4d;
        //=> (SignalSamplesCount - 1) / SamplingRate;

        // This is a pre-defined scalar processor for tuples of numeric scalars

        public static ScalarProcessorOfFloat64Signal ScalarSignalProcessor { get; }
            = ProcessorFactory.CreateFloat64ScalarSignalProcessor(
                SamplingRate, 
                SignalSamplesCount
            );

        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected tuple scalar processor
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


        //private static void ComputeValues1(string phaseName, ScalarSignalFloat64 tData, DifferentialFunction v1Func, DifferentialFunction v2Func, DifferentialFunction i1Func, DifferentialFunction i2Func)
        //{
        //    const double filterEpsilon = 0d;

        //    var v1 = tData.Select(v1Func.GetValue).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var v1Dt1 = tData.Select(v1Func.GetDerivative1Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var v1Dt2 = tData.Select(v1Func.GetDerivative2Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var v1Dt3 = tData.Select(v1Func.GetDerivative3Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);

        //    var v2 = tData.Select(v2Func.GetValue).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var v2Dt1 = tData.Select(v2Func.GetDerivative1Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var v2Dt2 = tData.Select(v2Func.GetDerivative2Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var v2Dt3 = tData.Select(v2Func.GetDerivative3Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);

        //    var i1 = tData.Select(i1Func.GetValue).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i1Dt1 = tData.Select(i1Func.GetDerivative1Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i1Dt2 = tData.Select(i1Func.GetDerivative2Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i1Dt3 = tData.Select(i1Func.GetDerivative3Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i1Dt4 = tData.Select(i1Func.GetDerivative4Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);

        //    var i2 = tData.Select(i2Func.GetValue).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i2Dt1 = tData.Select(i2Func.GetDerivative1Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i2Dt2 = tData.Select(i2Func.GetDerivative2Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i2Dt3 = tData.Select(i2Func.GetDerivative3Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);
        //    var i2Dt4 = tData.Select(i2Func.GetDerivative4Value).CreateSignal(SamplingRate).FilterSpikes(filterEpsilon);

        //    var z = 
        //        GeometricSignalProcessor.CreateVector(i1, i2, i1Dt1, i2Dt1, v1 - v2);
        
        //    var zDt1 = 
        //        GeometricSignalProcessor.CreateVector(i1Dt1, i2Dt1, i1Dt2, i2Dt2, v1Dt1 - v2Dt1);
        
        //    var zDt2 = 
        //        GeometricSignalProcessor.CreateVector(i1Dt2, i2Dt2, i1Dt3, i2Dt3, v1Dt2 - v2Dt2);

        //    var zDt3 = 
        //        GeometricSignalProcessor.CreateVector(i1Dt3, i2Dt3, i1Dt4, i2Dt4, v1Dt3 - v2Dt3);

        //    var zOp = 
        //        z.Op(zDt1).Op(zDt2).Op(zDt3);

        //    var zOp1234 = zOp[15.BasisBladeIdToIndex()].ScalarValue.FilterSpikes(filterEpsilon);
        //    var zOp1235 = zOp[23.BasisBladeIdToIndex()].ScalarValue.FilterSpikes(filterEpsilon);
        //    var zOp1245 = zOp[27.BasisBladeIdToIndex()].ScalarValue.FilterSpikes(filterEpsilon);
        //    var zOp1345 = zOp[29.BasisBladeIdToIndex()].ScalarValue.FilterSpikes(filterEpsilon);
        //    var zOp2345 = zOp[30.BasisBladeIdToIndex()].ScalarValue.FilterSpikes(filterEpsilon);

        //    var r1 = (-zOp2345 / zOp1234).FilterSpikes(filterEpsilon);
        //    var r2 = (-zOp1345 / zOp1234).FilterSpikes(filterEpsilon);
        //    var l1 = (-zOp1245 / zOp1234).FilterSpikes(filterEpsilon);
        //    var l2 = (-zOp1235 / zOp1234).FilterSpikes(filterEpsilon);
        
        //    v1.PlotScalarSignal("v1", Path.Combine(WorkingPath, $"{phaseName}_v1.png"));
        //    v1Dt1.PlotScalarSignal("v1Dt1", Path.Combine(WorkingPath, $"{phaseName}_v1Dt1.png"));
        //    v1Dt2.PlotScalarSignal("v1Dt2", Path.Combine(WorkingPath, $"{phaseName}_v1Dt2.png"));
        //    v1Dt3.PlotScalarSignal("v1Dt3", Path.Combine(WorkingPath, $"{phaseName}_v1Dt3.png"));

        //    v2.PlotScalarSignal("v2", Path.Combine(WorkingPath, $"{phaseName}_v2.png"));
        //    v2Dt1.PlotScalarSignal("v2Dt1", Path.Combine(WorkingPath, $"{phaseName}_v2Dt1.png"));
        //    v2Dt2.PlotScalarSignal("v2Dt2", Path.Combine(WorkingPath, $"{phaseName}_v2Dt2.png"));
        //    v2Dt3.PlotScalarSignal("v2Dt3", Path.Combine(WorkingPath, $"{phaseName}_v2Dt3.png"));

        //    i1.PlotScalarSignal("i1", Path.Combine(WorkingPath, $"{phaseName}_i1.png"));
        //    i1Dt1.PlotScalarSignal("i1Dt1", Path.Combine(WorkingPath, $"{phaseName}_i1Dt1.png"));
        //    i1Dt2.PlotScalarSignal("i1Dt2", Path.Combine(WorkingPath, $"{phaseName}_i1Dt2.png"));
        //    i1Dt3.PlotScalarSignal("i1Dt3", Path.Combine(WorkingPath, $"{phaseName}_i1Dt3.png"));
        //    i1Dt4.PlotScalarSignal("i1Dt4", Path.Combine(WorkingPath, $"{phaseName}_i1Dt4.png"));

        //    i2.PlotScalarSignal("i2", Path.Combine(WorkingPath, $"{phaseName}_i2.png"));
        //    i2Dt1.PlotScalarSignal("i2Dt1", Path.Combine(WorkingPath, $"{phaseName}_i2Dt1.png"));
        //    i2Dt2.PlotScalarSignal("i2Dt2", Path.Combine(WorkingPath, $"{phaseName}_i2Dt2.png"));
        //    i2Dt3.PlotScalarSignal("i2Dt3", Path.Combine(WorkingPath, $"{phaseName}_i2Dt3.png"));
        //    i2Dt4.PlotScalarSignal("i2Dt4", Path.Combine(WorkingPath, $"{phaseName}_i2Dt4.png"));

        //    zOp1234.PlotScalarSignal("sigma_1234", Path.Combine(WorkingPath, $"{phaseName}_sigma_1234.png"));
        //    zOp1235.PlotScalarSignal("sigma_1235", Path.Combine(WorkingPath, $"{phaseName}_sigma_1235.png"));
        //    zOp1245.PlotScalarSignal("sigma_1245", Path.Combine(WorkingPath, $"{phaseName}_sigma_1245.png"));
        //    zOp1345.PlotScalarSignal("sigma_1345", Path.Combine(WorkingPath, $"{phaseName}_sigma_1345.png"));
        //    zOp2345.PlotScalarSignal("sigma_2345", Path.Combine(WorkingPath, $"{phaseName}_sigma_2345.png"));

        //    r1.PlotScalarSignal("R1", Path.Combine(WorkingPath, $"{phaseName}_R1.png"));
        //    l1.PlotScalarSignal("L1", Path.Combine(WorkingPath, $"{phaseName}_L1.png"));
        //    r2.PlotScalarSignal("R2", Path.Combine(WorkingPath, $"{phaseName}_R2.png"));
        //    l2.PlotScalarSignal("L2", Path.Combine(WorkingPath, $"{phaseName}_L2.png"));

        //    var columnIndex = 1;
        
        //    var outputFilePath =
        //        Path.Combine(WorkingPath, @"DFIG test 1 open loop Output.xlsx");

        //    // Read Signal
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using var package = new ExcelPackage(new FileInfo(outputFilePath));

        //    var workSheet = package.Workbook.Worksheets.Add(phaseName);

        //    workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");
        
        //    workSheet.WriteScalarSignal(3, columnIndex++, v1, "v1");
        //    workSheet.WriteScalarSignal(3, columnIndex++, v1Dt1, "v1'");
        //    workSheet.WriteScalarSignal(3, columnIndex++, v1Dt2, "v1''");
        //    workSheet.WriteScalarSignal(3, columnIndex++, v1Dt3, "v1'''");

        //    workSheet.WriteScalarSignal(3, columnIndex++, v2, "v2");
        //    workSheet.WriteScalarSignal(3, columnIndex++, v2Dt1, "v2'");
        //    workSheet.WriteScalarSignal(3, columnIndex++, v2Dt2, "v2''");
        //    workSheet.WriteScalarSignal(3, columnIndex++, v2Dt3, "v2'''");
        
        //    workSheet.WriteScalarSignal(3, columnIndex++, i1, "i1");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i1Dt1, "i1'");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i1Dt2, "i1''");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i1Dt3, "i1'''");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i1Dt4, "i1''''");
        
        //    workSheet.WriteScalarSignal(3, columnIndex++, i2, "i2");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i2Dt1, "i2'");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i2Dt2, "i2''");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i2Dt3, "i2'''");
        //    workSheet.WriteScalarSignal(3, columnIndex++, i2Dt4, "i2''''");
        
        //    workSheet.WriteScalarSignal(3, columnIndex++, zOp1234, "sigma_1234");
        //    workSheet.WriteScalarSignal(3, columnIndex++, zOp1235, "sigma_1235");
        //    workSheet.WriteScalarSignal(3, columnIndex++, zOp1245, "sigma_1245");
        //    workSheet.WriteScalarSignal(3, columnIndex++, zOp1345, "sigma_1345");
        //    workSheet.WriteScalarSignal(3, columnIndex++, zOp2345, "sigma_2345");

        //    workSheet.WriteScalarSignal(3, columnIndex++, r1, "R1");
        //    workSheet.WriteScalarSignal(3, columnIndex++, l1, "L1");
        //    workSheet.WriteScalarSignal(3, columnIndex++, r2, "R2");
        //    workSheet.WriteScalarSignal(3, columnIndex++, l2, "L2");

        //    package.SaveAs(outputFilePath);
        //}
    
        private static void ComputeValues(string phaseName, Float64Signal tData, DifferentialFunction vFunc, DifferentialFunction iFunc, DifferentialFunction wFunc)
        {
            var (vDt0, vDt1, vDt2) = 
                tData.SampleFunctionDerivatives2(vFunc);
        
            var (iDt0, iDt1, iDt2, iDt3) = 
                tData.SampleFunctionDerivatives3(iFunc);
        
            var (wDt0, wDt1, wDt2) = 
                tData.SampleFunctionDerivatives2(wFunc);

            var s1 = 
                iDt2 * (iDt1 * wDt1 - wDt0 * iDt2) -
                iDt3 * (iDt0 * wDt1 - wDt0 * iDt1) +
                wDt2 * (iDt0 * iDt2 - iDt1 * iDt1);

            var s2 = 
                iDt2 * (iDt1 * vDt1 - vDt0 * iDt2) -
                iDt3 * (iDt0 * vDt1 - vDt0 * iDt1) +
                vDt2 * (iDt0 * iDt2 - iDt1 * iDt1);

            var s3 = 
                iDt3 * (wDt0 * vDt1 - vDt0 * wDt1) -
                wDt2 * (iDt1 * vDt1 - vDt0 * iDt2) +
                vDt2 * (iDt1 * wDt1 - wDt0 * iDt2);

            var s4 = 
                iDt2 * (wDt0 * vDt1 - vDt0 * wDt1) - 
                wDt2 * (iDt0 * vDt1 - vDt0 * iDt1) +
                vDt2 * (iDt0 * wDt1 - wDt0 * iDt1);

            var avgFactors = new[] { 3, 5, 7, 9 };
            var k = (s2 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);
            var r = (s3 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);
            var l = (-2d / 3d * s4 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);

            vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, $"{phaseName}_v.png"));
            vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, $"{phaseName}_vDt1.png"));
            vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, $"{phaseName}_vDt2.png"));
            //vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, $"{phaseName}_vDt3.png"));
        
            iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, $"{phaseName}_i.png"));
            iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, $"{phaseName}_iDt1.png"));
            iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, $"{phaseName}_iDt2.png"));
            iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, $"{phaseName}_iDt3.png"));
            //iDt4.PlotScalarSignal("iDt4", Path.Combine(WorkingPath, $"{phaseName}_iDt4.png"));

            wDt0.PlotScalarSignal("w", Path.Combine(WorkingPath, $"{phaseName}_w.png"));
            wDt1.PlotScalarSignal("wDt1", Path.Combine(WorkingPath, $"{phaseName}_wDt1.png"));
            wDt2.PlotScalarSignal("wDt2", Path.Combine(WorkingPath, $"{phaseName}_wDt2.png"));
            //wDt3.PlotScalarSignal("wDt3", Path.Combine(WorkingPath, $"{phaseName}_wDt3.png"));
        
            k.PlotScalarSignal("K", Path.Combine(WorkingPath, $"{phaseName}_K.png"));
            r.PlotScalarSignal("R", Path.Combine(WorkingPath, $"{phaseName}_R.png"));
            l.PlotScalarSignal("L", Path.Combine(WorkingPath, $"{phaseName}_L.png"));

            var columnIndex = 1;
        
            var outputFilePath =
                Path.Combine(WorkingPath, @"DFIG test 1 open loop Output.xlsx");

            // Read Signal
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(outputFilePath));

            var workSheet = package.Workbook.Worksheets.Add(phaseName);

            workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

            workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
            workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
            workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
            //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

            workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i1");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i1'");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i1''");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i1'''");
            //workSheet.WriteScalarSignal(3, columnIndex++, iDt4, "i1''''");

            workSheet.WriteScalarSignal(3, columnIndex++, wDt0, "w");
            workSheet.WriteScalarSignal(3, columnIndex++, wDt1, "w'");
            workSheet.WriteScalarSignal(3, columnIndex++, wDt2, "w''");
            //workSheet.WriteScalarSignal(3, columnIndex++, wDt3, "w'''");
        
            workSheet.WriteScalarSignal(3, columnIndex++, k, "K");
            workSheet.WriteScalarSignal(3, columnIndex++, r, "R");
            workSheet.WriteScalarSignal(3, columnIndex++, l, "L");

            package.SaveAs(outputFilePath);
        }
    
        private static void ComputeValues1(string phaseName, Float64Signal tData, DifferentialFunction vFunc, DifferentialFunction iFunc, DifferentialFunction wFunc)
        {
            var (vDt0, vDt1, vDt2, vDt3) = 
                tData.SampleFunctionDerivatives3(vFunc);
        
            var (iDt0, iDt1, iDt2, iDt3, iDt4) = 
                tData.SampleFunctionDerivatives4(iFunc);
        
            var (wDt0, wDt1, wDt2, wDt3) = 
                tData.SampleFunctionDerivatives3(wFunc);
        
            var s1 = 
                iDt3 * (iDt2 * wDt2 - wDt1 * iDt3) -
                iDt4 * (iDt1 * wDt2 - wDt1 * iDt2) +
                wDt3 * (iDt1 * iDt3 - iDt2 * iDt2);

            var s2 = 
                iDt3 * (iDt2 * vDt2 - vDt1 * iDt3) -
                iDt4 * (iDt1 * vDt2 - vDt1 * iDt2) +
                vDt3 * (iDt1 * iDt3 - iDt2 * iDt2);

            var s3 = 
                iDt4 * (wDt1 * vDt2 - vDt1 * wDt2) -
                wDt3 * (iDt2 * vDt2 - vDt1 * iDt3) +
                vDt3 * (iDt2 * wDt2 - wDt1 * iDt3);

            var s4 = 
                iDt3 * (wDt1 * vDt2 - vDt1 * wDt2) - 
                wDt3 * (iDt1 * vDt2 - vDt1 * iDt2) +
                vDt3 * (iDt1 * wDt2 - wDt1 * iDt2);

            var avgFactors = new[] { 3, 5, 7, 9 };
            var k = (s2 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);
            var r = (s3 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);
            var l = (-2d / 3d * s4 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);

            vDt0.PlotScalarSignal("v", Path.Combine(WorkingPath, $"{phaseName}_v.png"));
            vDt1.PlotScalarSignal("vDt1", Path.Combine(WorkingPath, $"{phaseName}_vDt1.png"));
            vDt2.PlotScalarSignal("vDt2", Path.Combine(WorkingPath, $"{phaseName}_vDt2.png"));
            vDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, $"{phaseName}_vDt3.png"));
        
            iDt0.PlotScalarSignal("i", Path.Combine(WorkingPath, $"{phaseName}_i.png"));
            iDt1.PlotScalarSignal("iDt1", Path.Combine(WorkingPath, $"{phaseName}_iDt1.png"));
            iDt2.PlotScalarSignal("iDt2", Path.Combine(WorkingPath, $"{phaseName}_iDt2.png"));
            iDt3.PlotScalarSignal("iDt3", Path.Combine(WorkingPath, $"{phaseName}_iDt3.png"));
            iDt4.PlotScalarSignal("iDt4", Path.Combine(WorkingPath, $"{phaseName}_iDt4.png"));

            wDt0.PlotScalarSignal("w", Path.Combine(WorkingPath, $"{phaseName}_w.png"));
            wDt1.PlotScalarSignal("wDt1", Path.Combine(WorkingPath, $"{phaseName}_wDt1.png"));
            wDt2.PlotScalarSignal("wDt2", Path.Combine(WorkingPath, $"{phaseName}_wDt2.png"));
            wDt3.PlotScalarSignal("wDt3", Path.Combine(WorkingPath, $"{phaseName}_wDt3.png"));
        
            k.PlotScalarSignal("K", Path.Combine(WorkingPath, $"{phaseName}_K.png"));
            r.PlotScalarSignal("R", Path.Combine(WorkingPath, $"{phaseName}_R.png"));
            l.PlotScalarSignal("L", Path.Combine(WorkingPath, $"{phaseName}_L.png"));

            var columnIndex = 1;
        
            var outputFilePath =
                Path.Combine(WorkingPath, @"DFIG test 1 open loop Output.xlsx");

            // Read Signal
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(outputFilePath));

            var workSheet = package.Workbook.Worksheets.Add(phaseName);

            workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

            workSheet.WriteScalarSignal(3, columnIndex++, vDt0, "v");
            workSheet.WriteScalarSignal(3, columnIndex++, vDt1, "v'");
            workSheet.WriteScalarSignal(3, columnIndex++, vDt2, "v''");
            workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");

            workSheet.WriteScalarSignal(3, columnIndex++, iDt0, "i1");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt1, "i1'");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt2, "i1''");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt3, "i1'''");
            workSheet.WriteScalarSignal(3, columnIndex++, iDt4, "i1''''");

            workSheet.WriteScalarSignal(3, columnIndex++, wDt0, "w");
            workSheet.WriteScalarSignal(3, columnIndex++, wDt1, "w'");
            workSheet.WriteScalarSignal(3, columnIndex++, wDt2, "w''");
            workSheet.WriteScalarSignal(3, columnIndex++, wDt3, "w'''");
        
            workSheet.WriteScalarSignal(3, columnIndex++, k, "K");
            workSheet.WriteScalarSignal(3, columnIndex++, r, "R");
            workSheet.WriteScalarSignal(3, columnIndex++, l, "L");

            package.SaveAs(outputFilePath);
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Direct-quadrature-zero_transformation
        /// </summary>
        /// <returns></returns>
        private static Triplet<double> Dqz(this double theta, double a, double b, double c)
        {
            // Apply Clarke rotation to the input 3-phase vector
            var x = (2d * a - b - c) * (1d / Math.Sqrt(6));
            var y = (b - c) * (1d / Math.Sqrt(2));
            var z = (a + b + c) * (1d / Math.Sqrt(3));

            // Apply Park rotation to the xyz signal
            var co = Math.Cos(theta);
            var si = Math.Sin(theta);

            var d = co * x + si * y;
            var q = co * y - si * x;

            return new Triplet<double>(d, q, z);
        }

        private static Triplet<Float64Signal> Dqz(Float64Signal aSignal, Float64Signal bSignal, Float64Signal cSignal)
        {
            const double f = 50d;

            var sampleCount = aSignal.Count;
            var samplingRate = aSignal.SamplingRate;

            var dSignalArray = new double[sampleCount];
            var qSignalArray = new double[sampleCount];
            var zSignalArray = new double[sampleCount];

            var tSignal = aSignal.GetSampledTimeSignal();

            for (var i = 0; i < sampleCount; i++)
            {
                var theta = 2d * Math.PI * f * tSignal[i];

                var (d, q, z) = theta.Dqz(
                    aSignal[i],
                    bSignal[i],
                    cSignal[i]
                );

                dSignalArray[i] = d;
                qSignalArray[i] = q;
                zSignalArray[i] = z;
            }

            return new Triplet<Float64Signal>(
                dSignalArray.CreateSignal(samplingRate),
                qSignalArray.CreateSignal(samplingRate),
                zSignalArray.CreateSignal(samplingRate)
            );
        }
    
        private static Triplet<DifferentialFunction> Dqz(Triplet<DifferentialFunction> abcFunc, double frequency)
        {
            var (aFunc, bFunc, cFunc) = abcFunc;

            // Apply Clarke rotation to the input 3-phase vector
            var xFunction = 
                2d / Math.Sqrt(6) * aFunc +
                -1d / Math.Sqrt(6) * bFunc +
                -1d / Math.Sqrt(6) * cFunc;

            var yFunction = 
                1d / Math.Sqrt(2) * bFunc +
                -1d / Math.Sqrt(2) * cFunc;

            var zFunction = 
                1d / Math.Sqrt(3) * aFunc +
                1d / Math.Sqrt(3) * bFunc +
                1d / Math.Sqrt(3) * cFunc;

            // Apply Park rotation to the xyz signal
            var co = DfCosPhasor.Create(1, frequency);
            var siPositive = DfSinPhasor.Create(1, frequency);
            var siNegative = DfSinPhasor.Create(-1, frequency);

            var dFunction = 
                co * xFunction + 
                siPositive * yFunction;
        
            var qFunction = 
                co * yFunction +
                siNegative * xFunction;
        
            return new Triplet<DifferentialFunction>(
                dFunction, 
                qFunction, 
                zFunction
            );
        }

        private static void ComputeValuesDq(string phaseName, Float64Signal tData, Triplet<DifferentialFunction> vsFunc, Triplet<DifferentialFunction> isFunc, Triplet<DifferentialFunction> irFunc)
        {
            const double freq = 2d * Math.PI * 0.005d;//50;

            var (vsdFunc, vsqFunc, vszFunc) = Dqz(vsFunc, freq);
            var (isdFunc, isqFunc, iszFunc) = Dqz(isFunc, freq);
            var (irdFunc, irqFunc, irzFunc) = Dqz(irFunc, freq);
        
            var i1Func = isdFunc - freq * isqFunc;
            var i2Func = irdFunc - freq * irqFunc;

            var (vsdDt0, vsdDt1, vsdDt2) = 
                tData.SampleFunctionDerivatives2(vsdFunc);
        
            var (isdDt0, isdDt1, isdDt2) = 
                tData.SampleFunctionDerivatives2(isdFunc);
        
            var vsqDt0 = tData.SampleFunction(vsqFunc);
            var vszDt0 = tData.SampleFunction(vszFunc);

            var isqDt0 = tData.SampleFunction(isqFunc);
            var iszDt0 = tData.SampleFunction(iszFunc);

            var irdDt0 = tData.SampleFunction(irdFunc);
            var irqDt0 = tData.SampleFunction(irqFunc);
            var irzDt0 = tData.SampleFunction(irzFunc);

            var (i1Dt0, i1Dt1, i1Dt2, i1Dt3) = 
                tData.SampleFunctionDerivatives3(i1Func);
        
            var (i2Dt0, i2Dt1, i2Dt2, i2Dt3) = 
                tData.SampleFunctionDerivatives3(i2Func);

            var s1 = 
                i2Dt1 * (isdDt0 * i1Dt2 - i1Dt1 * isdDt1) -
                i1Dt1 * (isdDt0 * i2Dt2 - i2Dt1 * isdDt1) +
                isdDt0 * (i1Dt1 * i2Dt2 - i2Dt1 * i1Dt2);

            var s2 = 
                vsdDt2 * (i1Dt1 * i2Dt2 - i2Dt1 * i1Dt2) -
                i2Dt3 * (i1Dt1 * vsdDt1 - vsdDt0 * i1Dt2) +
                i1Dt3 * (i2Dt1 * vsdDt1 - vsdDt0 * i2Dt2);

            var s3 = 
                vsdDt2 * (isdDt0 * i2Dt2 - i2Dt1 * isdDt1) -
                i2Dt3 * (isdDt0 * vsdDt1 - vsdDt0 * isdDt1) +
                isdDt2 * (i2Dt1 * vsdDt1 - vsdDt0 * i2Dt2);

            var s4 = 
                vsdDt2 * (isdDt0 * i1Dt2 - i2Dt1 * isdDt1) -
                i1Dt3 * (isdDt0 * vsdDt1 - vsdDt0 * isdDt1) +
                isdDt2 * (i1Dt1 * vsdDt1 - vsdDt0 * i1Dt2);

            var avgFactors = new[] { 3, 5, 7, 9 };
            var rs = (s2 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);
            var ls = (-s3 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);
            var lmu = (s4 / s1).MapSamples(s => s.NaNInfinityToZero()).GetRunningAverageSignal(avgFactors);

            vsdDt0.PlotScalarSignal("vsd", Path.Combine(WorkingPath, $"{phaseName}_vsd.png"));
            vsdDt1.PlotScalarSignal("vsdDt1", Path.Combine(WorkingPath, $"{phaseName}_vsdDt1.png"));
            vsdDt2.PlotScalarSignal("vsdDt2", Path.Combine(WorkingPath, $"{phaseName}_vsdDt2.png"));
            //vsdDt3.PlotScalarSignal("vDt3", Path.Combine(WorkingPath, $"{phaseName}_vsdDt3.png"));
        
            vsqDt0.PlotScalarSignal("vsq", Path.Combine(WorkingPath, $"{phaseName}_vsq.png"));
            vszDt0.PlotScalarSignal("vsz", Path.Combine(WorkingPath, $"{phaseName}_vsz.png"));

            isdDt0.PlotScalarSignal("isd", Path.Combine(WorkingPath, $"{phaseName}_isd.png"));
            isdDt1.PlotScalarSignal("isdDt1", Path.Combine(WorkingPath, $"{phaseName}_isdDt1.png"));
            isdDt2.PlotScalarSignal("isdDt2", Path.Combine(WorkingPath, $"{phaseName}_isdDt2.png"));
            //isdDt3.PlotScalarSignal("isdDt3", Path.Combine(WorkingPath, $"{phaseName}_isdDt3.png"));
            //isdDt4.PlotScalarSignal("isdDt4", Path.Combine(WorkingPath, $"{phaseName}_isdDt4.png"));

            isqDt0.PlotScalarSignal("isq", Path.Combine(WorkingPath, $"{phaseName}_isq.png"));
            iszDt0.PlotScalarSignal("isz", Path.Combine(WorkingPath, $"{phaseName}_isz.png"));

            irdDt0.PlotScalarSignal("isd", Path.Combine(WorkingPath, $"{phaseName}_ird.png"));
            irqDt0.PlotScalarSignal("isq", Path.Combine(WorkingPath, $"{phaseName}_irq.png"));
            irzDt0.PlotScalarSignal("isz", Path.Combine(WorkingPath, $"{phaseName}_irz.png"));
        
            i1Dt0.PlotScalarSignal("i1", Path.Combine(WorkingPath, $"{phaseName}_i1.png"));
            i1Dt1.PlotScalarSignal("i1Dt1", Path.Combine(WorkingPath, $"{phaseName}_i1Dt1.png"));
            i1Dt2.PlotScalarSignal("i1Dt2", Path.Combine(WorkingPath, $"{phaseName}_i1Dt2.png"));
            i1Dt3.PlotScalarSignal("i1Dt3", Path.Combine(WorkingPath, $"{phaseName}_i1Dt3.png"));
            //i1Dt4.PlotScalarSignal("i1Dt4", Path.Combine(WorkingPath, $"{phaseName}_i1Dt4.png"));
        
            i2Dt0.PlotScalarSignal("i2", Path.Combine(WorkingPath, $"{phaseName}_i2.png"));
            i2Dt1.PlotScalarSignal("i2Dt1", Path.Combine(WorkingPath, $"{phaseName}_i2Dt1.png"));
            i2Dt2.PlotScalarSignal("i2Dt2", Path.Combine(WorkingPath, $"{phaseName}_i2Dt2.png"));
            i2Dt3.PlotScalarSignal("i2Dt3", Path.Combine(WorkingPath, $"{phaseName}_i2Dt3.png"));
            //i2Dt4.PlotScalarSignal("i2Dt4", Path.Combine(WorkingPath, $"{phaseName}_i2Dt4.png"));
        
            rs.PlotScalarSignal("Rs", Path.Combine(WorkingPath, $"{phaseName}_Rs.png"));
            ls.PlotScalarSignal("Ls", Path.Combine(WorkingPath, $"{phaseName}_Ls.png"));
            lmu.PlotScalarSignal("Lmu", Path.Combine(WorkingPath, $"{phaseName}_Lmu.png"));

            var columnIndex = 1;
        
            var outputFilePath =
                Path.Combine(WorkingPath, @"DFIG test 1 open loop Output.xlsx");

            // Read Signal
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(outputFilePath));

            var workSheet = package.Workbook.Worksheets.Add(phaseName);

            workSheet.WriteScalarSignal(3, columnIndex++, tData, "t");

            workSheet.WriteScalarSignal(3, columnIndex++, vsdDt0, "vsd");
            workSheet.WriteScalarSignal(3, columnIndex++, vsdDt1, "vsd'");
            workSheet.WriteScalarSignal(3, columnIndex++, vsdDt2, "vsd''");
            //workSheet.WriteScalarSignal(3, columnIndex++, vDt3, "v'''");
        
            workSheet.WriteScalarSignal(3, columnIndex++, vsqDt0, "vsq");
            workSheet.WriteScalarSignal(3, columnIndex++, vszDt0, "vsz");

            workSheet.WriteScalarSignal(3, columnIndex++, isdDt0, "isd");
            workSheet.WriteScalarSignal(3, columnIndex++, isdDt1, "isd'");
            workSheet.WriteScalarSignal(3, columnIndex++, isdDt2, "isd''");
            //workSheet.WriteScalarSignal(3, columnIndex++, isdDt3, "isd'''");
            //workSheet.WriteScalarSignal(3, columnIndex++, isdDt4, "isd''''");
        
            workSheet.WriteScalarSignal(3, columnIndex++, isqDt0, "isq");
            workSheet.WriteScalarSignal(3, columnIndex++, iszDt0, "isz");
        
            workSheet.WriteScalarSignal(3, columnIndex++, irdDt0, "ird");
            workSheet.WriteScalarSignal(3, columnIndex++, irqDt0, "irq");
            workSheet.WriteScalarSignal(3, columnIndex++, irzDt0, "irz");

            workSheet.WriteScalarSignal(3, columnIndex++, i1Dt0, "i1");
            workSheet.WriteScalarSignal(3, columnIndex++, i1Dt1, "i1'");
            workSheet.WriteScalarSignal(3, columnIndex++, i1Dt2, "i1''");
            workSheet.WriteScalarSignal(3, columnIndex++, i1Dt3, "i1'''");
            //workSheet.WriteScalarSignal(3, columnIndex++, i1Dt4, "i1''''");

            workSheet.WriteScalarSignal(3, columnIndex++, i2Dt0, "i2");
            workSheet.WriteScalarSignal(3, columnIndex++, i2Dt1, "i2'");
            workSheet.WriteScalarSignal(3, columnIndex++, i2Dt2, "i2''");
            workSheet.WriteScalarSignal(3, columnIndex++, i2Dt3, "i2'''");
            //workSheet.WriteScalarSignal(3, columnIndex++, i2Dt4, "i2''''");
        
            workSheet.WriteScalarSignal(3, columnIndex++, rs, "Rs");
            workSheet.WriteScalarSignal(3, columnIndex++, ls, "Ls");
            workSheet.WriteScalarSignal(3, columnIndex++, lmu, "Lmu");

            package.SaveAs(outputFilePath);
        }
    
        private static DifferentialFunction ReadExcelColumnD3(this ExcelWorksheet workSheet, int columnIndex)
        {
            const int bezierDegree = 3;
            const CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal;

            var smoothingFactors = new[] { 3, 5, 7, 9 };

            return workSheet
                .ReadScalarColumn(2, SignalSamplesCount, SampleStep, columnIndex)
                .CreateSignal(SamplingRate)
                .ReSampleForBezierSmoothing(bezierDegree)
                .GetCatmullRomInterpolator(
                    new DfCatmullRomSplineSignalInterpolatorOptions
                    {
                        BezierDegree = bezierDegree,
                        SmoothingFactors = smoothingFactors,
                        SplineType = curveType
                    }
                );
        }
    
        private static DifferentialFunction ReadExcelColumnD4(this ExcelWorksheet workSheet, int columnIndex)
        {
            const int bezierDegree = 3;
            const CatmullRomSplineType curveType = CatmullRomSplineType.Centripetal;
        
            var smoothingFactors = new[] { 3, 5, 7, 9 };

            return workSheet
                .ReadScalarColumn(2, SignalSamplesCount, SampleStep, columnIndex)
                .CreateSignal(SamplingRate)
                .ReSampleForBezierSmoothing(bezierDegree)
                .GetCatmullRomInterpolator(
                    new DfCatmullRomSplineSignalInterpolatorOptions
                    {
                        BezierDegree = bezierDegree,
                        SmoothingFactors = smoothingFactors,
                        SplineType = curveType
                    }
                );
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

            var tData = 
                workSheet
                    .ReadScalarColumn(firstRowIndex, SignalSamplesCount, SampleStep, 1)
                    .Select(t => t * SamplesPerCycle)
                    .CreateSignal(SamplingRate);

            var usDataA = workSheet.ReadExcelColumnD3(2);
            var usDataB = workSheet.ReadExcelColumnD3(3);
            var usDataC = workSheet.ReadExcelColumnD3(4);

            var urDataA = workSheet.ReadExcelColumnD3(5);
            var urDataB = workSheet.ReadExcelColumnD3(6);
            var urDataC = workSheet.ReadExcelColumnD3(7);

            var isDataA = workSheet.ReadExcelColumnD3(8);
            var isDataB = workSheet.ReadExcelColumnD3(9);
            var isDataC = workSheet.ReadExcelColumnD3(10);

            var irDataA = workSheet.ReadExcelColumnD3(11);
            var irDataB = workSheet.ReadExcelColumnD3(12);
            var irDataC = workSheet.ReadExcelColumnD3(13);

            var wData = workSheet.ReadExcelColumnD3(15);

            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);
        
            ComputeValues(
                "Phase A - Stator",
                tData,
                usDataA,
                isDataA,
                wData
            );

            ComputeValues(
                "Phase A - Rotor",
                tData,
                urDataA,
                irDataA,
                wData
            );
        
            ComputeValues(
                "Phase B - Stator",
                tData,
                usDataB,
                isDataB,
                wData
            );
        
            ComputeValues(
                "Phase B - Rotor",
                tData,
                urDataB,
                irDataB,
                wData
            );

            ComputeValues(
                "Phase C - Stator",
                tData,
                usDataC,
                isDataC,
                wData
            );

            ComputeValues(
                "Phase C - Rotor",
                tData,
                urDataC,
                irDataC,
                wData
            );
        }
    
        public static void Example2()
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

            var tData = 
                workSheet
                    .ReadScalarColumn(firstRowIndex, SignalSamplesCount, SampleStep, 1)
                    .Select(t => t * SamplesPerCycle)
                    .CreateSignal(SamplingRate);

            var usDataA = workSheet.ReadExcelColumnD3(2);
            var usDataB = workSheet.ReadExcelColumnD3(3);
            var usDataC = workSheet.ReadExcelColumnD3(4);
        
            var isDataA = workSheet.ReadExcelColumnD3(8);
            var isDataB = workSheet.ReadExcelColumnD3(9);
            var isDataC = workSheet.ReadExcelColumnD3(10);

            var irDataA = workSheet.ReadExcelColumnD3(11);
            var irDataB = workSheet.ReadExcelColumnD3(12);
            var irDataC = workSheet.ReadExcelColumnD3(13);

            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);
        
            ComputeValuesDq(
                "Stator Parameters",
                tData,
                new Triplet<DifferentialFunction>(usDataA, usDataB, usDataC),
                new Triplet<DifferentialFunction>(isDataA, isDataB, isDataC),
                new Triplet<DifferentialFunction>(irDataA, irDataB, irDataC)
            );
        }

        public static void ValidationExample()
        {
            const double freqHz = 1;
            const double freq = 2d * Math.PI * freqHz;
            const int cycleCount = 5;
            const int samplesPerCycle = 1000;
            const int sampleCount = samplesPerCycle * cycleCount + 1;
            const double tMin = 0d;
            const double tMax = cycleCount / freqHz;
            const double samplingRate = (tMax - tMin) / (sampleCount - 1);

            var phaseA = DfCosPhasor.Create(1, freq, 0);
            var phaseB = DfCosPhasor.Create(1, freq, 120.DegreesToAngle());
            var phaseC = DfCosPhasor.Create(1, freq, 240.DegreesToAngle());

            var abcFunc = new Triplet<DifferentialFunction>(phaseA, phaseB, phaseC);

            var (dFunc, qFunc, zFunc) = Dqz(abcFunc, freq);

            var tSignal = 
                0d.GetLinearRange(5d, 1001, false).CreateSignal(samplingRate);

            var dSignal = tSignal.MapSamples(dFunc.GetValue);
            var qSignal = tSignal.MapSamples(qFunc.GetValue);
            var zSignal = tSignal.MapSamples(zFunc.GetValue);

            dSignal.PlotScalarSignal("d", Path.Combine(WorkingPath, $"d.png"));
            qSignal.PlotScalarSignal("q", Path.Combine(WorkingPath, $"q.png"));
            zSignal.PlotScalarSignal("z", Path.Combine(WorkingPath, $"z.png"));
        }

        public static void HistogramExample()
        {
            var inputFilePath =
                Path.Combine(WorkingPath, @"DFIG test 1 open loop Output.xlsx");

            // Read Signal
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(inputFilePath));

            var workSheet = package.Workbook.Worksheets[0];

            const int firstRowIndex = 3;

            var dataValues1 = 
                workSheet
                    .ReadScalarColumn(firstRowIndex, SignalSamplesCount, SampleStep, 19)
                    .CreateSignal(SamplingRate);

            var dataHist =
                Float64SignalLog2Histogram.Create(dataValues1).Trim(1e-2d);

            const double tMin = -25d;
            const double tMax = 25d;

            var tSignal =
                tMin.GetLinearRange(tMax, 51, false).CreateSignal(1);

            var hSignal =
                dataHist.ToArray(-25, 25).CreateSignal(1);
        
            Console.WriteLine(
                tSignal.Concatenate(", ", "t = [", "];")
            );

            Console.WriteLine(
                hSignal.Concatenate(", ", "h = [", "];")
            );

            hSignal.PlotSignal(
                tMin, 
                tMax, 
                Path.Combine(WorkingPath, "signalHistogram")
            );

            var dataValues2 =
                dataHist.FilterSignal(dataValues1);

            var diffCountRatio =
                (dataValues1 - dataValues2).Count(d => d != 0d) / (double)dataValues1.Count;

            dataValues1.PlotScalarSignal(
                "Signal", 
                Path.Combine(WorkingPath, "Signal1")
            );

            dataValues2.PlotScalarSignal(
                "Signal", 
                Path.Combine(WorkingPath, "Signal2")
            );
        }
    }
}